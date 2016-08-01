//Movement inputs
key_left = keyboard_check(ord("A"));
key_right = keyboard_check(ord("D"));
key_up = keyboard_check(ord("W"));
key_down = keyboard_check(ord("S"));
mouse_right = mouse_check_button(mb_right);
mouse_right_pressed = mouse_check_button_pressed(mb_right);


//Attack inputs
key_space = keyboard_check_pressed(vk_space);
key_ability[1] = keyboard_check_pressed(ord("Q"));
key_ability[2] = keyboard_check_pressed(ord("W"));
key_ability[3] = keyboard_check_pressed(ord("E"));
key_ability[4] = keyboard_check_pressed(ord("R"));
key_A = keyboard_check_pressed(ord("A"));
key_1 = keyboard_check_pressed(ord("1"));

//Any input?
key_any = sign(key_left + key_right + key_up + key_down + key_space + mouse_right)
