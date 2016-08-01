var error_check = scr_error_newtimer();
if error_check == 1
{
    newtimer = instance_create(150,0,obj_timer);
    
    with newtimer
    {
    //Call variables from inputs
    
    //Select colour
    sub = ipv_colour;
    
    
    annual = ipv_annual;
    timer_time = ipv_time;
    timer_name = ipv_name;
    
    target_year = real(ipv_year);
    target_month = real(ipv_month);
    target_day = real(ipv_day);
    target_hour = real(ipv_hour);
    target_minute = real(ipv_minute);
    
    
    tooltip = string(ipv_tooltip);
    
    scr_timer_event_sync_prep();
    
    
    // Reset creation menu variables
    scr_btn_event_inputreset();
    
    compress = scr_timer_event_compress(id);
    
    }
    //Sort the timers
    obj_initialize.alarm[2] = 1;
    
    scr_menu_changemenu("main");
}
