using UnityEngine;

namespace Submarines.Content.Damage
{
    /**
     * Handles applying internal fire in the submarine.
     */
    public class InternalFireManager : MonoBehaviour
    {
        public LiveMixin SubmarineLiveMixin { get; set; }

        public void Start()
        {
            if (SubmarineLiveMixin == null)
            {
                Utilities.Log.Error("InternalFireManager has not had the submarine's LiveMixin assigned. Destroying...");
                Destroy(this);
                return;
            }
        }
    }
}
