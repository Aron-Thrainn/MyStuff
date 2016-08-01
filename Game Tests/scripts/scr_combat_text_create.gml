number = 0;
grav = -180;
crit = 0;
col = c_black;
font = fnt_combat_text
offset = 0;
counter = 0;

dir = irandom_range(0,1);
if dir == 0 dir = -1;   // -1 or 1


sp = 60 / game_speed;
size = 1;
dir = 0;
lifespan = 1; // in seconds
minalpha = 0.05;
speed_mod = 1;
speed_mod_reduction = 0.1;
