using UnityEngine;

namespace PlayerAnimations.Core
{
    public class AnimatorDebugger : MonoBehaviour
    {
        public void PrintEvent(string s)
        {
            Utilities.Log.Print("PrintEvent: " + s + " called at: " + Time.time);
        }
    }
}
