using Submarines.Movement;
using UnityEngine;

namespace Submarines.PointsOfInterest
{
    /**
     * Steering console.
     */
    public class SteeringConsole : MonoBehaviour, IHandTarget
    {
        public MovementController MovementController { get; set; } = null;
        public GameObject ParentWhilePilotingGO { get; set; } = null;
        public string HoverText { get; set; } = "Pilot Sub";
        public HandReticle.IconType HoverHandReticle { get; set; } = HandReticle.IconType.Hand;

        private bool isPiloting = false;

        public void Start()
        {
            if (MovementController == null || ParentWhilePilotingGO == null)
            {
                Utilities.Log.Error("Steering console has no link to MovementController or link to ParentWhilePilotingGO");
                Destroy(this);
                return;
            }
        }

        public void Update()
        { 
            if (isPiloting == false)
            {
                return;
            }

            HandReticle.main.SetUseText("E to exit");
            if (Input.GetKeyDown(KeyCode.E))
            {
                EndPiloting();   
            }
        }

        public void OnHandClick(GUIHand hand)
        {
            if (isPiloting == false)
            {
                StartPiloting();
            }
        }

        public void OnHandHover(GUIHand hand)
        {
            if (isPiloting == false)
            {
                HandReticle.main.SetInteractTextRaw(HoverText, string.Empty);
                HandReticle.main.SetIcon(HoverHandReticle, 1f);
            }
            else
            {
                HandReticle.main.SetInteractTextRaw(string.Empty, string.Empty);
                HandReticle.main.SetIcon(HandReticle.IconType.Default, 1f);
            }
        }

        public void StartPiloting()
        {
            isPiloting = true;
            MovementController.IsControllable = true;
            Player.main.transform.parent = ParentWhilePilotingGO.transform;
        }

        public void EndPiloting()
        {
            isPiloting = false;
            MovementController.IsControllable = false;
            Player.main.transform.parent = null;
        }
    }
}
