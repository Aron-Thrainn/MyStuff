frame_count = 0;

countdown_year = target_year - c_year;
countdown_month = target_month - c_month;
countdown_day = target_day - c_day;
countdown_hour = target_hour - c_hour;
countdown_minute = target_minute - c_minute;
countdown_second = target_second - c_second;


scr_timer_event_sync_finishcheck();

scr_timer_math_removenegatives(); // needed before and after, else the minus can be a plus
if countdown_second != 0 && !done_minute && target_second < c_second     countdown_minute -= 1;
if countdown_minute != 0 && !done_hour && target_minute < c_minute     countdown_hour -= 1;
if countdown_hour != 0 && !done_day && target_hour < c_hour     countdown_day -= 1;
if countdown_day != 0 && !done_month && target_day < c_day     countdown_month -= 1;
if countdown_month != 0 && !done_year && target_month < c_month      countdown_year -= 1;
scr_timer_math_removenegatives();

