temp_dir = scr_radian(image_angle); //convert degrees to radians
x_offset = 32 * cos(temp_dir);
y_offset = 32 * -sin(temp_dir);
channel = instance_create(player.x+x_offset,player.y+y_offset,obj_flamestrike);

timer = 0;
player.cast[slot] = 1;
player.state = P_state.snared;
casting = 2;
sprite_index = spr_ui_flamestrike_casting;
