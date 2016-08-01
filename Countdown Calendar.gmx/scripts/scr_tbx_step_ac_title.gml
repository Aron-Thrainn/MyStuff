if string_width(keyboard_string) < max_width
{   
    tbx_text = keyboard_string;
    switch(tbx_name)
    {
        case "tbn_name": ipv_name = tbx_text;       break;
        case "m2_tbn_name": m2_ipv_name = tbx_text;       break;
    }
}
keyboard_string = tbx_text;
