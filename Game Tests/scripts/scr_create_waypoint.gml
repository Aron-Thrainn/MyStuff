//argument0 = x
//argument1 = y
scr_destroy_waypoint();

if abs(argument0 - player.x) > 8 || abs(argument1 - player.y) > 8
{
    instance_create(argument0,argument1,obj_waypoint);
}
