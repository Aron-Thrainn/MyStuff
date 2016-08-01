//argument0 = slot
globalvar ui_wailing;
ui_wailing = instance_create(0,0,obj_spell_wailingarrow);
with ui_wailing
{
    slot = argument0;
    scr_spell_slot(slot);
    player.spell[slot] = ui_wailing;
}
