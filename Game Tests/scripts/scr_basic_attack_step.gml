if position_meeting(x,y,target)
{
    scr_spell_hit_switch(target);
}
else
{
    var dir = point_direction(x,y,target.x,target.y);
    
    scr_move(missle_speed, dir, false);
    
    missle_speed += missle_acceleration;
}

