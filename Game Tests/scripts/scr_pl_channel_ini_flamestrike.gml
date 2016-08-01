temp_dir = image_angle * pi / 180; //convert degrees to radians
x_offset = 32 * cos(temp_dir);
y_offset = 32 * -sin(temp_dir);
channel = instance_create(x+x_offset,y+y_offset,obj_flamestrike);

flamestrike_timer = 0;
cast_2 = 3;
state = P_state.snared;
