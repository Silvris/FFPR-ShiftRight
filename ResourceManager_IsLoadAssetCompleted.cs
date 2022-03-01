using HarmonyLib;
using Last.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FFPR_ShiftRight
{
    //format for patching functions used by the game
    //https://harmony.pardeike.net/articles/intro.html
    //HarmonyPatch takes the type of the class (typeof(ResourceManager)) and then the name of the function "CheckCompleteAsset" or nameof(ResourceManager.CheckCompleteAsset)
    [HarmonyPatch(typeof(ResourceManager), nameof(ResourceManager.IsLoadAssetCompleted))]
    public sealed class ResourceManager_IsLoadAssetCompleted
    {

        //Postfix runs after the function, can be used to change results
        public static void Postfix(string addressName, ResourceManager __instance, bool __result)
        {
            //__result is the result of the function, __instance is the ResourceManager that is calling the non-static function
            //preceding these are the function arguments
            ModComponent.Log.LogInfo(addressName);
            if (addressName == "Assets/GameAssets/Serial/Res/UI/Key/Battle/Prefabs/battle_info_window")
            {
                if(__result == true)
                {
                    //asset exists in ResourceManager now
                    //as a note, if your target is a prefab, edit the dictionary version to copy it across all versions
                    //instead of searching for every instance
                    GameObject infoWindow = __instance.completeAssetDic[addressName].Cast<GameObject>();
                    GameObject r1 = ModComponent.GetDirectChild(infoWindow, "root");
                    if(r1 != null)
                    {
                        GameObject r2 = ModComponent.GetDirectChild(r1, "root");
                        if(r2 != null)
                        {
                            GameObject r3 = ModComponent.GetDirectChild(r2, "root");
                            if (r3 != null) 
                            {
                                r3.transform.localPosition = new Vector3(-160, r3.transform.localPosition.y, r3.transform.localPosition.z);
                                //get enemy info to move over
                                GameObject enemyInfo = ModComponent.GetDirectChild(r3, "enemy_info_window");
                                if(enemyInfo != null)
                                {
                                    RectTransform eiw = enemyInfo.GetComponent<RectTransform>();
                                    if(eiw != null)
                                    {
                                        eiw.offsetMax = new Vector2(462f, 0f);
                                        eiw.offsetMin = new Vector2(-338f, -228f);
                                    }
                                }
                            }
                            else ModComponent.Log.LogInfo("r3 was null");
                            GameObject commandWindow = ModComponent.GetDirectChild(r2, "command_select_window");
                            if(commandWindow != null)
                            {
                                RectTransform cwr = commandWindow.GetComponent<RectTransform>();
                                if(cwr != null)
                                {
                                    cwr.anchorMin = new Vector2(1f, 3.75f);
                                    cwr.anchorMax = new Vector2(1f, 3.75f);
                                }
                                ModComponent.Log.LogInfo("cwr was null");
                            }
                            else
                            {
                                ModComponent.Log.LogInfo("commandWindow was null");
                            }

                        }
                        else
                        {
                            ModComponent.Log.LogInfo("r2 was null");
                        }
                    }
                    else
                    {
                        ModComponent.Log.LogInfo("r1 was null");
                    }

                }
                else
                {
                    ModComponent.Log.LogInfo("result was false");
                    ModComponent.Log.LogInfo(__instance.completeAssetDic.ContainsKey(addressName));
                }
            }
        }
    }
}
