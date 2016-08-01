//argument0 = status effect
var f_effect_spawn_ApplyStatusEffect = instance_create(0,0,obj_spell_effect_ApplyStatusEffect);
with f_effect_spawn_ApplyStatusEffect
{
    statuseffect = argument0;
    owner = other.id;
}
return f_effect_spawn_ApplyStatusEffect;
