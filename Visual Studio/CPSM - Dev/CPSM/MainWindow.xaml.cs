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
    Drawing long partial notes
    Add Export Dialog funcion
    Add Copy/Paste functions
    prettify the load song select
    Name & Source changing
    Zooming
    Ctrl and Shift modifiers

    Minor:
    dont allow it to load if its already loading a song
    hotkey partial notes


    Bugs:
    first button icon size is incorrect
    preview flikkers with partial notes
    loading sometimes doesn't work if that song is already loaded
    dragging a note above its start makes the start an extension... feature??
*/
/*
    Done:
    smooth page turning
    change songdata data structure
    Add numbers to measures
    global mouse event handler
    Hotkey Control
    Black notes
    Rework note initialization
    Make Copy notes (middle click) appear in a button icon
    handle rightclick while creating a long note
    Smooth out long note creation
    copy note icon is black & white if note is simple
    Rework note images to have only 1 image
    Make measures fill out multiple columns
    Multiple Pages
    Partial Notes
    Add Save & load functions - export all pages at a time
    Add dates to saves

*/




namespace CPSM
{
    public partial class MainWindow : Window
    {
        public GUI _GUI { get; set; }
        public SongCanvas _SongCan { get; set; }
        public MouseControl _MouseCtrl { get; set; }
        public ScreenCapturer _ScreenCap { get; set; }
        private Hotkeycontrol _keyCtrl { get; set; }



        public MainWindow() {
            InitializeComponent();

            _MouseCtrl = new MouseControl(this);
            var f_tempcreator = new SongViewModalCreator(cnv_Measures, _MouseCtrl._noteCtrl, lbl_SongName, lbl_SongSource, lbl_SongVersion, lbl_PageNumber, _MouseCtrl._noteCtrl._NoteImageConrtol);
            _SongCan = new SongCanvas(this, cnv_SongCan, f_tempcreator);
            _GUI = new GUI(this, Cnv_GUI, _SongCan);
            _ScreenCap = new ScreenCapturer(cnv_SongCan, _MouseCtrl, _SongCan._Creator);
            _keyCtrl = new Hotkeycontrol(this);

            _SongCan.InitializeNoteDisplay(cnv_DisplayBox);

            InitFormSaveLoad();
            InitFormNoteColour();
            InitFormMeasureCreator();

            _GUI.initPagesForm(cbtn_Page_Next, cbtn_Page_Previous);
            _GUI.initVersionForm(lbl_SongVersion, new Version());

            _MouseCtrl._noteCtrl._Form = _GUI._FormNoteColour;

            TestModal();

            PreviewKeyDown += new KeyEventHandler(HotkeysDown);
            PreviewKeyUp += new KeyEventHandler(HotkeysUp);

            PreviewMouseLeftButtonUp += new MouseButtonEventHandler(MouseButtonUp);
        }

        private void HotkeysDown(object sender, KeyEventArgs e) {

            switch (e.Key) {
                case Key.Q: {
                    _GUI._FormNoteColour.SimulateClickDown(OctaveColour.Brown);
                    break;
                }
                case Key.W: {
                    _GUI._FormNoteColour.SimulateClickDown(OctaveColour.Teal);
                    break;
                }
                case Key.E: {
                    _GUI._FormNoteColour.SimulateClickDown(OctaveColour.Blue);
                    break;
                }
                case Key.R: {
                    _GUI._FormNoteColour.SimulateClickDown(OctaveColour.Green);
                    break;
                }
                case Key.T: {
                    _GUI._FormNoteColour.SimulateClickDown(OctaveColour.Red);
                    break;
                }
                case Key.Y: {
                    _GUI._FormNoteColour.SimulateClickDown(OctaveColour.Purple);
                    break;
                }
                case Key.U: {
                    _GUI._FormNoteColour.SimulateClickDown(OctaveColour.Yellow);
                    break;
                }
            }
        }
        private void HotkeysUp(object sender, KeyEventArgs e) {

            switch (e.Key) {
                case Key.Q: {
                    _GUI._FormNoteColour.SimulateClickUp(OctaveColour.Brown);
                    break;
                }
                case Key.W: {
                    _GUI._FormNoteColour.SimulateClickUp(OctaveColour.Teal);
                    break;
                }
                case Key.E: {
                    _GUI._FormNoteColour.SimulateClickUp(OctaveColour.Blue);
                    break;
                }
                case Key.R: {
                    _GUI._FormNoteColour.SimulateClickUp(OctaveColour.Green);
                    break;
                }
                case Key.T: {
                    _GUI._FormNoteColour.SimulateClickUp(OctaveColour.Red);
                    break;
                }
                case Key.Y: {
                    _GUI._FormNoteColour.SimulateClickUp(OctaveColour.Purple);
                    break;
                }
                case Key.U: {
                    _GUI._FormNoteColour.SimulateClickUp(OctaveColour.Yellow);
                    break;
                }
            }
            _MouseCtrl._noteCtrl.UpdatePreview();
        }
        private void MouseButtonUp(object sender, MouseEventArgs e) {
            _MouseCtrl._noteCtrl.GlobalMouseUp();
        }

