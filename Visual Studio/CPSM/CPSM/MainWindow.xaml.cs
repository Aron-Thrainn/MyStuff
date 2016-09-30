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
using CPSM;
using CPSM.ViewModals;
using CPSM.Forms;


/*
    ToDo:
    change songdata data structure

*/



namespace CPSM
{
    public partial class MainWindow : Window
    {
        public GUI _GUI { get; set; }
        public SongCanvas _SongCan { get; set; }
        public FormSongSelect _FormSongSelect { get; set; }
        public MouseControl _MouseCtrl { get; set; }
        public HotkeyControl _HotCtrl { get; set; }
        public SongControl _SongCtrl { get; set; }
        public ScreenCapturer _ScreenCap { get; set; }
        public Version _vers { get; set; }
        public NoteColourForm _FomrNoteColour { get; set; }


        public MainWindow() {
            InitializeComponent();

            _MouseCtrl = new MouseControl(this);
            _SongCan = new SongCanvas(this, cnv_SongCan, cnv_Measures, _MouseCtrl._noteCtrl);
            _GUI = new GUI(this, Cnv_GUI, _SongCan);
            _FormSongSelect = new FormSongSelect(this);
            _HotCtrl = new HotkeyControl(this);
            _ScreenCap = new ScreenCapturer(cnv_SongCan);
            _vers = new Version();

            InitFormNoteColour();

            TestModal();

            //temp for testing, delays until Loaded
            Loaded += delegate
            {
                _ScreenCap.SaveScreenShot();
            };

        }
        
        private void InitFormNoteColour() {
            _FomrNoteColour = new NoteColourForm(cnv_NoteColourButtons, _MouseCtrl._noteCtrl._colourctrl);

            var tempList = new List<Canvas>();
            tempList.Add(cbtn_NoteColour_None);
            tempList.Add(cbtn_NoteColour_Brown);
            tempList.Add(cbtn_NoteColour_Teal);
            tempList.Add(cbtn_NoteColour_Blue);

            _FomrNoteColour.Init(tempList);
        }

        public void TestModal() {
            var testsong = new SongData(0) {

            };

            var testmeasure = new MeasureData(testsong,MeasureSize.twelve) {

            };
            for (int i = 0; i < 14; i++) {
                for (int o = 0; o < (int)MeasureSize.twelve; o++) {
                    testmeasure.WhiteNotes[i, o].SetColour(OctaveColour.none);
                }
            }

            testmeasure.WhiteNotes[0, 0].SetColour(OctaveColour.Brown);
            testmeasure.WhiteNotes[2, 7].SetColour(OctaveColour.Brown);
            testmeasure.WhiteNotes[13, 9].SetColour(OctaveColour.Green);
            testmeasure.WhiteNotes[8, 2].SetColour(OctaveColour.Yellow);
            testmeasure.WhiteNotes[10, 10].SetColour(OctaveColour.Brown);

            testsong.Measures.Push(testmeasure);


            _SongCan.LoadSong(testsong);

        }
        
    }
}
