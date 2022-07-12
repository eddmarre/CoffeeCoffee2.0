using UnityEngine;

namespace Monitor
{
    public class MonitorBevPage : MonoBehaviour
    {
        [SerializeField] private MonitorBevButton[] monitorBevButtons;

        public void SetCollisions(bool value)
        {
            foreach (var button in monitorBevButtons)
            {
                button.SetCollision(value);
            }
        }
    }
}