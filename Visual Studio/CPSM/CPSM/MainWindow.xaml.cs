using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CPSM_class_diagram;

namespace CPSM_class_diagram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GUI _GUI { get; set; }
        public SongCanvas _SongCan { get; set; }
        public FormSongSelect _FormSongSelect { get; set; }
        public MouseControl _MouseCtrl { get; set; }
        public HotkeyControl _HotCtrl { get; set; }
        public SongControl _SongCtrl { get; set; }
        public ScreenCapturer _ScreenCap { get; set; }


        public MainWindow() {
            InitializeComponent();

            Measure(new Size(Width, Height));
            Arrange(new Rect(0, 0, DesiredSize.Width, DesiredSize.Height));

            _SongCan = new SongCanvas(this);
            _GUI = new GUI(this, Cnv_GUI, _SongCan);
            _FormSongSelect = new FormSongSelect(this);
            _MouseCtrl = new MouseControl(this);
            _HotCtrl = new HotkeyControl(this);
            _ScreenCap = new ScreenCapturer(cnv_SongCan);



            //temp for testing, delays until Loaded
            Loaded += delegate
            {
                _ScreenCap.SaveScreenShot();
            };

        }
    }
}
