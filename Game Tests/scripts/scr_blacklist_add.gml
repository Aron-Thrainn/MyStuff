//argument0 = owner of list
//argument1 = id to add
var f_owner = argument0;

with f_owner
{
    blacklist[blacklist_size - 1] = argument1;
    blacklist[blacklist_size] = 0;
    blacklist_size += 1;
}
