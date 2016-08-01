//argument0 = missle object
var f_casteffect_spawn_CreateMissle = instance_create(0,0,obj_spell_casteffect_CreateMissle);
with f_casteffect_spawn_CreateMissle
{
    missle = argument0;
    owner = other.id;
}
return f_casteffect_spawn_CreateMissle;
