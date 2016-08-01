if !instance_exists(obj_menu)
{
    mouseover = instance_position(mouse_x,mouse_y,pnt_mouseover);
    if mouseover != noone
    {
        switch(object_get_name(mouseover.object_index))
        {
            case "obj_timer": with mouseover  scr_mouseover_timer(); break;
            case "obj_inputbox": with mouseover  scr_mouseover_inputbox(); break;
            case "obj_button": with mouseover  scr_mouseover_button(); break;
            case "obj_textbox": with mouseover  scr_mouseover_textbox(); break;
        }
        
    }
    else 
    {
        scr_mouseover_noone();
    }
}
else 
{
    
    if !position_meeting(mouse_x,mouse_y,obj_menu)   scr_mouseover_menu();
    
    mouseover = instance_position(mouse_x,mouse_y,pnt_mouseover);
    if mouseover != noone
    {
        switch(object_get_name(mouseover.object_index))
        {
            case "obj_timer": with mouseover
            {
                if loc == evr_loc.menu_1 || loc == evr_loc.menu_2
                {
                    scr_mouseover_timer(); 
                }
            }  break;
            case "obj_inputbox": with mouseover  scr_mouseover_inputbox(); break;
            case "obj_button": with mouseover  
            {
                if loc == evr_loc.menu_1 || loc == evr_loc.menu_2
                {
                    scr_mouseover_button();
                }
            } break;
            case "obj_textbox": with mouseover  scr_mouseover_textbox(); break;
        }
        
    }
    else 
    {
        scr_mouseover_noone();
    }

}
