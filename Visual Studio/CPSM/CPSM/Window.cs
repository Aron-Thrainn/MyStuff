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
        public NoteCreator _Creator { get; set; }
        public int Debugvar { get; set; }
        public bool Creating { get; set; }

        public MouseNoteControl( ) {
            _colourctrl = new MouseNoteColour();
            Debugvar = 0;
        }
        
        public void NoteLeftClickedDown(NoteViewModal sender, MouseButtonEventArgs e, Point f_mousepos) {
            var f_HeldNote = new NoteTemplate(_colourctrl, f_mousepos);
            _Creator = new NoteCreator(f_HeldNote, sender, false);
            Creating = true;
        }
        public void NoteLeftClickedUp(NoteViewModal sender, MouseButtonEventArgs e, Point f_mousepos) {
            if (Creating) {
                _Creator.Activate();
                _Creator = null;
                Creating = false;
            }
            else {
                _Preview.Activate();
                MakePreview(sender, f_mousepos);
            }
        }
        public void NoteRightClickedDown(NoteViewModal sender, MouseButtonEventArgs e, Point f_mousepos) {
            sender.ClearNote();
        }
        public void NoteRightClickedUp(NoteViewModal sender, MouseButtonEventArgs e, Point f_mousepos) {
            
        }
        public void NoteMouseEnter(NoteViewModal sender, MouseEventArgs e, Point f_mousepos) {
            //leave triggers before enter when going from one to another
            if (Creating) {
                _Creator.AddNote(sender);
            }
            else if (e.RightButton == MouseButtonState.Pressed) {
               sender.ClearNote();
           }
           else {
                MakePreview(sender, f_mousepos);
           }
        }
        public void NoteMouseLeave(NoteViewModal sender, MouseEventArgs e, Point f_mousepos) {
            //leave triggers before enter when going from one to another
            if (Creating == false) {
                if (_Preview != null) {
                    _Preview.Cancel();
                    _Preview = null;
                }
            }
        }

        public void MakePreview(NoteViewModal sender, Point f_mousepos) {
            var f_HeldNote = new NoteTemplate(_colourctrl, f_mousepos);
            _Preview = new NotePreview(sender, f_HeldNote, false);
            if (_Preview.PreviewTemplate != null) {
                sender.SetPreview(_Preview.PreviewTemplate);
            }
        }
    }

    public class NotePreview
    {
        public NoteTemplate PreviewTemplate { get; set; }
        public NoteViewModal Note { get; set; }
        public bool NoteOverride { get; set; }

        public NotePreview(NoteViewModal f_note, NoteTemplate f_HeldNote, bool f_override) {
            Note = f_note;
            var existingnote = new NoteTemplate(f_note as WhiteNoteViewModal);

            PreviewTemplate = CreatePreview(f_override, f_HeldNote, existingnote);
            Display();
        }

        public void Display() {
            if (PreviewTemplate != null) {
                Note.SetPreview(PreviewTemplate);
            }
        }
        public void Activate() {
            Note.SetColour(PreviewTemplate);
            Note.CounterPart.SetNote(PreviewTemplate);
        }
        public void Cancel() {
            Note.ClearPreview();
        }

        private NoteTemplate CreatePreview(bool f_override, NoteTemplate f_HeldNote, NoteTemplate f_ExistingNote) {
            var f_NewNote = new NoteTemplate();
            var f_overrideNote = f_override;
            var f_HeldCol = f_HeldNote.isUsniform();
            if (f_HeldCol == null) {
                f_overrideNote = true;
            }
            if (f_HeldCol == OctaveColour.none) { //do nothing
                return null;
            }
            if (f_ExistingNote.isUsniform() == OctaveColour.none) {
                return f_HeldNote;
            }

            if (f_overrideNote == false) { //Combine notes, heldnote is simple
                var f_ExistingCol = f_ExistingNote.isUsniform();
                if (f_ExistingCol == null) { //Existing note is complex
                    if (f_ExistingNote.HalfColour(Half.Left) != null && f_ExistingNote.HalfColour(Half.Right) != null) { //note is dual-octival
                        if (f_ExistingNote.HalfColour(Half.Left) == f_HeldCol) { //making it uniform
                            f_NewNote.SetColour(f_HeldCol.Value);
                            return f_NewNote;
                        }
                        else if (f_ExistingNote.HalfColour(Half.Right) == f_HeldCol) { //making it uniform
                            f_NewNote.SetColour(f_HeldCol.Value);
                            return f_NewNote;
                        }
                        else { //override existing note
                            f_NewNote.SetColour(f_HeldCol.Value);
                            return f_NewNote;
                        }

                    }
                }
                else { //Held note & Existing note are simple
                    if ((int)f_ExistingCol == (int)f_HeldCol - 1) { //held colour is to the right of existing note
                        f_NewNote.SetHalfColour(Half.Left, f_ExistingCol.Value);
                        f_NewNote.SetHalfColour(Half.Right, f_HeldCol.Value);
                        return f_NewNote;
                    }
                    else if ((int)f_ExistingCol == (int)f_HeldCol + 1) { //held colour is to the left of existing note
                        f_NewNote.SetHalfColour(Half.Left, f_HeldCol.Value);
                        f_NewNote.SetHalfColour(Half.Right, f_ExistingCol.Value);
                        return f_NewNote;
                    }
                    else {  //held & existing colours are not next to each other
                        f_NewNote.SetColour(f_HeldCol.Value);
                        return f_NewNote;
                    }
                }
            }
            else {
                return f_HeldNote;
            }
            return null;
        }
    }

    public class NoteCreator
    {
        public Stack<NotePreview> NewNotes { get; set; }
        public NoteTemplate HeldNote { get; set; }
        public bool Override { get; set; }

        public NoteCreator(NoteTemplate f_heldNote, NoteViewModal f_startingnote, bool f_override) {
            NewNotes = new Stack<NotePreview>();
            HeldNote = f_heldNote;
            Override = f_override;

            PushNote(f_startingnote);
        }

        public void AddNote(NoteViewModal f_existingnote) {
            var f_notey = f_existingnote.NoteCan.Margin.Top;
            var f_notex = f_existingnote.NoteCan.Margin.Left;
            var f_preview = NewNotes.Peek();

            if (f_preview.Note.NoteCan.Margin.Left == f_notex) {
                if (f_preview.Note.NoteCan.Margin.Top > f_notey) {
                    while (true) {
                        if (NewNotes.Count > 1) {
                            PopNote();
                            f_preview = NewNotes.Peek();
                        }
                        else break; // only starting note left
                    }
                }
                else if (f_preview.Note.NoteCan.Margin.Top >= f_notey) {
                    bool exists = false;
                    foreach (var preview in NewNotes) {
                        if (preview.Note == f_existingnote) {
                            exists = true;
                            break;
                        }
                    }
                    if (exists == false) {
                        PushNote(f_existingnote);
                    }
                }
            }
        }
        public void PushNote(NoteViewModal f_existingnote) {
            NewNotes.Push(new NotePreview(f_existingnote, HeldNote, Override));
        }
        public void PopNote() {
            var f_preview = NewNotes.Pop();
            f_preview.Cancel();
        }
        public void Activate() {
            while(NewNotes.Count != 0) {
                var f_preview = NewNotes.Pop();
                f_preview.Activate();
            }
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
