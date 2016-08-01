///Select State

if (keyboard_check_pressed(vk_escape))  game_end();

scr_player_inputs();
scr_gcd_tick();
scr_player_pickup();

switch (state)
{
    case P_state.move: 
        {
            scr_player_movement_input();
            scr_player_movement(); 
            scr_player_spellinputs();
            //scr_pl_casting();
            break;
        }
    case P_state.cast_move: 
        {
            scr_player_movement_input()
            scr_player_movement(); 
            scr_player_spellinputs();
            //scr_pl_casting();
            break;
        }
    case P_state.attack: 
        {
            scr_player_movement_input()
            scr_player_movement();
            scr_pl_attack();
            scr_player_spellinputs(); 
            //scr_pl_casting();
            break;
        }
    case P_state.snared: 
        {
            scr_player_spellinputs();
            //scr_pl_casting();
            break;
        }
    case P_state.attack_move: 
        {
            scr_player_movement_input()
            scr_pl_attack_move();
            scr_player_spellinputs(); 
            //scr_pl_casting();
            break;
        }
    case P_state.charging: 
        {
            break;
        }
    
}




