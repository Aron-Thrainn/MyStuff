var temp_array, error, error_message, temp_day;
error = 0;

error_message = "Error#";
e_name = "#Please insert a name.";
e_year = "#Please insert a valid year.";
e_month = "#Please insert a valid month.(1,2,3...11,12)";
e_day = "#Please insert a valid day.(1,2,3...29,30)";
e_hour = "#Please insert a valid hour.(1,2,3...22,23)";
e_minute = "#Please insert a valid minute.(1,2,3...58,59)";
e_time = "#please use a valid input for the time."

for (i=0;i<=6;i+=1)    temp_array[i] = 0; // 0 = error, 1 = no error

if string_length(string_digits(ipv_time)) > 0 && string_length(string_digits(ipv_time)) < 5    
{
    temp_array[0] = 1;
    {
        with obj_textbox   if tbx_name == "tbn_time"
        {
            if !scr_tbx_time_state(tbx_text)
            {
                temp_array[0] = 0;
            }
        }    //time of day fromat script
    }
}



if ipv_name != ""   temp_array[1] = 1;

if ipv_year != ""   temp_array[2] = 1;
if real(ipv_month) > 0 && real(ipv_month) < 13   temp_array[3] = 1;
if real(ipv_day) > 0   && real(ipv_day) < (scr_call_daysinthemonth(real(ipv_month),0) + 1)    temp_array[4] = 1;
if real(ipv_hour) >= 0   && real(ipv_hour) < 24    temp_array[5] = 1;
if real(ipv_minute) >= 0   && real(ipv_minute) < 60    temp_array[6] = 1;

if temp_array[3] = 1
{
    e_day = "#Please insert a valid day.(1,2,3..."+string(scr_call_daysinthemonth(real(ipv_month),0))+")";
}

for (i=0;i<=6;i+=1)
{
    if temp_array[i] = 0        
    {
        error = 1;
        if i == 0    error_message += e_time;
        if i == 1    error_message += e_name; 
        if i == 2    error_message += e_year; 
        if i == 3    error_message += e_month; 
        if i == 4    {if temp_array[3] == 0{     if !real(ipv_day) > 0   or !real(ipv_day) < 32    error_message += e_day;} else if  temp_array[3] == 1{   error_message += e_day;}}
        if i == 5    {if temp_array[0] == 1     error_message += e_hour;}
        if i == 6    {if temp_array[0] == 1     error_message += e_minute;}
    }
}

if error == 1   { show_message(error_message); return(0);}
else return(1);
