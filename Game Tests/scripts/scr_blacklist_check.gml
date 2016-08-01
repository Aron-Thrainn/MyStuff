//argument0 = owner of list
//argument1 = id to find
var f_owner = argument0;

with f_owner
{
    for (i=0; i<blacklist_size; i++)
    {
        if blacklist[i] == argument1 return 1;
    }
}
return 0;



  
