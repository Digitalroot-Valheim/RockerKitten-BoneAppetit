using Jotunn.Managers;

namespace BoneAppetit
{
  internal static class BoneAppetitNames
  {
    internal const string Smokeless = nameof(Smokeless);
    internal const string PieceTable = "_HammerPieceTable";
    // ReSharper disable once StringLiteralTypo
    internal static readonly string ChefHat = LocalizationManager.Instance.TryTranslate("$mod_item_chefhat");
    internal static readonly string SmokelessFire = LocalizationManager.Instance.TryTranslate("$mod_item_smokeless_fire");
    // internal const string OriginalGrill = "Original Grill";
    // internal const string CookingSkill = "Cooking Skill";
    // internal const string CookingBonus = "Cooking Bonus";
    // internal const string ChefHatXPGain = "Chef Hat XP Gain";
    // internal const string ChefHatMessage = "Chef Hat Message";
    internal const string GriddlePrefabName = "rk_griddle";
    internal const string PrepTablePrefabName = "rk_prep";
    internal const string GrillPrefabName = "rk_grill";
    internal const string PorkPrefabName = "rk_pork";
    // ReSharper disable once IdentifierTypo
    internal const string PorkrindPrefabName = "rk_porkrind";
    internal const string KabobPrefabName = "rk_kabob";
    internal const string FriedLoxMeatPrefabName = "rk_friedloxmeat";
    internal const string EggPrefabName = "rk_egg";
    internal const string ButterPrefabName = "rk_butter";
    internal const string GlazedCarrotsPrefabName = "rk_glazedcarrots";
    internal const string MeadPrefabName = "rk_mead";
    internal const string MoochiPrefabName = "rk_moochi";
    internal const string DragonEggPrefabName = "rk_dragonegg";
    internal const string NutEllaPrefabName = "rk_nut_ella";
    // ReSharper disable once IdentifierTypo
    internal const string OmlettePrefabName = "rk_omlette";
    internal const string PancakePrefabName = "rk_pancake";
    internal const string PbjPrefabName = "rk_pbj";
    internal const string PizzaPrefabName = "rk_pizza";
    internal const string PorridgePrefabName = "rk_porridge";
    internal const string SmokedFishPrefabName = "rk_smokedfish";
    internal const string ChefHatPrefabName = "rk_chef";
    internal const string GrillExtensionOvenPrefabName = "rk_oven";
    internal const string SmokelessHangingBrazierPrefabName = "rk_brazier";
    internal const string SmokelessHearthPrefabName = "rk_hearth";
    internal const string SmokelessFirePitPrefabName = "rk_campfire";
    internal const string AcidCreamPrefabName = "rk_acidcream";
    internal const string BaconPrefabName = "rk_bacon";
    internal const string BloodSausagePrefabName = "rk_bloodsausage";
    internal const string BoiledEggPrefabName = "rk_boiledegg";
    internal const string BrothPrefabName = "rk_broth";
    internal const string BurgerPrefabName = "rk_burger";
    internal const string CakePrefabName = "rk_birthday";
    internal const string CandiedTurnipPrefabName = "rk_candiedturnip";
    internal const string CarrotSticksPrefabName = "rk_carrotsticks";
    internal const string CoffeePrefabName = "rk_coffee";
    internal const string ElectricCreamPrefabName = "rk_electriccream";
    internal const string FireCreamPrefabName = "rk_firecream";
    internal const string FishStewPrefabName = "rk_fishstew";
    internal const string HaggisPrefabName = "rk_haggis";
    internal const string IceCreamPrefabName = "rk_icecream";
    internal const string LattePrefabName = "rk_latte";
  }

  internal static class FoodNames
  {
    internal static readonly string Cones = LocalizationManager.Instance.TryTranslate("$mod_food_cones");
    // ReSharper disable once StringLiteralTypo
    internal static readonly string PorkRind = LocalizationManager.Instance.TryTranslate("$mod_food_porkrind");
    internal static readonly string FriedLox = LocalizationManager.Instance.TryTranslate("$mod_food_fried_lox");
    internal static readonly string GlazedCarrots = LocalizationManager.Instance.TryTranslate("$mod_food_glazed_carrots");
    internal static readonly string SmokedFish = LocalizationManager.Instance.TryTranslate("$mod_food_smoked_fish");
    internal static readonly string CandiedTurnip = LocalizationManager.Instance.TryTranslate("$mod_food_candied_turnip");
    internal static readonly string Kabob = LocalizationManager.Instance.TryTranslate("$mod_food_kabob");
    internal static readonly string Bacon = LocalizationManager.Instance.TryTranslate("$mod_food_bacon");
    internal static readonly string Pancakes = LocalizationManager.Instance.TryTranslate("$mod_food_pancakes");
    internal static readonly string Pizza = LocalizationManager.Instance.TryTranslate("$mod_food_pizza");
    internal static readonly string Coffee = LocalizationManager.Instance.TryTranslate("$mod_food_coffee");
    internal static readonly string Latte = LocalizationManager.Instance.TryTranslate("$mod_food_spice_latte");
    internal static readonly string Porridge = LocalizationManager.Instance.TryTranslate("$mod_food_porridge");
    // ReSharper disable once InconsistentNaming
    internal static readonly string PBJ = LocalizationManager.Instance.TryTranslate("$mod_food_pbj");
    internal static readonly string Cake = LocalizationManager.Instance.TryTranslate("$mod_food_cake");
    internal static readonly string Haggis = LocalizationManager.Instance.TryTranslate("$mod_food_haggis");
    internal static readonly string Moochi = LocalizationManager.Instance.TryTranslate("$mod_food_moochi");
    internal static readonly string Burger = LocalizationManager.Instance.TryTranslate("$mod_food_burger");
    // ReSharper disable once IdentifierTypo
    // ReSharper disable once StringLiteralTypo
    internal static readonly string Omlette = LocalizationManager.Instance.TryTranslate("$mod_food_omlette");
    internal static readonly string Broth = LocalizationManager.Instance.TryTranslate("$mod_food_bone_broth");
    internal static readonly string Butter = LocalizationManager.Instance.TryTranslate("$mod_food_butter");
    internal static readonly string Mead = LocalizationManager.Instance.TryTranslate("$mod_food_mead");
    internal static readonly string NutElla = LocalizationManager.Instance.TryTranslate("$mod_food_nut_ella");
    internal static readonly string FishStew = LocalizationManager.Instance.TryTranslate("$mod_food_fish_stew");
    internal static readonly string BloodSausage = LocalizationManager.Instance.TryTranslate("$mod_food_blood_sausage");
    internal static readonly string BoiledEgg = LocalizationManager.Instance.TryTranslate("$mod_food_boiled_egg");
    internal static readonly string CarrotSticks = LocalizationManager.Instance.TryTranslate("$mod_food_carrot_sticks");
    internal static readonly string AcidCream = LocalizationManager.Instance.TryTranslate("$mod_food_acid_cream");
    internal static readonly string ElectricCream = LocalizationManager.Instance.TryTranslate("$mod_food_electric_cream");
    internal static readonly string FireCream = LocalizationManager.Instance.TryTranslate("$mod_food_fire_cream");
    internal static readonly string IceCream = LocalizationManager.Instance.TryTranslate("$mod_food_ice_cream");
  }
}
