using BepInEx;
using BepInEx.Configuration;
using Digitalroot.Modding.Framework.Logging;
using Digitalroot.Modding.Framework.Names.Vanilla;
using HarmonyLib;
using JetBrains.Annotations;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BoneAppetit
{
  [BepInPlugin(Guid, Name, Version)]
  [BepInDependency(Jotunn.Main.ModGuid)]
  [NetworkCompatibility(CompatibilityLevel.VersionCheckOnly, VersionStrictness.Minor)]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  public partial class Main : BaseUnityPlugin, ITraceableLogging
  {
    private Harmony _harmony;

    public static Main Instance;

    // [Obsolete("Removed in favor of built-in cooking skill", true)]
    // public Skills.SkillType rkCookingSkill;
    public Sprite CookingSprite;

    #region AssetBundles

    public AssetBundle GrillAssetBundle;
    public AssetBundle FoodAssetBundle;

    #endregion

    #region EffectList

    public EffectList buildStone;
    public EffectList cookingSound;
    public EffectList breakStone;
    public EffectList hitStone;
    public EffectList buildKitten;
    public EffectList hearthAddFuel;
    public EffectList fireAddFuel;

    #endregion

    #region Configs

    [UsedImplicitly]
    public static ConfigEntry<int> NexusId { get; private set; }

    private ConfigEntry<bool> _configPorkRindEnable;
    private ConfigEntry<bool> _configKabobEnable;
    private ConfigEntry<bool> _configFriedLoxEnable;
    private ConfigEntry<bool> _configGlazedCarrotEnable;
    private ConfigEntry<bool> _configBaconEnable;
    private ConfigEntry<bool> _configSmokedFishEnable;
    private ConfigEntry<bool> _configPancakesEnable;
    private ConfigEntry<bool> _configPizzaEnable;
    private ConfigEntry<bool> _configCoffeeEnable;

    private ConfigEntry<bool> _configLatteEnable;

    // [Obsolete] private ConfigEntry<bool> SkillEnable;
    private ConfigEntry<bool> _configSmokelessEnable;
    private ConfigEntry<bool> _configHaggisEnable;
    private ConfigEntry<bool> _configCandiedTurnipEnable;
    private ConfigEntry<bool> _configMoochiEnable;
    private ConfigEntry<bool> _configConesEnable;
    private ConfigEntry<bool> _configNut_EllaEnable;
    private ConfigEntry<bool> _configBrothEnable;
    private ConfigEntry<bool> _configFishStewEnable;
    private ConfigEntry<bool> _configButterEnable;
    private ConfigEntry<bool> _configBloodSausageEnable;
    private ConfigEntry<bool> _configOmletteEnable;
    private ConfigEntry<bool> _configBurgerEnable;
    private ConfigEntry<bool> _configPorridgeEnable;
    private ConfigEntry<bool> _configPBJEnable;
    private ConfigEntry<bool> _configBoiledEggEnable;
    private ConfigEntry<bool> _configCakeEnable;
    private ConfigEntry<bool> _configCarrotSticksEnable;

    private ConfigEntry<bool> _configMeadEnable;

    // [Obsolete] private ConfigEntry<bool> _configCookingSkillEnable;
    // [Obsolete] private ConfigEntry<bool> _configBonusWhenCookingEnabled;
    private ConfigEntry<bool> _configChefHatEnable;
    // [Obsolete] public ConfigEntry<bool> ConfigHatSEMessage;
    // [Obsolete] public ConfigEntry<float> ConfigHatXpGain;
    //private static ConfigEntry<bool> ScrapsRecipe;

    #endregion

    #region Prefabs

    public Dictionary<string, GameObject> Prefabs = new();
    public GameObject icecream_prefab;
    public CustomItem icecream;
    public GameObject porkrind_prefab;
    public CustomItem porkrind;
    public GameObject kabob_prefab;
    public CustomItem kabob;
    public GameObject friedlox_prefab;
    public CustomItem friedlox;
    public GameObject glazedcarrot_prefab;
    public CustomItem glazedcarrot;
    public GameObject bacon_prefab;
    public CustomItem bacon;
    public GameObject smokedfish_prefab;
    public CustomItem smokedfish;
    public GameObject pancake_prefab;
    public CustomItem pancake;
    public GameObject pizza_prefab;
    public CustomItem pizza;
    public GameObject coffee_prefab;
    public CustomItem coffee;
    public GameObject latte_prefab;
    public CustomItem latte;
    public GameObject firecream_prefab;
    public CustomItem firecream;
    public CustomItem electriccream;
    public GameObject electriccream_prefab;
    public CustomItem acidcream;
    public GameObject acidcream_prefab;
    public GameObject porridge_prefab;
    public CustomItem porridge;
    public GameObject pbj_prefab;
    public CustomItem pbj;
    public GameObject cake_prefab;
    public CustomItem cake;
    public AudioSource fireVol;
    public GameObject haggisFab;
    public CustomItem haggis;
    public GameObject candiedTurnipFab;
    public CustomItem candiedTurnip;
    public GameObject moochiFab;
    public CustomItem moochi;
    public GameObject omletteFab;
    public CustomItem omlette;
    public GameObject fishStewFab;
    public CustomItem fishStew;
    public GameObject brothFab;
    public CustomItem broth;
    public GameObject butterFab;
    public CustomItem butter;
    public GameObject bloodsausageFab;
    public CustomItem bloodsausage;
    public GameObject burgerFab;
    public CustomItem burger;
    public GameObject nut_ellaFab;
    public CustomItem nut_ella;
    public GameObject boiledeggFab;
    public CustomItem boiledegg;
    public GameObject carrotstickFab;
    public CustomItem carrotstick;
    public GameObject meadFab;
    public CustomItem mead;
    public GameObject hatFab;
    public CustomItem hat;
    public GameObject fireFab1;
    public CustomPiece fire1;
    public GameObject fireFab2;
    public CustomPiece fire2;
    public GameObject fireFab3;
    private CustomPiece fire3;
    public GameObject eggFab;
    public GameObject deggFab;
    public GameObject porkFab;

    #endregion

    /// <summary>
    /// ctor
    /// </summary>
    public Main()
    {
      Instance = this;
      #if DEBUG
      EnableTrace = true;
      Log.RegisterSource(Instance); // Enable logging to a './BepInEx/logs' file.
      #else
      EnableTrace = false;
      #endif
      Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
    }

    /// <summary>
    /// Awake Handler
    /// </summary>
    [UsedImplicitly]
    private void Awake()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        // InitConfigs();
        LoadAssets();
        LoadNewItemDropPrefabs();
        // AddSkills();
        PrefabManager.OnVanillaPrefabsAvailable += PrefabManager_OnVanillaPrefabsAvailable;
        ItemManager.OnItemsRegistered += ItemManager_OnItemsRegistered;
        LocalizationManager.OnLocalizationAdded += LocalizationManager_OnLocalizationAdded;
        SynchronizationManager.OnConfigurationSynchronized += SynchronizationManager_OnConfigurationSynchronized;

        _harmony = Harmony.CreateAndPatchAll(typeof(Main).Assembly, Guid);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    /// <summary>
    /// dtor
    /// </summary>
    [UsedImplicitly]
    private void OnDestroy()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        _harmony?.UnpatchSelf();
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    #region Event Handlers

    /// <summary>
    /// OnItemsRegistered Handler
    /// </summary>
    private void ItemManager_OnItemsRegistered()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        AddNewItemDropsToDropTables();
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
      finally
      {
        ItemManager.OnItemsRegistered -= AddNewItemDropsToDropTables;
      }
    }

    /// <summary>
    /// Patch for CookingStation.
    /// </summary>
    /// <param name="__result">Was item placed on the cooking station successfully?</param>
    [Obsolete("Removed in favor of built-in cooking skill", true)]
    public void OnCookingStationCookItem(ref bool __result)
    {
      // try
      // {
      //   Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
      //   Log.Debug(Instance, $"__result : {__result}");
      //   if (!__result) return;
      //   if (!_configCookingSkillEnable.Value) return;
      //   RaiseCookingSkill();
      // }
      // catch (Exception e)
      // {
      //   Log.Error(Instance, e);
      // }
    }

    /// <summary>
    /// Patch for AddItem method.
    /// </summary>
    /// <param name="itemName">Name of the item</param>
    /// <param name="stack">Stack size</param>
    /// <param name="quality">Quality level</param>
    /// <param name="variant">Variant to use</param>
    /// <param name="crafterID">Id of the player who crafted the item</param>
    /// <param name="crafterName">Name of the player who is crafting</param>
    /// <param name="cheated"></param>
    /// <param name="pickedUp"></param>
    [Obsolete("Removed in favor of built-in cooking skill", true)]
    public void OnInventoryAddItemPostFix(string itemName, int stack, int quality, int variant, long crafterID, string crafterName, bool cheated, bool pickedUp)
    {
      // if (_isAddingExtraItem) return; // Recursive loop detection. 
      // Log.Debug(Instance, $"itemName: {itemName}, crafterID: {crafterID}, crafterName: {crafterName}, cheated: {cheated}, pickedUp: {pickedUp}");
      // Log.Debug(Instance, $"CookingSkillEnable.Value : {_configCookingSkillEnable?.Value}");
      // if (!_configCookingSkillEnable?.Value ?? false) return;
      // Log.Debug(Instance, $"IsFromCrafting : {IsFromCrafting(crafterID, crafterName)}");
      // if (!IsFromCrafting(crafterID, crafterName)) return; // Item is being bought from trader.
      // Log.Debug(Instance, $"IsConsumable : {IsConsumable(itemName)}");
      // if (!IsConsumable(itemName)) return;
      // Log.Debug(Instance, $"IsValidCookingCraftingStation : {IsValidCookingCraftingStation(Player.m_localPlayer.GetCurrentCraftingStation()?.name)}");
      // if (!IsValidCookingCraftingStation(Player.m_localPlayer.GetCurrentCraftingStation()?.name)) return;
      // Log.Debug(Instance, $"BonusWhenCookingEnabled.Value : {_configBonusWhenCookingEnabled?.Value}");
      // if (_configBonusWhenCookingEnabled?.Value ?? false)
      // {
      //   var skillLevel = Player.m_localPlayer.GetSkills().m_skillData.FirstOrDefault(s => s.Key == rkCookingSkill).Value?.m_level ?? 0;
      //   Log.Debug(Instance, $"skillLevel : {skillLevel}");
      //   // 1-100% chance to craft an extra item. 1% per level of skill.
      //   if (IsCrafterLucky(skillLevel))
      //   {
      //     Log.Debug(Instance, "[1][Start] -------------- ");
      //     AddExtraItem(itemName);
      //     Log.Debug(Instance, "[1][End] ---------------- ");
      //   }
      //
      //   // Max 25% chance to craft a 2nd extra after getting to skill level 25.
      //   if (skillLevel > 25f && IsCrafterLucky(skillLevel / 4))
      //   {
      //     Log.Debug(Instance, "[2][Start] -------------- ");
      //     AddExtraItem(itemName);
      //     Log.Debug(Instance, "[2][End] ---------------- ");
      //   }
      // }
      //
      // if (_configCookingSkillEnable?.Value == false) return;
      // RaiseCookingSkill();
    }

    /// <summary>
    /// OnVanillaPrefabsAvailable Handler
    /// </summary>
    private void PrefabManager_OnVanillaPrefabsAvailable()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        LoadSfxVfx();
        LoadButter();
        LoadNut_Ella();
        LoadIceCream();
        LoadPorkRind();
        LoadKabob();
        LoadFriedLox();
        LoadGlazedCarrot();
        LoadBacon();
        LoadSmokedFish();
        LoadPancakes();
        LoadPizza();
        LoadCoffee();
        LoadLatte();
        LoadFireCream();
        LoadElectricCream();
        LoadAcidCream();
        LoadPorridge();
        LoadPBJ();
        LoadCake();
        LoadHaggis();
        LoadCandiedTurnip();
        LoadMoochi();
        LoadBroth();
        LoadFishStew();
        LoadBloodSausage();
        LoadBurger();
        LoadOmlette();
        LoadBoiledEgg();
        LoadCarrotSticks();
        LoadChefHat();
        LoadMead();
        LoadGrillPiece();
        LoadGriddlePiece();
        LoadGrillExtensionOvenPiece();
        LoadSmokelessFirePitPiece();
        LoadSmokelessHearthPiece();
        LoadPrepstationPiece();
        LoadSmokelessHangingBrazierPiece();
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
      finally
      {
        PrefabManager.OnVanillaPrefabsAvailable -= PrefabManager_OnVanillaPrefabsAvailable;
      }
    }

    /// <summary>
    /// OnConfigurationSynchronized Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="attr"></param>
    private void SynchronizationManager_OnConfigurationSynchronized(object sender, ConfigurationSynchronizationEventArgs attr)
    {
      try
      {
        if (attr.InitialSynchronization)
        {
          Log.Info(Instance, "Initial Config sync event received");
          EnableConfiguredNewFoodRecipes();
          EnableConfiguredNewPieces();
        }
        else
        {
          Log.Info(Instance, "Config sync event received");
        }
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    /// <summary>
    /// OnLocalizationAdded Handler
    /// </summary>
    private void LocalizationManager_OnLocalizationAdded()
    {
      Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
      InitConfigs();
      LocalizationManager.OnLocalizationAdded -= LocalizationManager_OnLocalizationAdded;
    }

    #endregion

    #region Config Handlers

    /// <summary>
    /// Initialize Configs
    /// </summary>
    private void InitConfigs()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        NexusId = Config.Bind(PluginConfigSection.General, "NexusID", 1250, new ConfigDescription("Nexus mod ID for updates", null, new ConfigurationManagerAttributes { Browsable = false, ReadOnly = true }));
        _configConesEnable = Config.Bind(PluginConfigSection.Food, FoodNames.Cones, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable $mod_all {FoodNames.Cones}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configPorkRindEnable = Config.Bind(PluginConfigSection.Food, FoodNames.PorkRind, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.PorkRind}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configKabobEnable = Config.Bind(PluginConfigSection.Food, FoodNames.Kabob, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.Kabob}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configFriedLoxEnable = Config.Bind(PluginConfigSection.Food, FoodNames.FriedLox, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.FriedLox}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configGlazedCarrotEnable = Config.Bind(PluginConfigSection.Food, FoodNames.GlazedCarrots, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.GlazedCarrots}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configMeadEnable = Config.Bind(PluginConfigSection.Food, FoodNames.Mead, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.Mead}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configMoochiEnable = Config.Bind(PluginConfigSection.Food, FoodNames.Moochi, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.Moochi}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configNut_EllaEnable = Config.Bind(PluginConfigSection.Food, FoodNames.NutElla, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.NutElla}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configPancakesEnable = Config.Bind(PluginConfigSection.Food, FoodNames.Pancakes, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.Pancakes}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configOmletteEnable = Config.Bind(PluginConfigSection.Food, FoodNames.Omlette, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.Omlette}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configPBJEnable = Config.Bind(PluginConfigSection.Food, FoodNames.PBJ.Replace("'", "_"), true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.PBJ}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configButterEnable = Config.Bind(PluginConfigSection.Food, FoodNames.Butter, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.Butter}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configPizzaEnable = Config.Bind(PluginConfigSection.Food, FoodNames.Pizza, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.Pizza}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configPorridgeEnable = Config.Bind(PluginConfigSection.Food, FoodNames.Porridge, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.Porridge}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configSmokedFishEnable = Config.Bind(PluginConfigSection.Food, FoodNames.SmokedFish, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.SmokedFish}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configBloodSausageEnable = Config.Bind(PluginConfigSection.Food, FoodNames.BloodSausage, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.BloodSausage}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configBaconEnable = Config.Bind(PluginConfigSection.Food, FoodNames.Bacon, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.Bacon}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configBoiledEggEnable = Config.Bind(PluginConfigSection.Food, FoodNames.BoiledEgg, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.BoiledEgg}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configBrothEnable = Config.Bind(PluginConfigSection.Food, FoodNames.Broth, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.Broth}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configBurgerEnable = Config.Bind(PluginConfigSection.Food, FoodNames.Burger, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.Burger}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configCakeEnable = Config.Bind(PluginConfigSection.Food, FoodNames.Cake, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.Cake}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configCandiedTurnipEnable = Config.Bind(PluginConfigSection.Food, FoodNames.CandiedTurnip, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.CandiedTurnip}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configCarrotSticksEnable = Config.Bind(PluginConfigSection.Food, FoodNames.CarrotSticks, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.CarrotSticks}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configCoffeeEnable = Config.Bind(PluginConfigSection.Food, FoodNames.Coffee, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.Coffee}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configFishStewEnable = Config.Bind(PluginConfigSection.Food, FoodNames.FishStew, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.FishStew}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configHaggisEnable = Config.Bind(PluginConfigSection.Food, FoodNames.Haggis, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.Haggis}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configLatteEnable = Config.Bind(PluginConfigSection.Food, FoodNames.Latte, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {FoodNames.Latte}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configChefHatEnable = Config.Bind(PluginConfigSection.General, BoneAppetitNames.ChefHat, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {BoneAppetitNames.ChefHat}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        _configSmokelessEnable = Config.Bind(PluginConfigSection.General, BoneAppetitNames.Smokeless, true, new ConfigDescription(Localization.instance.Localize($"$mod_enable {BoneAppetitNames.SmokelessFire}"), null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        
        // _configCookingSkillEnable = Config.Bind(PluginConfigSection.CookingSkill, BoneAppetitNames.CookingSkill, true, new ConfigDescription($"Enable {BoneAppetitNames.CookingSkill}", null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        // _configBonusWhenCookingEnabled = Config.Bind(PluginConfigSection.CookingSkill, BoneAppetitNames.CookingBonus, true, new ConfigDescription($"Enable {BoneAppetitNames.CookingBonus}", null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        // ConfigHatXpGain = Config.Bind(PluginConfigSection.CookingSkill, BoneAppetitNames.ChefHatXPGain, 5f, new ConfigDescription($"XP Gain multiplier when cooking while wearing the {BoneAppetitNames.ChefHat}", null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = true }));
        // ConfigHatSEMessage = Config.Bind(PluginConfigSection.CookingSkill, BoneAppetitNames.ChefHatMessage, true, new ConfigDescription($"Enable Message when equipping the {BoneAppetitNames.ChefHat}", null, new ConfigurationManagerAttributes { Browsable = true, ReadOnly = false, isAdminOnly = false }));
        //ScrapsRecipe = Config.Bind("Leather Scraps Recipe", "Enable", true, new ConfigDescription("Enabled add a Deer Hide to Leather Scraps recipe", null, new ConfigurationManagerAttributes { IsAdminOnly = true }));
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    /// <summary>
    /// Enabled/Disables New Food Recipes
    /// </summary>
    public void EnableConfiguredNewFoodRecipes()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");

        #region Cones

        icecream.Recipe.Recipe.m_enabled = _configConesEnable.Value;
        firecream.Recipe.Recipe.m_enabled = _configConesEnable.Value;
        electriccream.Recipe.Recipe.m_enabled = _configConesEnable.Value; //idfk where the rest is going off what I see here
        acidcream.Recipe.Recipe.m_enabled = _configConesEnable.Value;     //cuz it gets used for all your ice cream      

        #endregion

        #region Food

        porkrind.Recipe.Recipe.m_enabled = _configPorkRindEnable.Value;
        kabob.Recipe.Recipe.m_enabled = _configKabobEnable.Value;
        friedlox.Recipe.Recipe.m_enabled = _configFriedLoxEnable.Value;
        glazedcarrot.Recipe.Recipe.m_enabled = _configGlazedCarrotEnable.Value;
        bacon.Recipe.Recipe.m_enabled = _configBaconEnable.Value;
        smokedfish.Recipe.Recipe.m_enabled = _configSmokedFishEnable.Value;
        pancake.Recipe.Recipe.m_enabled = _configPancakesEnable.Value;
        pizza.Recipe.Recipe.m_enabled = _configPizzaEnable.Value;
        coffee.Recipe.Recipe.m_enabled = _configCoffeeEnable.Value;
        latte.Recipe.Recipe.m_enabled = _configLatteEnable.Value;
        porridge.Recipe.Recipe.m_enabled = _configPorridgeEnable.Value;
        pbj.Recipe.Recipe.m_enabled = _configPBJEnable.Value;
        cake.Recipe.Recipe.m_enabled = _configCakeEnable.Value;
        haggis.Recipe.Recipe.m_enabled = _configHaggisEnable.Value;
        candiedTurnip.Recipe.Recipe.m_enabled = _configCandiedTurnipEnable.Value;
        moochi.Recipe.Recipe.m_enabled = _configMoochiEnable.Value;
        nut_ella.Recipe.Recipe.m_enabled = _configNut_EllaEnable.Value;
        burger.Recipe.Recipe.m_enabled = _configBurgerEnable.Value;
        omlette.Recipe.Recipe.m_enabled = _configOmletteEnable.Value;
        broth.Recipe.Recipe.m_enabled = _configBrothEnable.Value;
        fishStew.Recipe.Recipe.m_enabled = _configFishStewEnable.Value;
        butter.Recipe.Recipe.m_enabled = _configButterEnable.Value;
        bloodsausage.Recipe.Recipe.m_enabled = _configBloodSausageEnable.Value;
        boiledegg.Recipe.Recipe.m_enabled = _configBoiledEggEnable.Value;
        carrotstick.Recipe.Recipe.m_enabled = _configCarrotSticksEnable.Value;
        mead.Recipe.Recipe.m_enabled = _configMeadEnable.Value;

        #endregion
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    /// <summary>
    /// Enabled/Disables New Pieces
    /// </summary>
    private void EnableConfiguredNewPieces()
    {
      try
      {
        #region Smokeless

        fire1.Piece.m_enabled = _configSmokelessEnable.Value;
        fire2.Piece.m_enabled = _configSmokelessEnable.Value;
        fire3.Piece.m_enabled = _configSmokelessEnable.Value;

        #endregion
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    #endregion

    #region Loaders

    /// <summary>
    /// Adds new ItemDrops To monster DropTables
    /// </summary>
    public static void AddNewItemDropsToDropTables()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        var boarFab = PrefabManager.Instance.GetPrefab(EnemyNames.Boar);
        var hatchlingFab = PrefabManager.Instance.GetPrefab(PrefabNames.Hatchling);
        var seagullFab = PrefabManager.Instance.GetPrefab(EnemyNames.Seagal);
        var crowFab = PrefabManager.Instance.GetPrefab(EnemyNames.Crow);
        var porkFab_l = PrefabManager.Instance.GetPrefab("rk_pork");
        var eggFab_l = PrefabManager.Instance.GetPrefab("rk_egg");
        // ReSharper disable once IdentifierTypo
        // ReSharper disable once StringLiteralTypo
        var deggFab_l = PrefabManager.Instance.GetPrefab("rk_dragonegg");

        // ReSharper disable once IdentifierTypo
        var seagul = seagullFab.GetComponent<DropOnDestroyed>();
        seagul.m_dropWhenDestroyed.m_drops.Add(new DropTable.DropData
                                               {
                                                 m_item = eggFab_l
                                                 , m_stackMin = 1
                                                 , m_stackMax = 1
                                                 , m_weight = 1f
                                               }
                                              );

        seagul.m_dropWhenDestroyed.m_oneOfEach = true;
        seagul.m_dropWhenDestroyed.m_dropMax = 2;
        seagul.m_dropWhenDestroyed.m_dropMin = 2;
        seagul.m_dropWhenDestroyed.m_dropChance = 1;

        seagul.m_spawnYStep = 0.3f;
        seagul.m_spawnYOffset = 0.5f;

        var crow = crowFab.GetComponent<DropOnDestroyed>();
        crow.m_dropWhenDestroyed.m_drops.Add(new DropTable.DropData
                                             {
                                               m_item = eggFab_l
                                               , m_stackMin = 1
                                               , m_stackMax = 1
                                               , m_weight = 1f
                                             }
                                            );

        crow.m_dropWhenDestroyed.m_oneOfEach = true;
        crow.m_dropWhenDestroyed.m_dropMax = 2;
        crow.m_dropWhenDestroyed.m_dropMin = 2;
        crow.m_dropWhenDestroyed.m_dropChance = 1;

        crow.m_spawnYStep = 0.3f;
        crow.m_spawnYOffset = 0.5f;

        boarFab.GetComponent<CharacterDrop>().m_drops.Add(new CharacterDrop.Drop
        {
          m_prefab = porkFab_l
          , m_amountMin = 1
          , m_amountMax = 1
          , m_chance = 1f
          , m_levelMultiplier = true
          , m_onePerPlayer = false
        });

        hatchlingFab.GetComponent<CharacterDrop>().m_drops.Add(new CharacterDrop.Drop
        {
          m_prefab = deggFab_l
          , m_amountMin = 1
          , m_amountMax = 1
          , m_chance = 1f
          , m_levelMultiplier = true
          , m_onePerPlayer = false
        });
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    /// <summary>
    /// Adds the cooking skill to the game.
    /// </summary>
    [Obsolete("Removed in favor of built-in cooking skill", true)]
    public void AddSkills()
    {
      // try
      // {
      //   Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
      //   if (_configCookingSkillEnable.Value) // Test adding a skill with a texture
      //   {
      //     rkCookingSkill = SkillManager.Instance.AddSkill(new SkillConfig
      //     {
      //       Identifier = Guid
      //       , Name = "Gore-mand"
      //       , Description = "Learn to cook and eat like a Viking!"
      //       , Icon = CookingSprite
      //       , IncreaseStep = 1f,
      //     });
      //   }
      // }
      // catch (Exception e)
      // {
      //   Log.Error(Instance, e);
      // }
    }

    /// <summary>
    /// Load Asset Bundles & Sprites
    /// </summary>
    public void LoadAssets()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        GrillAssetBundle = AssetUtils.LoadAssetBundleFromResources("grill", Assembly.GetExecutingAssembly());
        FoodAssetBundle = AssetUtils.LoadAssetBundleFromResources(@"customfood", Assembly.GetExecutingAssembly());
        CookingSprite = FoodAssetBundle.LoadAsset<Sprite>(@"rkcookingsprite");
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    /// <summary>
    /// Load New ItemDrop Prefabs
    /// </summary>
    public void LoadNewItemDropPrefabs()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        Log.Info(Instance, "Prepping Kitchen...");
        porkFab = FoodAssetBundle.LoadAsset<GameObject>("rk_pork");
        ItemManager.Instance.AddItem(new CustomItem(porkFab, false));

        eggFab = FoodAssetBundle.LoadAsset<GameObject>("rk_egg");
        ItemManager.Instance.AddItem(new CustomItem(eggFab, false));

        deggFab = FoodAssetBundle.LoadAsset<GameObject>("rk_dragonegg");
        ItemManager.Instance.AddItem(new CustomItem(deggFab, false));
        Log.Info(Instance, "Big thanks to MeatwareMonster!");
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    /// <summary>
    /// Load Sound Fx and Visual Fx
    /// </summary>
    public void LoadSfxVfx()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        //var kittenPoof = assetBundle.LoadAsset<GameObject>("vfx_rainbowkitten");

        #region Build Stone SFX & VFX

        var sfxstone = PrefabManager.Cache.GetPrefab<GameObject>(PrefabNames.SfxBuildHammerStone);
        var vfxstone = PrefabManager.Cache.GetPrefab<GameObject>(PrefabNames.VfxPlaceStoneWall2X1);
        buildStone = new EffectList
        {
          m_effectPrefabs = new EffectList.EffectData[]
          {
            new()
            {
              m_prefab = sfxstone
            }
            , new()
            {
              m_prefab = vfxstone
            }
          }
        };

        #endregion

        #region Break Stone SFX

        var sfxbreak = PrefabManager.Cache.GetPrefab<GameObject>(PrefabNames.SfxRockDestroyed);
        breakStone = new EffectList
        {
          m_effectPrefabs = new EffectList.EffectData[]
          {
            new()
            {
              m_prefab = sfxbreak
            }
          }
        };

        #endregion

        #region Hit Stone SFX

        var sfxstonehit = PrefabManager.Cache.GetPrefab<GameObject>(PrefabNames.SfxRockHit);
        hitStone = new EffectList
        {
          m_effectPrefabs = new EffectList.EffectData[]
          {
            new()
            {
              m_prefab = sfxstonehit
            }
          }
        };

        #endregion

        #region Cooking Done SFX

        var sfxcook = PrefabManager.Cache.GetPrefab<GameObject>(PrefabNames.SfxCookingStationDone);
        cookingSound = new EffectList
        {
          m_effectPrefabs = new EffectList.EffectData[]
          {
            new()
            {
              m_prefab = sfxcook
            }
          }
        };

        #endregion

        #region Build Kitten SFX

        // ToDo: Is this used?
        buildKitten = new EffectList
        {
          m_effectPrefabs = new EffectList.EffectData[]
          {
            new()
            {
              m_prefab = sfxstone
            }
          }
        }; // new EffectList.EffectData { m_prefab = kittenPoof } } };

        #endregion

        #region Hearth Add Fuel SFX & VFX

        var sfxadd = PrefabManager.Cache.GetPrefab<GameObject>(PrefabNames.SfxFireAddFuel);
        var vfxaddfuel = PrefabManager.Cache.GetPrefab<GameObject>(PrefabNames.VfxHearthAddFuel);

        hearthAddFuel = new EffectList
        {
          m_effectPrefabs = new EffectList.EffectData[]
          {
            new()
            {
              m_prefab = vfxaddfuel
            }
            , new()
            {
              m_prefab = sfxadd
            }
          }
        };

        #endregion

        #region Fire Add Fuel SFX & VFX

        var vfxadd = PrefabManager.Cache.GetPrefab<GameObject>(PrefabNames.VfxFireAddFuel);
        fireAddFuel = new EffectList
        {
          m_effectPrefabs = new EffectList.EffectData[]
          {
            new()
            {
              m_prefab = vfxadd
            }
            , new()
            {
              m_prefab = sfxadd
            }
          }
        };

        #endregion

        Log.Debug(Instance, "Loaded Game VFX and SFX");

        fireVol = AudioMan.instance.m_ambientLoopSource;
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
      finally
      {
        Log.Info(Instance, "Load Complete. Bone Appetit yall.");
      }
    }

    #endregion

    #region Crafting Skill

    /// <summary>
    /// Adds an extra item to the player inventory.
    /// Checks that the player has room in their
    /// inventory before trying to add the item.
    /// </summary>
    /// <param name="itemName"></param>
    [Obsolete("Removed in favor of built-in cooking skill", true)]
    private void AddExtraItem(string itemName)
    {
      // try
      // {
      //   Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
      //   _harmony?.UnpatchSelf();
      // }
      // catch (Exception e)
      // {
      //   Log.Error(Instance, e);
      // }
      //
      // var itemPrefab = ObjectDB.instance.GetItemPrefab(itemName);
      // if (!Player.m_localPlayer.GetInventory().CanAddItem(itemPrefab, 1)) return;
      // Log.Debug(Instance, $"Trying to add extra item: {itemName}");
      // AddItem(itemName);
      // Log.Debug(Instance, $"Added extra item: {itemName}");
    }

    // /// <summary>
    // /// AddItem Recursive loop flag
    // /// </summary>
    // [Obsolete("Removed in favor of built-in cooking skill", true)]
    // private static bool _isAddingExtraItem;

    /// <summary>
    /// Adds an item to the players inventory.
    /// All checks for the player having space for a new
    /// item must be done before calling this method.
    /// 
    /// This is a recursive loop because the AddItem
    /// method is being patched. To break it, we are
    /// setting a flag to track this.
    /// </summary>
    /// <param name="itemName">Name of item to add.</param>
    [Obsolete("Removed in favor of built-in cooking skill", true)]
    private void AddItem(string itemName)
    {
      // try
      // {
      //   Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name} [{itemName}]");
      //   _isAddingExtraItem = true; // Recursive loop flag.
      //   Player.m_localPlayer.GetInventory().AddItem(itemName, 1, 1, 0, Player.m_localPlayer.GetPlayerID(), Player.m_localPlayer.GetPlayerName(), false);
      //   _isAddingExtraItem = false; // Reset flag.
      // }
      // catch (Exception e)
      // {
      //   Log.Error(Instance, e);
      // }
    }

    /// <summary>
    /// Calculate crafter's luck.
    /// </summary>
    /// <param name="skillLevel">Current skill level</param>
    /// <returns>true if crafter is lucky else false</returns>
    private bool IsCrafterLucky(float skillLevel)
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        if (skillLevel < 1) return false;
        var rand = Random.Range(1, 100);
        Log.Debug(Instance, $"Skill Level: {skillLevel} - Rand: {rand}");
        Log.Debug(Instance, $"rand < skillLevel : {rand < skillLevel}");
        return rand < skillLevel;
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }

      return false;
    }

    /// <summary>
    /// Raises Cooking skills
    /// </summary>
    [Obsolete("Removed in favor of built-in cooking skill", true)]
    private void RaiseCookingSkill()
    {
      // try
      // {
      //   Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
      //   PrintCookingSkillInfo();
      //   Player.m_localPlayer.RaiseSkill(rkCookingSkill);
      //   Log.Debug(Instance, "Cooking Skill Raised");
      //   PrintCookingSkillInfo();
      // }
      // catch (Exception e)
      // {
      //   Log.Error(Instance, e);
      // }
    }

    /// <summary>
    /// Print to the log details about the current cooking crafting skill level if a DEBUG Build
    /// </summary>
    [Conditional("DEBUG")]
    [Obsolete("Removed in favor of built-in cooking skill", true)]
    private void PrintCookingSkillInfo()
    {
      // Log.Debug(Instance, $"[Skill Level Info] Current Level: {Player.m_localPlayer.GetSkills().m_skillData.FirstOrDefault(s => s.Key == rkCookingSkill).Value?.m_level ?? 0} ({(Player.m_localPlayer.GetSkills().m_skillData.FirstOrDefault(s => s.Key == rkCookingSkill).Value?.GetLevelPercentage() ?? 0) * 100}%), " +
      //                     $"Next Level: {Player.m_localPlayer.GetSkills().m_skillData.FirstOrDefault(s => s.Key == rkCookingSkill).Value?.m_accumulator ?? 0}/{Player.m_localPlayer.GetSkills().m_skillData.FirstOrDefault(s => s.Key == rkCookingSkill).Value?.GetNextLevelRequirement() ?? 0}");
    }

    /// <summary>
    /// Check if the current crafting station is one used for cooking.
    /// </summary>
    /// <param name="currentCraftingStationName">Name of the crafting station</param>
    /// <returns>true if the current crafting station is one used for cooking else false</returns>
    private bool IsValidCookingCraftingStation(string currentCraftingStationName)
    {
      try
      {
        Log.Debug(Instance, $"currentCraftingStationName : {currentCraftingStationName}");
        switch (currentCraftingStationName)
        {
          case "rk_griddle(Clone)":
          case "rk_grill(Clone)":
          case "rk_prep(Clone)":
          case "piece_cauldron(Clone)":
            return true;
        }
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }

      return false;
    }

    /// <summary>
    /// Check if the item is being added via crafting.
    /// </summary>
    /// <param name="crafterID">Id of the player who crafted the item</param>
    /// <param name="crafterName">Name of the player who is crafting</param>
    /// <returns></returns>
    private bool IsFromCrafting(long crafterID, string crafterName)
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        Log.Debug(Instance, $"Player.m_localPlayer.GetPlayerID : {Player.m_localPlayer.GetPlayerID()}");
        return !string.IsNullOrEmpty(crafterName) && crafterID == Player.m_localPlayer.GetPlayerID();
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }

      return false;
    }

    /// <summary>
    /// Check if an item is a Consumable Type
    /// </summary>
    /// <param name="prefabName">Name of the item</param>
    /// <returns>true if the item is a Consumable else false.</returns>
    private bool IsConsumable(string prefabName)
    {
      try
      {
        var itemPrefab = ObjectDB.instance.GetItemPrefab(prefabName);
        if (itemPrefab == null) return false;
        var itemDrop = itemPrefab.GetComponent<ItemDrop>();
        if (itemDrop == null) return false;
        return itemDrop.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Consumable;
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }

      return false;
    }

    #endregion

    #region Implementation of ITraceableLogging

    /// <inheritdoc />
    public string Source => Namespace;

    /// <inheritdoc />
    public bool EnableTrace { get; }

    #endregion
  }
}
