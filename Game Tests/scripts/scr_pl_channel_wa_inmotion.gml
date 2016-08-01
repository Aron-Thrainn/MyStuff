wailingarrow.timer += 1;
with wailingarrow scr_wailingarrow_inmotion();
if key_ability_4 && wailingarrow.timer > (0.15 * room_speed)
{
    with wailingarrow scr_wailingarrow_impact_ini();
    scr_pl_channel_cleanup_wailingarrow(0);
}
else if wailingarrow.timer >= (range_wailing / player.speed_wailing * room_speed)
{
    with wailingarrow scr_wailingarrow_impact_ini();
    scr_pl_channel_cleanup_wailingarrow(0);
}


