using HarmonyLib;
using Last.UI.KeyInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FFPR_ShiftRight
{
    [HarmonyPatch(typeof(PagerController),nameof(PagerController.SetPage))]
    public sealed class PagerController_SetPage
    {
        public static void AdjustRectTransforms(RectTransform transform, float val)
        {
            transform.anchorMin = new Vector2(transform.anchorMin.x, val);
        }
        public static void Postfix(PagerController __instance)
        {
            if(ModComponent.Config.WindowMode == WindowModes.ShiftRight)
            {
                if (__instance.gameObject.name == "arrow_root")
                {
                    //this has strong potential for problems, but lets see where we can take it
                    //if nothing, scene checking could be done
                    if (__instance.gameObject.scene != null)
                    {
                        if (__instance.gameObject.scene.name == "BattleMenu")
                        {
                            GameObject csw = __instance.transform.parent.gameObject;
                            if (csw != null)
                            {
                                GameObject battleWindow = ModComponent.GetDirectChild(csw, "battle_window");
                                GameObject r3 = ModComponent.GetDirectChild(csw, "root");
                                if (battleWindow != null && r3 != null)
                                {
                                    int activeCommands = 0;
                                    for (int i = 0; i < r3.transform.childCount; i++)
                                    {
                                        Transform ct = r3.transform.GetChild(i);
                                        if (ct.gameObject.active)
                                        {
                                            activeCommands++;
                                        }
                                    }
                                    if (activeCommands > 4)
                                    {
                                        return;
                                    }
                                    for (int i = 0; i < battleWindow.transform.childCount; i++)
                                    {
                                        Transform ct = battleWindow.transform.GetChild(i);
                                        GameObject child = ct.gameObject;
                                        RectTransform rt = child.GetComponent<RectTransform>();
                                        if (rt != null)
                                        {
                                            AdjustRectTransforms(rt, ModComponent.WindowVals[activeCommands]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
            
    }
}
