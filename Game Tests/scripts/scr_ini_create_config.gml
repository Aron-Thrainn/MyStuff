//Game speed
globalvar game_speed;
game_speed = room_speed / 1;

//Character variables
globalvar def_speed, shield_decay_percent, decay_delay;
def_speed = 200 / game_speed;         //Default movement speed before effects
shield_decay_percent = 0.2;           //percent of shield decay per second
decay_delay = 5 * game_speed;         //number of seconds before decay kicks in

//Combat text
globalvar ct_physic, ct_magic, ct_heal, ct_heal_crit, ct_shield;
ct_physic = c_orange;
ct_magic = c_purple;
ct_heal = c_green;
ct_shield = c_aqua;

//Healbar
globalvar hpb_hp_pl, hpb_hp_enm, hpb_dot, hpb_hot, hpb_shield, hpb_miss;
hpb_hp_pl = c_yellow;
hpb_hp_enm = c_red;
hpb_dot = c_olive;
hpb_hot = c_lime;
hpb_shield = c_aqua;
hpb_miss = c_black;
