using UnityEngine;

namespace Odyssey.Components
{
    /**
     * Okay so heres the deal:
     * When sn deserliazes the odyssey it loses references that components has to other components.
     * Even if we get them through GetComponent rather than referencing them ourself.
     * So this script will just call a method to set up the remaining part of the odyssey where references will not be lost.
     * 
     * Yes this is messy code and yes hopefully a better saving/loading system will be created that works for mods and custom components
     * than the system we have now. This is a messy fix.
     * 
     * TO:DO One day fix this bug.
     */
    public class OdysseySerializationFixer : MonoBehaviour
    {
        public void Start()
        {
            Core.OdysseyMod.SetUpOdyssey(gameObject);
            Destroy(this);
        }
    }
}
