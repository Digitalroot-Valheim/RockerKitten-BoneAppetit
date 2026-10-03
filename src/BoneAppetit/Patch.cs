using HarmonyLib;
using JetBrains.Annotations;

namespace BoneAppetit
{
  [UsedImplicitly]
  public class Patch
  {
    #region CookingStation

    /// <summary>
    /// Patch the cooking station
    /// </summary>
    [HarmonyPatch(typeof(CookingStation))]
    public class PatchCookingStation
    {
      // /// <summary>
      // /// Patch the CookItem method
      // /// </summary>
      // /// <param name="__result">Was item placed on the cooking station successfully?</param>
      //
      // [HarmonyPostfix, HarmonyPatch(typeof(CookingStation), nameof(CookingStation.CookItem))]
      // private static void PostfixCookItem(ref bool __result)
      // {
      //   try
      //   {
      //     Log.Trace(Main.Instance, $"{Main.Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
      //     Main.Instance.OnCookingStationCookItem(ref __result);
      //   }
      //   catch (Exception e)
      //   {
      //     Log.Error(Main.Instance, e);
      //   }
      // }
    }

    #endregion

    // [HarmonyPatch(typeof(Inventory))]
    // public class PatchInventory
    // {
    //   [HarmonyPostfix, HarmonyPatch(typeof(Inventory), nameof(Inventory.AddItem), typeof(string), typeof(int), typeof(int), typeof(int), typeof(long), typeof(string), typeof(bool), typeof(bool))]
    //   private static void PostfixAddItem(string name, int stack, int quality, int variant, long crafterID, string crafterName, bool cheated, bool pickedUp)
    //   {
    //     try
    //     {
    //       Log.Trace(Main.Instance, $"{Main.Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
    //       if (Player.m_localPlayer == null)
    //       {
    //         Log.Debug(Main.Instance, "Player is null");
    //         return;
    //       }
    //       Main.Instance.OnInventoryAddItemPostFix(name, stack, quality, variant, crafterID, crafterName, cheated, pickedUp);
    //     }
    //     catch (Exception e)
    //     {
    //       Log.Error(Main.Instance, e);
    //     }
    //   }
    // }
  }
}
