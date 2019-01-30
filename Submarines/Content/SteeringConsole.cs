using Submarines.Movement;
using UnityEngine;
using UWE;

namespace Submarines.Content
{
    /**
     * Steering console.
     */
    public class SteeringConsole : PilotingChair
    {
        public Submarine Submarine { get; set; } = null;
        public MovementController MovementController { get; set; } = null;
        public GameObject ParentWhilePilotingGO { get; set; } = null;
        public GameObject LeftHandIKTarget { get; set; } = null;
        public GameObject RightHandIKTarget { get; set; } = null;
        public string HoverText { get; set; } = "Pilot Sub";
        public HandReticle.IconType HoverHandReticle { get; set; } = HandReticle.IconType.Hand;

        private bool isPiloting = false;

        public new void Start()
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
                StopPiloting();
            }
        }

        public override void OnHandHover(GUIHand hand)
        {
            if (IsValidHandTarget(hand))
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
        }

        public override void OnHandClick(GUIHand hand)
        {
            if (isPiloting == false && IsValidHandTarget(hand))
            {
                StartPiloting();
            }
        }

        private bool IsValidHandTarget(GUIHand hand)
        {
            return hand.IsFreeToInteract() && hand.player && hand.player.GetCurrentSub() == Submarine && hand.player.GetMode() == Player.Mode.Normal;
        }

        private void StartPiloting()
        {
            isPiloting = true;
            MovementController.IsControllable = true;
            Player.main.playerDeathEvent.AddHandler(gameObject, new Event<Player>.HandleFunction(OnPlayerDeath));
            Player.main.armsController.SetWorldIKTarget(
                LeftHandIKTarget == null ? null : LeftHandIKTarget.transform, 
                RightHandIKTarget == null ? null : RightHandIKTarget.transform
            );
            SetPlayerToPilotingMode();
            Submarine.OnSteeringStart();
        }

        public void StopPiloting()
        {
            isPiloting = false;
            Player.main.playerDeathEvent.RemoveHandler(gameObject, new Event<Player>.HandleFunction(OnPlayerDeath));
            MovementController.IsControllable = false;
            Player.main.armsController.SetWorldIKTarget(null, null);
            SetPlayerToNormalMode();
            Submarine.OnSteeringEnd();
        }

        private void SetPlayerToPilotingMode()
        {
            GameInput.ClearInput();
            typeof(Player).GetField("currChair", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Player.main, this);
            Player.main.cinematicModeActive = true;
            MainCameraControl.main.lookAroundMode = true;
            Player.main.transform.parent = ParentWhilePilotingGO.transform;
            UWE.Utils.ZeroTransform(Player.main.transform);
            typeof(Player).GetField("mode", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Player.main, Player.Mode.Piloting);
            Inventory.main.quickSlots.DeselectImmediate();
            Player.main.playerModeChanged.Trigger(Player.Mode.Piloting);
        }

        private void SetPlayerToNormalMode()
        {
            GameInput.ClearInput();
            Player.main.transform.parent = null;
            MainCameraControl.main.lookAroundMode = false;
            Player.main.cinematicModeActive = false;
            typeof(Player).GetField("mode", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Player.main, Player.Mode.Normal);
            typeof(Player).GetField("currChair", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Player.main, null);
            Player.main.playerModeChanged.Trigger(Player.Mode.Normal);
        }

        private void OnPlayerDeath(Player player)
        {
            Utilities.Log.Print("SteeringConsole::OnPlayerDeath");
        }

        private void CyclopsDeathEvent()
        {
            Utilities.Log.Print("SteeringConsole::CyclopsDeathEvent");
        }
    }
}
