//argument0 = cooldown in seconds
var f_casteffect_spawn_SetCooldown = instance_create(0,0,obj_spell_casteffect_SetCooldown);
with f_casteffect_spawn_SetCooldown
{
    cooldown = argument0;
    owner = other.id;
}
return f_casteffect_spawn_SetCooldown;
