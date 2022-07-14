using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorScreen : MonoBehaviour
{
    public static MonitorScreen Instance { get; private set; }

    public event Action<Order> OnOrderCreated; 
    
    [SerializeField] private Material[] buttonColors;
    
    [SerializeField] private MonitorSizesButton[] monitorSizesButtons;
    [SerializeField] private MonitorShotsButton[] monitorShotsButtons;
    [SerializeField] private MonitorEspressoButton[] monitorEspressoButtons;
    [SerializeField] private MonitorSyrupButton[] monitorSyrupButtons;
    [SerializeField] private MonitorOrderBevButton[] monitorBevButtons;
    [SerializeField] private MonitorTemperatureButton[] monitorTemperatureButtons;
    [SerializeField] private MonitorMilkButton[] monitorMikButtons;

    [SerializeField] private MonitorPageButton[] monitorPageButtons;

    [SerializeField] private MonitorPayButton monitorPayButton;


    private Order _order;

    private WaitForSeconds _resetButtonsWaitForSeconds;
    
    private float _resetTimer = .1f;
    

    private void Start()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;

        _resetButtonsWaitForSeconds = new WaitForSeconds(_resetTimer);
        
        SetPageButtonActive(monitorPageButtons[0]);
    }
    public void CreateOrder()
    {
        _order = new Order(
            GetActiveMonitorSizeButtonName(),
            GetActiveMonitorShotsButtonName(),
            GetActiveMonitorEspressoButtonName(),
            GetActiveMonitorSyrupButtonName(),
            GetActiveMonitorBeverageButtonName(),
            GetActiveMonitorTemperatureButtonName(),
            GetActiveMonitorMilksButtonName()
            );
        
        OnOrderCreated?.Invoke(_order);
        
        StartCoroutine(ResetButtonActivity());
    }

    private IEnumerator ResetButtonActivity()
    {
        monitorPayButton.SetColor(buttonColors[1]);
        SetAllButtonsInactive();
        yield return _resetButtonsWaitForSeconds;
        monitorPayButton.SetColor(buttonColors[0]);
    }

    private void SetAllButtonsInactive()
    {
        foreach (var button in monitorSizesButtons)
        {
            button.SetColor(buttonColors[0]);
            button.SetIsActive(false);
        }

        foreach (var button in monitorShotsButtons)
        {
            button.SetColor(buttonColors[0]);
            button.SetIsActive(false);
        }

        foreach (var button in monitorEspressoButtons)
        {
            button.SetColor(buttonColors[0]);
            button.SetIsActive(false);
        }

        foreach (var button in monitorSyrupButtons)
        {
            button.SetColor(buttonColors[0]);
            button.SetIsActive(false);
        }

        foreach (var button in monitorBevButtons)
        {
            button.SetColor(buttonColors[0]);
            button.SetIsActive(false);
        }

        foreach (var button in monitorTemperatureButtons)
        {
            button.SetColor(buttonColors[0]);
            button.SetIsActive(false);
        }

        foreach (var button in monitorMikButtons)
        {
            button.SetColor(buttonColors[0]);
            button.SetIsActive(false);
        }

        SetPageButtonActive(monitorPageButtons[0]);
    }

    private string GetActiveMonitorSizeButtonName()
    {
        foreach (var button in monitorSizesButtons)
        {
            if (button.GetIsActive())
            {
                return button.GetButtonOrderName();
            }
        }

        Debug.LogWarning("can't find active button");
        return OrderDictionary.SIZES[0];
    }

    private string GetActiveMonitorShotsButtonName()
    {
        foreach (var button in monitorShotsButtons)
        {
            if (button.GetIsActive())
            {
                return button.GetButtonOrderName();
            }
        }

        Debug.LogWarning("can't find active button");
        return OrderDictionary.SHOTS[0];
    }

    private string GetActiveMonitorEspressoButtonName()
    {
        foreach (var button in monitorEspressoButtons)
        {
            if (button.GetIsActive())
            {
                return button.GetButtonOrderName();
            }
        }

        Debug.LogWarning("can't find active button");
        return OrderDictionary.ESPRESSOS[0];
    }

    private string GetActiveMonitorSyrupButtonName()
    {
        foreach (var button in monitorSyrupButtons)
        {
            if (button.GetIsActive())
            {
                return button.GetButtonOrderName();
            }
        }

        Debug.LogWarning("can't find active button");
        return OrderDictionary.SYRUPS[0];
    }

    private string GetActiveMonitorBeverageButtonName()
    {
        foreach (var button in monitorBevButtons)
        {
            if (button.GetIsActive())
            {
                return button.GetButtonOrderName();
            }
        }

        //should never happen
        Debug.LogWarning("can't find active button");
        return OrderDictionary.BEVERAGES[0];
    }

    private string GetActiveMonitorTemperatureButtonName()
    {
        foreach (var button in monitorTemperatureButtons)
        {
            if (button.GetIsActive())
            {
                return button.GetButtonOrderName();
            }
        }

        Debug.LogWarning("can't find active button");
        return OrderDictionary.TEMPERATURES[0];
    }

    private string GetActiveMonitorMilksButtonName()
    {
        foreach (var button in monitorMikButtons)
        {
            if (button.GetIsActive())
            {
                return button.GetButtonOrderName();
            }
        }

        Debug.LogWarning("can't find active button");
        return OrderDictionary.MILKS[0];
    }

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
                button.SetIsActive(true);
            }
            else
            {
                button.SetColor(buttonColors[0]);
                button.SetIsActive(false);
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
                button.SetIsActive(true);
            }
            else
            {
                button.SetColor(buttonColors[0]);
                button.SetIsActive(false);
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
                button.SetIsActive(true);
            }
            else
            {
                button.SetColor(buttonColors[0]);
                button.SetIsActive(false);
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
                button.SetIsActive(true);
            }
            else
            {
                button.SetColor(buttonColors[0]);
                button.SetIsActive(false);
            }
        }
    }

    public void SetMonitorBevButtonActive(MonitorOrderBevButton currentButton)
    {
        foreach (MonitorOrderBevButton button in monitorBevButtons)
        {
            if (button == currentButton)
            {
                button.SetColor(buttonColors[1]);
                button.SetIsActive(true);
            }
            else
            {
                button.SetColor(buttonColors[0]);
                button.SetIsActive(false);
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
                button.SetIsActive(true);
            }
            else
            {
                button.SetColor(buttonColors[0]);
                button.SetIsActive(false);
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
                button.SetIsActive(true);
            }
            else
            {
                button.SetColor(buttonColors[0]);
                button.SetIsActive(false);
            }
        }
    }
}