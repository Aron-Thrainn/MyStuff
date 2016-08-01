var temp_num = (-1 * power((((1.68 / lifespan) * counter / game_speed) - 0.6), 2) + 1.2);
//         bias to normalize^      ^ duration in seconds
temp_num = min(temp_num, 1);
image_alpha = temp_num;

if image_alpha < minalpha   instance_destroy();
x += cos(dir) * sp * speed_mod;
y += -sin(dir) * sp * speed_mod;
speed_mod *= 1 - speed_mod_reduction;
counter += 1;
//show_debug_message(string(counter))
