//default variables
tlt_alpha = 0;
tlt_padding = 16;
tlt_maxlength = 4000;
show_tooltip = 0;
tlt_text = "Missing";
state = tlt_state.idle;
animate_tick = 0;


tlt_max_x = 600;
tlt_max_y = 800;

temp_parent = noone;
//if mouseover.pressed_r == true  instance_destroy();
if mouseover.tooltip != ""      tlt_text = mouseover.tooltip;
