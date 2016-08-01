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


//temporary variables
var w_year, w_month, w_day, w_hour, w_minute;
w_year = "";
w_month = "";
w_day = "";
w_hour = "";
w_minute = "";

//Draw self & all the numbers


//variables for plurals
if real_year == 1 plurals_year = " year ";
else plurals_year = " years ";
if real_month == 1 plurals_month = " month ";
else plurals_month = " months ";
if real_day == 1 plurals_day = " day ";
else plurals_day = " days ";

//do not display redundant text
if done_year == false {w_year = (string(real_year) + string(plurals_year))};
if done_month == false {w_month = (string(real_month) + string(plurals_month))};
if done_day == false {w_day = (string(real_day) + string(plurals_day))};
if done_hour == false {w_hour = (string(real_hour) + ":")};
if done_minute == false {w_minute = (string(real_minute) + ":")};

draw_set_colour(c_black);

//Draw sprite
draw_sprite_ext(spr_tmr_colour,sub,x_offset,y_offset,1,1,0,c_white,box_alpha);
//Draw name
draw_text_color(x_offset+5,y_offset+5,timer_name,c_black,c_black,c_black,c_black,box_alpha);
//Draw countdown
draw_text_color(x_offset+5,y_offset+45, (w_year + w_month + w_day + w_hour + w_minute + string(real_second))
,c_black,c_black,c_black,c_black,box_alpha);
//Draw target date
draw_text_color(x_offset+5,y_offset+25,"Countdown to: " + string(target_year) + " " + string(scr_call_daysinthemonth(0,target_month))
 + " " + string(target_day) + " " + timer_time,c_black,c_black,c_black,c_black,box_alpha);
 

//Draw annual icon
if annual
{
    draw_sprite(spr_btn_icons,13,x+410,y+40);
}
 
 
 
 
 
 
 
 
 
 
 
 
