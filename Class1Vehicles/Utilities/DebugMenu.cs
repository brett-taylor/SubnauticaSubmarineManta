using Manta.Core;
using Odyssey.Core;
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
        private static Rect SIZE = new Rect(5, 5, 500, 600);
        private bool isOpen = false;
        private bool showCursor = false;
        private int showDebugMenuNo = 0;

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
        private ReaperLeviathan reaper;
        private void DrawMiscDebugMenu()
        {
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

                    Log.Print("LiveMixin broadcastKillOnDeath: " + lm.broadcastKillOnDeath);
                    Log.Print("LiveMixin canResurrect: " + lm.canResurrect);
                    Log.Print("LiveMixin damageEffect: " + lm.damageEffect);
                    Log.Print("LiveMixin deathEffect: " + lm.deathEffect);
                    Log.Print("LiveMixin destroyOnDeath: " + lm.destroyOnDeath);
                    Log.Print("LiveMixin electricalDamageEffect: " + lm.electricalDamageEffect);
                    Log.Print("LiveMixin explodeOnDestroy: " + lm.explodeOnDestroy);
                    Log.Print("LiveMixin invincibleInCreative: " + lm.invincibleInCreative);
                    Log.Print("LiveMixin knifeable: " + lm.knifeable);
                    Log.Print("LiveMixin loopEffectBelowPercent: " + lm.loopEffectBelowPercent);
                    Log.Print("LiveMixin loopingDamageEffect: " + lm.loopingDamageEffect);
                    Log.Print("LiveMixin maxHealth: " + lm.maxHealth);
                    Log.Print("LiveMixin minDamageForSound: " + lm.minDamageForSound);
                    Log.Print("LiveMixin passDamageDataOnDeath: " + lm.passDamageDataOnDeath);
                    Log.Print("LiveMixin weldable: " + lm.weldable);
                    Log.Print("LiveMixin damageClip: " + lm.damageClip);
                    Log.Print("LiveMixin data: " + lm.data);
                    Log.Print("LiveMixin deathClip: " + lm.deathClip);
                    Log.Print("LiveMixin health: " + lm.health);
                    Log.Print("LiveMixin invincible: " + lm.invincible);
                    Log.Print("LiveMixin onHealDamage: " + lm.onHealDamage);
                    Log.Print("LiveMixin onHealTempDamage: " + lm.onHealTempDamage);
                    Log.Print("LiveMixin shielded: " + lm.shielded);
                    Log.Print("LiveMixin data broadcastKillOnDeath: " + lm.data.broadcastKillOnDeath);
                    Log.Print("LiveMixin data canResurrect: " + lm.data.canResurrect);
                    Log.Print("LiveMixin data damageEffect: " + lm.data.damageEffect);
                    Log.Print("LiveMixin data deathEffect: " + lm.data.deathEffect);
                    Log.Print("LiveMixin data destroyOnDeath: " + lm.data.destroyOnDeath);
                    Log.Print("LiveMixin data electricalDamageEffect: " + lm.data.electricalDamageEffect);
                    Log.Print("LiveMixin data explodeOnDestroy: " + lm.data.explodeOnDestroy);
                    Log.Print("LiveMixin data invincibleInCreative: " + lm.data.invincibleInCreative);
                    Log.Print("LiveMixin data knifeable: " + lm.data.knifeable);
                    Log.Print("LiveMixin data loopEffectBelowPercent: " + lm.data.loopEffectBelowPercent);
                    Log.Print("LiveMixin data loopingDamageEffect: " + lm.data.loopingDamageEffect);
                    Log.Print("LiveMixin data maxHealth: " + lm.data.maxHealth);
                    Log.Print("LiveMixin data minDamageForSound: " + lm.data.minDamageForSound);
                    Log.Print("LiveMixin data passDamageDataOnDeath: " + lm.data.passDamageDataOnDeath);
                    Log.Print("LiveMixin data weldable: " + lm.data.weldable);                  
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

            if (GUILayout.Button("Spawn Reaper"))
            {
                GameObject prefabForTechType = CraftData.GetPrefabForTechType(TechType.ReaperLeviathan, true);
                GameObject gameObject = Utils.CreatePrefab(prefabForTechType);
                LargeWorldEntity.Register(gameObject);
                CrafterLogic.NotifyCraftEnd(gameObject, TechType.ReaperLeviathan);
                gameObject.SendMessage("StartConstruction", SendMessageOptions.DontRequireReceiver);
                reaper = gameObject.GetComponentInChildren<ReaperLeviathan>();
                Log.Print("Repaer set: " + (reaper != null));
            }

            if (reaper != null)
            {
                if (GUILayout.Button("Reaper Components"))
                {
                    foreach(Component c in reaper.GetComponentsInChildren(typeof(Component)))
                    {
                        Log.Print("Component: " + c + " on go: " + c.gameObject.name);
                    }
                }

                if (GUILayout.Button("Delete Reaper"))
                {
                    Destroy(reaper.gameObject);
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

        private void OnGUI()
        {
            if (isOpen == false)
            {
                return;
            }

            Rect windowRect = GUILayout.Window(2352, SIZE, (id) =>
            {
                GUILayout.Box("P to show/hide cursor");
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("General Menu"))
                    showDebugMenuNo = 0;
                if (GUILayout.Button("Manta Menu"))
                    showDebugMenuNo = 1;
                if (GUILayout.Button("Odyssey Menu"))
                    showDebugMenuNo = 2;
                GUILayout.EndHorizontal();

                GUILayout.Space(10f);

                if (showDebugMenuNo == 0)
                    DrawMiscDebugMenu();
                else if (showDebugMenuNo == 1)
                    DrawMantaDebugMenu();
                else if (showDebugMenuNo == 2)
                    DrawOdysseyDebugMenu();
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
