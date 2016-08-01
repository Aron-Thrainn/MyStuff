var xx = room_width-180;
var yy = room_height-120;

draw_set_font(fnt_combat_text);
draw_rectangle_colour(xx,yy,room_width,room_height,
    c_white,c_white,c_white,c_white,false);
draw_text(xx,yy,"Total damage:");
draw_text(room_width-30,yy, string(total_damage));
draw_text(xx,yy+16,"Fireball damage:");
draw_text(room_width-30,yy+16, string(fireball_damage));
draw_text(xx,yy+32,"Flamestrke damage:");
draw_text(room_width-30,yy+32, string(flamestrike_damage));
draw_text(xx,yy+48,"Other damage:");
draw_text(room_width-30,yy+48, string(other_damage));
draw_text(xx,yy+64,"Wailing damage:");
draw_text(room_width-30,yy+64, string(wailing_shield));
draw_text(xx,yy+80,"Charge damage:");
draw_text(room_width-30,yy+80, string(charge_damage));
draw_text(xx,yy+96,"Attack damage:");
draw_text(room_width-30,yy+96, string(attack_damage));
