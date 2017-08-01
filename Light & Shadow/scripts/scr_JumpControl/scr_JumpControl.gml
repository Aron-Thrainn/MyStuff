if (object_index!= obj_JumpControl){
	return;
}
var f_key_jump = keyboard_check(ord("W"));
var f_key_jump_pressed = keyboard_check_pressed(ord("W"));	


var f_tempvsp = global.g_Player.o_vsp;
var f_onground;

//set variables on ground with buffer
with global.g_Player{ f_onground = scr_IsOnGround(); }
	
if (f_onground){
	o_jumpstate = 0;
	o_jumpstatebuffer = 0;
	o_jumpingbuffer = 0;
}
else{
	if (o_jumpstate = 0 && o_jumpstatebuffer < o_jumpststatebuffermax){ //incrementing jump state buffer
		o_jumpstatebuffer++;
	}
	else if (o_jumpstatebuffer == o_jumpststatebuffermax){
		o_jumpstate = 2;
		o_jumpstatebuffer = 0;
	}
}


if (o_jumpstate == 1 || o_jumpstate == 3){ //incrementing jumping buffer
	if (o_jumpingbuffer >= o_jumpingbuffermax){
		o_jumpingbuffer = 0;
		o_jumpstate++;
	}
	else o_jumpingbuffer++;
}


switch(o_jumpstate){
	case 0: { //jump from ground
		if (f_key_jump_pressed) {f_tempvsp = o_jumpheight; o_jumpstate = 1; scr_createJumpAnimation(global.g_Player); }
		break;
	} 
	case 1: { //continue jump
		if (f_key_jump){ f_tempvsp += o_jumpincrease; } else { o_jumpstate = 2; }
		break;
	}
	case 2: { //double jump
		if (f_key_jump_pressed){ f_tempvsp = o_doublejumpheight; o_jumpstate = 3;  scr_createJumpAnimation(global.g_Player); }
		break;
	}
	case 3: { //continue double jump
		if (f_key_jump){ f_tempvsp += o_doublejumpincrease; } else { o_jumpstate = 4; }
		break;
	}
	case 4: { break; } //out of jumps
}


return f_tempvsp;