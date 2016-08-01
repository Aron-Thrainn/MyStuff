//argument0 = slot number
var xx,yy;
xx = (room_width / 2) - (sprite_get_width(spr_ui_main) / 2);
yy = room_height - sprite_get_height(spr_ui_main)

switch(argument0)
{
    case 1: {
                x = xx + 43;
                y = yy + 104;
                break;
            }     
    case 2: {
                x = xx + 128;
                y = yy + 104;
                break;
            }       
    case 3: {
                x = xx + 215;
                y = yy + 104;
                break;
            }        
    case 4: {
                x = xx + 302;
                y = yy + 104;
                break;
            }        
}
