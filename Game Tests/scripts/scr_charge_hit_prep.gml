//argument0 = spell id
//argument1 = first hit

with pnt_enemy
{
    if !dead && place_meeting(x,y,argument0)
    {
        nohit = 0;
        for (i=0; i<other.blacklist_size; i++)
        {
            if other.blacklist[i] == id    nohit = 1;
        }
        if !nohit
        {
            with argument0  scr_charge_hit(argument1, other.id); //argument0 means guaranteed crit    
            for (i=0; i<other.blacklist_size; i++)
            {
                if other.blacklist[i] == 0
                {
                    other.blacklist[i] = id;
                    if i == (other.blacklist_size - 1)
                    {
                        other.blacklist[other.blacklist_size] = 0;
                        other.blacklist_size += 1;
                    }
                    break;
                }
            }
        }
    }
}
