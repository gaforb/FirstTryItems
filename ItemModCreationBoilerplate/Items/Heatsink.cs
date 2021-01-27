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
        public static float ChillDuration;

        public override string ItemName => "Heatsink";

        public override string ItemLangTokenName => "HEATSINK";

        public override string ItemPickupDesc => "Chance to chill enemies on hit, eventually freezing them";

        public override string ItemFullDescription => $"{FloatToPercentageString(ChillChance)} <style=cStack>(+{FloatToPercentageString(ChillChance)}, per stack)</style> chance to <style=cIsDamage>Chill</style> enemies. Chilled enemies are <style=cIsUtility>slowed by {FloatToPercentageString(ChillSlowAmount)}</style> and <style=IsDamage>freeze completely</style> once {ChillStacksFreeze} chill is applied.";

        public override string ItemLore => "Order: Computer Heatsinks\n" + "Tracking Number: 706*****\n" + "Estimated Delivery: 01/25/2056\n" + "Shipping Method: Standard\n" + "Shipping Address: 224 Ardent Blvd, Rendant, Mars\n" + "Shipping Details:\n" + "\nGreat deal on these ones, got them to you in bulk since liquid cooling is all the rage now. Next thing you know every computer in the system's gonna have some fancy schmancy lights and 3 liters of NR-G coolant pumping through it. Remember when you could just fire up Windows 20 and play some good old Risk of Rain 4? Sorry, I'm rambling again...";

        public override ItemTier Tier => ItemTier.Tier1;

        public override ItemTag[] ItemTags => new ItemTag[] { ItemTag.Damage };
        public override string ItemModelPath => "@FirstTryItems:Assets/Models/Prefabs/Item/Heatsink/Heatsink.prefab";

        public override string ItemIconPath => "@FirstTryItems:Assets/Textures/Icons/Item/heatsink.png";

        public BuffIndex Chilled { get; private set; }

        public override void CreateConfig(ConfigFile config)
        {
            ChillChance = config.Bind<float>("Item: " + ItemName, "Chance to chill per stack", 0.08f, "How often should chill proc?").Value;
            ChillSlowAmount = config.Bind<float>("Item: " + ItemName, "Amount Chill slows targets", 0.2f, "How much should Chill slow targets by?").Value;
            ChillDuration = config.Bind<float>("Item: " + ItemName, "Duration of Chill Stacks", 2f, "How long should each stack of chill last?").Value;
            ChillStacksFreeze = config.Bind<int>("Item: " + ItemName, "Amount of Chill needed to freeze", 8, "How many stacks of chill should freeze a target?").Value;
        }

        private void CreateBuff()
        {
            var chilled = new CustomBuff(
            new BuffDef
            {
                canStack = true,
                isDebuff = true,
                name = "FTI: Chill",
                iconPath = "@FirstTryItems:Assets/Textures/Icons/Buff/chill.png"
            });
            Chilled = BuffAPI.Add(chilled);
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
            On.RoR2.HealthComponent.TakeDamage += ChillOut;
        }
        private void ChillOut(On.RoR2.HealthComponent.orig_TakeDamage orig, RoR2.HealthComponent self, RoR2.DamageInfo damageInfo)
        {
            if (damageInfo?.attacker)
            {
                var attackerBody = damageInfo.attacker.GetComponent<RoR2.CharacterBody>();
                if (attackerBody)
                {
                    int ItemCount = GetCount(attackerBody);

                    //Not Implemented: Chance to chill, currently 100%
                    if (ItemCount > 0 && self.body.GetBuffCount(Chilled) < ChillStacksFreeze)
                    {
                        self.body.AddTimedBuffAuthority(Chilled, ChillDuration * damageInfo.procCoefficient);
                        if (self.body.GetBuffCount(Chilled) >= ChillStacksFreeze)
                        {
                            self.body.ClearTimedBuffs(Chilled);
                        }
                    }
                }
            }
            orig(self, damageInfo);
        }
    }
}