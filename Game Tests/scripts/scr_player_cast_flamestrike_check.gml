if distance_to_point(flamestrike_x,flamestrike_y) <= range_flamestrike - 16 //radius of player
{
    scr_destroy_waypoint(); 
    scr_pl_channel_ini_flamestrike();
}

