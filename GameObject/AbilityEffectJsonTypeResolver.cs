using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Text.Json.Serialization;

namespace GameObject
{
    public class AbilityEffectJsonTypeResolver : DefaultJsonTypeInfoResolver
    {
        public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
        {
            JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);

            Type baseEffectType = typeof(AbilityLogic.ITimedEffect);
            if (jsonTypeInfo.Type == baseEffectType)
            {
                jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                {
                    TypeDiscriminatorPropertyName = "$Effect_type",
                    IgnoreUnrecognizedTypeDiscriminators = true,
                    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
                    DerivedTypes =
                {
                    new JsonDerivedType(typeof(AbilityLogic.GameEffects.Effect_damage_bmd_full), "Effect_damage_bmd_full"),
                    new JsonDerivedType(typeof(AbilityLogic.GameEffects.Effect_dot_mwp), "Effect_dot_mwp"),
                    new JsonDerivedType(typeof(AbilityLogic.VisualEffects.VFX_throw_ball), "VFX_throw_ball"),
                    new JsonDerivedType(typeof(AbilityLogic.VisualEffects.VFX_charge_ball), "VFX_charge_ball"),
                    new JsonDerivedType(typeof(AbilityLogic.VisualEffects.VFX_animate), "VFX_animate")
                }
                };
            }

            return jsonTypeInfo;
        }
    }
}
