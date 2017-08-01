o_camera = camera_create();
o_follow = global.g_Player;

var f_vm = matrix_build_lookat(x, y, -10, x, y, 0, 0,1,0);
var f_pm = matrix_build_projection_ortho(800, 450, 1, 10000);

camera_set_view_mat(o_camera, f_vm);
camera_set_proj_mat(o_camera, f_pm);

view_camera[0] = o_camera;

o_xto = x;
o_yto = y;

