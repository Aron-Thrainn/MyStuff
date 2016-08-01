scr_initialize_keyinputs();
scr_initialize_current_time();
//scr_mouseover_detect();   //currently calles from end_step

if instance_exists(obj_menu)
{
    var temp_mnu = instance_find(obj_menu,0);
    if temp_mnu.mnu_name == "menu_1"     scr_keybind_menu1();
    else if temp_mnu.mnu_name == "menu_2"     scr_keybind_menu2();
}
else 
{
    scr_keybind_main();
}




//delay controlled scripts



//Debug
//if mouseover != noone     show_debug_message("mouseover: " + (object_get_name(mouseover.object_index)));
//else show_debug_message("mouseover: noone")
//show_debug_message(string(ipv_colour))
//show_debug_message(ipv_time)
//show_debug_message("stage 0: " + string(instance_number(obj_timer)));
//if instance_exists(obj_menu)    show_debug_message("Active input: " + string(active_menu.active_input));
//show_debug_message(string(m2_ipv_colour));
