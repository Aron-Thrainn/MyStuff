//argument0 = slot
globalvar ui_flamestrike;
ui_flamestrike = instance_create(0,0,obj_spell_flamestrike);
with ui_flamestrike
{
    slot = argument0;
    scr_spell_slot(slot);
    player.spell[slot] = ui_flamestrike;    
    /*switch(slot)
    {
        case 1:
        {
            player.spell_1 = ui_flamestrike;
            break;
        }
        case 2:
        {
            player.spell_2 = ui_flamestrike;
            break;
        }
    }*/
}
