//argument0 = damage
//argument1 = duration
//argument2 = target
//argument3 = caster
//argument4 = controller (from caster)


var f_SpellSword = instance_create(0,0,obj_buff_SpellSword);
with f_SpellSword
{
    duration = argument1;
    target = argument2;
    caster = argument3;
    controller = argument4;
    
    var f_trigger_basicattack = scr_ste_trigger_spawn_BasicAttack();
    var f_effect_directdamage = scr_ste_event_spawn_DirectDamage(argument0);
    
    scr_ste_AddTrigger(f_trigger_basicattack, f_effect_directdamage);
}

return f_SpellSword;
