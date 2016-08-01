//argument0 = spell id
//argument1 = aoe (1/0)    activates recursion
with pnt_enemy
{
    if !dead && place_meeting(x,y,argument0)
    {
        if !scr_blacklist_check(argument0, id)
        {
            scr_blacklist_add(argument0, id);
            with argument0  scr_spell_hit_switch(other.id);
            if !argument1  return 0;
        }
    }
}

