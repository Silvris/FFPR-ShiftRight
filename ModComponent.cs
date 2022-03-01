using BepInEx.Logging;
using Last.UI.KeyInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FFPR_ShiftRight
{
    public sealed class ModComponent : MonoBehaviour
    {
        public static ModComponent Instance { get; private set; }
        public static ManualLogSource Log { get; private set; }
        public static Configuration Config { get; set; }
        public static float[] WindowVals = { 1f, 0.6f, 0.4f, 0.2f, 0f };
        private Boolean _isDisabled;
        public ModComponent(IntPtr ptr) : base(ptr)
        {
        }
        public static List<GameObject> GetAllChildren(GameObject obj)
        {
            List<GameObject> children = new List<GameObject>();

            if (obj != null)
            {
                for (int i = 0; i < obj.transform.childCount; i++)
                {
                    Transform child = obj.transform.GetChild(i);
                    if (child != null)
                    {
                        if (child.gameObject != null)
                        {
                            children.Add(child.gameObject);
                            if (child.childCount != 0)
                            {
                                children.AddRange(GetAllChildren(child.gameObject));
                            }
                        }
                    }


                }
            }
            else
            {
                Log.LogWarning("Root object is null!");
            }

            return children;
        }
        public static GameObject GetDirectChild(GameObject obj, string childName)
        {

            if (obj != null)
            {
                for (int i = 0; i < obj.transform.childCount; i++)
                {
                    Transform child = obj.transform.GetChild(i);
                    if (child != null)
                    {
                        if (child.gameObject != null)
                        {
                            Log.LogInfo(child.name);
                            if(child.name == childName)
                            {
                                return child.gameObject;
                            }
                        }
                    }


                }
            }
            else
            {
                Log.LogWarning("Root object is null!");
            }

            return null;
        }
        public void Awake()
        {
            Log = BepInEx.Logging.Logger.CreateLogSource("FFPR_ShiftRight");
            try
            {
                Instance = this;
                Config = new Configuration(EntryPoint.Instance.Config);
                Log.LogMessage($"[{nameof(ModComponent)}].{nameof(Awake)}: Processed successfully.");
            }
            catch (Exception ex)
            {
                _isDisabled = true;
                Log.LogError($"[{nameof(ModComponent)}].{nameof(Awake)}(): {ex}");
                throw;
            }

        }
        public void Update()
        {
            try
            {
                if (_isDisabled)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                _isDisabled = true;
                Log.LogError($"[{nameof(ModComponent)}].{nameof(Update)}(): {ex}");
                throw;
            }

        }
    }
}
