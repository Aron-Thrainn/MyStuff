if channel != noone
{
    var c1 = casttime_flamestrike * room_speed;
    switch (flamestrike_timer)
    {
        case c1: 
        {
            //spell cast
            with channel scr_flamestrike_impact_ini();
            channel.x = flamestrike_x;
            channel.y = flamestrike_y;
            if powerup02 == 1{  instance_create(flamestrike_x,flamestrike_y,obj_lingeringstrike)}
            
            //player variables
            scr_pl_channel_cleanup_flamestrike();
            break;
        }
    }
    
    flamestrike_timer += 1;
    
    if mouse_right && channel != noone
    {
        with channel instance_destroy();
        scr_pl_channel_cleanup_flamestrike();
    }
    return(0);
}
show_message("Error, no cast object found")
