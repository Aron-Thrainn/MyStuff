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


