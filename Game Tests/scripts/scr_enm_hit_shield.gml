//argument0 = shield amount
shield_amount = argument0;

shield += round(shield_amount);


with hpbar  scr_hpbar_change_shield(other.shield);

//scr_combat_text_spawn(shield_amount, 0, 1, true);
scr_decay_delay();
