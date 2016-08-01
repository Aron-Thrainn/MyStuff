switch(state)
{
    case tlt_state.idle:   scr_tooltip_step_idle();   break;
    case tlt_state.active:   scr_tooltip_step_active();   break;
    case tlt_state.trans_1:   scr_tooltip_step_trans1();   break;
    case tlt_state.trans_2:   scr_tooltip_step_trans2();   break;    
}
