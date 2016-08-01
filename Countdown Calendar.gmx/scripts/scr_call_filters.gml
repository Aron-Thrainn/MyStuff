with obj_button
{
    if type == btn_type.ipv_filter
    {
        switch(btn_name)
        {
            case "btn_filter_blue":  if flr_blue == 1{   state = btn_state.toggled;}  break;
            case "btn_filter_green":  if flr_green == 1{   state = btn_state.toggled;}  break;
            case "btn_filter_red":  if flr_red == 1{   state = btn_state.toggled;}  break;
            case "btn_filter_orange":  if flr_orange == 1{   state = btn_state.toggled;}  break;
            case "btn_filter_pink":  if flr_pink == 1{   state = btn_state.toggled;} break; 
            case "btn_filter_yellow":  if flr_yellow == 1{   state = btn_state.toggled;} break;
            case "btn_filter_teal":  if flr_teal == 1{   state = btn_state.toggled;} break;
            case "btn_filter_purple":  if flr_purple == 1{   state = btn_state.toggled;} break;
        }
    }
}
