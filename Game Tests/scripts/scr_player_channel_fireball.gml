
if channel != noone
{
    with channel
    {
        scr_pl_channel_fireball_cast_location();
    }
    var c1,c2,c3,c4;
    c1 = 0.5 * room_speed;
    c2 = 1 * room_speed;
    c3 = 1.5 * room_speed;
    c4 = 3 * room_speed;
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
            
            scr_pl_channel_cleanup_fireball();
            break;            
        }        
    }
    
    
    
    if key_ability_1 && timer > 0.1 * room_speed
    {
        //impart variables
        temppower = channel.image_index;
        with channel  scr_fireball_inmotion_ini();
        //channel.image_index = temppower;
        channel.hits = temppower + 1;
        
        scr_pl_channel_cleanup_fireball();
    }
    timer += (1 + powerup01);
    
    return(0);
}
show_message("Error, no spell cast object found")