        private void InitFormNoteColour() {
            var f_FormNoteColour = new NoteColourForm(cnv_NoteColourButtons, _MouseCtrl._noteCtrl._colourctrl);

            var f_tempList = new List<Canvas>();
            f_tempList.Add(cbtn_NoteColour_None);
            f_tempList.Add(cbtn_NoteColour_Brown);
            f_tempList.Add(cbtn_NoteColour_Teal);
            f_tempList.Add(cbtn_NoteColour_Blue);
            f_tempList.Add(cbtn_NoteColour_Green);
            f_tempList.Add(cbtn_NoteColour_Red);
            f_tempList.Add(cbtn_NoteColour_Purple);
            f_tempList.Add(cbtn_NoteColour_Yellow);

            _GUI.initNoteColourForm(f_FormNoteColour, f_tempList, cbo_NoteSize);
        }
        private void InitFormMeasureCreator() {
            var f_measurecreatorform = new MeasureCreatorForm(cnv_MeasureCreatorForm, cbtn_createmeasure, cbtn_deletemeasure, cbo_measuresizes);
            _GUI.initMeasureCreatorForm(f_measurecreatorform);
        }

        private void InitFormSaveLoad() {
            var f_LoadSongSelect = new LoadSongSelect(cnv_SongLoadSelect, cbtn_LoadSong);
            _GUI._SaveLoadForm = new SaveAndLoadForm(cbtn_SaveSong, cbtn_LoadSong, f_LoadSongSelect, _GUI._SongCan);
            f_LoadSongSelect.LinkParent(_GUI._SaveLoadForm);
        }

        public void TestModal() {
            var testsong = new SongData() {
                Title = "TestTitle",
                Source = "TestSource"
            };

            var testmeasure = new MeasureData(testsong, MeasureSize.twelve) {
            };
            var testmeasure2 = new MeasureData(testsong, MeasureSize.four) {
            };

            for (int i = 0; i < 14; i++) {
                for (int o = 0; o < (int)MeasureSize.twelve; o++) {
                    testmeasure.WhiteNotes[i, o].SetNote(OctaveColour.none);
                }
            }
            for (int i = 0; i < 14; i++) {
                for (int o = 0; o < (int)MeasureSize.four; o++) {
                    testmeasure2.WhiteNotes[i, o].SetNote(OctaveColour.none);
                }
            }

            testmeasure.WhiteNotes[0, 0].SetNote(OctaveColour.Brown);
            testmeasure.WhiteNotes[0, 1].SetNote(OctaveColour.Teal);
            testmeasure.WhiteNotes[0, 2].SetNote(OctaveColour.Blue);
            testmeasure.WhiteNotes[0, 3].SetNote(OctaveColour.Green);
            testmeasure.WhiteNotes[0, 4].SetNote(OctaveColour.Red);
            testmeasure.WhiteNotes[0, 5].SetNote(OctaveColour.Yellow);
            testmeasure.WhiteNotes[0, 6].SetNote(OctaveColour.Yellow);
            testmeasure.WhiteNotes[13, 9].SetNote(OctaveColour.Green);
            testmeasure.WhiteNotes[8, 10].SetNote(OctaveColour.Yellow);

            for (int i = 0; i < 14; i++) {
                testmeasure.WhiteNotes[i, 11].SetNote(OctaveColour.Purple);
                testmeasure2.WhiteNotes[i, 3].SetNote(OctaveColour.Purple);
            }


            var notetemp1 = new NoteTemplate();
            notetemp1.SetHalfColour(Half.Left, OctaveColour.Teal);
            notetemp1.SetHalfColour(Half.Right, OctaveColour.Blue);

            var notetemp2 = new NoteTemplate();
            for (int i = 0; i < 16; i++) {
                notetemp2.Colours[i] = OctaveColour.Blue;
                notetemp2.Positions[i] = (NoteBitPos)i;
            }
            notetemp2.Positions[4] = NoteBitPos.a1;
            notetemp2.Positions[5] = NoteBitPos.a2;
            notetemp2.Positions[6] = NoteBitPos.a3;
            notetemp2.Positions[7] = NoteBitPos.a4;
            notetemp2.Positions[12] = NoteBitPos.b1;
            notetemp2.Positions[13] = NoteBitPos.b2;
            notetemp2.Positions[14] = NoteBitPos.b3;
            notetemp2.Positions[15] = NoteBitPos.b4;

            var notetemp3 = new NoteTemplate();
            notetemp3.SetColour(OctaveColour.Purple);
            notetemp3.SetAsExtension();


            testmeasure.WhiteNotes[10, 5].SetNote(notetemp1);
            testmeasure.WhiteNotes[10, 6].SetNote(notetemp2);

            testmeasure.WhiteNotes[10, 7].SetNote(OctaveColour.Purple);
            testmeasure.WhiteNotes[10, 8].SetNote(notetemp3);


            testmeasure.WhiteNotes[1, 0].SetNote(OctaveColour.Brown);
            testmeasure.WhiteNotes[2, 0].SetNote(OctaveColour.Brown);
            testmeasure.WhiteNotes[3, 0].SetNote(OctaveColour.Brown);
            testmeasure.WhiteNotes[4, 0].SetNote(OctaveColour.Brown);
            testmeasure.WhiteNotes[5, 0].SetNote(OctaveColour.Brown);
            testmeasure.WhiteNotes[6, 0].SetNote(OctaveColour.Brown);
            testmeasure.WhiteNotes[7, 0].SetNote(OctaveColour.Brown);
            testmeasure.WhiteNotes[8, 0].SetNote(OctaveColour.Brown);
            testmeasure.WhiteNotes[1, 1].SetNote(OctaveColour.Brown);
            testmeasure.WhiteNotes[2, 2].SetNote(OctaveColour.Brown);
            testmeasure.WhiteNotes[3, 3].SetNote(OctaveColour.Brown);
            testmeasure.WhiteNotes[4, 4].SetNote(OctaveColour.Brown);
            testmeasure.WhiteNotes[5, 5].SetNote(OctaveColour.Brown);
            testmeasure.WhiteNotes[6, 6].SetNote(OctaveColour.Brown);
            testmeasure.WhiteNotes[7, 7].SetNote(OctaveColour.Brown);
            testmeasure.WhiteNotes[8, 8].SetNote(OctaveColour.Brown);



            testsong.Measures.Add(testmeasure);
            testsong.Measures.Add(testmeasure2);

            //_SongCan.LoadSong(testsong.Title, testsong.Source);
            _SongCan.LoadSongFromData(testsong);

        }

    }


