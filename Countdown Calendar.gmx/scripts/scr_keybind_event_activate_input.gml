scr_mouseover_noone_sub();
with obj_textbox
{
    if tbx_name == argument0
    {
        keyboard_string = tbx_text;
        state = tbx_state.active;
        //active_menu.active_input = argument0;
        animate_tick = 0;
    }
}
