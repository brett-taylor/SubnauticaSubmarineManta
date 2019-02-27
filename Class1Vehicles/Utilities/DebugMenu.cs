using Manta.Core;
using MiniMantaDrone.Core;
using MiniMantaVehicle.Core;
using Odyssey.Core;
using Submarines.Content.Damage;
using Submarines.Content.Lighting;
using Submarines.Movement;
using Submarines.Utilities.Extensions;
using System.Reflection;
using UnityEngine;

namespace Class1Vehicles.Utilities
{
    /**
     * Debug menu. Messy code be living here.
     */
    public class DebugMenu : MonoBehaviour
    {
        private static Rect SIZE = new Rect(5, 5, 500, 600);
        private bool isOpen = false;
        private bool showCursor = false;
        private int showDebugMenuNo = 4;

        public void Update()
        {
            if (isOpen == false)
            {
                return;
            }

            if (Input.GetKey(KeyCode.CapsLock) && Input.GetKeyDown(KeyCode.Q))
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

        private string fmodAsset = "cyclops_door_open";
        private string prefabAsset = "cyclopsfire";
        private void DrawGeneralDebugMenu()
        {
            if (GUILayout.Button("Unlimited Oxygen"))
            {
                GameModeUtils.ToggleCheat(GameModeOption.NoOxygen);
                Player.main.oxygenMgr.AddOxygen(100);
            }

            if (GUILayout.Button("Full Food"))
            {
                Player.main.GetComponent<Survival>().food = 100;
            }

            if (GUILayout.Button("Full Water"))
            {
                Player.main.GetComponent<Survival>().water = 100;
            }

            if (GUILayout.Button("Full Health"))
            {
                Player.main.liveMixin.health = Player.main.liveMixin.maxHealth;
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Components"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    foreach (Component c in hit.rigidbody.gameObject.GetComponents(typeof(Component)))
                        Log.Print("Base Component: " + c.ToString());
                    foreach (Component c in hit.rigidbody.gameObject.GetComponentsInChildren(typeof(Component)))
                        Log.Print("Child " + c.gameObject + "  Component: " + c.ToString());
                }
            }

            if (GUILayout.Button("Delete Object"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                    Destroy(hit.rigidbody.gameObject);
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

            if (GUILayout.Button("Get Render Queue"))
            {
                Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray4, out RaycastHit raycastHit4))
                {
                    foreach (MeshRenderer mr in raycastHit4.rigidbody.GetComponentsInChildren<MeshRenderer>())
                    {
                        Log.Print("GO Name: " + mr.gameObject.name);
                        Log.Print("GO Material Count: " + mr.materials.Length);
                        foreach (Material m in mr.materials)
                        {
                            Log.Print("Material Name: " + m.name);
                            Log.Print("Material Render Queue: " + m.renderQueue);
                        }
                    }
                }
            }

            if (GUILayout.Button("Teleport"))
            {
                Player.main.transform.position += new Vector3(2f, 0f, 0f);
            }

            if (GUILayout.Button("Get LiveMixin Data"))
            {
                Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray4, out RaycastHit raycastHit4))
                {
                    LiveMixin lm = raycastHit4.rigidbody.GetComponent<LiveMixin>();
                    if (lm == null)
                    {
                        Log.Print("No Live Mixin Found");
                        return;
                    }

                    lm.PrintAllLiveMixinDetails();
                }
            }

