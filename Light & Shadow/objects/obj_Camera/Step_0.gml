o_xto = o_follow.x;
o_yto = o_follow.y;

x += (o_xto - x) / 10;
y += (o_yto - y) / 10;

var f_vm = matrix_build_lookat(x, y, -10, x, y, 0, 0, 1, 0);
camera_set_view_mat(o_camera, f_vm);