//argument0 = spell id

with pnt_enemy
{
    if !dead && place_meeting(x,y,argument0)
    {
        if !scr_blacklist_check(argument0, id)
        {
            return id;
        }
    }
}

return noone;

