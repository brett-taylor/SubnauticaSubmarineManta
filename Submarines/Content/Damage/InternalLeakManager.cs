using System.Collections.Generic;
using UnityEngine;

namespace Submarines.Content.Damage
{
    /**
     * Handles showing and hiding leaks.
     */
    public class InternalLeakManager : MonoBehaviour
    {
        public List<GameObject> LeakPrefabs { get; set; }
    }
}
