using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class SyrupBottle : Interactable
{
    public enum SyrupType
    {
        Vanilla,
        Caramel,
        Hazelnut,
        Classic,
        Mocha
    }

    [SerializeField] private SyrupType _syrupType;
    [SerializeField] private GameObject syurpHead;

    [SerializeField] private TextMeshProUGUI syrupTMP;

    [SyncVar(hook = nameof(OnSyrupTextChange))]
    private string _syrupText;

    public event Action OnSyrupPumped;

    [SerializeField] private bool _isPressed;

    private Vector3 originalSyrupHeadPos;
    private Vector3 moveToPos;
    float speed = 10f;

    public string GetSyrupText()
    {
        return _syrupText;
    }

    private void Start()
    {
        originalSyrupHeadPos = syurpHead.transform.localPosition;
        moveToPos = new Vector3(0f, -0.043f, 0f);


        switch (_syrupType)
        {
            case SyrupType.Vanilla:
                _syrupText = "Vanilla";
                break;
            case SyrupType.Caramel:
                _syrupText = "Caramel";
                break;
            case SyrupType.Hazelnut:
                _syrupText = "Hazelnut";
                break;
            case SyrupType.Classic:
                _syrupText = "Classic";
                break;
            case SyrupType.Mocha:
                _syrupText = "Mocha";
                break;
        }
    }

    [ClientRpc]
    protected override void RpcDeactivateInteractable(Interactable interactable, Player player)
    {
        if (this != interactable) return;

        if (_isPressed) return;

        _isPressed = true;
        
        StartCoroutine(StartPumpMotion());

        OnSyrupPumped?.Invoke();
    }


    private void Update()
    {
        if (!_isPressed) return;
        
        syurpHead.transform.localPosition =
            Vector3.Lerp(syurpHead.transform.localPosition, moveToPos, speed * Time.deltaTime);
    }

    private IEnumerator StartPumpMotion()
    {
        yield return new WaitForSeconds(.5f);
        moveToPos = originalSyrupHeadPos;
        yield return new WaitForSeconds(.5f);
        _isPressed = false;
        moveToPos = new Vector3(0f, -0.043f, 0f);
    }


    private void OnSyrupTextChange(string oldText, string newText)
    {
        syrupTMP.text = _syrupText;
    }
}