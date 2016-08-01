//argument0 = statuseffect (obj)
//Status Effects
for (var i=0; i<30; i+=1)
{
    if statuseffect[i] == noone{
        statuseffect[i] = argument0;
        show_debug_message("stage 1: Success");
        return 0;
    }
}

show_debug_message("Error-unit_add: Buff slots full");


