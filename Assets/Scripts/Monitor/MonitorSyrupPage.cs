using UnityEngine;

namespace Monitor
{
    public class MonitorSyrupPage : MonoBehaviour
    {
        [SerializeField] private MonitorSyrupButton[] monitorSyrupButtons;


        public void SetCollisions(bool value)
        {
            foreach (var button in monitorSyrupButtons)
            {
                button.SetCollision(value);
            }
        }
    }
}