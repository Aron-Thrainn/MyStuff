with player
{
    scr_move_mini(other.x_move,other.y_move,false);
}
with channel
{
    x = player.x + (2*other.x_move);
    y = player.y + (2*other.y_move);
}
cooldown = def_cd;

if timer >= (duration)
{
    if scr_spell_charge_collision()
    {
        scr_spell_charge_channel_cleanup();
    }
}

timer += 1;
