player.cast[slot] = 1;
timer = 0;
casting = 1;
sprite_index = spr_ui_wailing_inmotion;
channel = instance_create(x,y,obj_wailingarrow); 
with channel  scr_wailingarrow_inmotion_ini();
