## Changelog:

## v3.4.0
- Update for Valheim 1.0
- Removed `Gore-mand` skill
- Crafting raises Cooking Skill
- Grill, Griddle, Prep Table are all working.
- Grill Extention is working.
- All Smokeless Fire Sources are working.
- Chef Hat buff disabled for now.
- Moved PBJ to Prep Table
- Moved Smoked Fish to Grill
- Moved Pancakes to Griddle
- Disabled unused configs
- Localized Everything.

## v3.2.0
- Update for Jotunn

## v3.1.0
- Added Cooking Skill
  - Gain XP when cooking at a **Cooking Station**, or cooking a Consumable at the **rk_griddle**, **rk_grill**, **rk_prep** or **piece_cauldron**. Each level of cooking skill adds a 1% chance to craft an additional consumable. after lvl 25, your total cooking level is divided by 4 and the results is the % chance to crafted a 2nd additional consumable. 
  - e.g. At level 100, there is a 100% chance to craft an additional consumable and 25% chance to craft a 2nd additional consumable on top of that. At level 100 a food that crafts in stacks of x5, if lucky could output 7 total items.
  - Set **CookingSkillEnable** = false to disable.
- Added SE_CheffHat
  - Improves Cooking Skill XP Earned while wearing the Chef Hat. 
  - Set **HatXpGain** = 1 to disable.
  - Taking the Chef Hat on and off displays a random Julia Child's quote. 
    - Displaying this message can be disabled by setting **HatSEMessage** to false.
- Added Configs
  - CookingSkillEnable (server synced, enforced)
  - BonusWhenCookingEnabled (server synced, enforced)
  - HatXpGain (server synced, enforced)
  - HatSEMessage (local)
- Added NexusId for update notice mod.

## v3.0.2
- Load order fix
- maybe fix for prep table fire (hopefully)

## v3.0.1
- Hotfix for boar drops when using with CLLC calculate Amount
- Fix for missing Omlette
- Added smokeless Braziers by request.

## v3.0.0
- Added MasterChef 2.0 assets. Omlette, Nut-ella, Bloodsausage, Broth, Carrot Butter, Burgers, and Fish Stew.
- New Drops! You can now loot Pork from Boars, smaller Transportable Dragon Eggs from Drakes, and eggs from seagulls.
- New food station, the Prep Staton. Unlocks with Tin.
- More new food, Haggis, Moochi, Carrot Sticks, Boiled Eggs, and Candied Turnips.
- Major balance pass, changed some recipes and food values, moved all food out of the cauldron and spread between Prep, griddle, and grill.
- New Smokeless fires, for those of you struggling with FPS drops due to smoke particle physics, or who just want an indoor kitchen. There is now a campfire, a hearth that no longer produce particle smoke. (can be disabled in the config).

## v2.0.1
- Fixed "CloudBerry" so we can have CAKE! (Big thanks to PROXiCiDE for pointing this out to me)

## v2.0.0
- Code rework
- Updated Bacon Visuals (thicker) and fixed attach point
- Added "Elemental" Cream Cones updated visuals on Ice Cream
- First Rebalance pass
- Added Porridge
- Added A PJB for a special little boy by request.
- Added CONFIG!!! (only a true/false for enabling/disabling recipes, likely will stay this way) Server synced. 
- Everything defaults to on, when loading for the first time.
- Redid all "Flavor" text

### `MAKE SURE YOU HAVE THE MOD AND CONFIG ON THE SERVER FOR THE SERVER SYNC TO WORK`

## v1.1.1
- updated visuals on bacon
- Fixed collider on Griddle
- Fixed attach point on kabob 

## v1.1.0  
- fix for the grill making it difficult to load the fire underneath
- fix for Fried Lox not auto picking up
- 5 new foods. Pancakes, Smoked Fish, Bacon, Coffee, and Pizza

## v1.0.1  
- hotfix for those not using V+

## v1.0.0  
- release includes 2 food crafting stations that require a fire underneath to cook on, and 5 new food items, one for each biome.



