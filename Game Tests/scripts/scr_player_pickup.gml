temppickup = collision_point(x,y, obj_loot_01,true,true)

if temppickup != noone
{
    switch(temppickup.loot_name)
    {
        case "powerup01": //{scr_spell_create_fireball(1);   break;}
        case "powerup02": //{scr_spell_create_flamestrike(2);   break;}
        case "powerup03": //{scr_spell_create_wailing(4);   break;}
    }
    with temppickup{   instance_destroy();}
}
