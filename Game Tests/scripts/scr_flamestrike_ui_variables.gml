
def_cd = 4;                 //Cooldown for Flamestrike
def_dmg = 2;               //Damage for Flamestrike
def_crit = 15;              //Crit chance for Flamestrike
def_crit_bonus = 1;            //Crit multiplier for Flamestrike
delay = 1.5 * game_speed;
casttime = 0.5 * game_speed;     //Casttime in seconds
duration = 1 * game_speed;
animation_speed_cast = sprite_get_number(spr_flamestrike_cast) / casttime;
animation_speed_blast = sprite_get_number(spr_flamestrike_blast) / duration;
range = 300;
def_tickdmg = 1;
def_tickrate = 0.5 * game_speed;
def_tickdur = 3 * game_speed;
def_tickcrit = 0;
def_tickcrit_bonus = 2;


cooldown = 0;
damage = def_dmg;
crit = def_crit;
crit_bonus = def_crit_bonus;
