scr_spells_contr_default_vars_SkillShot();

owner = noone; //set from creator
sp_width = 12;
sp_max_targets = 1;
sp_damage = 8;
sp_missle_speed = 240 / game_speed;
sp_distance = 300;


missle = obj_spell_AvengersShield;

var f_casteffect_1 = scr_spell_casteffect_spawn_CreateMissle(missle);
var f_casteffect_2 = scr_spell_casteffect_spawn_SetCooldown(1);

scr_spells_func_AddCastEffect(f_casteffect_1);
scr_spells_func_AddCastEffect(f_casteffect_2);
