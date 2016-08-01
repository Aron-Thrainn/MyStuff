scr_initialize_variables();
scr_initialize_enumerator();
scr_call_timer_count();



scr_initialize_current_time(); //needed here for the autoload
alarm[5] = 1; //Call Autoload after objects have loaded in
alarm[0] = 5;
alarm[4] = 5; //Activate the timer sync clock


