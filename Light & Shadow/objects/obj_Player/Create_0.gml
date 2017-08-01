
global.g_Player = self;
o_hsp = 0;
o_vsp = 0;
o_grv = 0.8;
o_walkspd = 5;
o_dir = 1;

o_jumpcontrol = instance_create_layer(0,0,"lyr_Control", obj_JumpControl);
o_spearattackcontrol = instance_create_layer(0,0,"lyr_Control", obj_SpearAttackControl);
o_spear = instance_create_layer(0,0,"lyr_Weapons", obj_Spear);
o_spearattackcontrol.o_spear = o_spear;
o_health = scr_CreateHealth(self, 10);

