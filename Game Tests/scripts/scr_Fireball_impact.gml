

if timer < explosion_delay
{
    scr_move_mini(x_move, y_move, false);
}
//show_debug_message(string(timer) + " | " + string(explosion_delay))
switch (timer)
{
    case explosion_delay: 
    {
        image_index = 4;
        image_xscale = 3;
        image_yscale = 3;
        scr_find_target_spell(id,1);
        break;
    }
    case explosion_duration: instance_destroy(); break;
}
timer += 1
