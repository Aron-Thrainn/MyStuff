//argument0 == spell id


with obj_enemy_01
{
    if !dead && place_meeting(x,y,argument0)
    {
        nohit = 0;
        for (i=0; i<other.blacklist_size; i++)
        {
            //show_debug_message("i: " + string(i) + "  |  " + string(other.blacklist[i]));
            if other.blacklist[i] == id
            {
                nohit = 1;
                break;
            }
    
        }
        if !nohit
        {
            with argument0  scr_Flamestrike_hit(other.id);    
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
