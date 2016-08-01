//argument0 = slot
globalvar ui_charge;
ui_charge = instance_create(0,0,obj_spell_charge);
with ui_charge
{
    slot = argument0;
    scr_spell_slot(slot);
    player.spell[slot] = ui_charge;
}
