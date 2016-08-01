
channel_fireball_mod = 0.5;             //Movement modifier for fireball cast
def_cd = 1;                    //Cooldown for Fireball
def_dmg = 4;                  //Damage for Fireball
def_crit = 20;                 //Crit chance for Fireball
def_crit_bonus = 1;               //Crit multiplier for Fireball
range = 600 - 32;              //-32 for the starting position
missle_speed = def_speed * 2.25;
explosion_duration = 1 * game_speed;
explosion_delay = round(16 / missle_speed);
casting_delay = 0.1 * game_speed;   //delay before player can fire off the spell

c1 = 0.5 * game_speed;
c2 = 1 * game_speed;
c3 = 1.5 * game_speed;
c4 = 3 * game_speed;

cooldown = 0;
damage = def_dmg;
crit = def_crit;
crit_bonus = def_crit_bonus;

