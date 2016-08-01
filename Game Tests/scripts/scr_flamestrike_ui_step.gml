if casting == 1
{
    scr_spell_flamestrike_range();
}
if casting == 2
{
    scr_spell_flamestrike_channel();
}
if scr_spell_cooldown_tick()
{
    sprite_index = spr_ui_flamestrike;
}
