//Mouseover Feedbac
//if mouseover != noone       show_debug_message("staeg 0: " + (object_get_name(mouseover.object_index)));

//show_tooltip = 1;
//show_debug_message("stage 1");


if !instance_exists(obj_tooltip)
{
    tlt = instance_create(mouse_x, mouse_y, obj_tooltip);
    with tlt
    {
        //get text, box size etc.
        tlt_text = mouseover.tooltip;
        tlt_font_size = font_get_size(fnt_default);
        
        tlt_text_width = string_width_ext(tlt_text,tlt_font_size + (tlt_font_size/2), tlt_maxlength);
        tlt_text_height = string_height_ext(tlt_text,tlt_font_size + (tlt_font_size/2), tlt_maxlength);
        
        tlt_box_width = tlt_text_width + (tlt_padding*2);
        tlt_box_height = tlt_text_height + (tlt_padding*2);
    }
}
