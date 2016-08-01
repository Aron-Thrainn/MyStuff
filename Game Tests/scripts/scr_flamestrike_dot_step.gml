if debuff_tick >= tick_rate
{
    scr_flamestrike_dot_tick();
    debuff_tick = 0;
}
if debuff_timer >= duration
{
    scr_flamestrike_dot_remove();
}

debuff_timer += 1;
debuff_tick += 1;
