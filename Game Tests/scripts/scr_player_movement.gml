//Decide whether or not to move
if instance_exists(obj_waypoint)
{ 
    scr_player_collision();
    if position_meeting(x,y,obj_waypoint)  scr_destroy_waypoint();
}

