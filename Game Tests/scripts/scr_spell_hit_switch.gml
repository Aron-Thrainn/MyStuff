//argument0 = target id

f_spell_index = object_index;
switch(f_spell_index)
{
    case obj_basic_attack:
    {
        scr_basic_attack_hit(argument0);
        break;
    }
    case obj_fireball:
    {
        scr_Fireball_hit(explosion, argument0);
        break;
    }
    case obj_flamestrike:
    {
        scr_Flamestrike_hit(argument0);
        break;
    }
    case obj_charge:
    {
        scr_charge_hit(first_hit, argument0);
        break;
    }
    case obj_wailingarrow:
    {
        scr_wailingarrow_hit_shield(argument0);
        break;
    }
}
