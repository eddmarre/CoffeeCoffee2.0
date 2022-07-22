using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class Customer : Interactable
{
    [SerializeField] private GameObject chatBubble;

    [SerializeField] private TextMeshProUGUI customerOrderText;
    [SerializeField] private TextMeshProUGUI customerNameText;

    [SerializeField] private bool isFemale;

    [SerializeField] private Material[] customerSkins;
    [SerializeField] private Renderer customerRenderer;

    [SerializeField] private CustomerMovement customerMovement;

    [SyncVar(hook = nameof(OnCustomerOrderDialogueChange))]
    private string _customerOrderDialogue;

    [SyncVar(hook = nameof(OnCustomerNameChange))]
    private string _customerName;

    private Order _order;

    private RandomCustomerOrder _randomCustomerOrder;

    public static event Action<Customer> onCustomerServed;

    //try calling these with hooks so that they show up on everyone's machine
    //currently only host can see when the customer has been served
    [SyncVar] public bool hasOrdered;
    [SyncVar] private bool _isServed;

    private Random _randomSkin = new Random();
    [SyncVar] private int randomIndex;

    [ServerCallback]
    public void OnEnable()
    {
        randomIndex = _randomSkin.Next(customerSkins.Length);
        customerRenderer.material = customerSkins[randomIndex];
    }


    [ClientRpc]
    protected override void RpcDeactivateInteractable(Interactable interactable, Player player)
    {
        if (interactable != this) return;

        if (hasOrdered && !_isServed)
        {
            chatBubble.SetActive(true);

            _customerOrderDialogue = _randomCustomerOrder.GetRandomOutro();

            _isServed = true;

            StartCoroutine(RemoveCustomerAfterServed());
        }

        if (this != CustomerOrderTracker.Instance.CheckCurrentCustomerInFrontOfLine()) return;

        if (hasOrdered) return;

        chatBubble.SetActive(true);

        CreateCustomerOrderDialogue();

        hasOrdered = true;

        StartCoroutine(MoveAfterSomeTime());
    }

    private IEnumerator MoveAfterSomeTime()
    {
        yield return new WaitForSeconds(5f);
        CustomerOrderTracker.Instance.RemoveCustomerFromLine();
        chatBubble.SetActive(false);
        customerMovement.MoveAfterOrdering();
    }

    private IEnumerator RemoveCustomerAfterServed()
    {
        yield return new WaitForSeconds(5f);
        ResetCustomer();
        if (NetworkServer.active)
        {
            onCustomerServed?.Invoke(this);
        }
        //CustomerSpawner.Instance.DeleteCustomer(this);
    }

    private void CreateCustomerOrderDialogue()
    {
        _randomCustomerOrder = new RandomCustomerOrder();

        _order = _randomCustomerOrder.CreateRandomOrder();

        _customerName = _randomCustomerOrder.GetRandomName(isFemale);

        _customerOrderDialogue = $"{_randomCustomerOrder.GetRandomCustomerIntro()}, " +
                                 $"{_order.GetSize()} {_order.GetShot()} {_order.GetEspresso()} espresso " +
                                 $"{_order.GetSyrup()} {_order.GetBeverage()} with " +
                                 $"{_order.GetTemperature()} temp {_order.GetMilk()}";
    }


    private void OnCustomerOrderDialogueChange(string oldText, string newText)
    {
        customerOrderText.text = newText;
    }

    private void OnCustomerNameChange(string oldText, string newText)
    {
        customerNameText.text = newText;
    }

    private void ResetCustomer()
    {
        hasOrdered = false;
        _isServed = false;
        chatBubble.SetActive(false);
    }
}