using UnityEngine;

namespace Monitor
{
    public class MonitorEspressoPage : MonoBehaviour
    {
        [SerializeField] private MonitorSizesButton[] monitorSizesButtons;
        [SerializeField] private MonitorShotsButton[] monitorShotsButtons;
        [SerializeField] private MonitorEspressoButton[] monitorEspressoButtons;


        public void SetCollisions(bool value)
        {
            foreach (var button in monitorEspressoButtons)
            {
                button.SetCollision(value);
            }

            foreach (var button in monitorShotsButtons)
            {
                button.SetCollision(value);
            }

            foreach (var button in monitorSizesButtons)
            {
                button.SetCollision(value);
            }
        }
    }
}