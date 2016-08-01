casting = 1;
flamestrike_x = mouse_x;
flamestrike_y = mouse_y;

if scr_range_check(range,flamestrike_x,flamestrike_y)
{
    scr_spell_flamestrike_channel_ini();
}
else
{
    player.state = P_state.cast_move;
    scr_create_waypoint(flamestrike_x,flamestrike_y)
}
