



var pointer = "";
//animate pointer when active
animate_tick += 1;
if animate_tick < 90     pointer = "|";
else if animate_tick == 90   pointer = "";
else if animate_tick > 179   animate_tick = 0;

//Draw box & outline
var a = active_colour;
draw_self();
draw_rectangle_colour(x,y,x+box_width-1,y+box_height-1,a,a,a,a,true);
//Draw text
draw_text(x+2,y+1,tbx_text + pointer);


//Draw inputbox title
draw_text(x,y-20,tbx_title);
