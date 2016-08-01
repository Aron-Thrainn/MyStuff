direction = point_direction(x,y,obj_waypoint.x,obj_waypoint.y);
var sp_collide = sp / room_speed;        //Temporary variable for collision movement

//Collide & move



var dir = point_direction(x,y,obj_waypoint.x,obj_waypoint.y);

if dir >= 270 facing = 1;                       //down right
if dir >= 180 && dir < 270 facing = 2;          //down left
if dir >= 90 && dir < 180 facing = 3;           //up left
if dir >= 0 && dir < 90 facing = 4;             //up right


switch (facing)
{
    case 1: 
    {
        if place_meeting(x + sp_collide,y,obj_wall) //going right
        {
            sp_collide = ((360 - dir) / 90) * sp_collide;
            direction = 270;
        }
        if place_meeting(x,y + sp,obj_wall) //going down
        {
            sp_collide = ((dir - 270) / 90) * sp;
            if direction == 270 sp_collide = 0;
            else direction = 0;
        }
        break;
    }      
    case 2: 
    {
        if place_meeting(x - sp,y,obj_wall) //going left
        {
            sp_collide = ((dir - 180) / 90) * sp;
            direction = 270;
        }
        if place_meeting(x,y + sp,obj_wall) //going down
        {
            sp_collide = ((270 - dir) / 90) * sp;
            if direction == 270 sp_collide = 0;
            else direction = 180;
        }
        break;
    }        
    case 3: 
    {
        if place_meeting(x - sp,y,obj_wall) //going left
        {
            sp_collide = ((180 - dir) / 90) * sp;
            direction = 90;
        }
        if place_meeting(x,y - sp,obj_wall) //going up
        {
            sp_collide = ((dir - 90) / 90) * sp;
            if direction == 90 sp_collide = 0;
            else direction = 180;
        }
        break;
    }
    case 4: 
    {
        if place_meeting(x + sp,y,obj_wall) //going right
        {
            sp_collide = ((dir) / 90) * sp;
            direction = 90;
        }
        if place_meeting(x,y - sp,obj_wall) //going up
        {
            sp_collide = ((90 - dir) / 90) * sp;
            if direction == 90 sp_collide = 0;
            else direction = 0;
        }
        break;
    }   
}



motion_set(direction,sp_collide);
