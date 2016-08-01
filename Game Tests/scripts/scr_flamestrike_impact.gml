var delay = ui_flamestrike.delay;
var death_delay = delay + (ui_flamestrike.duration);
switch (flamestrike_timer)
{
    case delay:
    {
        sprite_index = spr_flamestrike_blast;
        image_index = 0;
        image_alpha = .75;      
        if place_meeting(x,y,obj_enemy_01)
        {
            scr_find_target_spell(id,1);
        }
        break;
    }
    case death_delay: 
    {
        instance_destroy();
        break;
    }
}
flamestrike_timer += 1;


