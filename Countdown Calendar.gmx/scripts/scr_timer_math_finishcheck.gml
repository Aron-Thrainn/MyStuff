
scr_timer_math_removenegatives();

if !countdown_year    {done_year = true;    countdown_year = 0;}
if !countdown_month && done_year   {done_month = true;   countdown_month = 0;}
if !countdown_day && done_month   {done_day = true;    countdown_day = 0;}
if !countdown_hour && done_day   {done_hour = true;    countdown_hour = 0;}
if !countdown_minute && done_hour   {done_minute = true;     countdown_minute = 0;}
if !countdown_second && done_minute   {done_second = true;     countdown_second = 0;    state = tmr_state.finished;}

