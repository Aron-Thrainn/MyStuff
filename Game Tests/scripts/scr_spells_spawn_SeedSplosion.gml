// This script is to be called from Seed Of Corruption controller

var f_SeedSplosion = instance_create(x,y,obj_spell_SeedSplosion);
with f_SeedSplosion
{
    
    caster = other.owner;
    controller = other.id;
    
    
    var f_effect_1 = scr_spell_effect_spawn_Damage();
    //var f_effect_2 = scr_spell_effect_spawn_Animate();
    var f_effect_3 = scr_spell_effect_spawn_DestroySelf();
    
    scr_spells_func_AddEffect(f_effect_1);
    scr_spells_func_AddEffect(f_effect_3);
}

return f_SeedSplosion;
