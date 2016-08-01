var f_effect_spawn_Bounce = instance_create(0,0,obj_spell_effect_Bounce);
with f_effect_spawn_Bounce
{
    owner = other.id;
}
return f_effect_spawn_Bounce;
