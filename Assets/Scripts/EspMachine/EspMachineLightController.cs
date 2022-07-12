using System;
using UnityEditor;
using UnityEngine;

public class EspMachineLightController : MonoBehaviour
{
    [SerializeField] private Renderer[] lightRenderers;
    [SerializeField] private Material[] lightMaterials;

    private void Start()
    {
        EspMachineButton.onBlondeButtonPressed += Button_onBlondeButtonPressed;
        EspMachineButton.onDecafButtonPressed += Button_onDecafButtonPressed;
        EspMachineButton.onRegularButtonPressed += Button_onRegualrButtonPressed;

        ChangeLightColor(1);
    }

    private void OnDisable()
    {
        EspMachineButton.onBlondeButtonPressed -= Button_onBlondeButtonPressed;
        EspMachineButton.onDecafButtonPressed -= Button_onDecafButtonPressed;
        EspMachineButton.onRegularButtonPressed -= Button_onRegualrButtonPressed;
    }


    private void Button_onBlondeButtonPressed()
    {
        ChangeLightColor(0);
    }
    private void Button_onRegualrButtonPressed()
    {
        ChangeLightColor(1);
    }
    private void Button_onDecafButtonPressed()
    {
        ChangeLightColor(2);
    }


    private void ChangeLightColor(int _targetLight)
    {
        int _counter = 0;
        foreach (var renderer in lightRenderers)
        {
            if (_counter == _targetLight)
            {
                renderer.material = lightMaterials[1];
                _counter++;
                continue;
            }

            renderer.material = lightMaterials[0];
            _counter++;
        }
    }
}