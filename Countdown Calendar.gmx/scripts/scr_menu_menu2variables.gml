


//call the timer name and target date
with obj_textbox
{    
    switch (tbx_name)
    {
        case "m2_tbn_name": tbx_text = string(timer_target.timer_name);       m2_ipv_name = tbx_text;    break;
        case "m2_tbn_description": tbx_text = string(timer_target.tooltip);       m2_ipv_tooltip = tbx_text;    break;
        
        case "m2_tbn_time": tbx_text = string(timer_target.timer_time);       m2_ipv_time = tbx_text;    break;
        
        case "m2_tbn_year": tbx_text = string(timer_target.target_year);       m2_ipv_year = tbx_text;     break;
        case "m2_tbn_month": tbx_text = string(timer_target.target_month);       m2_ipv_month = tbx_text;      break;
        case "m2_tbn_day": tbx_text = string(timer_target.target_day);       m2_ipv_day = tbx_text;     break;    
    }
}

//call the timer colour
m2_ipv_colour = timer_target.sub;
m2_ipv_annual = annual;

//set the active colourselect button
var tempcolour;

with obj_button
{
    if type = btn_type.m2_ipv_colour
    {
        state = btn_state.idle    
    }
    if type = btn_type.m2_ipv_annual
    {
        if other.annual  state = btn_state.toggled;
        else state = btn_state.idle;
    }
}

switch(m2_ipv_colour)
{
    case 0: tempcolour = "m2_btn_colour_blue"; break;
    case 1: tempcolour = "m2_btn_colour_green"; break;
    case 2: tempcolour = "m2_btn_colour_red"; break;
    case 3: tempcolour = "m2_btn_colour_orange"; break;
    case 4: tempcolour = "m2_btn_colour_pink"; break;
    case 5: tempcolour = "m2_btn_colour_yellow"; break;
    case 6: tempcolour = "m2_btn_colour_teal"; break;
    case 7: tempcolour = "m2_btn_colour_purple"; break;
}
with obj_button
{
    if btn_name == tempcolour
    {
        pressed = 1;
        state = btn_state.toggled;
        break;
    }

}