    public class Hotkeycontrol
    {
        public MainWindow _window { get; set; }

        #region Command Objects
        public class SpaceCommand : ICommand
        {
            public MainWindow _window { get; set; }
            public event EventHandler CanExecuteChanged;

            public SpaceCommand(MainWindow f_window) {
                _window = f_window;
            }

            public bool CanExecute(object parameter) {
                return true;
            }

            public void Execute(object parameter) {
                //hotkey action
                _window._SongCan.SaveSong();
            }
        }

        public class EnterCommand : ICommand
        {
            public MainWindow _window { get; set; }
            public event EventHandler CanExecuteChanged;

            public EnterCommand(MainWindow f_window) {
                _window = f_window;
            }
            public bool CanExecute(object parameter) {
                return true;
            }

            public void Execute(object parameter) {
                //hotkey action
                //_window._ScreenCap.SaveScreenShot();
                _window._ScreenCap.ExportAllPages();
            }
        }

        public class EscCommand : ICommand
        {
            public MainWindow _window { get; set; }
            public event EventHandler CanExecuteChanged;

            public EscCommand(MainWindow f_window) {
                _window = f_window;
            }
            public bool CanExecute(object parameter) {
                return true;
            }

            public void Execute(object parameter) {
                //hotkey action
                _window.Close();
            }
        }


        #endregion
        /*  Uses
         * escape to close app
         */

        public Hotkeycontrol(MainWindow f_wind) {
            _window = f_wind;
            SpaceFunc();
            EnterFunc();
            EscFunc();



        }

        private void SpaceFunc() {
            SpaceCommand Testcommand = new SpaceCommand(_window);

            KeyGesture OpenKeyGesture = new KeyGesture(
                Key.Space,
                ModifierKeys.None);

            KeyBinding OpenCmdKeybinding = new KeyBinding(
                Testcommand,
                OpenKeyGesture);

            _window.InputBindings.Add(OpenCmdKeybinding);
        }
        private void EscFunc() {
            EscCommand Testcommand = new EscCommand(_window);

            KeyGesture OpenKeyGesture = new KeyGesture(
                Key.Escape,
                ModifierKeys.None);

            KeyBinding OpenCmdKeybinding = new KeyBinding(
                Testcommand,
                OpenKeyGesture);

            _window.InputBindings.Add(OpenCmdKeybinding);
        }
        private void EnterFunc() {
            EnterCommand Testcommand = new EnterCommand(_window);

            KeyGesture OpenKeyGesture = new KeyGesture(
                Key.Enter,
                ModifierKeys.None);

            KeyBinding OpenCmdKeybinding = new KeyBinding(
                Testcommand,
                OpenKeyGesture);

            _window.InputBindings.Add(OpenCmdKeybinding);
        }
    }
}
