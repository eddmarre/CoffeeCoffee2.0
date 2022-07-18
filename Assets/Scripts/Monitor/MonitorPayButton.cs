using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MonitorPayButton : Interactable
{
        [SerializeField] private new Renderer renderer;
        [SerializeField] private new BoxCollider collider;
        
        
        [ClientRpc]
        protected override void RpcDeactivateInteractable(Interactable interactable, Player player)
        {
                if (this != interactable) return;
                MonitorScreen.Instance.CreateOrder();
        }
        
        public void SetColor(Material color)
        {
                renderer.material = color;
        }
}
