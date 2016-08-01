

with obj_timer
{
    //scr_initialize_current_time();
    var temp_comp = ((c_minute) + (c_hour * 60) + (c_day * 60*24) + (c_month * 60*24*31));
    var compress = scr_timer_event_compress(id);
    
    if compress <= temp_comp
    {
        if annual  state = tmr_state.annual_fin;
        else state = tmr_state.finished;
    }
    //else scr_timer_event_sync_prep(); Makes an infinite loop, this script triggers after timer wait
}
