using Submarines.Engine;
using UnityEngine;

namespace Odyssey.Components
{
    public class OdysseyTemporarySteeringHUD : MonoBehaviour
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

            if (isSteering == false)
            {
                Rect rect2 = new Rect(Screen.width - 410, 5, 400, 50);
                Rect windowRect2 = GUILayout.Window(2353, rect2, (id) =>
                {
                    GUILayout.Box("In submarine BUT NOT steering.");
                }, "Odyssey Temporary Steering HUD");

                return;
            }

            Rect rect = new Rect(Screen.width - 410, 5, 400, 50);
            Rect windowRect = GUILayout.Window(2353, rect, (id) =>
            {

                GUILayout.Box("In submarine AND steering.");
                GUILayout.Label("Current engine state: " + GetComponent<EngineManager>().EngineState);
            }, "Odyssey Temporary Steering HUD");

            return;
        }

        public void Update()
        {
            if (isSteering == false || isInSubmarine == false)
            {
                return;
            }
        }
    }
}
