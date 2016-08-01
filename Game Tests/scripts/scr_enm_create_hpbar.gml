hpbar = instance_create(x,y,obj_health_bar);

with hpbar
{
    subject  = other.id;
    depth = -55;
    scr_hpbar_change_hp(other.hp);
    max_hp = other.max_hp;
    width = other.sprite_width - 4;
    y_offset = 4 + (other.sprite_height / 2);
    x_offset = (other.sprite_width / 2) - 2;
    scr_hp_update();
}
