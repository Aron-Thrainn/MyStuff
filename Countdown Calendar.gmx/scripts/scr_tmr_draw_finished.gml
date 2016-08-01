//Draw self & all the numbers
draw_self();


if hover
{
    if box_alpha > alpha_min
    {
        box_alpha -= alpha_speed;
    }
    else box_alpha = alpha_min;
}
else
{
    if box_alpha < alpha_max
    {
        box_alpha += alpha_speed;
    }
    else box_alpha = alpha_max;
}

//Draw sprite
draw_sprite_ext(spr_tmr_colour,sub,x_offset,y_offset,1,1,0,c_white,box_alpha);
//Draw name
draw_text_color(x_offset+5,y_offset+5,timer_name,c_black,c_black,c_black,c_black,box_alpha);
//Draw Text
draw_text_color(x_offset+5, y_offset+45, "Finished!",c_black,c_black,c_black,c_black,box_alpha)
//Draw target date
draw_text_color(x_offset+5,y_offset+25,"Countdown to: " + string(target_year) + " " + string(scr_call_daysinthemonth(0,target_month))
 + " " + string(target_day) + " " + timer_time,c_black,c_black,c_black,c_black,box_alpha)
