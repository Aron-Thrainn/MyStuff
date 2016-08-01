switch(state)
{
    case tmr_state.idle:
    {
        scr_tmr_draw_idle();
        break;
    }    
    case tmr_state.finished:
    {
        scr_tmr_draw_finished();
        break;
    }    
    case tmr_state.waiting:
    {
        scr_tmr_draw_waiting();
        break;
    }    
    case tmr_state.test:
    {
        scr_tmr_draw_idle();
        break;
    } 
    case tmr_state.annual_fin:
    {
        scr_tmr_draw_finished_annual();
        break;
    }    
}