            if (GUILayout.Button("Set Health to 300 and knifable"))
            {
                Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray4, out RaycastHit raycastHit4))
                {
                    LiveMixin lm = raycastHit4.rigidbody.GetComponent<LiveMixin>();
                    if (lm == null)
                    {
                        Log.Print("No Live Mixin Found");
                        return;
                    }
                    lm.health = 300;
                    lm.data.knifeable = true;
                }
            }

            GUILayout.BeginHorizontal();
            prefabAsset = GUILayout.TextField(prefabAsset);
            if (GUILayout.Button("Spawn prefab"))
            {
                GameObject[] prefabs = Resources.FindObjectsOfTypeAll<GameObject>();
                foreach (GameObject prefab in prefabs)
                {
                    if (prefab.name.ToLower().Equals(prefabAsset))
                    {
                        GameObject newPrefab = Instantiate(prefab);
                        newPrefab.transform.position = Camera.main.transform.position;
                        newPrefab.transform.position += Camera.main.transform.forward * 3f;
                        foreach(Component c in prefab.GetComponentsInChildren(typeof(Component)))
                        {
                            Log.Print("Component:" + c);
                        }
                    }
                }
            }
            GUILayout.EndHorizontal();
        }

        private void DrawCyclopsDebugMenu()
        {
            if (GUILayout.Button("Destroy Cyclops static mesh"))
            {
                Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray4, out RaycastHit raycastHit4))
                {
                    if (raycastHit4.rigidbody.gameObject.name.ToLower().Contains("cyclops"))
                    {
                        Transform transform = raycastHit4.rigidbody.gameObject.transform.Find("CyclopsMeshStatic");
                        Log.Print("Size 1: " + transform.GetComponentInChildren<MeshFilter>().mesh.triangles.Length);
                        Log.Print("Size 2: " + transform.GetComponentInChildren<MeshFilter>().mesh.vertices.Length);
                        Destroy(transform.gameObject);
                    }
                }
            }

            if (GUILayout.Button("Cyclops Engine RPM SFX Manager"))
            {
                Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray4, out RaycastHit raycastHit4))
                {
                    if (raycastHit4.rigidbody.gameObject.name.ToLower().Contains("cyclops"))
                    {
                        EngineRpmSFXManager engine = raycastHit4.rigidbody.GetComponentInChildren<EngineRpmSFXManager>();
                        if (engine == null)
                        {
                            Utilities.Log.Print("EngineRpmSFXManager not found");
                            return;
                        }

                        Log.Print("engineRpmSFX: " + engine.engineRpmSFX?.asset?.path);
                        Log.Print("stopSoundInterval: " + engine.engineRpmSFX?.stopSoundInterval);
                        Log.Print("engineRevUp: " + engine.engineRevUp?.asset?.path);
                        Log.Print("rampUpSpeed: " + engine.rampUpSpeed);
                        Log.Print("rampDownSpeed: " + engine.rampDownSpeed);
                    }
                }
            }

            if (GUILayout.Button("Cyclops render materials"))
            {
                Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray4, out RaycastHit raycastHit4))
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

                        Log.Print("Materials count: " + gggg1.GetComponent<MeshRenderer>().materials.Length);
                        Material mat = gggg1.GetComponent<MeshRenderer>().material;
                        mat.PrintAllMarmosetUBERShaderProperties();
                    }
                }
            }

            if (GUILayout.Button("Cyclops Oxygen Manager"))
            {
                Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray4, out RaycastHit raycastHit4))
                {
                    if (raycastHit4.rigidbody.gameObject.name.ToLower().Contains("cyclops"))
                    {
                        OxygenManager[] oxygenManager = raycastHit4.rigidbody.GetComponentsInChildren<OxygenManager>();
                        Log.Print("Oxygen Manager Found Count " + oxygenManager.Length);
                    }
                }
            }

            if (GUILayout.Button("Cyclops Depth Cleaer"))
            {
                Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray4, out RaycastHit raycastHit4))
                {
                    if (raycastHit4.rigidbody.gameObject.name.ToLower().Contains("cyclops"))
                    {
                        SubRoot subRoot = raycastHit4.rigidbody.GetComponent<SubRoot>();
                        MeshRenderer mr = subRoot.depthClearer as MeshRenderer;
                        Log.Print("DepthClearer type: " + subRoot.depthClearer.GetType());
                        Log.Print("DepthClearer name: " + subRoot.depthClearer.name);
                        Log.Print("DepthClearer material: " + subRoot.depthClearer.material);
                        Log.Print("DepthClearer mesh: " + subRoot.depthClearer.GetComponent<MeshFilter>().mesh);
                        Log.Print("DepthClearer mesh name: " + subRoot.depthClearer.GetComponent<MeshFilter>().mesh.name);
                        Log.Print("DepthClearer Children count: " + subRoot.depthClearer.transform.childCount);
                        Log.Print("DepthClearer transform pos: " + subRoot.depthClearer.transform.localPosition);
                        Log.Print("DepthClearer transform size: " + subRoot.depthClearer.transform.localScale);
                        Log.Print("DepthClearer transform Parent: " + subRoot.depthClearer.transform.parent?.name);
                        subRoot.depthClearer.material.color = new Color(1f, 0f, 0f, 1f);
                        Log.Print("DepthClearer mat color: " + subRoot.depthClearer.material.color);
                        Log.Print("DepthClearer enabled?: " + subRoot.depthClearer.enabled);

                        Log.Print("DepthClearer Component cound: " + subRoot.depthClearer.GetComponents(typeof(Component)).Length);
                        foreach (Component c in subRoot.depthClearer.GetComponents(typeof(Component)))
                        {
                            Utilities.Log.Print("Component: " + c);
                        }
                    }
                }
            }

            if (GUILayout.Button("Cyclops Ladder"))
            {
                Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray4, out RaycastHit raycastHit4))
                {
                    if (raycastHit4.rigidbody.gameObject.name.ToLower().Contains("cyclops"))
                    {
                        Utilities.Log.Print(raycastHit4.rigidbody.gameObject.name);
                        foreach(SkinnedMeshRenderer smr in raycastHit4.rigidbody.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>())
                        {
                            if (smr.gameObject.name.ToLower().Equals("submarine_ladder_04") || smr.gameObject.name.ToLower().Equals("cyclops_ladder_long")
                                || smr.gameObject.name.ToLower().Equals("cyclops_ladder_short_right") || smr.gameObject.name.ToLower().Equals("cyclops_ladder_short_left")
                                || smr.gameObject.name.ToLower().Equals("submarine_ladder_02"))
                            {
                                Utilities.Log.Print("Mesh: " + smr.gameObject.GetComponent<SkinnedMeshRenderer>()?.sharedMesh?.name);
                                Utilities.Log.Print("Bones Length: " + smr.gameObject.GetComponent<SkinnedMeshRenderer>()?.bones?.Length);
                                smr.gameObject.GetComponent<SkinnedMeshRenderer>()?.material?.PrintAllMarmosetUBERShaderProperties();
                            }
                        }
                    }
                }
            }

            if (GUILayout.Button("Cyclops Force Damage Point"))
            {
                Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray4, out RaycastHit raycastHit4))
                {
                    if (raycastHit4.rigidbody.gameObject.name.ToLower().Contains("cyclops"))
                    {
                        CyclopsExternalDamageManager damage = raycastHit4.rigidbody.GetComponentInChildren<CyclopsExternalDamageManager>();
                        if (damage == null)
                        {
                            Log.Print("CyclopsExternalDamageManager not found");
                            return;
                        }

                        MethodInfo mi = SMLHelper.V2.Utility.ReflectionHelper.GetInstanceMethod(damage, "CreatePoint");
                        if (mi == null)
                        {
                            Log.Print("CreatePoint method not found");
                            return;
                        }

                        mi.FastInvoke(damage);
                    }
                }
            }

            if (GUILayout.Button("Cyclops Print Damage Info"))
            {
                Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray4, out RaycastHit raycastHit4))
                {
                    if (raycastHit4.rigidbody.gameObject.name.ToLower().Contains("cyclops"))
                    {
                        CyclopsExternalDamageManager damage = raycastHit4.rigidbody.GetComponentInChildren<CyclopsExternalDamageManager>();
                        if (damage == null)
                        {
                            Log.Print("CyclopsExternalDamageManager not found");
                            return;
                        }

                        CyclopsDamagePoint[] damagePoints = raycastHit4.rigidbody.gameObject.GetComponentsInChildren<CyclopsDamagePoint>();
                        foreach(var dp in damagePoints)
                        {
                            dp.gameObject.GetComponentInChildren<LiveMixin>().PrintAllLiveMixinDetails();
                            Log.Print(" ");
                        }
                    }
                }
            }

            if (GUILayout.Button("CyclopsExternalDamage information."))
            {
                Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray4, out RaycastHit raycastHit4))
                {
                    if (raycastHit4.rigidbody.gameObject.name.ToLower().Contains("cyclops"))
                    {
                        CyclopsExternalDamageManager dm = raycastHit4.rigidbody.GetComponentInChildren<CyclopsExternalDamageManager>();
                        if (dm == null)
                        {
                            Log.Print("Cyclop's CyclopsExternalDamage not found.");
                            return;
                        }


                        Utilities.Log.Print("Subleaks count: " + dm.subLeaks.Length);
                        foreach(GameObject go in dm.subLeaks) 
                        {
                            if (go.name.ToLower().Equals("helm"))
                            {
                                Utilities.Log.Print("Helm child count: " + go.transform.childCount);
                                foreach (Transform child in go.transform)
                                {

                                    Utilities.Log.Print("Helm child: " + child.name);
                                }
                            }
                        }
                    }
                }
            }
        }

        private MantaSubmarine manta;
        private MovementController mantaMovementController;
        private void DrawMantaDebugMenu()
        {
            if (GUILayout.Button("Spawn manta"))
            {
                GameObject gameObject2 = MantaMod.CreateManta();
                manta = gameObject2.GetComponent<MantaSubmarine>();
                mantaMovementController = gameObject2.GetComponent<MovementController>();
                gameObject2.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 22f;
                gameObject2.transform.LookAt(Player.main.transform);
            }

            if (GUILayout.Button("Connect to manta on cursor"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.rigidbody.gameObject.name.ToLower().Contains("manta"))
                    {
                        manta = hit.rigidbody.gameObject.GetComponent<MantaSubmarine>();
                        mantaMovementController = hit.rigidbody.gameObject.GetComponent<MovementController>();
                    }
                }
            }

            if (GUILayout.Button("Manta Count"))
            {
                MantaSubmarine[] submarines = FindObjectsOfType<MantaSubmarine>();
                Log.Print("Found " + submarines.Length + " Mantas");
            }

            if (GUILayout.Button("Delete All Mantas"))
            {
                MantaSubmarine[] submarines = FindObjectsOfType<MantaSubmarine>();
                Log.Print("Found " + submarines.Length + " Mantas to delete");
                foreach (MantaSubmarine s in submarines)
                {
                    Destroy(s.gameObject);
                }
            }

            if (GUILayout.Button("Manta render materials on raycast"))
            {
                Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray4, out RaycastHit raycastHit4))
                {
                    if (raycastHit4.rigidbody.gameObject.name.ToLower().Contains("manta"))
                    {
                        Transform gggg1 = raycastHit4.rigidbody.gameObject.transform.Find("Model").transform.Find("Exterior");
                        Log.Print("Materials count: " + gggg1.GetComponent<MeshRenderer>().materials.Length);
                        foreach (Material mat in gggg1.GetComponent<MeshRenderer>().materials)
                            if (mat.name.ToLower().Contains("glass"))
                                mat.PrintAllMarmosetUBERShaderProperties();
                    }
                }
            }

            if (manta == null)
            {
                GUILayout.Box("No Manta Connected");
                return;
            }

            GUILayout.Box("Current Health: " + manta.GetComponent<LiveMixin>()?.health);
            GUILayout.Box("Max Health: " + manta.GetComponent<LiveMixin>()?.maxHealth);
            GUILayout.Box("Health Per Damage Point: " + manta.GetComponent<ExternalDamageManager>().GetHealthPerDamagePoint());
            GUILayout.Box("Unused Damage Point: " + manta.GetComponent<ExternalDamageManager>().GetUnusedDamagePointsCount());
            GUILayout.Box("Used Damage Point: " + manta.GetComponent<ExternalDamageManager>().GetUsedDamagePointsCount());
            GUILayout.Box("Amount of Damage Points that *should* be showing: " + manta.GetComponent<ExternalDamageManager>().GetNumberOfDamagePointsThatShouldBeShowing());

            if (GUILayout.Button("Toggle Emergency Lighting"))
            {
                if (manta.GetComponent<EmergencyLighting>().IsRunning)
                {
                    manta.GetComponent<EmergencyLighting>().DisableEmergencyLighting();
                }
                else
                {
                    manta.GetComponent<EmergencyLighting>().EnableEmergencyLighting();
                }
            }
        }

        private OdysseySubmarine odyssey;
        private void DrawOdysseyDebugMenu()
        {
            if (GUILayout.Button("Spawn Odyssey"))
            {
                GameObject gameObject = OdysseyMod.CreateOdyssey();
                odyssey = gameObject.GetComponent<OdysseySubmarine>();
                gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 6f;
                gameObject.transform.LookAt(Player.main.transform);
            }

            if (GUILayout.Button("Connect to odyssey on cursor"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.rigidbody.gameObject.name.ToLower().Contains("odyssey"))
                    {
                        odyssey = hit.rigidbody.gameObject.GetComponent<OdysseySubmarine>();
                    }
                }
            }

            if (GUILayout.Button("Odyssey Count"))
            {
                OdysseySubmarine[] submarines = FindObjectsOfType<OdysseySubmarine>();
                Log.Print("Found " + submarines.Length + " Odysseys");
            }

            if (GUILayout.Button("Delete All Odysseys"))
            {
                OdysseySubmarine[] submarines = FindObjectsOfType<OdysseySubmarine>();
                Log.Print("Found " + submarines.Length + " Odysseys to delete");
                foreach (OdysseySubmarine s in submarines)
                {
                    Destroy(s.gameObject);
                }
            }

            if (odyssey == null)
            {
                GUILayout.Box("No Odyssey Connected");
                return;
            }
        }

        private ReaperLeviathan reaper;
        private CrabSquid crabsquid;
        private GhostLeviathan ghostlevi;
        private SeaDragon seaDragon;
        private Warper warper;
        private void DrawHostilesMenu()
        {
            if (reaper == null)
            {
                if (GUILayout.Button("Spawn Reaper"))
                {
                    GameObject prefabForTechType = CraftData.GetPrefabForTechType(TechType.ReaperLeviathan, true);
                    GameObject gameObject = Utils.CreatePrefab(prefabForTechType);
                    LargeWorldEntity.Register(gameObject);
                    CrafterLogic.NotifyCraftEnd(gameObject, TechType.ReaperLeviathan);
                    gameObject.SendMessage("StartConstruction", SendMessageOptions.DontRequireReceiver);
                    reaper = gameObject.GetComponentInChildren<ReaperLeviathan>();
                    gameObject.transform.position += 50f * gameObject.transform.forward;
                }
            }
            else
            {
                GUILayout.Space(10);

                if (GUILayout.Button("Reaper Components"))
                {
                    foreach (Component c in reaper.GetComponentsInChildren(typeof(Component)))
                    {
                        Log.Print("Component: " + c + " on go: " + c.gameObject.name);
                    }
                }

                if (GUILayout.Button("Delete Reaper"))
                {
                    Destroy(reaper.gameObject);
                }

                GUILayout.Space(10);
            }

            if (crabsquid == null)
            {
                if (GUILayout.Button("Spawn Crabsquid"))
                {
                    GameObject prefabForTechType = CraftData.GetPrefabForTechType(TechType.CrabSquid, true);
                    GameObject gameObject = Utils.CreatePrefab(prefabForTechType);
                    LargeWorldEntity.Register(gameObject);
                    CrafterLogic.NotifyCraftEnd(gameObject, TechType.CrabSquid);
                    gameObject.SendMessage("StartConstruction", SendMessageOptions.DontRequireReceiver);
                    crabsquid = gameObject.GetComponentInChildren<CrabSquid>();
                    gameObject.transform.position += 50f * gameObject.transform.forward;
                }
            }
            else
            {
                GUILayout.Space(10);

                if (GUILayout.Button("Crabsquid Components"))
                {
                    foreach (Component c in crabsquid.GetComponentsInChildren(typeof(Component)))
                    {
                        Log.Print("Component: " + c + " on go: " + c.gameObject.name);
                    }
                }

                if (GUILayout.Button("Delete Crabsquid"))
                {
                    Destroy(crabsquid.gameObject);
                }

                GUILayout.Space(10);
            }

            if (ghostlevi == null)
            {
                if (GUILayout.Button("Spawn Ghost Levi"))
                {
                    GameObject prefabForTechType = CraftData.GetPrefabForTechType(TechType.GhostLeviathan, true);
                    GameObject gameObject = Utils.CreatePrefab(prefabForTechType);
                    LargeWorldEntity.Register(gameObject);
                    CrafterLogic.NotifyCraftEnd(gameObject, TechType.GhostLeviathan);
                    gameObject.SendMessage("StartConstruction", SendMessageOptions.DontRequireReceiver);
                    ghostlevi = gameObject.GetComponentInChildren<GhostLeviathan>();
                    gameObject.transform.position += 50f * gameObject.transform.forward;
                }
            }
            else
            {
                GUILayout.Space(10);

                if (GUILayout.Button("Ghost Levi Components"))
                {
                    foreach (Component c in ghostlevi.GetComponentsInChildren(typeof(Component)))
                    {
                        Log.Print("Component: " + c + " on go: " + c.gameObject.name);
                    }
                }

                if (GUILayout.Button("Delete Ghost Levi"))
                {
                    Destroy(ghostlevi.gameObject);
                }

                GUILayout.Space(10);
            }

            if (seaDragon == null)
            {
                if (GUILayout.Button("Spawn Seadragon"))
                {
                    GameObject prefabForTechType = CraftData.GetPrefabForTechType(TechType.SeaDragon, true);
                    GameObject gameObject = Utils.CreatePrefab(prefabForTechType);
                    LargeWorldEntity.Register(gameObject);
                    CrafterLogic.NotifyCraftEnd(gameObject, TechType.SeaDragon);
                    gameObject.SendMessage("StartConstruction", SendMessageOptions.DontRequireReceiver);
                    seaDragon = gameObject.GetComponentInChildren<SeaDragon>();
                    gameObject.transform.position += 50f * gameObject.transform.forward;
                }
            }
            else
            {
                GUILayout.Space(10);

                if (GUILayout.Button("Seadragon Components"))
                {
                    foreach (Component c in seaDragon.GetComponentsInChildren(typeof(Component)))
                    {
                        Log.Print("Component: " + c + " on go: " + c.gameObject.name);
                    }
                }

                if (GUILayout.Button("Delete Seadragon"))
                {
                    Destroy(seaDragon.gameObject);
                }

                GUILayout.Space(10);
            }

            if (warper == null)
            {
                if (GUILayout.Button("Spawn warper"))
                {
                    GameObject prefabForTechType = CraftData.GetPrefabForTechType(TechType.Warper, true);
                    GameObject gameObject = Utils.CreatePrefab(prefabForTechType);
                    LargeWorldEntity.Register(gameObject);
                    CrafterLogic.NotifyCraftEnd(gameObject, TechType.Warper);
                    gameObject.SendMessage("StartConstruction", SendMessageOptions.DontRequireReceiver);
                    warper = gameObject.GetComponentInChildren<Warper>();
                    gameObject.transform.position += 50f * gameObject.transform.forward;
                }
            }
            else
            {
                GUILayout.Space(10);

                if (GUILayout.Button("Warper Components"))
                {
                    foreach (Component c in warper.GetComponentsInChildren(typeof(Component)))
                    {
                        Log.Print("Component: " + c + " on go: " + c.gameObject.name);
                    }
                }

                if (GUILayout.Button("Delete Warper"))
                {
                    Destroy(warper.gameObject);
                }

                GUILayout.Space(10);
            }
        }

        private void DrawMiniMantaVehicleMenu()
        {
            if (GUILayout.Button("Spawn Mini Manta Vehicle"))
            {
                GameObject gameObject = MiniMantaVehicleMod.CreateMiniMantaVehicle();
                gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 4f;
                gameObject.transform.LookAt(Player.main.transform);
            }
        }

        private void DrawMiniMantaDroneMenu()
        {
            if (GUILayout.Button("Spawn Mini Manta Drone"))
            {
                GameObject gameObject = MiniMantaDroneMod.CreateMiniMantaDrone();
                gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 3f;
                gameObject.transform.LookAt(Player.main.transform);
            }
        }

        private void DrawPlayerAnimationsMenu()
        {
            if (GUILayout.Button("Toggle Third Person"))
            {
                PlayerAnimations.ThirdPersonController.Toggle();
            }
        }

        private void OnGUI()
        {
            if (isOpen == false)
            {
                return;
            }

            Rect windowRect = GUILayout.Window(2352, SIZE, (id) =>
            {
                GUILayout.Box("'Capslock and Q' to show/hide cursor");
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("General Menu"))
                    showDebugMenuNo = 4;
                if (GUILayout.Button("Cyclops Menu"))
                    showDebugMenuNo = 0;
                if (GUILayout.Button("Manta Menu"))
                    showDebugMenuNo = 1;
                if (GUILayout.Button("Odyssey Menu"))
                    showDebugMenuNo = 2;
                if (GUILayout.Button("Hostiles Menu"))
                    showDebugMenuNo = 3;
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Mini Manta Vehicle"))
                    showDebugMenuNo = 5;
                if (GUILayout.Button("Mini Manta Drone"))
                    showDebugMenuNo = 6;
                if (GUILayout.Button("Player Animations"))
                    showDebugMenuNo = 7;
                GUILayout.EndHorizontal();

                GUILayout.Space(10f);

                if (showDebugMenuNo == 0)
                    DrawCyclopsDebugMenu();
                else if (showDebugMenuNo == 1)
                    DrawMantaDebugMenu();
                else if (showDebugMenuNo == 2)
                    DrawOdysseyDebugMenu();
                else if (showDebugMenuNo == 3)
                    DrawHostilesMenu();
                else if (showDebugMenuNo == 4)
                    DrawGeneralDebugMenu();
                else if (showDebugMenuNo == 5)
                    DrawMiniMantaVehicleMenu();
                else if (showDebugMenuNo == 6)
                    DrawMiniMantaDroneMenu();
                else if (showDebugMenuNo == 7)
                    DrawPlayerAnimationsMenu();
                else
                    GUILayout.Label("No Menu Selected");

            }, "Vehicles Framework Debug Menu");
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
