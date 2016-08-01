var input = active_menu.active_input;

if key_enter
{
    with obj_button  if btn_name == "btn_newtimer"
    {
        scr_btn_event_newtimer();
    }
}

if key_esc
{
    scr_btn_event_cancel1();
}

if key_tab
{
    
    if !key_shift_hold
    {
        switch(input)
        {
            case "": scr_keybind_event_activate_input("tbn_name"); break;
            case "tbn_name": scr_keybind_event_activate_input("tbn_year"); break;
            case "tbn_year": scr_keybind_event_activate_input("tbn_month"); break;
            case "tbn_month": scr_keybind_event_activate_input("tbn_day"); break;
            case "tbn_day": scr_keybind_event_activate_input("tbn_time"); break;
            case "tbn_time": scr_keybind_event_activate_input("tbn_description"); break;
            case "tbn_description": scr_keybind_event_activate_input("tbn_name"); break;
            
        }
    }
    else
    {
        switch(input)
        {
            case "": scr_keybind_event_activate_input("tbn_description"); break;
            case "tbn_name": scr_keybind_event_activate_input("tbn_description"); break;
            case "tbn_year": scr_keybind_event_activate_input("tbn_name"); break;
            case "tbn_month": scr_keybind_event_activate_input("tbn_year"); break;
            case "tbn_day": scr_keybind_event_activate_input("tbn_month"); break;
            case "tbn_time": scr_keybind_event_activate_input("tbn_day"); break;
            case "tbn_description": scr_keybind_event_activate_input("tbn_time"); break;
            
        }
    }
}

