if cooldown > 0
{
    cooldown -= player.cooldown;
    return 0;
}
else 
{
    cooldown = 0;
    return 1;
}
