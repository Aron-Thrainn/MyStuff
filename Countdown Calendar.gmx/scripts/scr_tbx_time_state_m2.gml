//Argument 0 = input string (9:32)
var input = argument0;
var state = 0;
input = string_replace(input,"-",":");
input = string_replace(input," ",":");
for(i=1; i < string_length(input)+1; i++) //Loopar þangað til að strengurinn er búinn(NULL terminated)
{
    state = scr_tbx_time_state_ext(string_copy(input,i,1), state); //Kalla á fall sem vinnur í gegnum state skrefin
    if (state == 11){   //Hættir forloopuna snemma ef state er error state
        m2_ipv_hour = 0;
        m2_ipv_minute = 0;
        return 0;
    }
}
if (state == 1){ //0x:00
    m2_ipv_hour = string_copy(input,1,1);
    m2_ipv_minute = 0;
    m2_ipv_time = "0" + m2_ipv_hour + ":00";
    return 1;
}
if (state == 2){ //xx:00
    m2_ipv_hour = string_copy(input,1,2);
    m2_ipv_minute = 0;
    m2_ipv_time = m2_ipv_hour + ":00";
    return 1;    
}
if (state == 3 || state == 10){ //0x:xx
    m2_ipv_hour = string_copy(input,1,1);
    m2_ipv_minute = string_copy(input,3,2);
    m2_ipv_time = "0" + m2_ipv_hour + ":" + m2_ipv_minute;
    return 1;    
}
if (state == 4 || state == 7){ //xx:xx
    m2_ipv_hour = string_copy(input,1,2);
    m2_ipv_minute = string_copy(input,4,2);
    m2_ipv_time = m2_ipv_hour + ":" + m2_ipv_minute;
    return 1;    
}
if (state == 6){ //xx:0x
    m2_ipv_hour = string_copy(input,1,2);
    m2_ipv_minute = string_copy(input,4,1);
    m2_ipv_time = m2_ipv_hour + ":0" + m2_ipv_minute;
    return 1;    
}
if (state == 9){ //0x:0x
    m2_ipv_hour = string_copy(input,1,1);
    m2_ipv_minute = string_copy(input,3,1);
    m2_ipv_time = "0" + m2_ipv_hour + ":0" + m2_ipv_minute;
    return 1;    
}

m2_ipv_hour = 0;
m2_ipv_minute = 0;
return 0; //else error


