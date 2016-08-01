
if pressed == 1    scr_tmr_pressed();

switch(state)
{
    case tmr_state.idle:
    {
        if pressed
        {
           image_index = 1;
           x_offset = x+5;
           y_offset = y+5;
        }
        else 
        {
            image_index = 0;
            x_offset = x+3;
            y_offset = y+3;
        }
        break;
    }
    case tmr_state.waiting:
    {
        if pressed 
        {
           image_index = 1;
           x_offset = x+5;
           y_offset = y+5;
        }
        else 
        {
            image_index = 0;
            x_offset = x+3;
            y_offset = y+3;
        }
        break;
    }
    case tmr_state.annual_fin:     //same code for finished and annual_fin
    case tmr_state.finished:
    {
        if pressed 
        {
           image_index = 3;
           x_offset = x+5;
           y_offset = y+5;
        }
        else 
        {
            image_index = 2;
            x_offset = x+3;
            y_offset = y+3;
        }
        break;
    }
}
hover = 0;


