if !done_second
{    
    if countdown_second < 0    countdown_second = 60 + countdown_second;
}
if !done_minute
{ 
    if countdown_minute < 0    countdown_minute = 60 + countdown_minute;
}
if !done_hour
{ 
    if countdown_hour < 0    countdown_hour = 24 + countdown_hour;

}
if !done_day
{ 
    if countdown_day < 0    countdown_day = (scr_call_daysinthemonth(c_month, 0))+ countdown_day;
    //if countdown_day > 0 && target_hour < c_hour{   countdown_day -= 1;}
}
if !done_month
{ 
    if countdown_month < 0    countdown_month = 12 + countdown_month;
    //if countdown_month > 0 && target_day < c_day{   countdown_month -= 1;}
}
if !done_year
{ 

}
