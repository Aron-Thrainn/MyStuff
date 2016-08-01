//Create the ui and a global variable for functions to reference it
globalvar ui;
var xx, yy;
xx = (room_width / 2) - (sprite_get_width(spr_ui_main) / 2);
yy = room_height - sprite_get_height(spr_ui_main)
ui = instance_create(xx,yy,obj_ui);


scr_spells_add_spell(obj_spell_contr_AvengersShield, 1);
scr_spells_add_spell(obj_spell_contr_SpellSword, 2);
scr_spells_add_spell(obj_spell_contr_SeedOfCorruption, 3);


//scr_spell_create_fireball(1);
//scr_spell_create_flamestrike(2);
//scr_spell_create_charge(3);
//scr_spell_create_wailing(4);
