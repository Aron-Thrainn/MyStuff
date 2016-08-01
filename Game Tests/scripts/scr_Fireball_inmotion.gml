scr_move_mini(x_move, y_move, false);

var f_temp_target = scr_find_target(id);
if f_temp_target != noone
{
    if explosion
    {
        scr_Fireball_impact_ini();
    }
    else
    {
        if !scr_blacklist_check(id, f_temp_target)  
        {
            scr_spell_hit_switch(f_temp_target);
            scr_blacklist_add(id, f_temp_target);
            hits -= 1;
        }
    }
    
}

if place_meeting(x,y,obj_wall) 
{
    if explosion
    {
        scr_Fireball_impact_ini();
    }
    else
    {
        instance_destroy();
    }
}

timer += 1;

if (timer) >= (range / missle_speed)
{
    if explosion
    {
        scr_Fireball_impact_ini();
    }
    else
    {
        instance_destroy();
    }
}

if hits < 1 instance_destroy();
