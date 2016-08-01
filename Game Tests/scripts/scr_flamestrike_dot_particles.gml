

//create emitter
ember_emitter = part_emitter_create(embers);
part_emitter_region(embers, ember_emitter, x-8, x+8, y-8, y+8, ps_shape_ellipse, ps_distr_linear);
part_emitter_stream(embers, ember_emitter, ember, -0.1 * room_speed);
