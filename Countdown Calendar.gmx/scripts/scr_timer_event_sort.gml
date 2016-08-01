var temp_name, temp_id, temp_comp, temp_dis, bar_pos;
bar_pos = 0;


//use ds grid to sort
with obj_initialize
{
    bar_count = instance_number(obj_timer);
    if bar_count != 0
    {
        //start by moving all the timers off screen
        with obj_timer
        {
            x = 1000;
            y = 0;
            scr_timer_event_chackdisplay();
        }
        
        // format for the ds grid information
        // 0 id;  1 name;  2 compress; 3 timer_display;
        
        //add the information to the grid
        sort = ds_grid_create(4,bar_count);
        for (i = 0; i < bar_count; i += 1;)
        {
            bar = instance_find(obj_timer, i);
        
            ds_grid_add(sort,0,i, bar.id);
            ds_grid_add(sort,1,i, bar.timer_name);
            ds_grid_add(sort,2,i, scr_timer_event_compress(bar));                
            ds_grid_add(sort,3,i, bar.timer_display); 
        }
        //sort
        ds_grid_sort(sort,2,sort_order);
        
        
        //move the timers based on the grids new order
        for (i = 0; i < bar_count; i += 1;)
        {
            temp_id = ds_grid_get(sort,0,i);
            temp_name = ds_grid_get(sort,1,i);
            temp_comp = ds_grid_get(sort,2,i);
            temp_dis = ds_grid_get(sort,3,i);
            
            if temp_dis == 1
            {
                temp_id.x = 150;
                temp_id.y = (75 * bar_pos) + scroll_counter;
                bar_pos += 1;
            }
        }
        ds_grid_destroy(sort);
    }
    
    scr_autosave();
    scr_call_timer_count();
}
