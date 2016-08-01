with obj_menu instance_destroy();
active_menu = noone;

with obj_button
{
    if loc == evr_loc.menu_1 || loc == evr_loc.menu_2
    {
        x = 601;
        y = 0;
    }
}
with obj_textbox
{
    if loc == evr_loc.menu_1 || loc == evr_loc.menu_2
    {        
        x = 601;
        y = 0;
    
    }
}
