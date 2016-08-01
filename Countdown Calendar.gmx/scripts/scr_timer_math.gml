frame_count += 1;
if frame_count >= 60
{
    frame_count = 0;
    scr_timer_math_finishcheck();

    
    if !done_second{     countdown_second -= 1;}
    if countdown_second < 0
    {
        if !done_minute{     countdown_minute -= 1;}
        countdown_second = 59;
        if countdown_minute < 0
        {
            
            if !done_hour{     countdown_hour -= 1;}
            countdown_minute = 59;
            if countdown_hour < 0
            {
                if !done_day{     countdown_day -= 1;}
                countdown_hour = 23;
                if countdown_day < 0
                {
                    if !done_month{     countdown_month -= 1;}
                    countdown_day = (scr_call_daysinthemonth(c_month,0) - 1);
                    if countdown_month < 0
                    {
                        if !done_year{     countdown_year -= 1;}
                        countdown_month = 11;
                    }
                }
            }
        }
    }
}

real_year = countdown_year;
real_month = countdown_month;
real_day = countdown_day;
real_hour = countdown_hour;
real_minute = countdown_minute;
real_second = countdown_second;
