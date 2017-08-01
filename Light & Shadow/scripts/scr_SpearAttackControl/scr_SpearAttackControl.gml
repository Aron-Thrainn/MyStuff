if (object_index!= obj_SpearAttackControl){
	return;
}
var f_key_attack = keyboard_check_pressed(vk_space);
switch(o_attackstate){
	case 0:{ //can attack
		if (f_key_attack){ o_attackstate = 1; o_spear.sprite_index = spr_spearAttack; }
		break;
	}
	case 1:{ //is attacking
		if (o_attackframecount == 9) {
			with (obj_enemy) { if (place_meeting(x,y,obj_Spear)) instance_destroy(); }
		}
		else if (o_attackframecount == 23){
			o_attackstate = 2; o_spear.sprite_index = spr_SpearIdle;
			o_attackframecount = 0;
		}
		o_attackframecount++;
		break;
	}
	case 2:{ //is recharging
		if (o_attackrecharge >= o_attackrechargemax){
			o_attackrecharge = 0;
			o_attackstate = 0;
		}
		else { o_attackrecharge++; }
		break;
	}
	
}
	