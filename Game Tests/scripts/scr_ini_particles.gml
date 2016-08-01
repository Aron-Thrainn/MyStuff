var sp = room_speed;
//create particle system
globalvar embers;
embers = part_system_create();
part_system_depth(embers,-49);

//create an ember particle
globalvar ember;
ember = part_type_create();
part_type_sprite(ember,spr_flamestrike_dot,0,0,0);
part_type_orientation(ember,0, 359, 0, 0, 1);
part_type_size(ember,0.6,1,-0.6 / sp,0);
part_type_speed(ember,3 / sp, 8 / sp, 0 / sp, 0);
part_type_direction(ember, 0, 359, 0, 0);
part_type_life(ember,1 * sp,2 * sp);
part_type_alpha1(ember,1);
