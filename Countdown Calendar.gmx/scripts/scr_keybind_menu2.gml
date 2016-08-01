var input = active_menu.active_input;

if key_enter
{
    with obj_button  if btn_name == "btn_newtimer"
    {
        scr_btn_event_edittimer();
    }
}

if key_esc
{
    scr_btn_event_cancel2();
}

if key_tab
{
    if !key_shift_hold
    {
        
        switch(input)
        {
            case "": scr_keybind_event_activate_input("m2_tbn_name"); break;
            case "m2_tbn_name": scr_keybind_event_activate_input("m2_tbn_year"); break;
            case "m2_tbn_year": scr_keybind_event_activate_input("m2_tbn_month"); break;
            case "m2_tbn_month": scr_keybind_event_activate_input("m2_tbn_day"); break;
            case "m2_tbn_day": scr_keybind_event_activate_input("m2_tbn_time"); break;
            case "m2_tbn_time": scr_keybind_event_activate_input("m2_tbn_description"); break;
            case "m2_tbn_description": scr_keybind_event_activate_input("m2_tbn_name"); break;
            
        }
    }
    else
    {
        switch(input)
        {
            case "": scr_keybind_event_activate_input("m2_tbn_description"); break;
            case "m2_tbn_name": scr_keybind_event_activate_input("m2_tbn_description"); break;
            case "m2_tbn_year": scr_keybind_event_activate_input("m2_tbn_name"); break;
            case "m2_tbn_month": scr_keybind_event_activate_input("m2_tbn_year"); break;
            case "m2_tbn_day": scr_keybind_event_activate_input("m2_tbn_month"); break;
            case "m2_tbn_time": scr_keybind_event_activate_input("m2_tbn_day"); break;
            case "m2_tbn_description": scr_keybind_event_activate_input("m2_tbn_time"); break;
            
        }
    }


}
