
if channel != noone
{
    cooldown = def_cd;
    with channel
    {
        scr_pl_channel_fireball_cast_location();
    }
    
    //Cast time milestones
    switch (timer)
    {
        case 0:  channel.image_index = 0; break;
        case c1: channel.image_index = 1; break;
        case c2: channel.image_index = 2; break;
        case c3: channel.image_index = 3; break;
        case c4:
        {
            //fizzle spell
            with channel    instance_destroy();
            
            scr_spell_fireball_channel_cleanup(1);
            break;            
        }        
    }
    
    
    if player.key_ability[slot] && timer > casting_delay
    {
        //impart variables
        channel.hits = channel.image_index + 1;
        with channel  scr_fireball_inmotion_ini();
        //channel.image_index = temppower;
        
        scr_spell_fireball_channel_cleanup(0);
    }
    timer += (1 + powerup01);
    
    return(0);
}
show_message("Error, no spell cast object found")


