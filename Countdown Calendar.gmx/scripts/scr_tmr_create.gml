//Constant Variables
image_index = 0;
image_speed = 0;
save_n0 = 0;

//Semi-Constant Variables
pressed = 0;
pressed_r = 0;
hover = 0;
box_alpha = 1;
x_offset = x+3;
y_offset = y+3;
timer_display = 1;
frame_count = 0;
sync_var = 0;
sync_flag = 0;
annual = 0;


//Variables for instance create event

default_tlt = "missing";    //Default tooltip
timer_name = "Temp_tmr";      //Identifier for the button
sub = 0;                    //Icon image
state = tmr_state.waiting;     //Default state
loc = evr_loc.main;         //Menu location
tooltip = default_tlt;      //Tooltip text

//Config variables
alpha_max = 1;          //Default state
alpha_min = 0.6;        //Hover state
alpha_speed = 0.04;     //Speed of change
synttime = 10;          //Time between syncs


scr_tmr_default_variables();
//Activate alarm
alarm[1] = 5;



//maybe pile

//temp_y = 0;
