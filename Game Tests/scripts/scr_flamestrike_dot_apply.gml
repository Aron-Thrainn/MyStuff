//argument0 = target id

temp_debuff = instance_create(argument0.x,argument0.y,obj_debuff_flamestrike);

with temp_debuff
{
    target = argument0;
    scr_flamestrike_dot_remainder();
}
