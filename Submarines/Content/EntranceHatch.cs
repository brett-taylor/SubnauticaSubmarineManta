using UnityEngine;

namespace Submarines.Content
{
    /**
     * Teleport hatch entrance.
     */
    public class EntranceHatch : HandTarget, IHandTarget
    {
        public string HoverText { get; set; }
        public HandReticle.IconType HoverHandReticle { get; set; }
        public GameObject TeleportTarget { get; set; }
        public bool EnteringSubmarine { get; set; }
        public Submarine Submarine { get; set; }

        public void OnHandClick(GUIHand hand)
        {
            if (Submarine == null)
            {
                return;
            }

            if (EnteringSubmarine)
            {
                Submarine.EnterSubmarine();
            }
            else
            {
                Submarine.LeaveSubmarine();
            }

            Player.main.transform.position = TeleportTarget.transform.position;
        }

        public void OnHandHover(GUIHand hand)
        {
            HandReticle.main.SetInteractTextRaw(HoverText, string.Empty);
            HandReticle.main.SetIcon(HoverHandReticle, 1f);
        }
    }
}
