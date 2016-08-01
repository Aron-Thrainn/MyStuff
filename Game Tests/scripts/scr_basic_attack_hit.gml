//argument0 = target id




with caster {
    scr_ste_trigger_seek(Trigger.BasicAttack);
}

scr_deal_hit(damage,crit,crit_bonus,1,argument0,player,player);

instance_destroy();

