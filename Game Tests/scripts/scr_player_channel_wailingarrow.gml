if wailingarrow = noone
{
    wailingarrow = instance_create(x,y,obj_wailingarrow);
    with wailingarrow
    {
        direction = obj_player.image_angle;
        image_angle = direction;
    }
    return 0;
}

if wailingarrow != noone
{
    switch(wailingarrow.state)
    {
        case Spell_state.idle: scr_pl_channel_wa_idle(); break;
        case Spell_state.inmotion: scr_pl_channel_wa_inmotion(); break;
        //Impact step called from object itself
    }
    return 0;
}
show_message("Error")
