// This script is to be called from Player


var f_AvengersShield_contr = instance_create(0,0,obj_spell_contr_AvengersShield);
with f_AvengersShield_contr
{
    owner = other.id;
    width = 12;
    max_targets = 1;
    damage = 8;
}

return f_AvengersShield_contr;
