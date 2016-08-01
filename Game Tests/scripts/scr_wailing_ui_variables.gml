def_cd = 4;                     //Cooldown for Wailing Arrow
def_dmg = 0;                    //Damage for Wailing Arrow
def_crit = 0;                   //Crit chance for Wailing Arrow
def_crit_bonus = 1;                //Crit multiplier for Wailing Arrow
def_shield = 16;
sp = def_speed * 1.5;
range = 300;
duration = 1 * game_speed;
animation_speed_inmotion = sprite_get_number(spr_wa_inmotion) / room_speed;
animation_speed_blast = (sprite_get_number(spr_wa_impact) - 1)/ duration;

cooldown = 0;
damage = def_dmg;
crit = def_crit;
crit_bonus = def_crit_bonus;
