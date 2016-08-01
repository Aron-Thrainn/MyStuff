scr_initialize_keyinputs();
if mouse_left_hold == false
{
    if position_meeting(mouse_x,mouse_y,self)
    {    
        scr_menu_changemenu("menu2");
        scr_menu_menu2variables();
    }
    pressed = 0;
}
