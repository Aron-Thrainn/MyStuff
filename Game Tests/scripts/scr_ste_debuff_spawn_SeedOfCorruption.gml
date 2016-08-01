//argument0 = damage
//argument1 = duration
//argument2 = target
//argument3 = caster
//argument4 = controller (from caster)

//    ToDO: finish this
var f_SeedOfCorruption = instance_create(0,0,obj_debuff_SeedOfCorruption);
with f_SeedOfCorruption
{
    duration = argument1;
    target = argument2;
    caster = argument3;
    controller = argument4;
    
    spelleffect_SeedSplosion = scr_spells_spawn_SeedSplosion;
    
    //var f_trigger_tick = scr_ste_trigger_spawn_Tick();
    //var f_event_damage = scr_ste_event_spawn_DirectDamage(argument0);
    
    var f_trigger_deathrattle = scr_ste_trigger_spawn_Deathrattle();
    var f_event_spelleffect = scr_ste_event_spawn_SpellEffect(spelleffect_SeedSplosion);
    
    //scr_ste_AddTrigger(f_trigger_tick, f_event_damage);
    scr_ste_AddTrigger(f_trigger_deathrattle, f_event_spelleffect);
}

return f_SeedOfCorruption;
