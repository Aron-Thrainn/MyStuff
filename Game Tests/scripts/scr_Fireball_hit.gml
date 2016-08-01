//argument0 = guaranteed crit
//argument1 = target id
var f_crit = ui_fireball.crit;
if argument0  f_crit = 100;
var f_crit_bonus = ui_fireball.crit_bonus;
var f_damage = ui_fireball.damage;
var f_target = argument1;



scr_deal_hit(f_damage,f_crit,f_crit_bonus,2,f_target,player, ui_fireball);
//scr_enm_hit(damage,crit,crit_mul,true);
