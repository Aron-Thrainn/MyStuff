dir = scr_radian(player.image_angle);   
x_move = sp * cos(dir);
y_move = sp * -sin(dir);

scr_destroy_waypoint();

player.cast[slot] = 1;
player.state = P_state.charging;

timer = 0;
channel = instance_create(0,0,obj_charge); 
with channel scr_charge_inmotion_ini();
casting = 1;
sprite_index = spr_ui_charge_cd;


