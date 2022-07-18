using System;
using Mirror;
using UnityEngine;

public class EspMachineButton : Interactable
{
    // public static EspMachineButton Instance { get; private set; }
    //
    // private void Awake()
    // {
    //     if(Instance!=null)
    //         Destroy(Instance);
    //     Instance = this;
    // }


    [Serializable]
    private enum ButtonType
    {
        Blonde,
        Regular,
        Decaf,
        Pour,
        Shots,
        Temperature,
        Lever
    }

    [SerializeField] private ButtonType button;

    public static event Action onBlondeButtonPressed;
    public static event Action onDecafButtonPressed;
    public static event Action onRegularButtonPressed;
    public static event Action onPourButtonPressed;
    public static event Action onShotsButtonPressed;
    public static event Action onTemperatureButtonPressed;
    public static event Action onLeverButtonPressed;

    [ClientRpc]
    protected override void RpcDeactivateInteractable(Interactable interactable, Player player)
    {
        //ensures this method will only get called for this button
        if (this != interactable) return;

        if (TryGetComponent(out Animator animator))
            animator.SetTrigger("isPressed");

        switch (button)
        {
            case ButtonType.Blonde:
                onBlondeButtonPressed?.Invoke();
                break;
            case ButtonType.Decaf:
                onDecafButtonPressed?.Invoke();
                break;
            case ButtonType.Regular:
                onRegularButtonPressed?.Invoke();
                break;
            case ButtonType.Pour:
                onPourButtonPressed?.Invoke();
                break;
            case ButtonType.Shots:
                onShotsButtonPressed?.Invoke();
                break;
            case ButtonType.Temperature:
                onTemperatureButtonPressed?.Invoke();
                break;
            case ButtonType.Lever:
                onLeverButtonPressed?.Invoke();
                break;
        }
    }
}