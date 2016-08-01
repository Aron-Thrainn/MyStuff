var f_effect_spawn_Damage = instance_create(0,0,obj_spell_effect_Damage);
with f_effect_spawn_Damage 
{
    owner = other.id;
}
return f_effect_spawn_Damage;
