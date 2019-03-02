using UnityEngine;

namespace Submarines.Content.Death
{
    /**
     * Kills the player if they are inside the sub when the sub dies.
     */
    public class KillPlayerInsideOnSubDeath : MonoBehaviour
    {
        private bool isInside = false;

        public void OnPlayerEnteredSubmarine()
        {
            isInside = true;
        }

        public void OnPlayerExitedSubmarine()
        {
            isInside = false;
        }

        public void OnDeathOccuring()
        {
            if (isInside)
            {
                Player.main.liveMixin.Kill(DamageType.Fire);
            }
        }
    }
}
