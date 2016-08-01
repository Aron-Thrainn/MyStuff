player.cast[slot] = 0;
player.state = P_state.move;

timer = 0;

if channel.first_hit  cooldown = def_cd;
else cooldown = hit_cd;

with channel  instance_destroy();
channel = noone;
casting = 0;

