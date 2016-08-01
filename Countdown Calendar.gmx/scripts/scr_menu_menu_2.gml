temp_mnu = instance_create(100,100,obj_menu);
with temp_mnu
{
    mnu_name = "menu_2";
    sprite_index = spr_menu_2;

}

active_menu = temp_mnu;
with obj_button
{
    if loc == evr_loc.menu_2
    {
        switch(btn_name)
        {
            case "m2_btn_colour_blue":{ x = other.temp_mnu.x + 30; y = other.temp_mnu.y + 70; break;}
            case "m2_btn_colour_green":{ x = other.temp_mnu.x + 80; y = other.temp_mnu.y + 70; break;}
            case "m2_btn_colour_red":{ x = other.temp_mnu.x + 130; y = other.temp_mnu.y + 70; break;}
            case "m2_btn_colour_orange":{ x = other.temp_mnu.x + 180; y = other.temp_mnu.y + 70; break;}
            case "m2_btn_colour_pink":{ x = other.temp_mnu.x + 30; y = other.temp_mnu.y + 120; break;}
            case "m2_btn_colour_yellow":{ x = other.temp_mnu.x + 80; y = other.temp_mnu.y + 120; break;}
            case "m2_btn_colour_teal":{ x = other.temp_mnu.x + 130; y = other.temp_mnu.y + 120; break;}
            case "m2_btn_colour_purple":{ x = other.temp_mnu.x + 180; y = other.temp_mnu.y + 120; break;}
            
            case "m2_btn_annual":{ x = other.temp_mnu.x + 240; y = other.temp_mnu.y + 180; break;}
            
            case "btn_edittimer":{ x = other.temp_mnu.x + 10; y = other.temp_mnu.y + 440; break;}
            case "btn_deletetimer":{ x = other.temp_mnu.x + 80; y = other.temp_mnu.y + 440; break;}
            case "btn_cancel2":{ x = other.temp_mnu.x + 340; y = other.temp_mnu.y + 440; break;}
        }
    }
}
with obj_textbox
{
    if loc == evr_loc.menu_2
    {
        switch(tbx_name)
        {
            case "m2_tbn_name":{ x = other.temp_mnu.x + 10; y = other.temp_mnu.y + 40; break;}
            case "m2_tbn_year":{ x = other.temp_mnu.x + 30; y = other.temp_mnu.y + 190; break;}
            case "m2_tbn_month":{ x = other.temp_mnu.x + 120; y = other.temp_mnu.y + 190; break;}
            case "m2_tbn_day":{ x = other.temp_mnu.x + 30; y = other.temp_mnu.y + 235; break;}
            case "m2_tbn_time":{ x = other.temp_mnu.x + 120; y = other.temp_mnu.y + 235; break;}
            case "m2_tbn_description":{ x = other.temp_mnu.x + 30; y = other.temp_mnu.y + 280; break;}
        }
    }
}
