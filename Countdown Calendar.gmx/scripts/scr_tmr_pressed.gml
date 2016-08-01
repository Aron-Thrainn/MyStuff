if mouse_left_hold == false
{
    if position_meeting(mouse_x,mouse_y,self)
    {        
        //timer_target = id;
        scr_menu_changemenu("menu2");
        scr_menu_menu2variables();
    }
    pressed = 0;
}

