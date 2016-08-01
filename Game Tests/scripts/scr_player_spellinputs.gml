if !gcd             //if the global cooldown is 0
{
    //Ability casts
    for (i=1; i<=4; i++)
    {        
        if key_ability[i] && !cast[i] && spell[i] != noone
        {
            if !scr_spell_get_cd(spell[i])  with spell[i]  scr_spell_cast();  /* scr_spell_ini(spell[i]); */
        }
    }
}



//range indicator
if key_1
{
    if instance_exists(obj_spell_radius)
        {
            with obj_spell_radius instance_destroy();
        }
        else
        {
            instance_create(x,y,obj_spell_radius);
        }
}
