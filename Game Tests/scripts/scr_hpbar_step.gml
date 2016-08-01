x = subject.x;
y = subject.y;

if aa != a
{
    if aa < a    aa = min(aa + hp_update_speed, a);
    else   aa = max(aa - hp_update_speed, a);
}
if bb != b
{
    if bb < b    bb = min(bb + hp_update_speed, b);
    else   bb = max(bb - hp_update_speed, b);
}
if cc != c
{
    if cc < c    cc = min(aa + hp_update_speed, c);
    else   cc = max(cc - hp_update_speed, c);
}
if dd != d
{
    if dd < d    dd = min(dd + hp_update_speed, d);
    else   dd = max(dd - hp_update_speed, d);
}
