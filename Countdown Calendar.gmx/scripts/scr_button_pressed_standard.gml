scr_initialize_keyinputs();
if mouse_left_hold == false
{
    if position_meeting(mouse_x,mouse_y,self)
    {
        switch(btn_name)
        {
            case "btn_newtimer": scr_btn_event_newtimer();      break;
            case "btn_menu1": scr_btn_event_menu1();      break;
            case "btn_deletetimer": scr_btn_event_deletetimer();      break;
            case "btn_edittimer": scr_btn_event_edittimer();      break;
            case "btn_cancel1": scr_btn_event_cancel1();      break;
            case "btn_cancel2": scr_btn_event_cancel2();      break;
            case "btn_test1": scr_test();      break;
            case "btn_test2":       break;
        }
    }
    state = btn_state.idle;
    pressed = 0;
}
