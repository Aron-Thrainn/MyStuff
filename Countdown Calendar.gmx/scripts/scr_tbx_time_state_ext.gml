//argument0 = inputstring (1 char '1')
//argument1 = current state
var input = argument0;
var state = argument1;
var type;

if string_letters(input) != ""{
    type = 'a'
}
else if string_digits(input) != ""{
    type = 'n'
}
else if (input == ':'){
    type = 's'
}
else type = '0'
switch(state)
{
    case 0: if (type == 'n') return 1;       //Start line 1
        else return 11;//else error
        break;
    case 1: if (type == 'n') return 2;      //0x:00 line 1
        else if (type == 's') return 8;
        else return 11;//else error
        break;
    case 2: if (type == 'n') return 3;      //xx:00 line 1
        else if (type == 's') return 5;
        else return 11;//else error
        break;
    case 3: if (type == 'n') return 4;      //0x:xx line 1
        else return 11;//else error
        break;
    case 4: return 11;                      //xx:xx line 1
        break;
    case 5: if (type == 'n') return 6;     //start line 2
        else return 11;//else error
        break;
    case 6: if (type == 'n') return 7;     //xx:0x line 2
        else return 11;//else error
        break;
    case 7: return 11;                     //xx:xx line 2
        break;
    case 8: if (type == 'n') return 9;    //Start line 3
        else return 11;//else error
        break;
    case 9: if (type == 'n') return 10;   //0x:0x line 3
        else return 11;//else error
        break;
    case 10: return 11;                   //0x:xx line 3
        break;
    case 11:                              //Error
        break;
}
return 0;       //Fara úr fallinu og gefa error skilaboð
