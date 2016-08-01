if temp_target != noone && !temp_target.dead
{
    if distance_to_point(temp_target.x,temp_target.y) < basic_attack_range
    {
        if attack_cd == 0
        {
            scr_basic_attack_spawn(basic_attack_dmg,basic_attack_crit,basic_attack_crit_bonus,temp_target);
            scr_destroy_waypoint();
        }
    }
    else 
    {
        scr_create_waypoint(temp_target.x,temp_target.y);
    }
}
