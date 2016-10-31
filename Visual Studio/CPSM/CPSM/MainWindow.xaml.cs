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
    Make measures fill out multiple columns
    handle rightclick while creating a long note



    Was working on save song & hotkeys last time

    Bugs: 
    
*/
/*
    Done:
    change songdata data structure
    Add numbers to measures
    global mouse event handler
    Hotkey Control
    Black notes


*/




namespace CPSM
{
    public partial class MainWindow : Window {
        public GUI _GUI { get; set; }
        public SongCanvas _SongCan { get; set; }
        public FormSongSelect _FormSongSelect { get; set; }
        public MouseControl _MouseCtrl { get; set; }
        public ScreenCapturer _ScreenCap { get; set; }
        public Version _vers { get; set; }
        private Hotkeycontrol _keyCtrl { get; set; }



        public MainWindow() {
            InitializeComponent();

            _MouseCtrl = new MouseControl(this);
            var f_tempcreator = new SongViewModalCreator(cnv_Measures, _MouseCtrl._noteCtrl, lbl_SongName, lbl_SongSource, lbl_SongVersion);
            _SongCan = new SongCanvas(this, cnv_SongCan, f_tempcreator);
            _GUI = new GUI(this, Cnv_GUI, _SongCan);
            _FormSongSelect = new FormSongSelect(this);
            _ScreenCap = new ScreenCapturer(cnv_SongCan);
            _vers = new Version();
            _keyCtrl = new Hotkeycontrol(this);

            _SongCan.InitializeNoteDisplay(cnv_DisplayBox);
    
            InitFormNoteColour();
            InitFormMeasureCreator();

            TestModal();

            PreviewKeyDown += new KeyEventHandler(HotkeysDown);
            PreviewKeyUp += new KeyEventHandler(HotkeysUp);

            PreviewMouseLeftButtonUp += new MouseButtonEventHandler(MouseButtonUp);

            //temp for testing, delays until Loaded
            //enter to take screenshot
            /*Loaded += delegate
            {
                _ScreenCap.SaveScreenShot();
            };*/

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

            _GUI.initNoteColourForm(f_FormNoteColour, f_tempList);
        }
        private void InitFormMeasureCreator() {
            var f_measurecreatorform = new MeasureCreatorForm(cnv_MeasureCreatorForm, cbtn_createmeasure ,cbtn_deletemeasure, cbo_measuresizes);
            _GUI.initMeasureCreatorForm(f_measurecreatorform);            
        }
        public void TestModal() {
            var testsong = new SongData(0) {
                Name = "We Stand for Everfree",
                Source = "Legend of Everfree"
            };

            var testmeasure = new MeasureData(testsong, MeasureSize.twelve) {
            };
            var testmeasure2 = new MeasureData(testsong, MeasureSize.four) {
            };

            for (int i = 0; i < 14; i++) {
                for (int o = 0; o < (int)MeasureSize.twelve; o++) {
                    testmeasure.WhiteNotes[i, o].SetColour(OctaveColour.none);
                }
            }
            for (int i = 0; i < 14; i++) {
                for (int o = 0; o < (int)MeasureSize.four; o++) {
                    testmeasure2.WhiteNotes[i, o].SetColour(OctaveColour.none);
                }
            }
            
            testmeasure.WhiteNotes[0, 0].SetColour(OctaveColour.Brown);
            testmeasure.WhiteNotes[0, 1].SetColour(OctaveColour.Teal);
            testmeasure.WhiteNotes[0, 2].SetColour(OctaveColour.Blue);
            testmeasure.WhiteNotes[0, 3].SetColour(OctaveColour.Green);
            testmeasure.WhiteNotes[0, 4].SetColour(OctaveColour.Red);
            testmeasure.WhiteNotes[0, 5].SetColour(OctaveColour.Yellow);
            testmeasure.WhiteNotes[0, 6].SetColour(OctaveColour.Yellow);
            testmeasure.WhiteNotes[13, 9].SetColour(OctaveColour.Green);
            testmeasure.WhiteNotes[8, 10].SetColour(OctaveColour.Yellow);
            
            for (int i=0; i<14; i++) {
                testmeasure.WhiteNotes[i, 11].SetColour(OctaveColour.Purple);
                testmeasure2.WhiteNotes[i, 3].SetColour(OctaveColour.Purple);
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
            notetemp2.Colours[15] = OctaveColour.Yellow;

            testmeasure.WhiteNotes[10, 5].SetColour(notetemp1);
            testmeasure.WhiteNotes[10, 6].SetColour(notetemp2);


            testmeasure.WhiteNotes[1, 0].SetColour(OctaveColour.Brown);
            testmeasure.WhiteNotes[2, 0].SetColour(OctaveColour.Brown);
            testmeasure.WhiteNotes[3, 0].SetColour(OctaveColour.Brown);
            testmeasure.WhiteNotes[4, 0].SetColour(OctaveColour.Brown);
            testmeasure.WhiteNotes[5, 0].SetColour(OctaveColour.Brown);
            testmeasure.WhiteNotes[6, 0].SetColour(OctaveColour.Brown);
            testmeasure.WhiteNotes[7, 0].SetColour(OctaveColour.Brown);
            testmeasure.WhiteNotes[8, 0].SetColour(OctaveColour.Brown);
            testmeasure.WhiteNotes[1, 1].SetColour(OctaveColour.Brown);
            testmeasure.WhiteNotes[2, 2].SetColour(OctaveColour.Brown);
            testmeasure.WhiteNotes[3, 3].SetColour(OctaveColour.Brown);
            testmeasure.WhiteNotes[4, 4].SetColour(OctaveColour.Brown);
            testmeasure.WhiteNotes[5, 5].SetColour(OctaveColour.Brown);
            testmeasure.WhiteNotes[6, 6].SetColour(OctaveColour.Brown);
            testmeasure.WhiteNotes[7, 7].SetColour(OctaveColour.Brown);
            testmeasure.WhiteNotes[8, 8].SetColour(OctaveColour.Brown);


            
            testsong.Measures.Add(testmeasure);
            testsong.Measures.Add(testmeasure2);

            _SongCan.LoadSong(testsong);
            //Space to save to .txt
            //_SongCan.SaveSong();

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
                _window._ScreenCap.SaveScreenShot();
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
