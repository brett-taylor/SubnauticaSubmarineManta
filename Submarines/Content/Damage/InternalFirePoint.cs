using UnityEngine;

namespace Submarines.Content.Damage
{
    /**
     * Internal fire point
     * Created on each potential fire point as told by the InteralFireManager
     */
    public class InternalFirePoint : MonoBehaviour
    {
        public Fire Fire { get; set; }
        public InternalFireManager FireManager { get; set; }

        public void Start()
        {
            if (Fire == null)
            {
                Utilities.Log.Error("InternalFirePoint has not had Fire assigned. Destroying...");
                Destroy(this);
                return;
            }
        }

        public void FireExtinguished()
        {
            if (Fire.livemixin.health == 0 )
            {
                FireManager.FireExtinguished(transform.position);
                Destroy(this);
            }
        }
    }
} 
