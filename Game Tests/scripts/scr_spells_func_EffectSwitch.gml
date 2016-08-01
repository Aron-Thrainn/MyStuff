//argument0 = effect obj id


switch(argument0.object_index)
{
    case obj_spell_effect_Damage : scr_spell_effect_activate_Damage();  break;
    case obj_spell_effect_Bounce : scr_spell_effect_activate_Bounce();  break;
    case obj_spell_effect_DestroySelf : scr_spell_effect_activate_DestroySelf();  break;
}


