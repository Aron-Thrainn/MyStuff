if player.key_ability[slot] && timer > 0.2 * room_speed
{
    with channel  scr_wailingarrow_impact_ini();
    scr_spell_wa_channel_cleanup();
}
cooldown = def_cd;
timer += 1;
