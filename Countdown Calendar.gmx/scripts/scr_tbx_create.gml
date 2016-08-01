//Constant Variables


//Semi-Constant Variables
rmb_flag = 0;
tbx_text = "";
tbx_text_show = "";
animate_tick = 0;

//Variables for instance create event

tbx_title = "";             //Text above the inputbox
tbx_name = "tbn_temp";      //Identifier for the button
state = tbx_state.idle;     //Default state
type = tbx_type.title;      //Button type
depth = -1;                 //Depth
loc = evr_loc.main;         //Menu location
default_tooltip = "";       //Default tooltip
tooltip = default_tooltip;  //tooltip
sprite_index = spr_tbx_date;      //collision only, not drawn
box_width = 120;         //title = 260 | date = 60 | description = 300 | time = 90
box_height = 20;         //title = 20 | date = 20 | description = 120 | time = 20
max_width = box_width;         
max_height = box_height;        //height for the description box

//config variables
idle_colour = c_black;
active_colour = c_red;



