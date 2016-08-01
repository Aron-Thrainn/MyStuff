//Argument 0 = input string (9:32)
var input = argument0;
var state = 0;
input = string_replace(input,"-",":");
input = string_replace(input," ",":");
for(i=1; i < string_length(input)+1; i++) //Loops until the string is done(NULL terminated)
{
    state = scr_tbx_time_state_ext(string_copy(input,i,1), state); //Calls a function that runs through the state machine
    if (state == 11){   //Ends early if it goes to the error state
        ipv_hour = 0;
        ipv_minute = 0;
        return 0;
    }
}
if (state == 1){ //0x:00
    ipv_hour = string_copy(input,1,1);
    ipv_minute = 0;
    ipv_time = "0" + ipv_hour + ":00";
    return 1;
}
if (state == 2){ //xx:00
    ipv_hour = string_copy(input,1,2);
    ipv_minute = 0;
    ipv_time = ipv_hour + ":00";
    return 1;    
}
if (state == 3 || state == 10){ //0x:xx
    ipv_hour = string_copy(input,1,1);
    ipv_minute = string_copy(input,3,2);
    ipv_time = "0" + ipv_hour + ":" + ipv_minute;
    return 1;    
}
if (state == 4 || state == 7){ //xx:xx
    ipv_hour = string_copy(input,1,2);
    ipv_minute = string_copy(input,4,2);
    ipv_time = ipv_hour + ":" + ipv_minute;
    return 1;    
}
if (state == 6){ //xx:0x
    ipv_hour = string_copy(input,1,2);
    ipv_minute = string_copy(input,4,1);
    ipv_time = ipv_hour + ":0" + ipv_minute;
    return 1;    
}
if (state == 9){ //0x:0x
    ipv_hour = string_copy(input,1,1);
    ipv_minute = string_copy(input,3,1);
    ipv_time = "0" + ipv_hour + ":0" + ipv_minute;
    return 1;    
}

ipv_hour = 0;
ipv_minute = 0;
return 0; //else error


