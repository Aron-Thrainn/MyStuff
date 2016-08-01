//argument0 = controller obj index
//argument1 = slot


var f_contr = instance_create(0,0,argument0);
with f_contr
{
    slot = argument1;
    scr_spell_slot(slot);
    player.spell[slot] = f_contr;
    
    owner = player;
}

