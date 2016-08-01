if casting
{
    scr_spell_fireball_channel();
}
if scr_spell_cooldown_tick()
{
    sprite_index = spr_ui_fireball;
}
