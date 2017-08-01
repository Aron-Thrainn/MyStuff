/// @description Initialize healthbar for a unit
/// @param owner
/// @param max hp

var f_owner = argument0;
var f_hpmax = argument1;

var f_hpbar = instance_create_layer(f_owner.x, f_owner.y - 32, "lyr_Healthbars",obj_Health);
with f_hpbar {
	o_owner = f_owner;
	o_hpmax = f_hpmax;
	o_hp = f_hpmax;
}