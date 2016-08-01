switch(state)
{
    case tmr_state.idle:
    {
        //show_debug_message("stage 0: Idle");
        scr_timer_math();
        scr_tmr_mouse();
        break;
    }    
    case tmr_state.finished:
    {
        //show_debug_message("stage 0: finished");
        if annual{ state = tmr_state.annual_fin; }
        scr_tmr_mouse();
        break;
    }    
    case tmr_state.waiting:
    {
        //show_debug_message("stage 0: waiting("+ string(sync_var)+")");
        scr_tmr_waiting();
        scr_tmr_mouse();
        break;
    }    
    case tmr_state.test:
    {
        //show_debug_message("stage 0: test");
        scr_timer_math();
        scr_tmr_mouse();
        break;
    }
    case tmr_state.annual_fin:
    {
        scr_timer_event_sync_annual();
        scr_tmr_mouse();
        break;
    }
}






