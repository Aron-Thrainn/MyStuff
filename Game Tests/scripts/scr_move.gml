//argument0 = speed
//argument1 = dir (degrees)
//argument2 = collide with walls (true / false)
var sp = argument0;
var radian = scr_radian(argument1);

var x_move = sp * cos(radian);
var y_move = sp * -sin(radian);

scr_move_mini(x_move, y_move, argument2);
