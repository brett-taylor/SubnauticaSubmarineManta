using UnityEngine;

namespace Submarines.DefaultCyclopsContent
{
    /**
     * Adds collision sounds to the submarine as found on the cyclops.
     * 
     * If you wish to set the sounds that the cyclops use.
     * Set the following:
     * ImpactHitHard to the FMOD asset named "impact_solid_hard"
     * ImpactHitMedium to the FMOD asset named "impact_solid_medium"
     * ImpactHitSoft to the FMOD asset named "impact_solid_soft"
     */
    public class CyclopsCollisionSounds : MonoBehaviour
    {
        public FMODAsset ImpactHitHard { get; set; }
        public FMODAsset ImpactHitMedium { get; set; }
        public FMODAsset ImpactHitSoft { get; set; }

        public void Start()
        {
            if (EntryPoint.LOAD_DEFAULT_CYCLOPS_ASSETS)
            {
                if (ImpactHitHard == null)
                {
                    ImpactHitHard = CyclopsDefaultAssets.COLLISION_IMPACT_SOLID_HARD;
                }

                if (ImpactHitMedium == null)
                {
                    ImpactHitMedium = CyclopsDefaultAssets.COLLISION_IMPACT_SOLID_MEDIUM;
                }

                if (ImpactHitSoft == null)
                {
                    ImpactHitSoft = CyclopsDefaultAssets.COLLISION_IMPACT_SOLID_SOFT;
                }
            }

            if (ImpactHitHard == null)
            {
                Utilities.Log.Error("CollisionSounds::ImpactHitHard is not set. To set attach the FMOD asset name 'impact_solid_hard' ");
            }

            if (ImpactHitMedium == null)
            {
                Utilities.Log.Error("CollisionSounds::ImpactHitMedium is not set. To set attach the FMOD asset name 'impact_solid_medium' ");
            }

            if (ImpactHitSoft == null)
            {
                Utilities.Log.Error("CollisionSounds::ImpactHitMedium is not set. To set attach the FMOD asset name 'impact_solid_soft' ");
            }
        }

        private void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.CompareTag("Creature") || col.gameObject.CompareTag("Player"))
            {
                return;
            }

            if (ImpactHitHard == null || ImpactHitMedium == null || ImpactHitSoft == null)
            {
                return;
            }

            float magnitude = col.relativeVelocity.magnitude;
            float soundRadiusObsolete = Mathf.Clamp01(magnitude / 8f);
            if (magnitude > 8f)
            {
                Utils.PlayFMODAsset(ImpactHitHard, col.contacts[0].point, soundRadiusObsolete);
            }
            else if (magnitude > 4f)
            {
                Utils.PlayFMODAsset(ImpactHitMedium, col.contacts[0].point, soundRadiusObsolete);
            }
            else
            {
                Utils.PlayFMODAsset(ImpactHitSoft, col.contacts[0].point, soundRadiusObsolete);
            }
        }
    }
}
