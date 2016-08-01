//argument0 = slot
globalvar ui_fireball;
ui_fireball = instance_create(0,0,obj_spell_fireball);
with ui_fireball
{
    slot = argument0;
    scr_spell_slot(slot);
    player.spell[slot] = ui_fireball;
    
}
