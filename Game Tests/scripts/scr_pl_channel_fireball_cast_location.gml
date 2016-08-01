direction = player.image_angle;
image_angle = direction;
var temp_x, temp_y, temp_dir, x_offset, y_offset;

temp_dir = scr_radian(direction); //convert degrees to radians

x_offset = cos(temp_dir);
y_offset = -sin(temp_dir);

//temp_x
temp_x = 32 * x_offset;

//temp_y
temp_y = 32 * y_offset;

//place the fireball
x = player.x + temp_x;
y = player.y + temp_y;
