o_jumpmod = 1;

o_jumpheight = -6 * o_jumpmod;
o_jumpincrease = -1.2 * o_jumpmod;
o_doublejumpheight = -4 * o_jumpmod;
o_doublejumpincrease = -0.8 * o_jumpmod;
o_jumpstate = 0;
//states:
//0 = can jump
//1 = is jumping
//2 = has double jump
//3 = is double jummping
//4 = no jump
o_jumpstatebuffer = 0;
o_jumpststatebuffermax = 10;
o_jumpingbuffer = 0;
o_jumpingbuffermax = 12;
