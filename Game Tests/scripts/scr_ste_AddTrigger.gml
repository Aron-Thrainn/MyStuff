//argument0 = trigger obj
//argument1 = effect obj

// if trigger already exists, add the effect onto it

var f_trigger_enum = scr_ste_TriggerSwitch(argument0);
if f_trigger_enum != Trigger.Error {
    if trigger[f_trigger_enum] == noone {
        trigger[f_trigger_enum] = argument0;
    }
    else if trigger[f_trigger_enum] != argument0 {
        show_debug_message("Error-AddTrigger: tried to add multiple objects as trigger");
    }
    
    with trigger[f_trigger_enum] {
        scr_ste_AddEvent(argument1);
    }
}
else
{
    show_debug_message("Error: scr_steAddTrigger - trigger enum script returned error");
}
