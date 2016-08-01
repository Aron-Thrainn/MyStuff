//argument0 = trigger obj

var f_trigger_enum;
switch(argument0.object_index)
{
    case obj_trigger_BasicAttack: f_trigger_enum = Trigger.BasicAttack;  break;
    case obj_trigger_Deathrattle: f_trigger_enum = Trigger.Deathrattle;  break;
    case obj_trigger_Tick: f_trigger_enum = Trigger.Tick;  break;
    
    default: f_trigger_enum = Trigger.Error;  break;
}


return f_trigger_enum;
