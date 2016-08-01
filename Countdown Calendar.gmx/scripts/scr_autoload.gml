//called from initialize_create


with obj_initialize
{
    var n0,n1,tempinst,temp_name,temp_tooltip,temp_year,temp_month,temp_day,temp_time,temp_hour,temp_minute,temp_sprite;

    
    ini_open("autosave");
    n0 = 0;
    n1 = 0;
    
    //Timer bars
    while ini_key_exists("save",string(n0) +string(n1))
    {   
        tempinst = instance_create(0,0,obj_timer);
        with tempinst
        {
            timer_name = ini_read_string("save", string(n0) +string(n1),0);
            n1 += 1;
            tooltip = ini_read_string("save", string(n0) +string(n1),0);
            n1 += 1;
            target_year = ini_read_real("save", string(n0) +string(n1),0);
            n1 += 1;
            target_month = ini_read_real("save", string(n0) +string(n1),0);
            n1 += 1;
            target_day = ini_read_real("save", string(n0) +string(n1),0);
            n1 += 1;
            timer_time = ini_read_string("save", string(n0) +string(n1),0);
            n1 += 1;
            target_hour = ini_read_real("save", string(n0) +string(n1),0);
            n1 += 1;
            target_minute = ini_read_real("save", string(n0) +string(n1),0);
            n1 += 1;
            sub = ini_read_real("save", string(n0) +string(n1),0);
            n1 += 1;
            annual = ini_read_real("save", string(n0) +string(n1),0);
            
            n0 += 1;
            n1 = 0;
        }            
    }
    
    n0 = 0;
    n1 = 0;
    
    //Filter Variables
    flr_blue = ini_read_real("filters",string(n0)+string(n1),1);
    n1 += 1;
    flr_green = ini_read_real("filters",string(n0)+string(n1),1);
    n1 += 1;
    flr_red = ini_read_real("filters",string(n0)+string(n1),0);
    n1 += 1;
    flr_orange = ini_read_real("filters",string(n0)+string(n1),0);
    n1 += 1;
    flr_pink = ini_read_real("filters",string(n0)+string(n1),0);
    n1 += 1;
    flr_yellow = ini_read_real("filters",string(n0)+string(n1),0);
    n1 += 1;
    flr_teal = ini_read_real("filters",string(n0)+string(n1),0);
    n1 += 1;
    flr_purple = ini_read_real("filters",string(n0)+string(n1),0);
    n0 += 1;
    n1 = 0;

    n0 = 0;
    n1 = 0;
    
    //Filter button tooltips
    with obj_button
    {
        if type == btn_type.ipv_filter
        {
            tooltip = ini_read_string("filter tooltips",string(save_n0),default_tlt);
        }
    }
    
    ini_close();
    
    obj_initialize.alarm[2] = 3;
    obj_initialize.alarm[3] = 2;

    with obj_timer
    {
        scr_timer_event_sync_prep();
        sync_flag = 0;
    }
}
    
    
    
