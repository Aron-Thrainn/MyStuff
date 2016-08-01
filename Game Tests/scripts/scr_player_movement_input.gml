scr_player_direction();
//Create waypoint
if mouse_right_pressed
{
    clicking = 1;
    if position_meeting(mouse_x,mouse_y,obj_enemy_01)
    {
        scr_pl_mov_attack();
    }
    else
    {
        scr_pl_mov_move();
    }
}
if !mouse_right  clicking = 0;
if clicking
{
    if state == P_state.attack
    {
        
    }
    else if state == P_state.move
    {
        scr_pl_mov_move();
    }
}

if key_A
{
    scr_pl_mov_attack_move();
}

