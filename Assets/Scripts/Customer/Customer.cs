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

    [SyncVar(hook = nameof(OnCustomerOrderDialogueChange))]
    private string _customerOrderDialogue;

    [SyncVar(hook = nameof(OnCustomerNameChange))]
    private string _customerName;

    private Order _order;

    private RandomCustomerOrder _randomCustomerOrder;
    //try calling these with hooks so that they show up on everyone's machine
    //currently only host can see when the customer has been served
    [SyncVar] private bool _hasOrdered;
    [SyncVar] private bool _isServed;


    [ClientRpc]
    protected override void RpcDeactivateInteractable(Interactable interactable, Player player)
    {
        if (interactable != this) return;

        if (_hasOrdered && !_isServed)
        {
            _customerOrderDialogue = _randomCustomerOrder.GetRandomOutro();

            _isServed = true;
        }

        if (_hasOrdered) return;

        chatBubble.SetActive(true);

        CreateCustomerOrderDialogue();

       _hasOrdered = true;
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
}