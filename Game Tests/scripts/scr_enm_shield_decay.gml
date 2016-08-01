if hit_timer > 0  hit_timer -= 1;
else if shield > 0
{
    decay_timer++;
    if decay_timer mod game_speed == 0
    {
        shield -= ceil(shield * shield_decay_percent);
        with hpbar  scr_hpbar_change_shield(other.shield);
        decay_timer = 0;
    }
}

