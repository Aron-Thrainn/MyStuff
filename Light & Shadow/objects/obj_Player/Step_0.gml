//imputs
f_key_left = keyboard_check(ord("A"));
f_key_right = keyboard_check(ord("D"));

//virtual movement
var f_hmove = f_key_right - f_key_left;
if (o_dir == f_hmove) { o_hsp = f_hmove * o_walkspd; }
else { o_hsp = 0; }


if (f_hmove == -1) { o_dir = -1; }
else if (f_hmove == 1) { o_dir = 1; }

var f_tempvsp = o_vsp;
if (!place_meeting(x, y+1, obj_Wall)){
	o_vsp = min(o_vsp + o_grv, 12);
	f_tempvsp = o_vsp;
}

var f_tempresult;
with o_jumpcontrol{ f_tempresult = scr_JumpControl(); }
if (f_tempresult != 0){ o_vsp = f_tempresult; f_tempvsp = f_tempresult; }



//collision
if (place_meeting(x+o_hsp, y, obj_Wall)){
	var f_hsp = 0;
	while (!place_meeting(x+f_hsp+sign(o_hsp), y, obj_Wall)){
		f_hsp += sign(o_hsp);
	}
	o_hsp = f_hsp;
}


if (place_meeting(x, y+f_tempvsp, obj_Wall)){
	var f_vsp = 0;
	while (!place_meeting(x, y+f_vsp+sign(f_tempvsp), obj_Wall)){
		f_vsp += sign(f_tempvsp);
	}
	f_tempvsp = f_vsp;
	o_vsp = 0;
	
}

//practical movement
x += o_hsp;
y += f_tempvsp;
image_xscale = o_dir;

//Attacking

with o_spearattackcontrol{ scr_SpearAttackControl(); }