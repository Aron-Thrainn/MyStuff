state = E_state.dead;
sprite_index = spr_enemy_01_dead;
dead = 1;

with obj_debuff_flamestrike
{
    if target == other.id
    {
        instance_destroy();
    }
}
scr_enm_destroy_hpbar();
