//hpbar
var x1,x2,y1,y2;

x1 = round(x-x_offset);
x2 = round(x+x_offset);
y1 = round(y-y_offset-hpbar_height);
y2 = round(y-y_offset);


//draw health
draw_set_alpha(missing_hp_alpha);
draw_rectangle_colour(x1,y1,x2,y2,c_miss,c_miss,c_miss,c_miss,0);

draw_set_alpha(def_alpha);
draw_rectangle_colour(x1,y1,x1 +(width * aa)-1,y2,c_hp,c_hp,c_hp,c_hp,0);
draw_rectangle_colour(x1 + (width * aa),y1,x1 + (width * (aa+bb))-1,y2,c_dot,c_dot,c_dot,c_dot,0);
//draw_rectangle_colour(x1,y1,x2,y2,c_hot,c_hot,c_hot,c_hot,0);

draw_rectangle_colour(x1 + (width * (aa+bb+cc)),y1,x1 +(width * (aa+bb+cc+dd))-1,y2,c_shield,c_shield,c_shield,c_shield,0);

//draw frame
draw_rectangle(x1-1,y1-1,x2,y2, 1);

//Draw Beats
var beat_width = width / (max_hp + shield);
display_size = (aa + bb + cc + dd) * (max_hp + shield);
for (i = 0; i <= (display_size); i += (hp_beat))
{
    draw_line(x1 + (i * beat_width)-1, y1-1,x1 + (i * beat_width)-1, y2);
    if i mod hp_measure == 0 && i != 0 && i != display_size
    {
        draw_line(x1 + (i * beat_width)-2, y1-1,x1 + (i * beat_width)-2, y2);
        draw_line(x1 + (i * beat_width), y1-1,x1 + (i * beat_width), y2);
    }
}


