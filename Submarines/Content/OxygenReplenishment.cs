using UnityEngine;

namespace Submarines.Content
{
    /**
     * Replenishes the oxygen of the player while inside the submarine.
     * TO:DO Take energy to replenish oxygen.
     */
    public class OxygenReplenishment : MonoBehaviour
    {
        public float OxygenPerSecond { get; set; } = 15f;
        public float OxygenEnergyCost { get; set; } = 0.1f;
        private bool shouldReplenish = false;

        public void OnPlayerEnteredSubmarine()
        {
            shouldReplenish = true;
        }

        public void OnPlayerExitedSubmarine()
        {
            shouldReplenish = false;
        }

        public void Update()
        {
            if (shouldReplenish == false)
            {
                return;
            }

            OxygenManager oxygen = Player.main.oxygenMgr;
            oxygen.AddOxygen(Time.deltaTime * OxygenPerSecond);
        }
    }
}
