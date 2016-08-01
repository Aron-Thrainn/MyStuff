
if string_height_ext(keyboard_string,20,max_width) < box_height
{
    if key_enter == 1     keyboard_string += "#";
    if string_width_ext(keyboard_string,0,max_width+10) > (max_width-5)
    {
        keyboard_string = string_insert("#", keyboard_string, string_length(keyboard_string));
    } 
    tbx_text = keyboard_string;
    
    switch(tbx_name)
    {
        case "tbn_description": ipv_tooltip = tbx_text;      break;
        case "m2_tbn_description": m2_ipv_tooltip = tbx_text;      break;
    }
}
//show_debug_message("-------------------------")
//show_debug_message(keyboard_string);
//show_debug_message(string(string_height_ext(keyboard_string,20,max_width)));
//show_debug_message(string(string_width_ext(keyboard_string,0,max_width+10)));
keyboard_string = tbx_text;


