if instance_exists(obj_menu)    active_menu.active_input = tbx_name;
switch(type)
{
    case tbx_type.title: scr_tbx_step_ac_title();       break;
    case tbx_type.date: scr_tbx_step_ac_date();        break;
    case tbx_type.description: scr_tbx_step_ac_description();        break;
    case tbx_type.time:  scr_tbx_step_ac_time();    break;
}
