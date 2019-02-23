using System.Collections.Generic;
using UnityEngine;

namespace Submarines.Content.Lighting
{
    /**
     * Allows the lighting to swap to a colour then flicker between that colour and black.
     */
    public class EmergencyLighting : MonoBehaviour
    {
        public List<Light> LightsAffected { get; set; }
        public Color FlickerColor { get; set; }
        public float FlickerTime { get; set; } = 2f;
        public float FlickerIntensity { get; set; } = 2f;
        public bool IsRunning { get; private set; } = false;
        public float LerpColorTime { get; set; } = 0.25f;

        private Color startEndColor;
        private float originalIntensity = 1f;
        private bool isStarting = false;
        private bool isWantingToEnd = false;
        private bool isEnding = false;
        private float currentTimer = 0f;
        private bool isAdding = true;

        public virtual void Start()
        {
            if (LightsAffected == null || LightsAffected.Count == 0)
            {
                Utilities.Log.Print("EmergencyLighting has no lights assigned. Destroying.");
                Destroy(this);
                return;
            }

            if (FlickerColor == null)
            {
                Utilities.Log.Print("EmergencyLighting doesn't have LerpOneColor assigned. Destroying.");
                Destroy(this);
                return;
            }

            originalIntensity = LightsAffected[0].intensity;
            startEndColor = LightsAffected[0].color;
        }

        public virtual void Update()
        {
            if (IsRunning == false)
            {
                return;
            }

            if (isStarting)
            {
                currentTimer += Time.deltaTime;
                Color newColor = UWE.Utils.LerpColor(startEndColor, FlickerColor, currentTimer / LerpColorTime);
                float newIntensity = Mathf.SmoothStep(originalIntensity, FlickerIntensity, currentTimer / LerpColorTime);
                AssignColorToLights(newColor);
                AssignIntensityToLights(newIntensity);
                if (currentTimer > LerpColorTime)
                {
                    isStarting = false;
                    currentTimer = FlickerTime + 1f;
                }
            }
            else if (isEnding)
            {
                currentTimer += Time.deltaTime;
                Color newColor = UWE.Utils.LerpColor(FlickerColor, startEndColor, currentTimer / LerpColorTime);
                float newIntensity = Mathf.SmoothStep(FlickerIntensity, originalIntensity, currentTimer / LerpColorTime);
                AssignColorToLights(newColor);
                AssignIntensityToLights(newIntensity);
                if (currentTimer > LerpColorTime)
                {
                    isEnding = false;
                    IsRunning = false;
                    currentTimer = 0f;
                }
            }
            else
            {
                if (isAdding)
                {
                    currentTimer += Time.deltaTime;
                }
                else
                {
                    currentTimer -= Time.deltaTime;
                }

                if (currentTimer > FlickerTime)
                {
                    isAdding = false;
                    if (isWantingToEnd == true)
                    {
                        currentTimer = 0f;
                        isEnding = true;
                        isWantingToEnd = false;
                        return;
                    }
                }
                else if (currentTimer < 0)
                {
                    isAdding = true;
                }

                float newIntensity = Mathf.SmoothStep(0f, FlickerIntensity, currentTimer / FlickerTime);
                AssignIntensityToLights(newIntensity);
            }
        }

        private void AssignColorToLights(Color color)
        {
            foreach(Light light in LightsAffected)
            {
                light.color = color;
            }
        }

        private void AssignIntensityToLights(float intensity)
        {
            foreach (Light light in LightsAffected)
            {
                light.intensity = intensity;
            }
        }

        public virtual void EnableEmergencyLighting()
        {
            if (IsRunning)
            {
                return;
            }

            IsRunning = true;
            isStarting = true;
            isAdding = true;
            isWantingToEnd = false;
            isEnding = false;
            currentTimer = 0f;
        }

        public virtual void DisableEmergencyLighting()
        {
            if (IsRunning == false)
            {
                return;
            }

            IsRunning = true;
            isStarting = false;
            isEnding = false;
            isWantingToEnd = true;
        }
    }
}
