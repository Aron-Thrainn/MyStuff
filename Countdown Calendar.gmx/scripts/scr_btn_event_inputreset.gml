
var temp_tomorrow,temp_daycheck, temp_tomorrowmonth, temp_tomorrowyear;
temp_daycheck = scr_call_daysinthemonth(current_month,0);
temp_tomorrow = c_day + 1;
temp_tomorrowmonth = c_month;
temp_tomorrowyear = c_year;

if temp_tomorrow > temp_daycheck    {temp_tomorrow = 1;     temp_tomorrowmonth += 1;}
if temp_tomorrowmonth > 12    {temp_tomorrowmonth = 1;     temp_tomorrowyear += 1;}



with obj_textbox
{
    scr_tbx_step_trans2();
    
    if tbx_name == "tbn_name"     tbx_text = "Insert Name";
    if tbx_name == "tbn_description"{    tbx_text = "";   }
    if tbx_name == "tbn_time"    tbx_text = "0:00"
    if type == tbx_type.date
    {
        switch (tbx_name)
        {
            case "tbn_year": tbx_text = string(temp_tomorrowyear);       break;
            case "tbn_month": tbx_text = string(temp_tomorrowmonth);       break;
            case "tbn_day": tbx_text = string(temp_tomorrow);       break;
            case "tbn_hour": tbx_text = "0";       break;
            case "tbn_minute": tbx_text = "0";       break;
        }
    }
}
ipv_tooltip = "";
ipv_annual = 0;

ipv_colour = 0;
with obj_button
{
    if type == btn_type.ipv_colour
    {
        state = btn_state.idle;
    }
    if btn_name == "btn_colour_blue"        state = btn_state.toggled;
    if type == btn_type.ipv_annual
    {
        state = btn_state.idle;
    }
}
scr_call_inputs();
