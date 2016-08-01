//render textbox and text
draw_set_alpha(tlt_alpha)
if tlt_alpha < 1 tlt_alpha += 0.06; else tlt_alpha = 1;


clamped_x = min(mouse_x, (tlt_max_x - (tlt_box_width + 4)));
clamped_y = min(mouse_y, (tlt_max_y - (tlt_box_height + 4)));

draw_set_font(fnt_default);
draw_set_colour(c_gray);
draw_rectangle(clamped_x, clamped_y, clamped_x + tlt_box_width, clamped_y + tlt_box_height, 0);
draw_set_colour(c_black);
draw_rectangle(clamped_x, clamped_y, clamped_x + tlt_box_width, clamped_y + tlt_box_height, 1);

draw_set_halign(fa_left);
draw_set_valign(fa_top);
draw_text_ext
(
    clamped_x + tlt_padding,
    clamped_y + tlt_padding,
    tlt_text,
    tlt_font_size + (tlt_font_size/2),
    tlt_maxlength        
);



draw_set_alpha(1);
