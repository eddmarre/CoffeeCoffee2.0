using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MilkSteamTrigger : MonoBehaviour
{
    [SerializeField] private EspMachineLeverController leverController;
    [SerializeField] private EspMachineTemperatureScreen temperatureScreen;

    [SerializeField] private Placeable pitcherPlaceable;
    [SerializeField] private BoxCollider pitcherPlaceableBoxCollider;

    [SerializeField] private GameObject steamProgressGO;

    [SerializeField] private Image steamProgressBar;

    [SerializeField] private float progressBarIncreasePerSecond = 5f;

    private WaitForSeconds _waitForSeconds;

    private string _currentTemperatureSetting;

    private bool _isSteaming;

    private float _fillAmountMax = 100f;

    private float _fillAmountCurrent = 0f;


    private float _timerCount = 0f;

    private void Start()
    {
        float _delayTime = .1f;
        _waitForSeconds = new WaitForSeconds(_delayTime);

        steamProgressGO.SetActive(false);

        _currentTemperatureSetting = temperatureScreen.GetCurrentTemperatureSetting();

        EspMachineButton.onLeverButtonPressed += EspMachineButton_OnLeverButtonPressed;

        EspMachineButton.onTemperatureButtonPressed += EspMachineButton_OnTemperatureButtonPressed;
    }


    private void OnDestroy()
    {
        EspMachineButton.onLeverButtonPressed -= EspMachineButton_OnLeverButtonPressed;

        EspMachineButton.onTemperatureButtonPressed -= EspMachineButton_OnTemperatureButtonPressed;
    }


    private void EspMachineButton_OnLeverButtonPressed()
    {
        StartCoroutine(WaitForLeverDown());
    }


    private IEnumerator WaitForLeverDown()
    {
        yield return _waitForSeconds;
        LeverDownBehaviour();
    }

    private void LeverDownBehaviour()
    {
        if (!leverController.GetIsLeverDown()) return;

        if (!pitcherPlaceable.GetHasItem()) return;

        if (!pitcherPlaceable.GetPickupInPlaceable().TryGetComponent(out MilkPitcher milkPitcher)) return;

        if (milkPitcher.GetIsSteamed()) return;

        if (milkPitcher.GetMilkInPitcher() == "none") return;
        
        _fillAmountCurrent = 0f;
        
        steamProgressBar.fillAmount = 0f;

        steamProgressGO.SetActive(true);

        pitcherPlaceableBoxCollider.enabled = false;

        _isSteaming = true;

        milkPitcher.SteamMilkInPitcher(_currentTemperatureSetting);
    }


    private void Update()
    {
        if (!_isSteaming && !leverController.GetIsLeverDown())
        {
            pitcherPlaceableBoxCollider.enabled = true;
        }

        if (!_isSteaming) return;

        UpdateProgressBar();

        StartProgressPerSecond();

        FinishSteaming();
    }

    private void UpdateProgressBar()
    {
        float fillAmount = _fillAmountCurrent / _fillAmountMax;
        steamProgressBar.fillAmount = fillAmount;
    }

    private void StartProgressPerSecond()
    {
        _timerCount += Time.deltaTime;
        if (_timerCount > 1)
        {
            _fillAmountCurrent += progressBarIncreasePerSecond;
            _timerCount = 0f;
        }
    }

    private void FinishSteaming()
    {
        if (_fillAmountCurrent < _fillAmountMax) return;

        steamProgressBar.fillAmount = 1f;

        steamProgressGO.SetActive(false);

        _isSteaming = false;
    }


    private void EspMachineButton_OnTemperatureButtonPressed()
    {
        StartCoroutine(WaitForTemperutureButton());
    }

    private IEnumerator WaitForTemperutureButton()
    {
        yield return _waitForSeconds;

        _currentTemperatureSetting = temperatureScreen.GetCurrentTemperatureSetting();
    }

    public bool GetIsSteaming()
    {
        return _isSteaming;
    }
}