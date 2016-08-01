if channel != noone
{
    cooldown = def_cd;
    var c1 = casttime;
    switch (timer)
    {
        case c1: 
        {
            //spell cast
            with channel scr_flamestrike_impact_ini();
            channel.x = flamestrike_x;
            channel.y = flamestrike_y;
            if powerup02 == 1{  instance_create(flamestrike_x,flamestrike_y,obj_lingeringstrike)}
            
            //player variables
            scr_spell_flamestrike_channel_cleanup();
            break;
        }
    }
    
    timer += 1;
    
    if player.mouse_right && channel != noone
    {
        with channel instance_destroy();
        scr_spell_flamestrike_channel_cleanup();
    }
    return(0);
}
show_message("Error, no cast object found")
