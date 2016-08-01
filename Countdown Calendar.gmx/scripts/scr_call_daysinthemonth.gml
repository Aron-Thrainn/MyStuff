if argument0 != 0
{
    switch (argument0)// Argument0 gives the number of days
    {
        case 1: return 31; 
        case 2: return 28; 
        case 3: return 31; 
        case 4: return 30; 
        case 5: return 31; 
        case 6: return 30; 
        case 7: return 31; 
        case 8: return 31;
        case 9: return 30; 
        case 10: return 31; 
        case 11: return 30; 
        case 12: return 31; 
    }
}

if argument1 != 0
{
    switch (argument1)// Argument1 gives the name of the month
    {
        case 1: return "January";
        case 2: return "Febuary";
        case 3: return "March";
        case 4: return "April";
        case 5: return "May";
        case 6: return "June";
        case 7: return "July";
        case 8: return "August";
        case 9: return "September";
        case 10: return "October"; 
        case 11: return "November";
        case 12: return "December";
    }
}
