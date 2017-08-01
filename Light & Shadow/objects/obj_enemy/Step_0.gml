var f_tempvsp = o_vsp;
if (!place_meeting(x, y+1, obj_Wall)){
	o_vsp = min(o_vsp + o_grv, 12);
	f_tempvsp = o_vsp;
}

if (place_meeting(x, y+f_tempvsp, obj_Wall)){
	var f_vsp = 0;
	while (!place_meeting(x, y+f_vsp+sign(f_tempvsp), obj_Wall)){
		f_vsp += sign(f_tempvsp);
	}
	f_tempvsp = f_vsp;
	o_vsp = 0;
}

y += f_tempvsp;