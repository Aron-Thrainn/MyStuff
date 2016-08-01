//argument0 = damage
//argument1 = crit
//argument2 = critmul
//argument3 = target
var temp_inst = instance_create(player.x,player.y,obj_basic_attack);
attack_cd = basic_attack_speed;
with temp_inst
{
    damage = argument0;
    crit = argument1;
    crit_bonus = argument2;    
    target = argument3;
    caster = player;
}
