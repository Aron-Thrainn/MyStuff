player.flamestrike_x = mouse_x;
player.flamestrike_y = mouse_y;

if distance_to_point(flamestrike_x,flamestrike_y) > range_flamestrike - 16 //radius of player
{
    scr_create_waypoint(flamestrike_x,flamestrike_y);
    cast_2 = 2;
}
else scr_pl_channel_ini_flamestrike();

