player.cast[slot] = 0;
player.state = P_state.move;
scr_pl_gcd();

casting = 0;
channel = noone;
cooldown = def_cd;
