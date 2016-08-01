scr_initialize_keyinputs();

//left click to add text
if mouse_left == 1
{
    with obj_inputbox       state = ipb_state.idle;
    
    keyboard_string = ipb_text;
    state = ipb_state.active;
    animate_tick = 0;
    
}
//right click to remove and add text
if mouse_right == 1
{
    with obj_inputbox       state = ipb_state.idle;
    
    ipb_text = "";
    keyboard_string = ipb_text;
    state = ipb_state.active;
    animate_tick = 0;
    
}
