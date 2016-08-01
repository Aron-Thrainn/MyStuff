//argument0 = target id
var f_target = argument0;
var f_crit = ui_flamestrike.def_crit;
var f_crit_bonus = ui_flamestrike.def_crit_bonus;
var f_damage = ui_flamestrike.def_dmg;



scr_flamestrike_dot_apply(f_target);
//scr_enm_hit(damage,crit,crit_mul,true);
scr_deal_hit(f_damage, f_crit, f_crit_bonus, 2, f_target, player, ui_flamestrike);
