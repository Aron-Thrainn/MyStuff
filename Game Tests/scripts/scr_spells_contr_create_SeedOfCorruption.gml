scr_spells_contr_default_vars_SingleTarget();

owner = noone; //set from creator
sp_missle_speed = 240 / game_speed;


missle = obj_spell_SeedOfCorruption;

var f_casteffect_1 = scr_spell_casteffect_spawn_CreateMissle(missle);
var f_casteffect_2 = scr_spell_casteffect_spawn_SetCooldown(4);

scr_spells_func_AddCastEffect(f_casteffect_1);
scr_spells_func_AddCastEffect(f_casteffect_2);
