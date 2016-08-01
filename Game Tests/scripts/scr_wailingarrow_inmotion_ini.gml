state = Spell_state.inmotion;
timer = 0;

image_speed = ui_wailing.animation_speed_inmotion;
x = player.x;
y = player.y;
image_angle = player.image_angle;
range = ui_wailing.range;
missle_speed = ui_wailing.sp;
duration = ui_wailing.duration;

var temp_dir = scr_radian(image_angle);
x_move = missle_speed * cos(temp_dir);
y_move = missle_speed * -sin(temp_dir);
