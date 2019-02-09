using System.Collections.Generic;
using UnityEngine;

namespace Submarines.Content.Lighting
{
    /**
     * Holds references to lights that are marked "internal" or "external".
     * Allows ability to turn internal/external lights on/off.
     */
    public class LightsManager : MonoBehaviour
    {
        public List<Light> InternalLights { get; set; }
        public List<Light> ExternalLights { get; set; }
        public bool InternalLightsOn { get; private set; } = true;
        public bool ExternalLightsOn { get; private set; } = true;
        public float ExternalLightsOnIntensity { get; set; } = 1f;
        public float ExternalLightsOffIntensity { get; set; } = 0f;
        public float InternalLightsOnIntensity { get; set; } = 1f;
        public float InternalLightsOffIntensity { get; set; } = 0f;
        public bool EnableInternalLightsOnStart { get; set; } = true;
        public bool EnableExternalLightsOnStart { get; set; } = true;

        public void Start()
        {
            if (InternalLights == null)
            {
                InternalLights = new List<Light>();
            }

            if (ExternalLights == null)
            {
                ExternalLights = new List<Light>();
            }

            if (EnableInternalLightsOnStart)
            {
                EnableInternalLights();
            }
            else
            {
                DisableInternalLights();
            }

            if (EnableExternalLightsOnStart)
            {
                EnableExternalLights();
            }
            else
            {
                DisableExternalLights();
            }
        }

        public void EnableInternalLights()
        {
            InternalLightsOn = true;
            foreach(Light light in InternalLights)
            {
                light.intensity = InternalLightsOnIntensity;
            }
            SendMessage("InternalLightsTurnedOff", SendMessageOptions.DontRequireReceiver);
        }

        public void DisableInternalLights()
        {
            InternalLightsOn = false;
            foreach (Light light in InternalLights)
            {
                light.intensity = InternalLightsOffIntensity;
            }

            SendMessage("InternalLightsTurnedOn", SendMessageOptions.DontRequireReceiver);
        }

        public void ToggleInternalLights()
        {
            if (InternalLightsOn)
            {
                DisableInternalLights();
            }
            else
            {
                EnableInternalLights();
            }
        }

        public void EnableExternalLights()
        {
            ExternalLightsOn = true;
            foreach (Light light in ExternalLights)
            {
                light.intensity = ExternalLightsOnIntensity;
            }

            SendMessage("ExternalLightsTurnedOff", SendMessageOptions.DontRequireReceiver);
        }

        public void DisableExternalLights()
        {
            ExternalLightsOn = false;
            foreach (Light light in ExternalLights)
            {
                light.intensity = ExternalLightsOffIntensity;
            }

            SendMessage("ExternalLightsTurnedOn", SendMessageOptions.DontRequireReceiver);
        }

        public void ToggleExternalLights()
        {
            if (ExternalLightsOn)
            {
                DisableExternalLights();
            }
            else
            {
                EnableExternalLights();
            }
        }
    }
}
