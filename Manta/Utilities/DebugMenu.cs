using Manta.Core;
using Submarines;
using Submarines.Movement;
using UnityEngine;

namespace Manta.Utilities
{
    /**
     * Debug menu. Messy code be living here.
     */
    public class DebugMenu : MonoBehaviour
    {
        private static Rect SIZE = new Rect(5, 5, 500, 800);
        private bool isOpen = false;
        private bool showCursor = false;
        private bool freezePlayer = false;
        private Submarine submarine;
        private MovementController movementController;
        private MovementStabiliser stabiliser;
        private GameObject teleportSpot;
        private Vector3 oldTeleportSpot;
        private bool isInside = false;
        private bool submarineIsParent = false;

        public void Update()
        {
            if (isOpen == false)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                showCursor = !showCursor;
            }

            if (showCursor)
            {
                UWE.Utils.alwaysLockCursor = false;
                UWE.Utils.lockCursor = false;
            }
            else
            {
                UWE.Utils.alwaysLockCursor = true;
                UWE.Utils.lockCursor = true;
            }

            if (freezePlayer)
            {
                Player.main.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        public void Open()
        {
            isOpen = true;
            showCursor = true;
        }

        public void Close()
        {
            isOpen = false;
            showCursor = false;
            UWE.Utils.alwaysLockCursor = true;
            UWE.Utils.lockCursor = true;
        }

        public void Toggle()
        {
            if (isOpen)
            {
                Close();
            }
            else
            {
                Open();
            }
        }

        public ProtoBuf.Meta.RuntimeTypeModel testm;
        private void OnGUI()
        {
            if (isOpen == false)
            {
                return;
            }

            Rect windowRect = GUILayout.Window(2352, SIZE, (id) =>
            {
                GUILayout.Box("Press p to show/hide curosr");

                if (GUILayout.Button("Components"))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        foreach (Component c in hit.rigidbody.gameObject.GetComponents(typeof(Component)))
                        {
                            Log.Print("Base Component: " + c.ToString());
                        }

                        foreach (Component c in hit.rigidbody.gameObject.GetComponentsInChildren(typeof(Component)))
                        {
                            Log.Print("Child " + c.gameObject + "  Component: " + c.ToString());
                        }
                    }
                }

                if (GUILayout.Button("Cyclops static mesh"))
                {
                    Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit raycastHit4;
                    if (Physics.Raycast(ray4, out raycastHit4))
                    {
                        if (raycastHit4.rigidbody.gameObject.name.ToLower().Contains("cyclops"))
                        {
                            Transform transform = raycastHit4.rigidbody.gameObject.transform.Find("CyclopsMeshStatic");
                            GameObject obj = (transform != null) ? transform.gameObject : null;
                            Log.Print("Size 1: " + obj.GetComponentInChildren<MeshFilter>().mesh.triangles.Length);
                            Log.Print("Size 2: " + obj.GetComponentInChildren<MeshFilter>().mesh.vertices.Length);
                            Destroy(obj);
                        }
                    }
                }

                if (GUILayout.Button("Delete All Mantas"))
                {
                    Submarine[] submarines = FindObjectsOfType<Submarine>();
                    Log.Print("Found " + submarines.Length + " Mantas to delete");
                    foreach (Submarine s in submarines)
                    {
                        Destroy(s.gameObject);
                    }
                }

                if (GUILayout.Button("Manta Count"))
                {
                    Submarine[] submarines = FindObjectsOfType<Submarine>();
                    Log.Print("Found " + submarines.Length + " Mantas to delete");
                    foreach (Submarine s in submarines)
                    {
                        Log.Print("Attached to gameobject: " + s.gameObject.name + " at position: " + s.gameObject.transform.position);
                    }
                }

                if (GUILayout.Button(freezePlayer ? "Unfreeze Player" : "Freeze Player"))
                {
                    freezePlayer = !freezePlayer;
                }

                if (GUILayout.Button("Connect to manta on cursor"))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        if (hit.rigidbody.gameObject.name.ToLower().Contains("manta"))
                        {
                            submarine = hit.rigidbody.gameObject.GetComponent<Submarine>();
                            movementController = hit.rigidbody.gameObject.GetComponent<MovementController>();
                            stabiliser = hit.rigidbody.gameObject.GetComponent<MovementStabiliser>();
                        }
                    }
                }

                if (GUILayout.Button("Spawn manta"))
                {
                    GameObject gameObject2 = MantaMod.CreateManta();
                    this.submarine = gameObject2.GetComponent<Submarine>();
                    this.movementController = gameObject2.GetComponent<MovementController>();
                    this.stabiliser = gameObject2.GetComponent<MovementStabiliser>();
                    gameObject2.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 22f;
                    gameObject2.transform.LookAt(Player.main.transform);
                }

                if (submarine == null)
                {
                    GUILayout.Box("No Submarine Found");
                    return;
                }

                if (GUILayout.Button(movementController.IsControllable ? "Release Control Of Submarine" : "Take Control Of Submarine"))
                {
                    movementController.IsControllable = !movementController.IsControllable;
                }

                if (GUILayout.Button(stabiliser.IsStabilising ? "No Longer Stabilise" : "Stabilse"))
                {
                    stabiliser.IsStabilising = !stabiliser.IsStabilising;
                }

                if (GUILayout.Button(submarineIsParent ? "Unparent from submarine." : "Parent to submarine"))
                {
                    if (submarineIsParent)
                    {
                        Player.main.transform.SetParent(null, true);
                    }
                    else
                    {
                        Player.main.transform.SetParent(submarine.transform, true);
                    }
                }

                if (GUILayout.Button("Buildings inside Manta Count"))
                {
                    Log.Print("Buildings inside manta count: " + submarine.GetModulesRoot().transform.childCount);
                }


            }, "The Manta Mod Debug Menu");
        }

#region "Singleton"

        public static DebugMenu main
        {
            get
            {
                if (_main == null)
                {
                    _main = Player.main.gameObject.AddComponent<DebugMenu>();
                }

                return _main;
            }
        }

        private static DebugMenu _main;
#endregion
    }
}
