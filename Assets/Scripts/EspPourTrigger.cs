using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspPourTrigger : MonoBehaviour
{
    [SerializeField] private Placeable myPlaceable;
    [SerializeField] private EspMachineShotsScreen shotsScreen;

    private string shotSetting;
    private string espSetting;

    private void Start()
    {
        EspMachineButton.onPourButtonPressed += EspMachineButton_OnPourButtonPressed;

        EspMachineButton.onBlondeButtonPressed += EspMachineButton_OnBlondeButtonPressed;
        EspMachineButton.onRegularButtonPressed += EspMachineButton_OnRegularButtonPressed;
        EspMachineButton.onDecafButtonPressed += EspMachineButton_OnDecafButtonPressed;

        EspMachineButton.onShotsButtonPressed += EspMachineButton_OnShotsButtonPressed;

        shotSetting = shotsScreen.GetCurrentSetting();
        espSetting = OrderDictionary.ESPRESSOS[0];
    }

    private void OnDestroy()
    {
        EspMachineButton.onPourButtonPressed -= EspMachineButton_OnPourButtonPressed;
    }

    private void EspMachineButton_OnPourButtonPressed()
    {
        if (!myPlaceable.GetHasItem()) return;

        if (!myPlaceable.GetPickupInPlaceable().TryGetComponent(out CupContents cupContents))
        {
            Debug.Log("can't find cup contents");
            return;
        }

        cupContents.SetShot(shotSetting);
        cupContents.SetEspresso(espSetting);
    }

    private void EspMachineButton_OnBlondeButtonPressed()
    {
        espSetting = OrderDictionary.ESPRESSOS[1];
    }

    private void EspMachineButton_OnRegularButtonPressed()
    {
        espSetting = OrderDictionary.ESPRESSOS[0];
    }

    private void EspMachineButton_OnDecafButtonPressed()
    {
        espSetting = OrderDictionary.ESPRESSOS[2];
    }

    private void EspMachineButton_OnShotsButtonPressed()
    {
        shotSetting = shotsScreen.GetCurrentSetting();
    }
}