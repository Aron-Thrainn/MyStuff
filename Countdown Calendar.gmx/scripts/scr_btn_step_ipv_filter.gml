switch(state)
{
    case btn_state.idle:
    {
        if pressed == 1    scr_button_pressed();
        else if pressed_r == 1    scr_button_pressed_r();
        break;
    }
    case btn_state.toggled:
    {
        if pressed == 1    scr_button_pressed();
        else if pressed_r == 1    scr_button_pressed_r();
        break;
    }
}
switch(state)
{
    case btn_state.idle:
    {
        if pressed || pressed_r 
        {
           image_index = 2;
           x_offset = x+7;
           y_offset = y+7;
        }
        else 
        {
            image_index = 0;
            x_offset = x+5;
            y_offset = y+5;
        }
        break;
    }
    case btn_state.toggled:
    {
        if pressed || pressed_r 
        {
           image_index = 2;
           x_offset = x+7;
           y_offset = y+7;
        }
        else 
        {
            image_index = 1;
            x_offset = x+6;
            y_offset = y+6;
        }
        break;
    }
}
hover = 0;


