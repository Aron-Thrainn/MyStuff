//Draw box & outline
var a = idle_colour;
draw_self();
draw_rectangle_colour(x,y,x+box_width-1,y+box_height-1,a,a,a,a,true);
//Draw text
draw_text(x+2,y+1,tbx_text);
//Draw inputbox title
draw_text(x,y-20,tbx_title);
