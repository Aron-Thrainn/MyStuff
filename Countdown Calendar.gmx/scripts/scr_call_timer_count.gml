count_blue = 0;
count_green = 0;
count_red = 0;
count_orange = 0;
count_pink = 0;
count_yellow = 0;
count_teal = 0;
count_purple = 0;

with obj_timer
{
    switch(sub)
    {
        case 0: count_blue += 1; break;
        case 1: count_green += 1; break;
        case 2: count_red += 1; break;
        case 3: count_orange += 1; break;
        case 4: count_pink += 1; break;
        case 5: count_yellow += 1; break;
        case 6: count_teal += 1; break;
        case 7: count_purple += 1; break;
        
    }
}

