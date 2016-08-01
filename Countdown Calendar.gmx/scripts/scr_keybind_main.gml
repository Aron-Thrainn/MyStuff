if mouse_wh_up && scroll_counter < 0
{
    scroll_counter += scroll_speed;
    with obj_timer
    {
        y += other.scroll_speed;
    }
    //show_debug_message("up: " + string(scroll_counter));
}

if key_space
{
    scr_btn_event_menu1();
}

if key_esc
{
     game_end()
}

else if mouse_wh_down
{
    var temp_num = instance_number(obj_timer);
    temp_num *= sprite_get_height(spr_tmr_blue);
    temp_num += scroll_speed * 2; 
    temp_num -= view_hview[1];
    temp_num *= -1;
    if scroll_counter > temp_num
    {
        scroll_counter -= scroll_speed;
        with obj_timer
        {
            y -= other.scroll_speed;
        }
    }
    //show_debug_message("down: "  + string(scroll_counter) + " < " + string(temp_num));
}
