state = Spell_state.inmotion;

range = ui_fireball.range;
missle_speed = ui_fireball.missle_speed;
explosion_duration = ui_fireball.explosion_duration;
explosion_delay = ui_fireball.explosion_delay;

if hits == 4  explosion = true;

var temp_dir = scr_radian(direction);
x_move = missle_speed * cos(temp_dir);
y_move = missle_speed * -sin(temp_dir);
