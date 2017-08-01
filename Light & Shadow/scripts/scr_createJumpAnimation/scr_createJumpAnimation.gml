var f_target = argument0;
var f_animation = instance_create_layer(x, y, "lyr_Particles", obj_JumpAnimation);
f_animation.o_target = argument0;

f_animation.x = argument0.x;
f_animation.y = argument0.y + 32;