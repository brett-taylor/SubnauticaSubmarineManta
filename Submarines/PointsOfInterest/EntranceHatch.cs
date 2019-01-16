using UnityEngine;

namespace Submarines.PointsOfInterest
{
    /**
     * Teleport hatch entrance.
     */
     [ProtoBuf.ProtoContract]
    public class EntranceHatch : HandTarget, IHandTarget
    {
        public string HoverText { get; set; }
        public HandReticle.IconType HoverHandReticle { get; set; }
        public GameObject TeleportTarget { get; set; }
        public bool EnteringSubmarine { get; set; }
        public Submarine Submarine { get; set; }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Utilities.Log.Print("Submarine " + (Submarine == null ? "Null" : "Not Null"));
            }
        }

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
