scr_spells_contr_default_vars();

owner = noone; //set from creator
target = caster;

buff = scr_ste_buff_SpellSword_spawn(1,15,target,caster,id);

var f_casteffect_1 = scr_spell_casteffect_spawn_ApplyStatusEffect(buff);
var f_casteffect_2 = scr_spell_casteffect_spawn_SetCooldown(1);

scr_spells_func_AddCastEffect(f_casteffect_1);
scr_spells_func_AddCastEffect(f_casteffect_2);
