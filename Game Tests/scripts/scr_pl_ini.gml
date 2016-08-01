scr_unit_default_vars();
scr_pl_ini_variables();
scr_pl_ini_config();
scr_pl_ini_config_technical();
scr_pl_create_hpbar();

//status effects
scr_ste_unit_initialize();


/*
//debug
debug_buff_controller = instance_create(0,0,obj_control_buff_SpellSword);
var f_buff = scr_ste_buff_SpellSword_spawn(2,(15*game_speed),id,id,debug_buff_controller);

scr_ste_unit_add(f_buff);
