//argument0 = number
//argument1 = crit (1/0)
//argument2 = 1 = physical | 2 = magic | 3 = heal | 4 = shield
var temp_inst = instance_create(0,0,obj_combat_text);
with temp_inst
{
    var type = argument2;
    crit = argument1;
    number = argument0;
    switch(type)
    {
        case 1:         //physical
        {
            col = ct_physic;
            break;
        }
        case 2:         //magic
        {
            col = ct_magic;
            break;
        }
        case 3:         //Heal
        {
            col = ct_heal;
            break;
        }
        case 4:         //shield
        {
            col = ct_shield;
            break;
        }
    }
    //if crit  font = fnt_combat_text_crit;
    //else font = fnt_combat_text;
    font = fnt_orange;
    
    dir = choose(random_range(120,240),random_range(-60,60));
    dir = scr_radian(dir);
    x = other.x + (cos(dir)*12);
    y = other.y + (-sin(dir)*8);
    offset = string_width(string(number)) / 2;
}
