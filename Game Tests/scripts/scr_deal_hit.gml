
//argument0 = heal if + / damage if -
//argument1 = crit chance (0-100)
//argument2 = crit bonus
//argument3 = 1 = basic attack | 2 = spell | 3 = heal | 4 = shield
//argument4 = target id
//argument5 = caster id
//argument6 = spell id
var f_damage = argument0;
var f_crit = argument1;
var f_crit_bonus = argument2;
var f_type = argument3;
var f_target = argument4;
var f_caster = argument5;
var f_spell = argument6;


switch(f_type)
{
    case 1:         //physical
    {
        f_crit_bonus = f_crit_bonus * f_caster.crit_physical_bonus;
        f_damage = f_damage * f_caster.physical_bonus;
        break;
    }
    case 2:         //magic
    {
        f_crit_bonus = f_crit_bonus * f_caster.crit_magic_bonus;
        f_damage = f_damage * f_caster.magic_bonus;
        break;
    }
    case 3:         //heal
    {
        f_crit_bonus = f_crit_bonus * f_caster.crit_heal_bonus;
        f_damage = f_damage * f_caster.heal_bonus;
        break;
    }
    case 4:         //shield
    {
        f_crit_bonus = f_crit_bonus * f_caster.crit_heal_bonus;
        f_damage = f_damage * f_caster.heal_bonus;        
        break;
    }
}

with f_target  scr_take_hit(f_damage, f_crit, f_crit_bonus, f_type, f_caster, f_spell);


