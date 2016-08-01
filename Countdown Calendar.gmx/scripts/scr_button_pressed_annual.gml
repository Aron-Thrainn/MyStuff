scr_initialize_keyinputs();
if mouse_left_hold == false
{
    if position_meeting(mouse_x,mouse_y,self)
    {
        if state = btn_state.idle
        {
            state = btn_state.toggled;
            
            ipv_annual = 1;
        }
        else if state = btn_state.toggled
        {
            state = btn_state.idle;
                       
            ipv_annual = 0;
        }
    }
    pressed = 0;
}
