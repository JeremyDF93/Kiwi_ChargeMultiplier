using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace Kiwi_ChargeMultiplier;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    public static Plugin instance;
    
    internal ConfigEntry<float> configChargeMultiplier;

    private void Awake()
    {
        Harmony.CreateAndPatchAll(typeof(Plugin));
        
        instance = this;
        
        configChargeMultiplier = Config.Bind("General", "ChargeMultiplier", 2.0f, "A charge multiplier given to items that make crystals more efficient. This applies to the energy drone as well.");
    }
    
    [HarmonyPatch(typeof(ItemBattery), "ChargeBattery"), HarmonyPrefix]
    static void ChargeBattery(ref float chargeAmount)
    {
        chargeAmount = chargeAmount * Plugin.instance.configChargeMultiplier.Value;
    }
}