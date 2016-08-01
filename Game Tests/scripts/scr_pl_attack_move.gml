if collision_circle(x,y,basic_attack_range,obj_enemy_01,true,false)
{
    distance = basic_attack_range + 1;
    temp_id = noone;
    with obj_enemy_01
    {
        if distance_to_object(player) < player.distance
        {
            other.distance = distance_to_object(player);
            other.temp_id = id;
        }
    }
    temp_target = temp_id;    
}

if temp_target == noone
{
    scr_player_movement();
}
else
{
    if attack_cd == 0
    {
        scr_basic_attack_spawn(basic_attack_dmg,basic_attack_crit,basic_attack_crit_bonus,temp_target);
    }
}
