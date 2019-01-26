using Manta.Core;
using Odyssey.Core;
using Submarines;
using Submarines.Movement;
using Submarines.Utilities.Extensions;
using UnityEngine;

namespace Class1Vehicles.Utilities
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
        private bool submarineIsParent = false;
        private string fmodAsset = "cyclops_door_close";
        private string fmodCustomEmitterAsset = "AI_engine_up";

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

                if (GUILayout.Button("Delete Object"))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        Destroy(hit.rigidbody.gameObject);
                    }
                }

                GUILayout.BeginHorizontal();
                fmodAsset = GUILayout.TextField(fmodAsset);
                if (GUILayout.Button("Play Standard FMOD"))
                {
                    FMODAsset[] fmods = Resources.FindObjectsOfTypeAll<FMODAsset>();
                    foreach (FMODAsset fmod in fmods)
                    {
                        if (fmod.name.ToLower().Equals(fmodAsset))
                        {
                            Log.Print("Playing FMOD: " + fmod.name);
                            Utils.PlayFMODAsset(fmod, MainCamera.camera.transform, 20f);
                        }
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                fmodCustomEmitterAsset = GUILayout.TextField(fmodCustomEmitterAsset);
                if (GUILayout.Button("Play Custom Emitter"))
                {
                    FMODAsset[] fmods = Resources.FindObjectsOfTypeAll<FMODAsset>();
                    foreach (FMODAsset fmod in fmods)
                    {
                        if (fmod.name.ToLower().Equals(fmodCustomEmitterAsset))
                        {
                            FMODUWE.PlayOneShot(fmod, Player.main.transform.position, 5f);
                        }
                    }
                }
                GUILayout.EndHorizontal();

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

                if (GUILayout.Button("Cyclops render materials"))
                {
                    Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit raycastHit4;
                    if (Physics.Raycast(ray4, out raycastHit4))
                    {
                        if (raycastHit4.rigidbody.gameObject.name.ToLower().Contains("cyclops"))
                        {
                            //Transform gggg1 = raycastHit4.rigidbody.gameObject.transform.Find("CyclopsMeshStatic").transform.Find("undamaged").Find("cyclops_LOD0").Find("cyclops_submarine_exterior");
                            Transform gggg1 = raycastHit4.rigidbody.gameObject.transform.Find("CyclopsMeshStatic").transform.Find("undamaged").Find("cyclops_LOD0").Find("Cyclops_submarine_exterior_glass");
                            //Transform gggg1 = raycastHit4.rigidbody.gameObject.transform.Find("CyclopsMeshStatic").transform.Find("undamaged").Find("cyclops_LOD0").Find("cyclops_submarine_exterior_decals");
                            //Transform gggg1 = raycastHit4.rigidbody.gameObject.transform.Find("CyclopsMeshStatic").transform.Find("undamaged").Find("cyclops_LOD0");

                            /*foreach (MeshRenderer mr in gggg1.gameObject.GetComponentsInChildren<MeshRenderer>())
                            {
                                Utilities.Log.Print("Gameobject: " + mr.gameObject.name);
                                foreach (Material mat2 in mr.materials)
                                {
                                    Utilities.Log.Print("Material: " + mat2.name);
                                    Utilities.Log.Print("keywords: " + mat2.shaderKeywords.Length);
                                    foreach(var s in mat2.shaderKeywords)
                                    {
                                        Utilities.Log.Print("Keyword: " + s);
                                    }
                                    Utilities.Log.Print("-");
                                }
                                Utilities.Log.Print("--");
                            }*/

                            Utilities.Log.Print("Materials count: " + gggg1.GetComponent<MeshRenderer>().materials.Length);
                            Material mat = gggg1.GetComponent<MeshRenderer>().material;
                            mat.PrintAllMarmosetUBERShaderProperties();
                        }
                    }
                }

                if (GUILayout.Button("Manta render materials"))
                {
                    Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit raycastHit4;
                    if (Physics.Raycast(ray4, out raycastHit4))
                    {
                        if (raycastHit4.rigidbody.gameObject.name.ToLower().Contains("manta"))
                        {
                            Transform gggg1 = raycastHit4.rigidbody.gameObject.transform.Find("Model").transform.Find("Exterior");

                            Utilities.Log.Print("Materials count: " + gggg1.GetComponent<MeshRenderer>().materials.Length);
                            foreach(Material mat in gggg1.GetComponent<MeshRenderer>().materials)
                            {
                                if (mat.name.ToLower().Contains("glass"))
                                {
                                    mat.PrintAllMarmosetUBERShaderProperties();
                                }
                            }
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

                if (GUILayout.Button("Spawn Odyssey"))
                {
                    GameObject gameObject2 = OdysseyMod.CreateOdyssey();
                    gameObject2.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 6f;
                    gameObject2.transform.LookAt(Player.main.transform);
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

                GUILayout.Space(5f);
                GUILayout.Box("Speed Settings");

                GUILayout.BeginHorizontal();
                GUILayout.Label("ForwardAccelerationSpeed:");
                submarine.GetComponent<MovementData>().ForwardAccelerationSpeed = float.Parse(GUILayout.TextField("" + submarine.GetComponent<MovementData>().ForwardAccelerationSpeed));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("BackwardsAccelerationSpeed:");
                submarine.GetComponent<MovementData>().BackwardsAccelerationSpeed = float.Parse(GUILayout.TextField("" + submarine.GetComponent<MovementData>().BackwardsAccelerationSpeed));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("AscendDescendSpeed:");
                submarine.GetComponent<MovementData>().AscendDescendSpeed = float.Parse(GUILayout.TextField("" + submarine.GetComponent<MovementData>().AscendDescendSpeed));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("RotationSpeed:");
                submarine.GetComponent<MovementData>().RotationSpeed = float.Parse(GUILayout.TextField("" + submarine.GetComponent<MovementData>().RotationSpeed));
                GUILayout.EndHorizontal();

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
