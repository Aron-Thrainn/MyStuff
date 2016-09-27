using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Interop;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Input;
using CPSM.ViewModals;
using CPSM.Forms;
using System.Threading;
using System.Windows.Threading;

namespace CPSM
{
    #region Main UI Elements
    public class GUI {
        public MainWindow _Window { get; set; }
        public Canvas Can { get; set; }
        public SongCanvas _SongCan { get; set; }

        public GUI(MainWindow f_wind, Canvas f_can, SongCanvas f_songcan) {
            _Window = f_wind;
            Can = f_can;
        }
        
    }
    
    public class SongCanvas {
        public Canvas SongCan { get; set; }
        public Canvas MeasuresCan { get; set; }
        public MainWindow _Window { get; set; }
        public List<MeasureViewModal> Measures { get; set; }
        public SongViewModalCreator _Creator { get; set; }
        public NoteDisplay _NoteDisp { get; set; }
        public SongData ActiveSongData { get; set; }

        public SongCanvas(MainWindow f_wind, Canvas f_songcan, Canvas f_measurescan, MouseNoteControl f_mousectrl) {
            _Window = f_wind;
            SongCan = f_songcan;
            _Creator = new SongViewModalCreator(f_measurescan, f_mousectrl);
        }

        public void LoadSong(SongData f_song) {
            ActiveSongData = f_song;
            _Creator.LoadSong(f_song);
        }
        public void CreateNoteDisplay(Canvas f_can) {
            _NoteDisp = new NoteDisplay(f_can);
        }

        public class NoteDisplay
        {
            public NoteDisplay(Canvas f_candisplay) {
                throw new NotImplementedException();
                for (int i = 0; i < 8; i++) {
                    //create image & set position
                }
            }
        }
    }
    #endregion
    #region Mouse Control
    public class MouseControl {
        public MainWindow _Window { get; set; }
        public MouseNoteControl _noteCtrl { get; set; }

        public MouseControl(MainWindow f_wind) {
            _Window = f_wind;
            _noteCtrl = new MouseNoteControl();
        }
    }
    public enum PartialNote
    {
        Full,
        Half,
        Quarter,
        Eighth
    }
    public class MouseNoteColour
    {
        public OctaveColour ActiveColour { get; set; }
        public PartialNote ActivePatrial { get; set; } 

        public MouseNoteColour() {
            ActiveColour = OctaveColour.none;
            ActivePatrial = PartialNote.Full;
        }
    }
    public class MouseNoteCopyTemplate
    {
        public NoteTemplate Template { get; set; }
    }

    public class MouseNoteControl
    {
        public MouseNoteColour _colourctrl { get; set; }
        public NotePreview _Preview { get; set; }
        public int Debugvar { get; set; }

        public MouseNoteControl( ) {
            _colourctrl = new MouseNoteColour();
            Debugvar = 0;
        }
        
        public void NoteLeftClickedDown(object sender, MouseButtonEventArgs e, Point f_mousepos, NoteViewModal f_modal) {
            f_modal.SetColour(_colourctrl.ActiveColour, _colourctrl.ActivePatrial);
        }
        public void NoteLeftClickedUp(object sender, MouseButtonEventArgs e, Point f_mousepos, NoteViewModal f_modal) {
            throw new NotImplementedException();
        }
        public void NoteRightClickedDown(object sender, MouseButtonEventArgs e, Point f_mousepos, NoteViewModal f_modal) {
            f_modal.ClearNote();
        }
        public void NoteRightClickedUp(object sender, MouseButtonEventArgs e, Point f_mousepos, NoteViewModal f_modal) {

        }
        public void NoteMouseEnter(object sender, MouseEventArgs e, Point f_mousepos, NoteViewModal f_modal) {
            //leave triggers before enter when going from one to another
            if (e.RightButton == MouseButtonState.Pressed) {
               f_modal.ClearNote();
           }
           else {
               var newNote = new NoteTemplate(_colourctrl, f_mousepos);
               _Preview = new NotePreview(this, f_modal, newNote, false);
           }
        }
        public void NoteMouseLeave(object sender, MouseEventArgs e, Point f_mousepos, NoteViewModal f_modal) {
            //leave triggers before enter when going from one to another
            _Preview = null;
        }

    }

    public class NotePreview
    {
        public NoteTemplate PreviewTemplate { get; set; }
        public NoteViewModal Note { get; set; }
        public bool NoteOverride { get; set; }
        public MouseNoteControl Parent { get; set; }

        public NotePreview(MouseNoteControl f_parent, NoteViewModal f_note, NoteTemplate f_NewNote, bool f_override) {
            Parent = f_parent;
            Note = f_note;
            var oldnote = new NoteTemplate(f_note as WhiteNoteViewModal);

            PreviewTemplate = CreatePreview(f_override, f_NewNote, oldnote);
        }

        public void Activate() {
            Note.SetColour(PreviewTemplate);
        }

        private NoteTemplate CreatePreview(bool f_override, NoteTemplate f_NewNote, NoteTemplate f_OldNote) {
            throw new NotImplementedException();
        }

    }
    #endregion

    #region Hotkey Control
    public class HotkeyControl {
        public MainWindow _Window { get; set; }

        public HotkeyControl(MainWindow f_wind) {
            _Window = f_wind;
        }
    }
    #endregion

    #region Misc
    public class ScreenCapturer {
        public Canvas SongCan { get; set; }
        public string FilePath { get; set; }

        
        public ScreenCapturer(Canvas f_SongCan) {
            //Saves in project folder
            //FilePath = new FileInfo("TempImage.png").FullName.ToString();
            //Saves to desktop
            FilePath = "C:\\Users\\Notandi\\Desktop\\TempImage.png";
            SongCan = f_SongCan;
        }

        public void SaveScreenShot() {
            PresentationSource source = PresentationSource.FromVisual(SongCan);
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)SongCan.RenderSize.Width,
                  (int)SongCan.RenderSize.Height, 96, 96, PixelFormats.Default);

            VisualBrush sourceBrush = new VisualBrush(SongCan);
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            using (drawingContext) {
                drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0),
                      new Point(SongCan.RenderSize.Width, SongCan.RenderSize.Height)));
            }
            rtb.Render(drawingVisual);


            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(rtb));
            using (Stream fileStream = File.Create(FilePath)) {
                pngImage.Save(fileStream);
            }
            
        }
        
    }

    public class Version
    {
        public string CurrentVersion { get; set; }

        public Version() {
            CurrentVersion = "Pre-Alpha";
        }
    }
    #endregion
}
