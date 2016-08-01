// This script is to be called from Avengers Shield controller


var f_AvengersShield = instance_create(x,y,obj_spell_AvengersShield);
with f_AvengersShield
{
    dir = player.image_angle;
    
    caster = other.owner;
    controller = other.id;
    width = other.sp_width;
    max_targets = other.sp_max_targets;
    max_distance = other.sp_distance;    
    missle_speed = other.sp_missle_speed;
    damage = other.sp_damage;
    target = noone;    //set when target is touched
    
    //x_speed & y_speed
    scr_move_set(missle_speed, dir);
    
    x = caster.x;
    y = caster.y;
    distance_travelled = 0;
    //standard vars
    scr_spells_default_vars_SkillShot();
    
    
    var f_effect_1 = scr_spell_effect_spawn_Damage();
    var f_effect_2 = scr_spell_effect_spawn_Bounce();
    var f_effect_3 = scr_spell_effect_spawn_DestroySelf();
    
    scr_spells_func_AddEffect(f_effect_1);
    scr_spells_func_AddEffect(f_effect_2);
    scr_spells_func_AddEffect(f_effect_3);
}

return f_AvengersShield;
