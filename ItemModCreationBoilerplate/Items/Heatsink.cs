using BepInEx.Configuration;
using R2API;
using RoR2;
using static FirstTryItems.Utils.MathHelpers;

namespace FirstTryItems.Items
{
    public class Heatsink : ItemBase
    {
        public static float ChillChance;
        public static float ChillSlowAmount;
        public static int ChillStacksFreeze;


        public override string ItemName => "Heatsink";

        public override string ItemLangTokenName => "HEATSINK";

        public override string ItemPickupDesc => "Chance to chill enemies on hit, eventually freezing them";

        public override string ItemFullDescription => $"{FloatToPercentageString(ChillChance)} chance to Chill enemies. Chilled enemies are slowed by {FloatToPercentageString(ChillSlowAmount)} and freeze completely once {ChillStacksFreeze} chill is applied.";

        public override string ItemLore => "Order: Computer Heatsinks\n" + "Tracking Number: 706*****\n" + "Estimated Delivery: 01/25/2056\n" + "Shipping Method: Standard\n" + "Shipping Address: 224 Ardent Blvd, Rendant, Mars\n" + "Shipping Details\n" + "\nGreat deal on these ones, got them to you in bulk since liquid cooling is all the rage now. Next thing you know every computer in the system's gonna have some fancy schmancy lights and 3 liters of NR-G coolant pumping through it. Remember when you could just fire up Windows 20 and play some good old Risk of Rain 4? Sorry, I'm rambling again...";

        public override ItemTier Tier => ItemTier.Tier1;

        public override string ItemModelPath => "@FirstTryItems:Assets/Models/Prefabs/Item/Heatsink/Heatsink.prefab";

        public override string ItemIconPath => "@FirstTryItems:Assets/Textures/Icons/Item/heatsink.png";


        public override void CreateConfig(ConfigFile config)
        {
            ChillChance = config.Bind<float>("Item: " + ItemName, "Chance to chill per stack", 8f, "How often should chill proc?").Value;
            ChillSlowAmount = config.Bind<float>("Item: " + ItemName, "Amount Chill slows targets", 20f, "How much should Chill slow targets by?").Value;
            ChillStacksFreeze = config.Bind<int>("Item: " + ItemName, "Amount of Chill needed to freeze", 8, "How many stacks of chill should freeze a target?").Value;
        }

        public override ItemDisplayRuleDict CreateItemDisplayRules()
        {
            return new ItemDisplayRuleDict();
        }

        protected override void Initialization()
        {

        }

        public override void Hooks()
        {

        }

    }
}
