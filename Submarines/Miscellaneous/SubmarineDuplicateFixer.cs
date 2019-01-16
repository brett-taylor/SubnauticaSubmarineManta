using UnityEngine;

namespace Submarines.Miscellaneous
{
    /**
     * When we create a submarine. The submarine is first created at (-5000, -5000, -5000). SMLHelper's Fixer does not consider vehicles that have physics.
     * As when they have physics they can go flying...
     * This deletes all Submarines in a 2000m range of the (-5000, -5000, -5000) point.
     * NOTE: This is done in absolute terms.
     * TO:DO Create a better system where infact submarines spawn with no collision turned on at all then we can ues SMLHelper's Fixer once again.
     */
    public class SubmarineDuplicateFixer : MonoBehaviour
    {
        private static readonly float XYZ_POSITION = 5000f;
        private static readonly float ERROR_MARGIN = 2000f;

        private float startTime;

        public void Start()
        {
            startTime = Time.time;
        }

        public void Update()
        {
            if (Time.time > startTime)
            {
                if (InRange(transform.position.x) && InRange(transform.position.y) && InRange(transform.position.z))
                {
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(this);
                }
            }
        }

        private bool InRange(float number)
        {
            // If number is between e.g. -4000 and -6000 
            number = Mathf.Abs(number);
            return (number >= (XYZ_POSITION - ERROR_MARGIN) && number <= (XYZ_POSITION + ERROR_MARGIN));
        }
    }
}
