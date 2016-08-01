state = tbx_state.idle;
pointer = "";
if tbx_name == "tbn_time"
{
    //time of day fromat script
    var valid = scr_tbx_time_state(tbx_text);
    if valid
    {
        //show_debug_message("Safe: " + ipv_time + " | " + string(ipv_hour) + ":" + string(ipv_minute));
    }
    else
    {
        //show_debug_message("Error: " + ipv_time);
    }
}
if tbx_name == "m2_tbn_time" 
{
    //time of day fromat script 2
    var valid = scr_tbx_time_state_m2(tbx_text);
    if valid
    {
        //show_debug_message("m2 Safe: " + m2_ipv_time);
    }
    else
    {
        //show_debug_message("m2 Error: " + m2_ipv_time);
    }
}
