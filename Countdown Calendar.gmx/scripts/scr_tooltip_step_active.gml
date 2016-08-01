scr_initialize_keyinputs();
if key_enter == 1   state = tlt_state.trans_2
tlt_text = keyboard_string;





tlt_text_width = string_width_ext(tlt_text,tlt_font_size + (tlt_font_size/2), tlt_maxlength);
tlt_text_height = string_height_ext(tlt_text,tlt_font_size + (tlt_font_size/2), tlt_maxlength);
        
tlt_box_width = tlt_text_width + (tlt_padding*2);
tlt_box_height = tlt_text_height + (tlt_padding*2);
