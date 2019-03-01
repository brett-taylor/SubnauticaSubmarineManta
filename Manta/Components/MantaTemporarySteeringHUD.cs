using Submarines.Content.Lighting;
using Submarines.DefaultCyclopsContent;
using Submarines.Engine;
using System.Collections;
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
            if (GetComponent<EngineManager>().IsPoweredUp())
            {
                GUILayout.Label("Current engine state: " + GetComponent<EngineManager>().EngineState);
                GUILayout.Label("Press 6 to turn engine off. 7-0 for other engine states.");
            }
            else
            {
                GUILayout.Label("Press 6 to turn engine on.");
            }

            GUILayout.Label("Internal Lights On: " + GetComponent<LightsManager>().InternalLightsOn);
            GUILayout.Label("External Lights On: " + GetComponent<LightsManager>().ExternalLightsOn);
            GUILayout.Label("Press LShift and 6 to toggle internal lights");
            GUILayout.Label("Press LShift and 7 to toggle external lights");
            GUILayout.Label("Emergency Lights On: " + GetComponent<EmergencyLighting>().IsRunning);
            GUILayout.Label("Press LShift and 8 to enable emergency lighting");
            GUILayout.Label("Press LShift and 9 to disable emergency lighting");
        }

        public void Update()
        {
            if (isSteering == false || isInSubmarine == false)
            {
                return;
            }

            if (GetComponent<EngineManager>().IsPoweredUp())
            {
                if (!Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha6))
                {
                    IEnumerator coroutine = GetComponent<EngineManager>().PowerDown(0f);
                    GetComponent<EngineManager>().StartCoroutine(coroutine);
                }
                if (!Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha7))
                {
                    GetComponent<EngineManager>().SetNewEngineState(EngineState.SLOW);
                }
                if (!Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha8))
                {
                    GetComponent<EngineManager>().SetNewEngineState(EngineState.NORMAL);
                }
                if (!Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha9))
                {
                    GetComponent<EngineManager>().SetNewEngineState(EngineState.FAST);
                }
                if(!Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha0))
                {
                    GetComponent<EngineManager>().SetNewEngineState(EngineState.SPECIAL);
                }
            }
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha6))
                {
                    IEnumerator coroutine = GetComponent<EngineManager>().PowerUp(EngineState.NORMAL, CyclopsStartupPowerDownSequence.TOTAL_START_UP_DELAY);
                    GetComponent<EngineManager>().StartCoroutine(coroutine);
                }
            }

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha6))
            {
                GetComponent<LightsManager>().ToggleInternalLights();
            }
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha7))
            {
                GetComponent<LightsManager>().ToggleExternalLights();
            }

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha8))
            {
                GetComponent<EmergencyLighting>().EnableEmergencyLighting();
            }
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha9))
            {
                GetComponent<EmergencyLighting>().DisableEmergencyLighting();
            }
        }
    }
}
