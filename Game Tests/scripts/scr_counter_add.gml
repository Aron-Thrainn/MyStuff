//argument0 = value
//argument1 = variable to add to

with obj_counter
{
    total_damage += argument0;
    var f_spell = argument1.object_index;
    switch(f_spell)
    {
    case obj_spell_fireball:
        {
            fireball_damage += argument0;
            break;
        }
    case obj_spell_flamestrike:
        {
            flamestrike_damage += argument0;
            break;
        }
    case obj_debuff_flamestrike:
        {
            flamestrike_damage += argument0;
            break;
        }
    case obj_spell_wailingarrow:
        {
            wailing_shield += argument0;
            break;
        }
    case obj_spell_charge:
        {
            charge_damage += argument0;
            break;
        }
    case obj_player:
        {
            attack_damage += argument0;
            break;
        }
        default :
        {
            other_damage += argument0;
            break;
        }
    }
}
