//argument0 = healing / damage / shield
//argument1 = crit chance (0-100)
//argument2 = crit bonus
//argument3 = 1 = physical | 2 = magic | 3 = heal | 4 = shield
//argument4 = caster id
//argument5 = spell id
var f_damage = argument0;
var f_crit = scr_dice_roll(argument1);
var f_crit_bonus = argument2;
var f_type = argument3;
var f_caster = argument4;
var f_spell = argument5;

switch(f_type)
{
    case 1:         //physical
    {
        f_crit_bonus = f_crit_bonus * crit_armor;
        f_damage = f_damage * armor;
        f_damage = f_damage + (f_damage * f_crit_bonus * f_crit);
        scr_take_hit_sub(f_damage);
        break;
    }
    case 2:         //magic
    {
        f_crit_bonus = f_crit_bonus * crit_resist;
        f_damage = f_damage * resist;
        f_damage = f_damage + (f_damage * f_crit_bonus * f_crit);
        scr_take_hit_sub(f_damage);
        break;
    }
    case 3:         //heal
    {
        f_crit_bonus = f_crit_bonus * crit_heal_amplifier;
        f_damage = f_damage * heal_amplifier;
        f_damage = f_damage + (f_damage * f_crit_bonus * f_crit);
        hp += f_damage;
        break;
    }
    case 4:         //shield
    {
        f_crit_bonus = f_crit_bonus * crit_heal_amplifier;
        f_damage = f_damage * heal_amplifier;        
        f_damage = f_damage + (f_damage * f_crit_bonus * f_crit);
        shield += f_damage;
        break;
    }
}

if (hp > max_hp)  hp = max_hp;
if (hpbar != noone && f_type == 4) with hpbar  scr_hpbar_change_shield(other.shield);
if (hpbar != noone && f_type != 4) with hpbar  scr_hpbar_change_hp(other.hp);

scr_combat_text_spawn(f_damage, f_crit, f_type);
scr_decay_delay();
scr_counter_add(f_damage, f_spell);

scr_enm_deathcheck();
//show_debug_message("hit: " + string(f_damage) + " | crit: " + string(f_crit)
//+ " | hp: " + string(hp)+ " | id: " + string(id));



