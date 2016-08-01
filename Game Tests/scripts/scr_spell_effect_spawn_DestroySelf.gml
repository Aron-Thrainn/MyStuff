var f_effect_spawn_DestroySelf = instance_create(0,0,obj_spell_effect_DestroySelf);
with f_effect_spawn_DestroySelf
{
    owner = other.id;
}
return f_effect_spawn_DestroySelf;
