//called from sort
with obj_initialize
{
    var file,inst_num,n0,n1,inst;
    
    if file_exists("autosave")     file_delete("autosave");
    ini_open("autosave");
    inst_num = instance_number(obj_timer);
    n0 = 0;
    n1 = 0;
    
    while inst_num > 0
    {
        inst = instance_find(obj_timer, inst_num - 1);
        ini_write_string("save", string(n0) +string(n1), inst.timer_name);
        n1 += 1;
        ini_write_string("save", string(n0) +string(n1), inst.tooltip);
        n1 += 1;
        ini_write_real("save", string(n0) +string(n1), inst.target_year);
        n1 += 1;
        ini_write_real("save", string(n0) +string(n1), inst.target_month);
        n1 += 1;
        ini_write_real("save", string(n0) +string(n1), inst.target_day);
        n1 += 1;
        ini_write_string("save", string(n0) +string(n1), inst.timer_time);
        n1 += 1;
        ini_write_real("save", string(n0) +string(n1), inst.target_hour);
        n1 += 1;
        ini_write_real("save", string(n0) +string(n1), inst.target_minute);
        n1 += 1;
        ini_write_real("save", string(n0) +string(n1), inst.sub);
        n1 += 1;
        ini_write_real("save", string(n0) +string(n1), inst.annual);
        inst_num -= 1;
        n0 += 1;
        n1 = 0;
    }
    
    n0 = 0;
    n1 = 0;
    
    //Filter Variables
    ini_write_real("filters",string(n0)+string(n1),flr_blue);
    n1 += 1;
    ini_write_real("filters",string(n0)+string(n1),flr_green);
    n1 += 1;
    ini_write_real("filters",string(n0)+string(n1),flr_red);
    n1 += 1;
    ini_write_real("filters",string(n0)+string(n1),flr_orange);
    n1 += 1;
    ini_write_real("filters",string(n0)+string(n1),flr_pink);
    n1 += 1;
    ini_write_real("filters",string(n0)+string(n1),flr_yellow);
    n1 += 1;
    ini_write_real("filters",string(n0)+string(n1),flr_teal);
    n1 += 1;
    ini_write_real("filters",string(n0)+string(n1),flr_purple);
    n0 += 1;
    
    n0 = 0;
    n1 = 0;
    
    //Filter button tooltips
    with obj_button
    {
        if type == btn_type.ipv_filter
        {
            ini_write_string("filter tooltips",string(save_n0),tooltip);
        }
    }
    
    
    ini_close();
}
