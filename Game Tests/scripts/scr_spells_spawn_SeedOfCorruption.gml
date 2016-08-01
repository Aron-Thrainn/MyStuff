// This script is to be called from Seed Of Corruption controller

var f_SeedOfCorruption = instance_create(x,y,obj_spell_SeedOfCorruption);
with f_SeedOfCorruption
{
    target = player.temp_target;
    
    caster = other.owner;
    controller = other.id;
    missle_speed = other.sp_missle_speed;
    
    //standard vars
    scr_spells_default_vars_SkillShot();
    
    debuff = scr_ste_debuff_spawn_SeedOfCorruption(1,15,target,caster,controller);
    
    var f_effect_1 = scr_spell_effect_spawn_ApplyStatusEffect(debuff);
    var f_effect_2 = scr_spell_effect_spawn_DestroySelf();
    
    scr_spells_func_AddEffect(f_effect_1);
    scr_spells_func_AddEffect(f_effect_2);
}

return f_SeedOfCorruption;
