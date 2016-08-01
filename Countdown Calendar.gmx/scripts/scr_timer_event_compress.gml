var compress,years,months,days,hours,minutes;
years = argument0.target_year;
months = argument0.target_month;
days = argument0.target_day;
hours = argument0.target_hour;
minutes = argument0.target_minute;

c_years = years - c_year;
c_months = months;
c_days = days;
c_hours = hours;
c_minutes = minutes;

compress = ((c_minutes) + (c_hours * 60) + (c_days * 1440) + (c_months * 44640) + (c_years * 535680));



/*
var inst_num, inst, temp;
temp = 0;
inst = noone;
inst_num = instance_number(obj_timer)

while inst_num > 0
{
    inst = instance_find(obj_timer, inst_num - 1);
    if inst != id && floor(inst.compress) == argument0.compress    temp += 0.01;
    inst_num -= 1;
}

show_debug_message("stage 2: " + string(compress));
argument0.compress += temp;
show_debug_message("stage 3: " + string(compress));
*/
return(compress);
