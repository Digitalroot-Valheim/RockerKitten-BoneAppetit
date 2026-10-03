using Digitalroot.Modding.Framework.Logging;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using System;
using System.Reflection;
using UnityEngine;
using DMF = Digitalroot.Modding.Framework;

namespace BoneAppetit
{
  public partial class Main
  {
    #region Non-Food

    private void LoadChefHat()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        hatFab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.ChefHatPrefabName);
        hat = new CustomItem(hatFab
                             , true
                             , new ItemConfig
                             {
                               Name = BoneAppetitNames.ChefHat
                               , Description = "Improves Cooking Skill XP Earned."
                               , Enabled = _configChefHatEnable.Value
                               , Amount = 1
                               , CraftingStation = string.Empty
                               , Requirements = new[]
                               {
                                 new RequirementConfig
                                 {
                                   Item = DMF.Names.Vanilla.ItemDropNames.Dandelion
                                   , Amount = 5
                                 }
                               }
                               ,
                             });

        // var itemDrop = hat.ItemDrop;
        // var hatSe = ScriptableObject.CreateInstance<SE_ChefHat>();
        // itemDrop.m_itemData.m_shared.m_equipStatusEffect = hatSe;
        ItemManager.Instance.AddItem(hat);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadGriddlePiece()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        var griddlePrefab = GrillAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.GriddlePrefabName);
        var griddle = new CustomPiece(griddlePrefab
                                      , true
                                      , new PieceConfig
                                      {
                                        CraftingStation = string.Empty
                                        , AllowedInDungeons = false
                                        , Enabled = true
                                        , PieceTable = BoneAppetitNames.PieceTable
                                        , Requirements = new[]
                                        {
                                          new RequirementConfig
                                          {
                                            Item = DMF.Names.Vanilla.ItemDropNames.Stone
                                            , Amount = 10
                                            , Recover = true
                                          }
                                        }
                                      });

        var griddleThing = griddlePrefab.GetComponent<Piece>();
        griddleThing.m_placeEffect = buildStone;

        var griddleStation = griddlePrefab.GetComponent<CraftingStation>();
        griddleStation.m_craftItemEffects = cookingSound;
        griddleStation.m_craftingSkill = Skills.SkillType.Cooking;

        var griddleBreak = griddlePrefab.GetComponent<WearNTear>();
        griddleBreak.m_destroyedEffect = breakStone;
        griddleBreak.m_hitEffect = hitStone;

        PieceManager.Instance.AddPiece(griddle);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadGrillExtensionOvenPiece()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        var ovenPrefab = GrillAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.GrillExtensionOvenPrefabName);
        var oven = new CustomPiece(ovenPrefab
                                   , true
                                   , new PieceConfig
                                   {
                                     CraftingStation = string.Empty
                                     , AllowedInDungeons = false
                                     , Enabled = true
                                     , PieceTable = BoneAppetitNames.PieceTable
                                     , ExtendStation = BoneAppetitNames.GrillPrefabName
                                     , Requirements = new[]
                                     {
                                       new RequirementConfig
                                       {
                                         Item = DMF.Names.Vanilla.ItemDropNames.SurtlingCore
                                         , Amount = 2
                                         , Recover = true
                                       }
                                       , new RequirementConfig
                                       {
                                         Item = DMF.Names.Vanilla.ItemDropNames.TrophySurtling
                                         , Amount = 1
                                         , Recover = true
                                       }
                                       , new RequirementConfig
                                       {
                                         Item = DMF.Names.Vanilla.ItemDropNames.Stone
                                         , Amount = 10
                                         , Recover = true
                                       }
                                     }
                                   });

        var ovenPiece = ovenPrefab.GetComponent<Piece>();
        ovenPiece.m_placeEffect = buildStone;

        PieceManager.Instance.AddPiece(oven);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadGrillPiece()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        var grillFab = GrillAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.GrillPrefabName);

        var grill = new CustomPiece(grillFab
                                    , true
                                    , new PieceConfig
                                    {
                                      CraftingStation = DMF.Names.Vanilla.CraftingStationNames.Forge
                                      , AllowedInDungeons = false
                                      , Enabled = true
                                      , PieceTable = BoneAppetitNames.PieceTable
                                      , Requirements = new[]
                                      {
                                        new RequirementConfig
                                        {
                                          Item = DMF.Names.Vanilla.ItemDropNames.Stone
                                          , Amount = 10
                                          , Recover = true
                                        }
                                        , new RequirementConfig
                                        {
                                          Item = DMF.Names.Vanilla.ItemDropNames.Iron
                                          , Amount = 2
                                          , Recover = true
                                        }
                                      }
                                    });

        var grillThing = grillFab.GetComponent<Piece>();
        grillThing.m_placeEffect = buildKitten;

        var grillBreak = grillFab.GetComponent<WearNTear>();
        grillBreak.m_hitEffect = hitStone;
        grillBreak.m_destroyedEffect = breakStone;

        var grillStation = grillFab.GetComponent<CraftingStation>();
        grillStation.m_craftItemEffects = cookingSound;
        grillStation.m_craftingSkill = Skills.SkillType.Cooking;
        PieceManager.Instance.AddPiece(grill);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadPrepstationPiece()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        var prepFab = GrillAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.PrepTablePrefabName);
        var prep = new CustomPiece(prepFab
                                   , true
                                   , new PieceConfig
                                   {
                                     PieceTable = BoneAppetitNames.PieceTable
                                     , AllowedInDungeons = false
                                     , CraftingStation = DMF.Names.Vanilla.CraftingStationNames.Forge
                                     , Enabled = true
                                     , Requirements = new[]
                                     {
                                       new RequirementConfig
                                       {
                                         Item = DMF.Names.Vanilla.ItemDropNames.Wood
                                         , Amount = 4
                                         , Recover = true
                                       }
                                       , new RequirementConfig
                                       {
                                         Item = DMF.Names.Vanilla.ItemDropNames.Tin
                                         , Amount = 5
                                         , Recover = true
                                       }
                                       , new RequirementConfig
                                       {
                                         Item = DMF.Names.Vanilla.ItemDropNames.Stone
                                         , Amount = 3
                                         , Recover = true
                                       }
                                     }
                                   });

        var prepBuild = prepFab.GetComponent<Piece>();
        prepBuild.m_placeEffect = buildKitten;

        var prepDestroy = prepFab.GetComponent<WearNTear>();
        prepDestroy.m_hitEffect = hitStone;
        prepDestroy.m_destroyedEffect = breakStone;

        fireVol = prepFab.GetComponentInChildren<AudioSource>();

        var prepStation = prepFab.GetComponent<CraftingStation>();
        prepStation.m_craftingSkill = Skills.SkillType.Cooking;

        PieceManager.Instance.AddPiece(prep);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadSmokelessHangingBrazierPiece()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        fireFab3 = GrillAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.SmokelessHangingBrazierPrefabName);
        fire3 = new CustomPiece(fireFab3
                                , true
                                , new PieceConfig
                                {
                                  CraftingStation = DMF.Names.Vanilla.CraftingStationNames.Forge
                                  , AllowedInDungeons = false
                                  , Enabled = _configSmokelessEnable.Value
                                  , PieceTable = BoneAppetitNames.PieceTable
                                  , Requirements = new[]
                                  {
                                    new RequirementConfig
                                    {
                                      Item = DMF.Names.Vanilla.ItemDropNames.Bronze
                                      , Amount = 5
                                      , Recover = true
                                    }
                                    , new RequirementConfig
                                    {
                                      Item = DMF.Names.Vanilla.ItemDropNames.Coal
                                      , Amount = 2
                                      , Recover = true
                                    }
                                    , new RequirementConfig
                                    {
                                      Item = DMF.Names.Vanilla.ItemDropNames.Chain
                                      , Amount = 1
                                      , Recover = true
                                    }
                                  }
                                });

        var fireBuild = fireFab3.GetComponent<Piece>();
        fireBuild.m_placeEffect = buildStone;

        var fireDecay = fireFab3.GetComponent<WearNTear>();
        fireDecay.m_destroyedEffect = breakStone;

        var addFuel = fireFab3.GetComponent<Fireplace>();
        addFuel.m_fuelAddedEffects = hearthAddFuel;

        fireVol = fireFab3.GetComponentInChildren<AudioSource>();

        PieceManager.Instance.AddPiece(fire3);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadSmokelessHearthPiece()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        fireFab2 = GrillAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.SmokelessHearthPrefabName);
        fire2 = new CustomPiece(fireFab2
                                , true
                                , new PieceConfig
                                {
                                  CraftingStation = DMF.Names.Vanilla.CraftingStationNames.Stonecutter
                                  , AllowedInDungeons = false
                                  , Enabled = _configSmokelessEnable.Value
                                  , PieceTable = BoneAppetitNames.PieceTable
                                  , Requirements = new[]
                                  {
                                    new RequirementConfig
                                    {
                                      Item = DMF.Names.Vanilla.ItemDropNames.Stone
                                      , Amount = 15
                                      , Recover = true
                                    }
                                  }
                                });

        var fireBuild = fireFab2.GetComponent<Piece>();
        fireBuild.m_placeEffect = buildStone;

        var fireDecay = fireFab2.GetComponent<WearNTear>();
        fireDecay.m_destroyedEffect = breakStone;

        var addFuel = fireFab2.GetComponent<Fireplace>();
        addFuel.m_fuelAddedEffects = hearthAddFuel;

        fireVol = fireFab2.GetComponentInChildren<AudioSource>();

        PieceManager.Instance.AddPiece(fire2);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadSmokelessFirePitPiece()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        fireFab1 = GrillAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.SmokelessFirePitPrefabName);
        fire1 = new CustomPiece(fireFab1
                                , true
                                , new PieceConfig
                                {
                                  CraftingStation = string.Empty
                                  , AllowedInDungeons = false
                                  , Enabled = _configSmokelessEnable.Value
                                  , PieceTable = BoneAppetitNames.PieceTable
                                  , Requirements = new[]
                                  {
                                    new RequirementConfig
                                    {
                                      Item = DMF.Names.Vanilla.ItemDropNames.Stone
                                      , Amount = 5
                                      , Recover = true
                                    }
                                    , new RequirementConfig
                                    {
                                      Item = DMF.Names.Vanilla.ItemDropNames.Wood
                                      , Amount = 2
                                      , Recover = true
                                    }
                                  }
                                });

        var fireBuild = fireFab1.GetComponent<Piece>();
        fireBuild.m_placeEffect = buildStone;

        var fireDecay = fireFab1.GetComponent<WearNTear>();
        fireDecay.m_destroyedEffect = breakStone;

        var addFuel = fireFab1.GetComponent<Fireplace>();
        addFuel.m_fuelAddedEffects = fireAddFuel;

        fireVol = fireFab1.GetComponentInChildren<AudioSource>();

        PieceManager.Instance.AddPiece(fire1);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    #endregion

    #region Food

    private void LoadAcidCream()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        acidcream_prefab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.AcidCreamPrefabName);
        acidcream = new CustomItem(acidcream_prefab
                                   , false
                                   , new ItemConfig
                                   {
                                     Name = FoodNames.AcidCream
                                     , Enabled = _configConesEnable.Value
                                     , Amount = 2
                                     , CraftingStation = BoneAppetitNames.PrepTablePrefabName
                                     , Requirements = new[]
                                     {
                                       new RequirementConfig
                                       {
                                         Item = DMF.Names.Vanilla.ItemDropNames.Guck
                                         , Amount = 4
                                       }
                                       , new RequirementConfig
                                       {
                                         Item = DMF.Names.Vanilla.ItemDropNames.MushroomYellow
                                         , Amount = 8
                                       }
                                       , new RequirementConfig
                                       {
                                         Item = DMF.Names.Vanilla.ItemDropNames.Honey
                                         , Amount = 2
                                       }
                                       , new RequirementConfig
                                       {
                                         Item = BoneAppetitNames.DragonEggPrefabName
                                         , Amount = 2
                                       }
                                     }
                                   });

        ItemManager.Instance.AddItem(acidcream);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadBacon()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        bacon_prefab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.BaconPrefabName);
        bacon = new CustomItem(bacon_prefab
                               , false
                               , new ItemConfig
                               {
                                 Name = FoodNames.Bacon
                                 , Enabled = _configBaconEnable.Value
                                 , Amount = 2
                                 , CraftingStation = BoneAppetitNames.GriddlePrefabName
                                 , Requirements = new[]
                                 {
                                   new RequirementConfig
                                   {
                                     Item = BoneAppetitNames.PorkPrefabName
                                     , Amount = 2
                                   }
                                 }
                               });

        ItemManager.Instance.AddItem(bacon);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadBloodSausage()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        bloodsausageFab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.BloodSausagePrefabName);
        bloodsausage = new CustomItem(bloodsausageFab
                                      , false
                                      , new ItemConfig
                                      {
                                        Name = FoodNames.BloodSausage
                                        , Enabled = _configBloodSausageEnable.Value
                                        , Amount = 2
                                        , CraftingStation = BoneAppetitNames.GrillPrefabName
                                        , Requirements = new[]
                                        {
                                          new RequirementConfig
                                          {
                                            Item = DMF.Names.Vanilla.ItemDropNames.Entrails
                                            , Amount = 2
                                          }
                                          , new RequirementConfig
                                          {
                                            Item = DMF.Names.Vanilla.ItemDropNames.Bloodbag
                                            , Amount = 1
                                          }
                                          , new RequirementConfig
                                          {
                                            Item = DMF.Names.Vanilla.ItemDropNames.Thistle
                                            , Amount = 2
                                          }
                                          , new RequirementConfig
                                          {
                                            Item = BoneAppetitNames.PorkPrefabName
                                            , Amount = 2
                                          }
                                        }
                                      });

        ItemManager.Instance.AddItem(bloodsausage);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadBoiledEgg()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        boiledeggFab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.BoiledEggPrefabName);
        boiledegg = new CustomItem(boiledeggFab
                                   , false
                                   , new ItemConfig
                                   {
                                     Name = FoodNames.BoiledEgg
                                     , Enabled = _configBoiledEggEnable.Value
                                     , Amount = 1
                                     , CraftingStation = BoneAppetitNames.PrepTablePrefabName
                                     , Requirements = new[]
                                     {
                                       new RequirementConfig
                                       {
                                         Item = BoneAppetitNames.EggPrefabName
                                         , Amount = 2
                                       }
                                     }
                                   });

        ItemManager.Instance.AddItem(boiledegg);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadBroth()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        brothFab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.BrothPrefabName);
        broth = new CustomItem(brothFab
                               , false
                               , new ItemConfig
                               {
                                 Name = FoodNames.Broth
                                 , Enabled = _configBrothEnable.Value
                                 , Amount = 1
                                 , CraftingStation = BoneAppetitNames.PrepTablePrefabName
                                 , Requirements = new[]
                                 {
                                   new RequirementConfig
                                   {
                                     Item = DMF.Names.Vanilla.ItemDropNames.BoneFragments
                                     , Amount = 2
                                   }
                                   , new RequirementConfig
                                   {
                                     Item = BoneAppetitNames.ButterPrefabName
                                     , Amount = 1
                                   }
                                 }
                               });

        ItemManager.Instance.AddItem(broth);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadBurger()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        burgerFab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.BurgerPrefabName);
        burger = new CustomItem(burgerFab
                                , false
                                , new ItemConfig
                                {
                                  Name = FoodNames.Burger
                                  , Enabled = _configBurgerEnable.Value
                                  , Amount = 2
                                  , CraftingStation = BoneAppetitNames.GrillPrefabName
                                  , Requirements = new[]
                                  {
                                    new RequirementConfig
                                    {
                                      Item = DMF.Names.Vanilla.ItemDropNames.RawMeat
                                      , Amount = 2
                                    }
                                    , new RequirementConfig
                                    {
                                      Item = DMF.Names.Vanilla.ItemDropNames.LoxMeat
                                      , Amount = 2
                                    }
                                    , new RequirementConfig
                                    {
                                      Item = DMF.Names.Vanilla.ItemDropNames.Turnip
                                      , Amount = 2
                                    }
                                    , new RequirementConfig
                                    {
                                      Item = DMF.Names.Vanilla.ItemDropNames.Bread
                                      , Amount = 1
                                    }
                                  }
                                });

        ItemManager.Instance.AddItem(burger);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadButter()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        butterFab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.ButterPrefabName);
        butter = new CustomItem(butterFab
                                , false
                                , new ItemConfig
                                {
                                  Name = FoodNames.Butter
                                  , Enabled = _configButterEnable.Value
                                  , Amount = 2
                                  , CraftingStation = BoneAppetitNames.PrepTablePrefabName
                                  , Requirements = new[]
                                  {
                                    new RequirementConfig
                                    {
                                      Item = DMF.Names.Vanilla.ItemDropNames.CarrotSeeds
                                      , Amount = 8
                                    }
                                  }
                                });

        ItemManager.Instance.AddItem(butter);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadCake()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        cake_prefab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.CakePrefabName);
        cake = new CustomItem(cake_prefab
                              , false
                              , new ItemConfig
                              {
                                Name = FoodNames.Cake
                                , Enabled = _configCakeEnable.Value
                                , Amount = 1
                                , CraftingStation = BoneAppetitNames.GrillPrefabName
                                , MinStationLevel = 2
                                , Requirements = new[]
                                {
                                  new RequirementConfig
                                  {
                                    Item = DMF.Names.Vanilla.ItemDropNames.BarleyFlour
                                    , Amount = 2
                                  }
                                  , new RequirementConfig
                                  {
                                    Item = DMF.Names.Vanilla.ItemDropNames.Honey
                                    , Amount = 4
                                  }
                                  , new RequirementConfig
                                  {
                                    Item = DMF.Names.Vanilla.ItemDropNames.Cloudberry
                                    , Amount = 4
                                  }
                                  , new RequirementConfig
                                  {
                                    Item = BoneAppetitNames.EggPrefabName
                                    , Amount = 2
                                  }
                                }
                              });

        ItemManager.Instance.AddItem(cake);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadCandiedTurnip()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        candiedTurnipFab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.CandiedTurnipPrefabName);
        candiedTurnip = new CustomItem(candiedTurnipFab
                                       , false
                                       , new ItemConfig
                                       {
                                         Name = FoodNames.CandiedTurnip
                                         , Enabled = _configCandiedTurnipEnable.Value
                                         , Amount = 1
                                         , CraftingStation = BoneAppetitNames.GrillPrefabName
                                         , Requirements = new[]
                                         {
                                           new RequirementConfig
                                           {
                                             Item = DMF.Names.Vanilla.ItemDropNames.Thistle
                                             , Amount = 1
                                           }
                                           , new RequirementConfig
                                           {
                                             Item = DMF.Names.Vanilla.ItemDropNames.Honey
                                             , Amount = 2
                                           }
                                           , new RequirementConfig
                                           {
                                             Item = DMF.Names.Vanilla.ItemDropNames.Turnip
                                             , Amount = 2
                                           }
                                         }
                                       });

        ItemManager.Instance.AddItem(candiedTurnip);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadCarrotSticks()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        carrotstickFab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.CarrotSticksPrefabName);
        carrotstick = new CustomItem(carrotstickFab
                                     , false
                                     , new ItemConfig
                                     {
                                       Name = FoodNames.CarrotSticks
                                       , Enabled = _configCarrotSticksEnable.Value
                                       , Amount = 1
                                       , CraftingStation = BoneAppetitNames.PrepTablePrefabName
                                       , Requirements = new[]
                                       {
                                         new RequirementConfig
                                         {
                                           Item = DMF.Names.Vanilla.ItemDropNames.Carrot
                                           , Amount = 2
                                         }
                                         , new RequirementConfig
                                         {
                                           Item = BoneAppetitNames.NutEllaPrefabName
                                           , Amount = 1
                                         }
                                       }
                                     });

        ItemManager.Instance.AddItem(carrotstick);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadCoffee()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        coffee_prefab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.CoffeePrefabName);
        coffee = new CustomItem(coffee_prefab
                                , false
                                , new ItemConfig
                                {
                                  Name = FoodNames.Coffee
                                  , Enabled = _configCoffeeEnable.Value
                                  , Amount = 1
                                  , CraftingStation = BoneAppetitNames.PrepTablePrefabName
                                  , Requirements = new[]
                                  {
                                    new RequirementConfig
                                    {
                                      Item = DMF.Names.Vanilla.ItemDropNames.AncientSeed
                                      , Amount = 2
                                    }
                                  }
                                });

        ItemManager.Instance.AddItem(coffee);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadElectricCream()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        electriccream_prefab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.ElectricCreamPrefabName);
        electriccream = new CustomItem(electriccream_prefab
                                       , false
                                       , new ItemConfig
                                       {
                                         Name = FoodNames.ElectricCream
                                         , Enabled = _configConesEnable.Value
                                         , Amount = 2
                                         , CraftingStation = BoneAppetitNames.PrepTablePrefabName
                                         , Requirements = new[]
                                         {
                                           new RequirementConfig
                                           {
                                             Item = DMF.Names.Vanilla.ItemDropNames.Crystal
                                             , Amount = 4
                                           }
                                           , new RequirementConfig
                                           {
                                             Item = DMF.Names.Vanilla.ItemDropNames.Cloudberry
                                             , Amount = 8
                                           }
                                           , new RequirementConfig
                                           {
                                             Item = DMF.Names.Vanilla.ItemDropNames.Honey
                                             , Amount = 2
                                           }
                                           , new RequirementConfig
                                           {
                                             Item = BoneAppetitNames.DragonEggPrefabName
                                             , Amount = 2
                                           }
                                         }
                                       });

        ItemManager.Instance.AddItem(electriccream);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadFireCream()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        firecream_prefab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.FireCreamPrefabName);
        firecream = new CustomItem(firecream_prefab
                                   , false
                                   , new ItemConfig
                                   {
                                     Name = FoodNames.FireCream
                                     , Enabled = _configConesEnable.Value
                                     , Amount = 2
                                     , CraftingStation = BoneAppetitNames.PrepTablePrefabName
                                     , Requirements = new[]
                                     {
                                       new RequirementConfig
                                       {
                                         Item = DMF.Names.Vanilla.ItemDropNames.SurtlingCore
                                         , Amount = 4
                                       }
                                       , new RequirementConfig
                                       {
                                         Item = DMF.Names.Vanilla.ItemDropNames.Raspberry
                                         , Amount = 8
                                       }
                                       , new RequirementConfig
                                       {
                                         Item = DMF.Names.Vanilla.ItemDropNames.Honey
                                         , Amount = 2
                                       }
                                       , new RequirementConfig
                                       {
                                         Item = BoneAppetitNames.DragonEggPrefabName
                                         , Amount = 2
                                       }
                                     }
                                   });

        ItemManager.Instance.AddItem(firecream);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadFishStew()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        fishStewFab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.FishStewPrefabName);
        fishStew = new CustomItem(fishStewFab
                                  , false
                                  , new ItemConfig
                                  {
                                    Name = FoodNames.FishStew
                                    , Enabled = _configFishStewEnable.Value
                                    , Amount = 1
                                    , CraftingStation = BoneAppetitNames.PrepTablePrefabName
                                    , Requirements = new[]
                                    {
                                      new RequirementConfig
                                      {
                                        Item = BoneAppetitNames.BrothPrefabName
                                        , Amount = 1
                                      }
                                      , new RequirementConfig
                                      {
                                        Item = DMF.Names.Vanilla.ItemDropNames.FishRaw
                                        , Amount = 2
                                      }
                                      , new RequirementConfig
                                      {
                                        Item = DMF.Names.Vanilla.ItemDropNames.Thistle
                                        , Amount = 2
                                      }
                                      , new RequirementConfig
                                      {
                                        Item = BoneAppetitNames.EggPrefabName
                                        , Amount = 2
                                      }
                                    }
                                  });

        ItemManager.Instance.AddItem(fishStew);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadHaggis()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        haggisFab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.HaggisPrefabName);
        haggis = new CustomItem(haggisFab
                                , false
                                , new ItemConfig
                                {
                                  Name = FoodNames.Haggis
                                  , Enabled = _configHaggisEnable.Value
                                  , Amount = 1
                                  , CraftingStation = BoneAppetitNames.PrepTablePrefabName
                                  , Requirements = new[]
                                  {
                                    new RequirementConfig
                                    {
                                      Item = DMF.Names.Vanilla.ItemDropNames.RawMeat
                                      , Amount = 1
                                    }
                                    , new RequirementConfig
                                    {
                                      Item = DMF.Names.Vanilla.ItemDropNames.Carrot
                                      , Amount = 2
                                    }
                                    , new RequirementConfig
                                    {
                                      Item = DMF.Names.Vanilla.ItemDropNames.Entrails
                                      , Amount = 2
                                    }
                                    , new RequirementConfig
                                    {
                                      Item = DMF.Names.Vanilla.ItemDropNames.Turnip
                                      , Amount = 2
                                    }
                                  }
                                });

        ItemManager.Instance.AddItem(haggis);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadIceCream()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        icecream_prefab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.IceCreamPrefabName);
        icecream = new CustomItem(icecream_prefab
                                  , false
                                  , new ItemConfig
                                  {
                                    Name = FoodNames.IceCream
                                    , Enabled = _configConesEnable.Value
                                    , Amount = 2
                                    , CraftingStation = BoneAppetitNames.PrepTablePrefabName
                                    , Requirements = new[]
                                    {
                                      new RequirementConfig
                                      {
                                        Item = DMF.Names.Vanilla.ItemDropNames.FreezeGland
                                        , Amount = 4
                                      }
                                      , new RequirementConfig
                                      {
                                        Item = DMF.Names.Vanilla.ItemDropNames.Blueberries
                                        , Amount = 8
                                      }
                                      , new RequirementConfig
                                      {
                                        Item = DMF.Names.Vanilla.ItemDropNames.Honey
                                        , Amount = 2
                                      }
                                      , new RequirementConfig
                                      {
                                        Item = BoneAppetitNames.DragonEggPrefabName
                                        , Amount = 1
                                      }
                                    }
                                  });

        ItemManager.Instance.AddItem(icecream);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadLatte()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        latte_prefab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.LattePrefabName);
        latte = new CustomItem(latte_prefab
                               , false
                               , new ItemConfig
                               {
                                 Name = FoodNames.Latte
                                 , Enabled = _configLatteEnable.Value
                                 , Amount = 2
                                 , CraftingStation = BoneAppetitNames.PrepTablePrefabName
                                 , Requirements = new[]
                                 {
                                   new RequirementConfig
                                   {
                                     Item = DMF.Names.Vanilla.ItemDropNames.Crystal
                                     , Amount = 2
                                   }
                                   , new RequirementConfig
                                   {
                                     Item = DMF.Names.Vanilla.ItemDropNames.Barley
                                     , Amount = 2
                                   }
                                   , new RequirementConfig
                                   {
                                     Item = DMF.Names.Vanilla.ItemDropNames.Honey
                                     , Amount = 10
                                   }
                                 }
                               });

        ItemManager.Instance.AddItem(latte);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadFriedLox()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        friedlox_prefab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.FriedLoxMeatPrefabName);
        friedlox = new CustomItem(friedlox_prefab
                                  , false
                                  , new ItemConfig
                                  {
                                    Name = FoodNames.FriedLox == "Fried Lox" ? "Chicken Fried Lox Meat" : FoodNames.FriedLox
                                    , Enabled = _configFriedLoxEnable.Value
                                    , Amount = 1
                                    , CraftingStation = BoneAppetitNames.GrillPrefabName
                                    , Requirements = new[]
                                    {
                                      new RequirementConfig
                                      {
                                        Item = DMF.Names.Vanilla.ItemDropNames.LoxMeat
                                        , Amount = 2
                                      }
                                      , new RequirementConfig
                                      {
                                        Item = DMF.Names.Vanilla.ItemDropNames.BarleyFlour
                                        , Amount = 2
                                      }
                                      , new RequirementConfig
                                      {
                                        Item = BoneAppetitNames.EggPrefabName
                                        , Amount = 1
                                      }
                                      , new RequirementConfig
                                      {
                                        Item = BoneAppetitNames.ButterPrefabName
                                        , Amount = 2
                                      }
                                    }
                                  });

        ItemManager.Instance.AddItem(friedlox);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadGlazedCarrot()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        glazedcarrot_prefab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.GlazedCarrotsPrefabName);
        glazedcarrot = new CustomItem(glazedcarrot_prefab
                                      , false
                                      , new ItemConfig
                                      {
                                        Name = FoodNames.GlazedCarrots == "Glazed Carrots" ? "Honey Glazed Carrots" : FoodNames.GlazedCarrots
                                        , Enabled = _configGlazedCarrotEnable.Value
                                        , Amount = 1
                                        , CraftingStation = BoneAppetitNames.GriddlePrefabName
                                        , Requirements = new[]
                                        {
                                          new RequirementConfig
                                          {
                                            Item = DMF.Names.Vanilla.ItemDropNames.Carrot
                                            , Amount = 3
                                          }
                                          , new RequirementConfig
                                          {
                                            Item = DMF.Names.Vanilla.ItemDropNames.Honey
                                            , Amount = 2
                                          }
                                          , new RequirementConfig
                                          {
                                            Item = DMF.Names.Vanilla.ItemDropNames.Dandelion
                                            , Amount = 2
                                          }
                                        }
                                      });

        ItemManager.Instance.AddItem(glazedcarrot);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadKabob()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        kabob_prefab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.KabobPrefabName);
        kabob = new CustomItem(kabob_prefab
                               , false
                               , new ItemConfig
                               {
                                 Name = FoodNames.Kabob
                                 , Enabled = _configKabobEnable.Value
                                 , Amount = 1
                                 , CraftingStation = BoneAppetitNames.GriddlePrefabName
                                 , Requirements = new[]
                                 {
                                   new RequirementConfig
                                   {
                                     Item = DMF.Names.Vanilla.ItemDropNames.Turnip
                                     , Amount = 1
                                   }
                                   , new RequirementConfig
                                   {
                                     Item = DMF.Names.Vanilla.ItemDropNames.Carrot
                                     , Amount = 2
                                   }
                                   , new RequirementConfig
                                   {
                                     Item = DMF.Names.Vanilla.ItemDropNames.RawMeat
                                     , Amount = 1
                                   }
                                   , new RequirementConfig
                                   {
                                     Item = DMF.Names.Vanilla.ItemDropNames.BoneFragments
                                     , Amount = 2
                                   }
                                 }
                               });

        ItemManager.Instance.AddItem(kabob);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadPorkRind()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        porkrind_prefab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.PorkrindPrefabName);
        porkrind = new CustomItem(porkrind_prefab
                                  , false
                                  , new ItemConfig
                                  {
                                    // ReSharper disable once StringLiteralTypo
                                    Name = FoodNames.PorkRind
                                    , Enabled = _configPorkRindEnable.Value
                                    , Amount = 1
                                    , CraftingStation = BoneAppetitNames.GriddlePrefabName
                                    , Requirements = new[]
                                    {
                                      new RequirementConfig
                                      {
                                        Item = DMF.Names.Vanilla.ItemDropNames.LeatherScraps
                                        , Amount = 1
                                      }
                                      , new RequirementConfig
                                      {
                                        Item = BoneAppetitNames.PorkPrefabName
                                        , Amount = 1
                                      }
                                    }
                                  });

        ItemManager.Instance.AddItem(porkrind);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadMead()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        meadFab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.MeadPrefabName);
        mead = new CustomItem(meadFab
                              , false
                              , new ItemConfig
                              {
                                Name = FoodNames.Mead
                                , Enabled = _configMeadEnable.Value
                                , Amount = 1
                                , CraftingStation = BoneAppetitNames.PrepTablePrefabName
                                , MinStationLevel = 1
                                , Requirements = new[]
                                {
                                  new RequirementConfig
                                  {
                                    Item = DMF.Names.Vanilla.ItemDropNames.Barley
                                    , Amount = 3
                                  }
                                  , new RequirementConfig
                                  {
                                    Item = DMF.Names.Vanilla.ItemDropNames.Honey
                                    , Amount = 4
                                  }
                                }
                              });

        ItemManager.Instance.AddItem(mead);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadMoochi()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        moochiFab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.MoochiPrefabName);
        moochi = new CustomItem(moochiFab
                                , false
                                , new ItemConfig
                                {
                                  Name = FoodNames.Moochi
                                  , Enabled = _configMoochiEnable.Value
                                  , Amount = 1
                                  , CraftingStation = BoneAppetitNames.PrepTablePrefabName
                                  , Requirements = new[]
                                  {
                                    new RequirementConfig
                                    {
                                      Item = BoneAppetitNames.DragonEggPrefabName
                                      , Amount = 1
                                    }
                                    , new RequirementConfig
                                    {
                                      Item = DMF.Names.Vanilla.ItemDropNames.Honey
                                      , Amount = 2
                                    }
                                    , new RequirementConfig
                                    {
                                      Item = DMF.Names.Vanilla.ItemDropNames.FreezeGland
                                      , Amount = 1
                                    }
                                    , new RequirementConfig
                                    {
                                      Item = DMF.Names.Vanilla.ItemDropNames.Blueberries
                                      , Amount = 4
                                    }
                                  }
                                });

        ItemManager.Instance.AddItem(moochi);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadNut_Ella()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        nut_ellaFab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.NutEllaPrefabName);
        nut_ella = new CustomItem(nut_ellaFab
                                  , false
                                  , new ItemConfig
                                  {
                                    Name = FoodNames.NutElla
                                    , Enabled = _configNut_EllaEnable.Value
                                    , Amount = 1
                                    , CraftingStation = BoneAppetitNames.PrepTablePrefabName
                                    , Requirements = new[]
                                    {
                                      new RequirementConfig
                                      {
                                        Item = DMF.Names.Vanilla.ItemDropNames.BeechSeeds
                                        , Amount = 6
                                      }
                                      , new RequirementConfig
                                      {
                                        Item = BoneAppetitNames.ButterPrefabName
                                        , Amount = 1
                                      }
                                    }
                                  });

        ItemManager.Instance.AddItem(nut_ella);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    // ReSharper disable once IdentifierTypo
    private void LoadOmlette()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        omletteFab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.OmlettePrefabName);
        omlette = new CustomItem(omletteFab
                                 , false
                                 , new ItemConfig
                                 {
                                   Name = FoodNames.Omlette
                                   , Enabled = _configOmletteEnable.Value
                                   , Amount = 1
                                   , CraftingStation = BoneAppetitNames.GriddlePrefabName
                                   , Requirements = new[]
                                   {
                                     new RequirementConfig
                                     {
                                       Item = BoneAppetitNames.EggPrefabName
                                       , Amount = 2
                                     }
                                     , new RequirementConfig
                                     {
                                       Item = DMF.Names.Vanilla.ItemDropNames.Thistle
                                       , Amount = 2
                                     }
                                     , new RequirementConfig
                                     {
                                       Item = BoneAppetitNames.PorkPrefabName
                                       , Amount = 1
                                     }
                                     , new RequirementConfig
                                     {
                                       Item = BoneAppetitNames.ButterPrefabName
                                       , Amount = 1
                                     }
                                   }
                                 });

        ItemManager.Instance.AddItem(omlette);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadPancakes()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        pancake_prefab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.PancakePrefabName);
        pancake = new CustomItem(pancake_prefab
                                 , false
                                 , new ItemConfig
                                 {
                                   Name = FoodNames.Pancakes
                                   , Enabled = _configPancakesEnable.Value
                                   , Amount = 1
                                   , CraftingStation = BoneAppetitNames.GrillPrefabName
                                   , MinStationLevel = 2
                                   , Requirements = new[]
                                   {
                                     new RequirementConfig
                                     {
                                       Item = DMF.Names.Vanilla.ItemDropNames.Honey
                                       , Amount = 2
                                     }
                                     , new RequirementConfig
                                     {
                                       Item = DMF.Names.Vanilla.ItemDropNames.BarleyFlour
                                       , Amount = 3
                                     }
                                     , new RequirementConfig
                                     {
                                       Item = BoneAppetitNames.ButterPrefabName
                                       , Amount = 5
                                     }
                                     , new RequirementConfig
                                     {
                                       Item = BoneAppetitNames.EggPrefabName
                                       , Amount = 2
                                     }
                                   }
                                 });

        ItemManager.Instance.AddItem(pancake);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadPBJ()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        pbj_prefab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.PbjPrefabName);
        pbj = new CustomItem(pbj_prefab
                             , false
                             , new ItemConfig
                             {
                               Name = FoodNames.PBJ
                               , Enabled = _configPBJEnable.Value
                               , Amount = 4
                               , CraftingStation = BoneAppetitNames.PrepTablePrefabName
                               , MinStationLevel = 1
                               , Requirements = new[]
                               {
                                 new RequirementConfig
                                 {
                                   Item = DMF.Names.Vanilla.ItemDropNames.Bread
                                   , Amount = 1
                                 }
                                 , new RequirementConfig
                                 {
                                   Item = DMF.Names.Vanilla.ItemDropNames.QueensJam
                                   , Amount = 1
                                 }
                                 , new RequirementConfig
                                 {
                                   Item = BoneAppetitNames.NutEllaPrefabName
                                   , Amount = 4
                                 }
                               }
                             });

        ItemManager.Instance.AddItem(pbj);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadPizza()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        pizza_prefab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.PizzaPrefabName);
        pizza = new CustomItem(pizza_prefab
                               , false
                               , new ItemConfig
                               {
                                 Name = FoodNames.Pizza
                                 , Enabled = _configPizzaEnable.Value
                                 , Amount = 1
                                 , CraftingStation = BoneAppetitNames.GrillPrefabName
                                 , MinStationLevel = 2
                                 , Requirements = new[]
                                 {
                                   new RequirementConfig
                                   {
                                     Item = DMF.Names.Vanilla.ItemDropNames.Mushroom
                                     , Amount = 2
                                   }
                                   , new RequirementConfig
                                   {
                                     Item = DMF.Names.Vanilla.ItemDropNames.BarleyFlour
                                     , Amount = 3
                                   }
                                   , new RequirementConfig
                                   {
                                     Item = BoneAppetitNames.EggPrefabName
                                     , Amount = 2
                                   }
                                   , new RequirementConfig
                                   {
                                     Item = DMF.Names.Vanilla.ItemDropNames.RawMeat
                                     , Amount = 2
                                   }
                                 }
                               });

        ItemManager.Instance.AddItem(pizza);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadPorridge()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        porridge_prefab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.PorridgePrefabName);
        porridge = new CustomItem(porridge_prefab
                                  , false
                                  , new ItemConfig
                                  {
                                    Name = FoodNames.Porridge
                                    , Enabled = _configPorridgeEnable.Value
                                    , Amount = 1
                                    , CraftingStation = BoneAppetitNames.GrillPrefabName
                                    , MinStationLevel = 2
                                    , Requirements = new[]
                                    {
                                      new RequirementConfig
                                      {
                                        Item = DMF.Names.Vanilla.ItemDropNames.Barley
                                        , Amount = 2
                                      }
                                      , new RequirementConfig
                                      {
                                        Item = DMF.Names.Vanilla.ItemDropNames.Cloudberry
                                        , Amount = 4
                                      }
                                      , new RequirementConfig
                                      {
                                        Item = DMF.Names.Vanilla.ItemDropNames.Honey
                                        , Amount = 2
                                      }
                                      , new RequirementConfig
                                      {
                                        Item = BoneAppetitNames.ButterPrefabName
                                        , Amount = 1
                                      }
                                    }
                                  });

        ItemManager.Instance.AddItem(porridge);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    private void LoadSmokedFish()
    {
      try
      {
        Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod()?.DeclaringType?.Name}.{MethodBase.GetCurrentMethod()?.Name}");
        smokedfish_prefab = FoodAssetBundle.LoadAsset<GameObject>(BoneAppetitNames.SmokedFishPrefabName);
        smokedfish = new CustomItem(smokedfish_prefab
                                    , false
                                    , new ItemConfig
                                    {
                                      Name = FoodNames.SmokedFish
                                      , Enabled = _configSmokedFishEnable.Value
                                      , Amount = 1
                                      , CraftingStation = BoneAppetitNames.GrillPrefabName
                                      , Requirements = new[]
                                      {
                                        new RequirementConfig
                                        {
                                          Item = DMF.Names.Vanilla.ItemDropNames.FishRaw
                                          , Amount = 1
                                        }
                                      }
                                    });

        ItemManager.Instance.AddItem(smokedfish);
      }
      catch (Exception e)
      {
        Log.Error(Instance, e);
      }
    }

    #endregion
  }
}
