///Enumerator

enum tmr_state
{
    idle,
    finished,
    waiting,
    annual_fin,
    test
}

enum btn_state
{
    idle,
    toggled,
    pressed,
    unpressed
}

enum btn_type
{
    idle,
    ipv_colour,
    ipv_filter,
    standard,
    m2_ipv_colour,
    ipv_annual,
    m2_ipv_annual,
    test
}

enum ipb_state
{
    idle,
    active,
    transition
}
enum ipb_type
{
    title,
    date,
    tooltip,
    compressed
}
enum tbx_state
{
    idle,
    active,
    trans
}
enum tbx_type
{
    title,
    date,
    description,
    time
}
enum evr_loc
{
    main,
    menu_1,
    menu_2
}
enum tlt_state
{
    idle,
    active,
    trans_1,
    trans_2
}
