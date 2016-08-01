//argument0 = x_move
//argument1 = y_move
//argument2 = collide with walls (true / false)
var x_move = argument0;
var y_move = argument1;
if argument2
{
    if !(place_meeting(x+x_move,y,pnt_wall))   x += x_move;
    if !(place_meeting(x,y+y_move,pnt_wall))   y += y_move;
}
else 
{
    x += x_move;
    y += y_move;
}

