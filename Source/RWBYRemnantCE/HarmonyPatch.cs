using CombatExtended;
using Harmony;
using RimWorld;
using RWBYRemnant;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RWBYRemnantCE
{
    [StaticConstructorOnStartup]
    static class HarmonyPatch
    {
        static HarmonyPatch()
        {
            var harmony = HarmonyInstance.Create("rimworld.carnysenpai.rwbyremnantce");
            harmony.Patch(AccessTools.Method(typeof(Verb_MeleeAttackCE), "DamageInfosToApply"), null, new HarmonyMethod(typeof(HarmonyPatch).GetMethod("DamageInfosToApply_PostFix")), null); // strenghtens certain pawns melee attacks
        }

        [HarmonyPostfix]
        public static void DamageInfosToApply_PostFix(Verb_MeleeAttackDamage __instance, ref IEnumerable<DamageInfo> __result, LocalTargetInfo target) // strenghtens certain pawns melee attacks
        {
            if (!__instance.CasterIsPawn) return;
            List<DamageInfo> newOutput = new List<DamageInfo>();
            newOutput.AddRange(__result);

            if (__instance.CasterPawn.health.hediffSet.HasHediff(RWBYDefOf.RWBY_YangReturnDamage)) // add Yang damage
            {
                CompAbilityUserAura compAbilityUserAura = __instance.CasterPawn.TryGetComp<CompAbilityUserAura>();
                if (compAbilityUserAura == null) return;
                Aura_Yang aura_Yang = (Aura_Yang)compAbilityUserAura.aura;
                if (aura_Yang == null) return;
                if (aura_Yang.absorbedDamage == 0f) return;
                float damageAmount = aura_Yang.absorbedDamage;

                DamageInfo extraDinfo = new DamageInfo(DamageDefOf.Blunt, damageAmount, 0.5f, -1f, __instance.caster, null, __instance.CasterPawn.def, DamageInfo.SourceCategory.ThingOrUnknown, null);
                extraDinfo.SetBodyRegion(BodyPartHeight.Undefined, BodyPartDepth.Outside);
                BodyPartGroupDef bodyPartGroupDef = __instance.verbProps.AdjustedLinkedBodyPartsGroup(__instance.tool);
                extraDinfo.SetWeaponBodyPartGroup(bodyPartGroupDef);
                HediffDef hediffDef = null;
                if (__instance.HediffCompSource != null)
                {
                    hediffDef = __instance.HediffCompSource.Def;
                }
                extraDinfo.SetWeaponHediff(hediffDef);
                Vector3 direction = (target.Thing.Position - __instance.CasterPawn.Position).ToVector3();
                extraDinfo.SetAngle(direction);
                newOutput.Add(extraDinfo);
            }
            if (__instance.CasterPawn.health.hediffSet.HasHediff(RWBYDefOf.RWBY_LightningBuff)) // add Nora damage
            {
                float damageAmount = 20f;

                DamageInfo extraDinfo = new DamageInfo(RWBYDefOf.RWBY_Lightning_Blunt, damageAmount, 0.5f, -1f, __instance.caster, null, __instance.CasterPawn.def, DamageInfo.SourceCategory.ThingOrUnknown, null);
                extraDinfo.SetBodyRegion(BodyPartHeight.Undefined, BodyPartDepth.Outside);
                BodyPartGroupDef bodyPartGroupDef = __instance.verbProps.AdjustedLinkedBodyPartsGroup(__instance.tool);
                extraDinfo.SetWeaponBodyPartGroup(bodyPartGroupDef);
                HediffDef hediffDef = null;
                if (__instance.HediffCompSource != null)
                {
                    hediffDef = __instance.HediffCompSource.Def;
                }
                extraDinfo.SetWeaponHediff(hediffDef);
                Vector3 direction = (target.Thing.Position - __instance.CasterPawn.Position).ToVector3();
                extraDinfo.SetAngle(direction);
                newOutput.Add(extraDinfo);
            }

            if (__instance.CasterPawn.health.hediffSet.HasHediff(RWBYDefOf.RWBY_InjectedFireCrystal)) // add Fire Dust Crystal injection damage
            {
                float damageAmount = 20f + 10f * __instance.CasterPawn.health.hediffSet.hediffs.Find(h => h.def == RWBYDefOf.RWBY_InjectedFireCrystal).CurStageIndex;

                DamageInfo extraDinfo = new DamageInfo(RWBYDefOf.RWBY_Inflame_Blunt, damageAmount, 0.5f, -1f, __instance.caster, null, __instance.CasterPawn.def, DamageInfo.SourceCategory.ThingOrUnknown, null);
                extraDinfo.SetBodyRegion(BodyPartHeight.Undefined, BodyPartDepth.Outside);
                BodyPartGroupDef bodyPartGroupDef = __instance.verbProps.AdjustedLinkedBodyPartsGroup(__instance.tool);
                extraDinfo.SetWeaponBodyPartGroup(bodyPartGroupDef);
                HediffDef hediffDef = null;
                if (__instance.HediffCompSource != null)
                {
                    hediffDef = __instance.HediffCompSource.Def;
                }
                extraDinfo.SetWeaponHediff(hediffDef);
                Vector3 direction = (target.Thing.Position - __instance.CasterPawn.Position).ToVector3();
                extraDinfo.SetAngle(direction);
                newOutput.Add(extraDinfo);
            }
            if (__instance.CasterPawn.health.hediffSet.HasHediff(RWBYDefOf.RWBY_InjectedIceCrystal)) // add Ice Dust Crystal injection damage
            {
                float damageAmount = 20f + 10f * __instance.CasterPawn.health.hediffSet.hediffs.Find(h => h.def == RWBYDefOf.RWBY_InjectedIceCrystal).CurStageIndex;

                DamageInfo extraDinfo = new DamageInfo(RWBYDefOf.RWBY_Ice_Blunt, damageAmount, 0.5f, -1f, __instance.caster, null, __instance.CasterPawn.def, DamageInfo.SourceCategory.ThingOrUnknown, null);
                extraDinfo.SetBodyRegion(BodyPartHeight.Undefined, BodyPartDepth.Outside);
                BodyPartGroupDef bodyPartGroupDef = __instance.verbProps.AdjustedLinkedBodyPartsGroup(__instance.tool);
                extraDinfo.SetWeaponBodyPartGroup(bodyPartGroupDef);
                HediffDef hediffDef = null;
                if (__instance.HediffCompSource != null)
                {
                    hediffDef = __instance.HediffCompSource.Def;
                }
                extraDinfo.SetWeaponHediff(hediffDef);
                Vector3 direction = (target.Thing.Position - __instance.CasterPawn.Position).ToVector3();
                extraDinfo.SetAngle(direction);
                newOutput.Add(extraDinfo);
            }
            if (__instance.CasterPawn.health.hediffSet.HasHediff(RWBYDefOf.RWBY_InjectedLightningCrystal)) // add Lightning Dust Crystal injection damage
            {
                float damageAmount = 20f + 10f * __instance.CasterPawn.health.hediffSet.hediffs.Find(h => h.def == RWBYDefOf.RWBY_InjectedLightningCrystal).CurStageIndex;

                DamageInfo extraDinfo = new DamageInfo(RWBYDefOf.RWBY_Lightning_Blunt, damageAmount, 0.5f, -1f, __instance.caster, null, __instance.CasterPawn.def, DamageInfo.SourceCategory.ThingOrUnknown, null);
                extraDinfo.SetBodyRegion(BodyPartHeight.Undefined, BodyPartDepth.Outside);
                BodyPartGroupDef bodyPartGroupDef = __instance.verbProps.AdjustedLinkedBodyPartsGroup(__instance.tool);
                extraDinfo.SetWeaponBodyPartGroup(bodyPartGroupDef);
                HediffDef hediffDef = null;
                if (__instance.HediffCompSource != null)
                {
                    hediffDef = __instance.HediffCompSource.Def;
                }
                extraDinfo.SetWeaponHediff(hediffDef);
                Vector3 direction = (target.Thing.Position - __instance.CasterPawn.Position).ToVector3();
                extraDinfo.SetAngle(direction);
                newOutput.Add(extraDinfo);
            }
            if (__instance.CasterPawn.health.hediffSet.HasHediff(RWBYDefOf.RWBY_InjectedGravityCrystal)) // add Gravity Dust Crystal injection damage
            {
                float damageAmount = 20f + 10f * __instance.CasterPawn.health.hediffSet.hediffs.Find(h => h.def == RWBYDefOf.RWBY_InjectedGravityCrystal).CurStageIndex;

                DamageInfo extraDinfo = new DamageInfo(DamageDefOf.Blunt, damageAmount, 0.5f, -1f, __instance.caster, null, __instance.CasterPawn.def, DamageInfo.SourceCategory.ThingOrUnknown, null);
                extraDinfo.SetBodyRegion(BodyPartHeight.Undefined, BodyPartDepth.Outside);
                BodyPartGroupDef bodyPartGroupDef = __instance.verbProps.AdjustedLinkedBodyPartsGroup(__instance.tool);
                extraDinfo.SetWeaponBodyPartGroup(bodyPartGroupDef);
                HediffDef hediffDef = null;
                if (__instance.HediffCompSource != null)
                {
                    hediffDef = __instance.HediffCompSource.Def;
                }
                extraDinfo.SetWeaponHediff(hediffDef);
                Vector3 direction = (target.Thing.Position - __instance.CasterPawn.Position).ToVector3();
                extraDinfo.SetAngle(direction);
                newOutput.Add(extraDinfo);
            }
            if (__instance.CasterPawn.health.hediffSet.HasHediff(RWBYDefOf.RWBY_InjectedHardLightCrystal)) // add HardLight Dust Crystal injection damage
            {
                float damageAmount = 20f + 10f * __instance.CasterPawn.health.hediffSet.hediffs.Find(h => h.def == RWBYDefOf.RWBY_InjectedHardLightCrystal).CurStageIndex;

                DamageInfo extraDinfo = new DamageInfo(RWBYDefOf.RWBY_Burn_Blunt, damageAmount, 0.5f, -1f, __instance.caster, null, __instance.CasterPawn.def, DamageInfo.SourceCategory.ThingOrUnknown, null);
                extraDinfo.SetBodyRegion(BodyPartHeight.Undefined, BodyPartDepth.Outside);
                BodyPartGroupDef bodyPartGroupDef = __instance.verbProps.AdjustedLinkedBodyPartsGroup(__instance.tool);
                extraDinfo.SetWeaponBodyPartGroup(bodyPartGroupDef);
                HediffDef hediffDef = null;
                if (__instance.HediffCompSource != null)
                {
                    hediffDef = __instance.HediffCompSource.Def;
                }
                extraDinfo.SetWeaponHediff(hediffDef);
                Vector3 direction = (target.Thing.Position - __instance.CasterPawn.Position).ToVector3();
                extraDinfo.SetAngle(direction);
                newOutput.Add(extraDinfo);
            }

            __result = newOutput;
        }
    }
}
