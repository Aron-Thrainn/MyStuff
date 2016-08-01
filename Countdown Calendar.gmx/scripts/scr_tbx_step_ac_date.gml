keyboard_string = string_digits(keyboard_string);
if string_width(keyboard_string) < max_width
{
    tbx_text = keyboard_string;
    
    switch(tbx_name)
    {
        case "tbn_year": ipv_year = tbx_text;       break;
        case "tbn_month": ipv_month = tbx_text;       break;
        case "tbn_day": ipv_day = tbx_text;       break;
    
        
        case "m2_tbn_year": m2_ipv_year = tbx_text;       break;
        case "m2_tbn_month": m2_ipv_month = tbx_text;       break;
        case "m2_tbn_day": m2_ipv_day = tbx_text;       break;
    }
}
keyboard_string = tbx_text;
