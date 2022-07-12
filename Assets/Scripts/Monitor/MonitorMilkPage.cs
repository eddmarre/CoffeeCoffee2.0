using UnityEngine;

namespace Monitor
{
    public class MonitorMilkPage : MonoBehaviour
    {
        [SerializeField] private MonitorTemperatureButton[] monitorTemperatureButtons;
        [SerializeField] private MonitorMilkButton[] monitorMikButtons;

        public void SetCollisions(bool value)
        {
            foreach (var button in monitorTemperatureButtons)
            {
                button.SetCollision(value);
            }

            foreach (var button in monitorMikButtons)
            {
                button.SetCollision(value);
            }
        }
    }
}