


//need to make another script for edittimer error checking

var error_check = scr_error_edittimer();
if error_check == 1
{
    with timer_target
    {
        timer_name = m2_ipv_name;
        if !string(m2_ipv_tooltip) == ""     tooltip = string(m2_ipv_tooltip);
    
        annual = m2_ipv_annual;
        //time of day fromat script
        timer_time = m2_ipv_time;
        
        target_year = real(m2_ipv_year);
        target_month = real(m2_ipv_month);
        target_day = real(m2_ipv_day);
        target_hour = real(m2_ipv_hour);
        target_minute = real(m2_ipv_minute);
    
        sub = m2_ipv_colour;
        scr_timer_event_sync_prep();
        sync_flag = 0;
    }
    
    scr_menu_changemenu("main");
    scr_btn_event_inputreset();
    obj_initialize.alarm[2] = 1; // sort
}
