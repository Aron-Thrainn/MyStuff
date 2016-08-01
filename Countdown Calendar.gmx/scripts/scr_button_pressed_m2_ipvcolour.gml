scr_initialize_keyinputs();
if mouse_left_hold == false
{
    if position_meeting(mouse_x,mouse_y,self) && state = btn_state.idle
    {
        with obj_button
        {
            if type == btn_type.m2_ipv_colour
            {
                state = btn_state.idle;
            }
        }
        
        state = btn_state.toggled;
        
        switch(btn_name)
        {
            case "m2_btn_colour_blue": m2_ipv_colour = 0;     break;
            case "m2_btn_colour_green": m2_ipv_colour = 1;     break;
            case "m2_btn_colour_red": m2_ipv_colour = 2;      break;
            case "m2_btn_colour_orange": m2_ipv_colour = 3;        break;
            case "m2_btn_colour_pink": m2_ipv_colour = 4;      break;
            case "m2_btn_colour_yellow": m2_ipv_colour = 5;        break;
            case "m2_btn_colour_teal": m2_ipv_colour = 6;      break;
            case "m2_btn_colour_purple": m2_ipv_colour = 7;        break;
        }
    }
    pressed = 0;
}
