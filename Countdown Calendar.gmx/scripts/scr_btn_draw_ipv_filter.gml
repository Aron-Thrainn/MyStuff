draw_self();

if hover
{
    if icon_alpha > alpha_min
    {
        icon_alpha -= alpha_speed;
    }
    else icon_alpha = alpha_min;
}
else
{
    if icon_alpha < alpha_max
    {
        icon_alpha += alpha_speed;
    }
    else icon_alpha = alpha_max;
}
draw_sprite_ext(spr_btn_icons,sub,x_offset,y_offset,1,1,0,c_white,icon_alpha);

//display the number of timers in each catagory
draw_set_colour(c_black);


var x_off, y_off;
x_off = x_offset + filter_off_x;
y_off = y_offset + filter_off_y;

switch(btn_name)
{
    case "btn_filter_blue":  draw_text_color(x_off,y_off,string(count_blue),c_black,c_black,c_black,c_black,icon_alpha);   break;
    case "btn_filter_green":  draw_text_color(x_off,y_off,string(count_green),c_black,c_black,c_black,c_black,icon_alpha);   break;
    case "btn_filter_red":  draw_text_color(x_off,y_off,string(count_red),c_black,c_black,c_black,c_black,icon_alpha);   break;
    case "btn_filter_orange":  draw_text_color(x_off,y_off,string(count_orange),c_black,c_black,c_black,c_black,icon_alpha);   break;
    case "btn_filter_pink":  draw_text_color(x_off,y_off,string(count_pink),c_black,c_black,c_black,c_black,icon_alpha);   break;
    case "btn_filter_yellow":  draw_text_color(x_off,y_off,string(count_yellow),c_black,c_black,c_black,c_black,icon_alpha);   break;
    case "btn_filter_teal":  draw_text_color(x_off,y_off,string(count_teal),c_black,c_black,c_black,c_black,icon_alpha);   break;
    case "btn_filter_purple":  draw_text_color(x_off,y_off,string(count_purple),c_black,c_black,c_black,c_black,icon_alpha);   break;
}
