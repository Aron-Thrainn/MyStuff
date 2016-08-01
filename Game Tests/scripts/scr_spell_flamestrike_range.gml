if scr_range_check(range,flamestrike_x,flamestrike_y)
{
    scr_spell_flamestrike_channel_ini();
    scr_destroy_waypoint();
}
else
{
    player.state = P_state.cast_move;
}
