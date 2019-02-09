using System.Collections.Generic;
using UnityEngine;

namespace Submarines.Content.Lighting
{
    /**
     * Allows the lighting to toggle between two colours and a start and ending color.
     */
    public class EmergencyLighting : MonoBehaviour
    {
        public List<Light> LightsAffected { get; set; }
        public Color StartEndColor { get; set; }
        public Color LerpOneColor { get; set; }
        public Color LerpTwoColor { get; set; }
        public float LerpTime { get; set; } = 1f;

        private bool isRunning = false;
        private bool isStarting = false;
        private bool isEnding = false;
        private Color nextColor;

        public virtual void Start()
        {
            if (LightsAffected == null || LightsAffected.Count == 0)
            {
                Utilities.Log.Print("EmergencyLighting has no lights assigned. Destroying.");
                Destroy(this);
                return;
            }

            if (StartEndColor == null)
            {
                Utilities.Log.Print("EmergencyLighting doesn't have StartEndColor assigned. Destroying.");
                Destroy(this);
                return;
            }

            if (LerpOneColor == null)
            {
                Utilities.Log.Print("EmergencyLighting doesn't have LerpOneColor assigned. Destroying.");
                Destroy(this);
                return;
            }

            if (LerpTwoColor == null)
            {
                Utilities.Log.Print("EmergencyLighting doesn't have LerpTwoColor assigned. Destroying.");
                Destroy(this);
                return;
            }
        }

        public virtual void Update()
        {
            if (isRunning == false)
            {
                return;
            }

            if (nextColor == null)
            {

            }
        }

        public virtual void EnableEmergencyLighting()
        {
            isStarting = true;
            isEnding = false;
            isRunning = true;
        }

        public virtual void DisableEmergencyLighting()
        {
            isStarting = false;
            isEnding = true;
            isRunning = true;
        }
    }
}
