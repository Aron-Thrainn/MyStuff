scr_spells_contr_default_vars_AoE();

owner = noone; //set from creator
sp_radius = 12;
sp_damage = 6;


missle = obj_spell_SeedSplosion;

var f_casteffect_1 = scr_spell_casteffect_spawn_CreateMissle(missle);

scr_spells_func_AddCastEffect(f_casteffect_1);
