var f_temp_target = scr_find_target(id);
if f_temp_target != noone
{
    target = f_temp_target;
    scr_spells_func_ActivateEffects();
}
else scr_move_mini(x_speed,y_speed,false);

distance_travelled += missle_speed;
if distance_travelled >= max_distance  instance_destroy();
