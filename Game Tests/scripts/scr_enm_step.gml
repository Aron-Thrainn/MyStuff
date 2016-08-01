


scr_enm_shield_decay();


switch (state)
{
    case E_state.idle: scr_enemy_idle(); break;
    case E_state.chase: scr_enemy_chase(); break;
    case E_state.attack: scr_enemy_attack(); break;
    case E_state.patrol: scr_enemy_patrol(); break;
    case E_state.dead: scr_enemy_idle(); break;
}
