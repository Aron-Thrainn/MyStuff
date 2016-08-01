if !annual
{
    state = tmr_state.finished;
}

if c_day > target_day
{
    target_year += 1; 
    scr_timer_event_sync_prep();
}
