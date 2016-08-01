if casting
{
    scr_spell_charge_channel();
}
if scr_spell_cooldown_tick()
{
    sprite_index = spr_ui_charge;
}
