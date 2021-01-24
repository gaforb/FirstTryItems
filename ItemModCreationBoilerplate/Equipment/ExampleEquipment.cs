﻿using BepInEx.Configuration;
using R2API;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;

namespace ItemModCreationBoilerplate.Equipment
{
    public class ExampleEquipment : EquipmentBase
    {
        public override string EquipmentName => "Deprecate Me Equipment";

        public override string EquipmentLangTokenName => "DEPRECATE_ME_EQUIPMENT";

        public override string EquipmentPickupDesc => "";

        public override string EquipmentFullDescription => "";

        public override string EquipmentLore => "";

        public override string EquipmentModelPath => "";

        public override string EquipmentIconPath => "";

        public override void Init(ConfigFile config)
        {
            CreateConfig(config);
            CreateLang();
            CreateEquipment();
            Hooks();
        }

        public void CreateConfig(ConfigFile config)
        {

        }

        public override ItemDisplayRuleDict CreateItemDisplayRules()
        {
            return new ItemDisplayRuleDict();
        }

        public override void Hooks()
        {

        }

        protected override bool ActivateEquipment(EquipmentSlot slot)
        {
            return false;
        }
    }
}