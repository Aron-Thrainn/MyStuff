player.cast[slot] = 1;

scr_pl_func_sp_change1(player,ui_fireball.channel_fireball_mod);
timer = 0;
channel = instance_create(x,y,obj_fireball); 
casting = 1;
sprite_index = spr_ui_fireball_casting;
