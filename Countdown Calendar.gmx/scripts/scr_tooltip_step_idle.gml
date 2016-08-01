x = mouse_x;
y = mouse_y;

if mouseover == noone   instance_destroy();
else if mouseover.tooltip == ""    instance_destroy();
//else if mouseover.tooltip != tlt_text instance_destroy();
else tlt_text = mouseover.tooltip;
