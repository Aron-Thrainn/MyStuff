//argument0 = heal if + / damage if -
//argument1 = crit (1/0)
//argument2 = crit multiplier
//argument3 = spell (1/0)
var damage = argument0;
var crit = argument1
var crit_mul = argument2
damage = damage * mul_damage;

if crit
{
    damage = round(damage * crit_mul);
    damage = round(damage * mul_crit);
}
if shield
{
    shield = ceil(shield + damage); //ciel instead of round becouse of shield decay
    if shield < 0
    {
        hp = round(hp + shield);
        shield = 0;
    } 
    with hpbar  scr_hpbar_change_shield(other.shield)
}
else hp = round(hp + damage);


if (hp > max_hp)  hp = max_hp;
scr_enm_deathcheck();
with hpbar  scr_hpbar_change_hp(other.hp);
//scr_combat_text_spawn(damage, argument1, 0,argument3);
scr_decay_delay();

debug = argument3;
//show_debug_message("hit: " + string(damage) + " | crit: " + string(argument1)
//+ " | hp: " + string(hp)+ " | id: " + string(id));
