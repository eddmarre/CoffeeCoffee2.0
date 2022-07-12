using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorScreen : MonoBehaviour
{
    public static MonitorScreen Instance { get; private set; }
    [SerializeField] private Material[] buttonColors;

    private void Start()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;

        SetPageButtonActive(monitorPageButtons[0]);
    }


    [SerializeField] private MonitorSizesButton[] monitorSizesButtons;
    [SerializeField] private MonitorShotsButton[] monitorShotsButtons;
    [SerializeField] private MonitorEspressoButton[] monitorEspressoButtons;
    [SerializeField] private MonitorSyrupButton[] monitorSyrupButtons;
    [SerializeField] private MonitorBevButton[] monitorBevButtons;
    [SerializeField] private MonitorTemperatureButton[] monitorTemperatureButtons;
    [SerializeField] private MonitorMilkButton[] monitorMikButtons;
    [SerializeField] private MonitorPageButton[] monitorPageButtons;


    public void SetPageButtonActive(MonitorPageButton currentButton)
    {
        
        foreach (MonitorPageButton button in monitorPageButtons)
        {
            
            button.SetPageScreenVisible(false);
            button.SetMaterial(buttonColors[0]);
            
            if (button == currentButton)
            {
                button.SetMaterial(buttonColors[1]);
                button.SetPageScreenVisible(true);
            }

        }
    }

    public void SetMonitorSizeButtonActive(MonitorSizesButton currentButton)
    {
        foreach (MonitorSizesButton button in monitorSizesButtons)
        {
            if (button == currentButton)
            {
                button.SetColor(buttonColors[1]);
            }
            else
            {
                button.SetColor(buttonColors[0]);
            }
        }
    }

    public void SetMonitorShotsButtonActive(MonitorShotsButton currentButton)
    {
        foreach (MonitorShotsButton button in monitorShotsButtons)
        {
            if (button == currentButton)
            {
                button.SetColor(buttonColors[1]);
            }
            else
            {
                button.SetColor(buttonColors[0]);
            }
        }
    }

    public void SetMonitorEspressoButtonActive(MonitorEspressoButton currentButton)
    {
        foreach (MonitorEspressoButton button in monitorEspressoButtons)
        {
            if (button == currentButton)
            {
                button.SetColor(buttonColors[1]);
            }
            else
            {
                button.SetColor(buttonColors[0]);
            }
        }
    }

    public void SetMonitorSyrupButtonActive(MonitorSyrupButton currentButton)
    {
        foreach (MonitorSyrupButton button in monitorSyrupButtons)
        {
            if (button == currentButton)
            {
                button.SetColor(buttonColors[1]);
            }
            else
            {
                button.SetColor(buttonColors[0]);
            }
        }
    }

    public void SetMonitorBevButtonActive(MonitorBevButton currentButton)
    {
        foreach (MonitorBevButton button in monitorBevButtons)
        {
            if (button == currentButton)
            {
                button.SetColor(buttonColors[1]);
            }
            else
            {
                button.SetColor(buttonColors[0]);
            }
        }
    }

    public void SetMonitorTemperatureButtonActive(MonitorTemperatureButton currentButton)
    {
        foreach (MonitorTemperatureButton button in monitorTemperatureButtons)
        {
            if (button == currentButton)
            {
                button.SetColor(buttonColors[1]);
            }
            else
            {
                button.SetColor(buttonColors[0]);
            }
        }
    }

    public void SetMonitorMilkButtonActive(MonitorMilkButton currentButton)
    {
        foreach (MonitorMilkButton button in monitorMikButtons)
        {
            if (button == currentButton)
            {
                button.SetColor(buttonColors[1]);
            }
            else
            {
                button.SetColor(buttonColors[0]);
            }
        }
    }
}