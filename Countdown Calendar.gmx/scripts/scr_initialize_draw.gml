
draw_set_colour(c_black);
draw_text(10,10,"Current Time");
draw_text(10,30,string(c_year) + " " + string(scr_call_daysinthemonth(0,c_month)));
draw_text(10,50,string(c_day) + " " + string(c_hour) + ":" + string(c_minute) + ":" + string(c_second));
