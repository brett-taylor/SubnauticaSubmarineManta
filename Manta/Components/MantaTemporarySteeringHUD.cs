using Submarines.Engine;
using UnityEngine;

namespace Manta.Components
{
    public class MantaTemporarySteeringHUD : MonoBehaviour
    {
        private bool isInSubmarine = false;
        private bool isSteering = false;

        public void OnPlayerEnteredSubmarine()
        {
            isInSubmarine = true;
        }

        public void OnPlayerExitedSubmarine()
        {
            isInSubmarine = false;
        }

        public void OnSteeringStarted()
        {
            isSteering = true;
        }

        public void OnSteeringEnded()
        {
            isSteering = false;
        }

        private void OnGUI()
        {
            if (isInSubmarine == false)
            {
                return;
            }

            Rect rect = new Rect(Screen.width - 410, 5, 400, 50);
            Rect windowRect = GUILayout.Window(2353, rect, (id) =>
            {
                GUILayout.Box(isSteering ? "In submarine and steering" : "In submarine and not steering.");
               
                if (isSteering)
                {
                    DrawDrivingHUD();
                }
                else
                {
                    DrawNotDrivingHUD();
                }
            }, "Manta Temporary Steering HUD");

            return;
        }

        private void DrawNotDrivingHUD()
        {
        }

        private void DrawDrivingHUD()
        {
            GUILayout.Label("Current engine state: " + GetComponent<EngineManager>().EngineState);
            GUILayout.Label("Press 6 to set engine state to off");
            GUILayout.Label("Press 7 to set engine state to slow");
            GUILayout.Label("Press 8 to set engine state to normal");
            GUILayout.Label("Press 9 to set engine state to fast");
            GUILayout.Label("Press 0 to set engine state to silent running");
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                GetComponent<EngineManager>().SetNewEngineState(EngineState.OFF, false, true);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                GetComponent<EngineManager>().SetNewEngineState(EngineState.SLOW, false, true);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                GetComponent<EngineManager>().SetNewEngineState(EngineState.NORMAL, false, true);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                GetComponent<EngineManager>().SetNewEngineState(EngineState.FAST, false, true);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha0))
            {
                GetComponent<EngineManager>().SetNewEngineState(EngineState.SILENTRUNNING, false, true);
            }
        }
    }
}
