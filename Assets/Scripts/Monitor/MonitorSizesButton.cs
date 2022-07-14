using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MonitorSizesButton : Interactable, IMonitorOrderButton
{
       [SerializeField] private new Renderer renderer;
       [SerializeField] private new BoxCollider collider;

       [Serializable]
       private enum SizeButton
       {
              Small,
              Medium,
              Large
       }

       [SerializeField] private SizeButton sizeButton;
       
       public void SetCollision(bool value)
       {
              collider.enabled = value;
       }
       [ClientRpc]
       protected override void RpcDeactivateInteractable(Interactable interactable)
       {
              if(this!=interactable) return;
              MonitorScreen.Instance.SetMonitorSizeButtonActive(this);
       }

       public void SetColor(Material color)
       {
              renderer.material = color;
       }

       public string GetButtonOrderName()
       {
              switch (sizeButton)
              {
                     case SizeButton.Small:
                            return OrderDictionary.SIZES[0];
                     case SizeButton.Medium:
                            return OrderDictionary.SIZES[1];
                     case SizeButton.Large:
                            return OrderDictionary.SIZES[2];
                     default:
                            return OrderDictionary.SIZES[0];
              }
       }
}
