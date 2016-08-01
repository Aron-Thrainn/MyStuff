scr_initialize_keyinputs();
if mouse_left_hold == false
{
    if position_meeting(mouse_x,mouse_y,self) && state = btn_state.idle
    {
        with obj_button
        {
            if type == btn_type.ipv_colour
            {
                state = btn_state.idle;
            }
        }
        
        state = btn_state.toggled;
        
        switch(btn_name)
        {
            case "btn_colour_blue": ipv_colour = 0;      break;
            case "btn_colour_green": ipv_colour = 1;        break;
            case "btn_colour_red": ipv_colour = 2;      break;
            case "btn_colour_orange": ipv_colour = 3;        break;
            case "btn_colour_pink": ipv_colour = 4;      break;
            case "btn_colour_yellow": ipv_colour = 5;        break;
            case "btn_colour_teal": ipv_colour = 6;      break;
            case "btn_colour_purple": ipv_colour = 7;        break;
        }
    }
    pressed = 0;
}
