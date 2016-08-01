//argument0 = damage
var f_damage = argument0;

if shield
{
    shield = ceil(shield - round(f_damage)); //ciel instead of round becouse of shield decay
    if shield < 0
    {
        hp = round(hp + shield);
        shield = 0;
    } 
    with hpbar  scr_hpbar_change_shield(other.shield);
}
else
{
    hp -= round(f_damage);
}

