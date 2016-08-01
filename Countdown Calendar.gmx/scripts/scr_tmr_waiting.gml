if sync_var != c_second
{
    state = tmr_state.idle;
    sync_flag = 1;
    scr_timer_event_sync();
    scr_timer_math_finishcheck();
}
//show_debug_message("stage 1: " + string(sync_var) + " | " + string(c_second));
