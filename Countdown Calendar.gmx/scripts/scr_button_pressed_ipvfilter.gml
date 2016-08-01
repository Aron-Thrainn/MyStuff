scr_initialize_keyinputs();
if mouse_left_hold == false
{
    if position_meeting(mouse_x,mouse_y,self) && state = btn_state.idle
    {
        state = btn_state.toggled;
        switch(btn_name)
        {
            case "btn_filter_blue":  flr_blue = true;   break;
            case "btn_filter_green":  flr_green = true;   break;
            case "btn_filter_red":  flr_red = true;   break;
            case "btn_filter_orange":  flr_orange = true;   break;
            case "btn_filter_pink":  flr_pink = true;   break;
            case "btn_filter_yellow":  flr_yellow = true;   break;
            case "btn_filter_teal":  flr_teal = true;   break;
            case "btn_filter_purple":  flr_purple = true;   break;
        }
    }
    else if position_meeting(mouse_x,mouse_y,self) && state == btn_state.toggled
    {
        state = btn_state.idle;
        switch(btn_name)
        {
            case "btn_filter_blue":  flr_blue = false;   break;
            case "btn_filter_green":  flr_green = false;   break;
            case "btn_filter_red":  flr_red = false;   break;
            case "btn_filter_orange":  flr_orange = false;   break;
            case "btn_filter_pink":  flr_pink = false;   break;
            case "btn_filter_yellow":  flr_yellow = false;   break;
            case "btn_filter_teal":  flr_teal = false;   break;
            case "btn_filter_purple":  flr_purple = false;   break;
        }
    }
    pressed = 0;
    if pressed_r == false    obj_initialize.alarm[2] = 1;
}
    
    
    
    
