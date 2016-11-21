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
using CommonClasses.Images;

namespace CPSM
{
    #region Main UI Elements
    public class GUI {
        public MainWindow _Window { get; set; }
        public Canvas Can { get; set; }
        public SongCanvas _SongCan { get; set; }
        public NoteColourForm _FormNoteColour { get; set; }
        public MeasureCreatorForm _FormMeasureCreator { get; set; }

        public GUI(MainWindow f_wind, Canvas f_can, SongCanvas f_songcan) {
            _Window = f_wind;
            Can = f_can;
            _SongCan = f_songcan;
        }

        public void initNoteColourForm(NoteColourForm f_form ,List<Canvas> f_cans) {
            _FormNoteColour = f_form;
            _FormNoteColour.Init(f_cans);
        }
        public void initMeasureCreatorForm(MeasureCreatorForm f_form) {
            _FormMeasureCreator = f_form;
            _FormMeasureCreator._Song = _SongCan;
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

        public SongCanvas(MainWindow f_wind, Canvas f_songcan, SongViewModalCreator f_creator) {
            _Window = f_wind;
            SongCan = f_songcan;
            _Creator = f_creator;
        }

        public void LoadSong(SongData f_song) {
            ActiveSongData = f_song;
            _Creator.LoadSong(f_song);
        }
        public void SaveSong() {
            var saver = new SongSaver(ActiveSongData);
        }
        public void CreateNoteDisplay(Canvas f_can) {
            _NoteDisp = new NoteDisplay(f_can);
        }
        public void AddNewMeasure(MeasureSize f_size) {
            ActiveSongData.AddMeasure(f_size);
            var f_tempmes = new MeasureData(ActiveSongData, f_size);
            _Creator.CreateMeasure(f_tempmes, true);
        }
        public void DeleteMeasure() {
            ActiveSongData.DeleteMeasure();
            _Creator.DeleteMeasure();
        }
        public void InitializeNoteDisplay(Canvas f_can) {
            _NoteDisp = new NoteDisplay(f_can);
        }

        

        public class SongSaver {

            public int i { get; set; }
            public int o { get; set; }
            public int count { get; set; }
            public int currMesSize { get; set; }
            public int state { get; set; }
            public NoteTemplate prevNote { get; set; }

            #region static chars
            private static string TITLE = "T";
            private static string SOURCE = "S";
            private static string MEASURE = "M";
            private static string NOTE = "N";
            private static string BIT = "B";
            private static string DUPLICATE = "X";
            private static string OPEN = "(";
            private static string CLOSE = ")";


            private static string O = "o";
            private static string Q = "q";
            private static string W = "w";
            private static string E = "e";
            private static string R = "r";
            private static string T = "t";
            private static string Y = "y";
            private static string U = "u";

            //a & b reserved for note bit pos


            #endregion



            public SongSaver(SongData f_Song) {
                using (StreamWriter file = new StreamWriter(@"C:\\Users\\Notandi\\Desktop\\temptxt.txt")) {

                    file.Write(TITLE + OPEN + f_Song.Name + CLOSE);
                    file.Write(SOURCE + OPEN + f_Song.Source + CLOSE);

                    foreach (var measure in f_Song.Measures) {
                        currMesSize = (int)measure.Size;
                        file.Write(MEASURE);
                        if ((int)measure.Size < 10) {
                            file.Write(0);
                            file.Write(currMesSize + OPEN);
                        }
                        else file.Write(currMesSize + OPEN);
                        
                        i = 0;
                        o = 0;
                        count = 0;
                        state = 0;
                        // 0 = norm, 1 = counting 0s, 2 = counting non-0s
                        while (true) {
                            if (i >= 14) { // reached end of this measure
                                file.Write(WriteNote(prevNote, count));
                                break;
                            }
                            var temp = new NoteTemplate(measure.WhiteNotes[i, o]);
                            var tempcol = temp.isUsniform();

                            switch (state) {
                                case 0: {
                                        if (tempcol == OctaveColour.none) { // note is empty
                                            state = 1;
                                        }
                                        else { // note is not empty
                                            state = 2;
                                        }
                                        prevNote = temp;
                                        count = 1;
                                        IncrimentNote();
                                        break;
                                    }
                                case 1: { // note is empty
                                        if (tempcol == OctaveColour.none) {
                                            count++;
                                            IncrimentNote();
                                            break;
                                        }
                                        else {
                                            file.Write(WriteNote(prevNote, count));
                                            state = 0;
                                        }
                                        break;
                                    }
                                case 2: { // note is not empty
                                        var tempprev = prevNote.GetAsExtension();
                                        if (CompareNotes(temp, tempprev)) {
                                            count++;
                                            IncrimentNote(); 
                                            break;
                                        }
                                        else {
                                            file.Write(WriteNote(prevNote, count));
                                            state = 0;
                                        }
                                        break;
                                    }
                            }
                        }
                        file.Write(CLOSE);

                    }
                }
            }

            private void IncrimentNote() {
                o++;
                if (o >= currMesSize) {
                    o = 0;
                    i++;
                }
            }
            private string WrittenNote(NoteTemplate f_note) {
                if (f_note.isUsniform() != null) { // note is simple
                    return GetNoteKey(f_note.isUsniform().Value);
                }
                else if (f_note.HalfColour(Half.Left) != null && f_note.HalfColour(Half.Right) != null) { // note is Dual-octival
                    return NOTE + OPEN + GetNoteKey(f_note.HalfColour(Half.Left).Value) + GetNoteKey(f_note.HalfColour(Half.Right).Value) + CLOSE;
                }
                else { // note is complex
                    var tempstring = new StringBuilder();
                    tempstring.Append(NOTE + OPEN);

                    OctaveColour f_tempcol = f_note.Colours[0];
                    NoteBitPos f_temppos = f_note.Positions[0];
                    tempstring.Append(AppendBit(f_tempcol,f_temppos));
                    int count = 1;



                    for (int i = 1; i < 16; i++) {

                        if (f_note.Colours[i] == f_tempcol && f_note.Positions[i] == (NoteBitPos)((int)f_temppos + 1)) {
                            //note bit is a continuation of previous one
                            count++;
                        }
                        else {
                            if (count != 0) {
                                // writing the duplicate notes
                                tempstring.Append(DUPLICATE + count);
                                count = 1;
                            }
                            //note bit is not a continuation of previous one
                            tempstring.Append(AppendBit(f_note.Colours[i], f_note.Positions[i]));
                        }
                        f_temppos = f_note.Positions[i];
                        f_tempcol = f_note.Colours[i];
                    }

                    tempstring.Append(CLOSE);
                    return tempstring.ToString();

                }
            }
            private string WriteNote(NoteTemplate f_note, int f_count) {
                if (f_count == 1) {
                    return WrittenNote(f_note);
                }
                else if (f_count == 2) {
                    string f_char = WrittenNote(f_note);
                    return f_char + f_char;
                }
                else {
                    return WrittenNote(f_note) + DUPLICATE + count.ToString();
                }
            }
            private string GetNoteKey(OctaveColour f_col) {
                switch (f_col) {
                    case OctaveColour.none: {
                            return O;
                        }
                    case OctaveColour.Brown: {
                            return Q;
                        }
                    case OctaveColour.Teal: {
                            return W;
                        }
                    case OctaveColour.Blue: {
                            return E;
                        }
                    case OctaveColour.Green: {
                            return R;
                        }
                    case OctaveColour.Red: {
                            return T;
                        }
                    case OctaveColour.Purple: {
                            return Y;
                        }
                    case OctaveColour.Yellow: {
                            return U;
                        }
                }
                throw new Exception();
            }
            private string AppendBit(OctaveColour f_oct, NoteBitPos f_pos) {
                string pos;
                string oct = GetNoteKey(f_oct);
                pos = f_pos.ToString();
                return(BIT + OPEN + pos + oct + CLOSE);
            }
            private bool CompareNotes(NoteTemplate f_note1, NoteTemplate f_note2) {
                for (int i=0; i<16; i++) {
                    if (f_note1.Colours[i] != f_note2.Colours[i] || f_note1.Positions[i] != f_note2.Positions[i]) {
                        return false;
                    }
                }
                return true;
            }
        }

        public class NoteDisplay
        {
            public NoteDisplay(Canvas f_candisplay) {
                for (int i = 1; i < 8; i++) {
                    //create image & set position
                    var f_NoteImage = new Image() {
                        Height = 16,
                        Width = 12,
                        Stretch = Stretch.Fill,
                        Source = ImageControl.NoteImg((OctaveColour)i, NoteType.White),
                        Margin = new Thickness(2 + i * 14, 2, 0, 0)
                    };
                    f_candisplay.Children.Add(f_NoteImage);
                    
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
        public MouseNoteControl _Parent { get; set; }

        public MouseNoteColour(MouseNoteControl f_parent) {
            _Parent = f_parent;
            ActiveColour = OctaveColour.none;
            ActivePatrial = PartialNote.Full;
        }

        public void UpdatePreview(OctaveColour f_oct) {
            _Parent.UpdatePreview(f_oct);
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
        public bool Creating { get; set; }

        public MouseNoteControl( ) {
            _colourctrl = new MouseNoteColour(this);
        }
        
        public void NoteLeftClickedDown(NoteViewModal sender, MouseButtonEventArgs e, Point f_mousepos) {
            var f_HeldNote = new NoteTemplate(_colourctrl, f_mousepos);
            _Creator = new NoteCreator(f_HeldNote, sender, false);
            Creating = true;
        }
        public void NoteLeftClickedUp(NoteViewModal sender, MouseButtonEventArgs e, Point f_mousepos) {
            MakePreview(sender, f_mousepos);
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
        //need global mouse enevt handler for _creator to work properly

        public void MakePreview(NoteViewModal sender, Point f_mousepos) {
            var f_HeldNote = new NoteTemplate(_colourctrl, f_mousepos);
            _Preview = new NotePreview(sender, f_HeldNote, false, true);
        }
        public void UpdatePreview(OctaveColour f_oct) {
            var f_tempnote = _Preview.Note;
            var f_temptemplate = new NoteTemplate();
            f_temptemplate.SetColour(f_oct); 
            // sets the heldnote to f_oct colour full note, does not currently work for partial notes

            if (_Preview != null) {
                _Preview.Cancel();
                _Preview = null;
            }
            _Preview = new NotePreview(f_tempnote, f_temptemplate, false, true);
        }
        public void GlobalMouseUp() {
            if (Creating) {
                _Creator.Activate();
                _Creator = null;
                Creating = false;
            }
            else {
            }
        }
    }

    public class NotePreview
    {
        private NoteTemplate PreviewTemplate { get;  set; }
        public NoteViewModal Note { get; set; }
        public bool NoteOverride { get; set; }
        public bool StartPoint { get; set; }
        public Canvas NoteImage { get; set; }

        public NotePreview(NoteViewModal f_note, NoteTemplate f_HeldNote, bool f_override, bool f_startpoint) {
            
            Note = f_note;
            NoteOverride = f_override;
            StartPoint = f_startpoint;
            var existingnote = new NoteTemplate(f_note as NoteViewModal);

            if (!f_startpoint) {
                f_HeldNote.SetAsExtension();
            }


            PreviewTemplate = CreatePreview(f_override, f_HeldNote, existingnote);
            if (PreviewTemplate != null) { CreatePreviewImage(); }
            Display();
        }

        public void Display() {
            if (PreviewTemplate != null) {
                Note.NoteCan.Children.Add(NoteImage);
            }
        }
        public void Activate() {
            if (PreviewTemplate != null) {
                //Note.SetColour(PreviewTemplate);
            }
            Cancel(); //removes the preview image
        }
        public void Cancel() {
            Note.NoteCan.Children.Remove(NoteImage);
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
                return new NoteTemplate(f_HeldNote);
            }

            if (f_ExistingNote.isUsniform() == f_HeldNote.isUsniform()) {
                if (!f_ExistingNote.IsExtension() && !f_HeldNote.IsExtension()) { //both are simple and not extensions
                    return null;
                }
                else if (f_ExistingNote.IsExtension() && f_HeldNote.IsExtension()) { //both are simple and extensions
                    return null;
                }
                else return new NoteTemplate(f_HeldNote);
                
            }

            if (f_overrideNote == false) { //Combine notes, heldnote is simple
                var f_ExistingCol = f_ExistingNote.isUsniform();
                if (f_ExistingCol == null) { 
                    if (f_ExistingNote.HalfColour(Half.Left) != null && f_ExistingNote.HalfColour(Half.Right) != null) { //note is dual-octival
                        if (f_ExistingNote.HalfColour(Half.Left) == f_HeldCol) { //making it uniform
                            return new NoteTemplate(f_HeldNote);
                        }
                        else if (f_ExistingNote.HalfColour(Half.Right) == f_HeldCol) { //making it uniform
                            return new NoteTemplate(f_HeldNote);
                        }
                        else { //override existing note
                            return new NoteTemplate(f_HeldNote);
                        }

                    }
                    else { //Existing note is complex
                        return new NoteTemplate(f_HeldNote);

                        //simply replacing existing note with held note, 
                        //todo: complex combinations
                    }
                }
                else { //Held note & Existing note are simple
                    if ((int)f_ExistingCol == (int)f_HeldCol - 1) { //held colour is to the right of existing note
                        f_NewNote.SetHalfColour(Half.Left, f_ExistingNote);
                        f_NewNote.SetHalfColour(Half.Right, f_HeldNote);
                        return f_NewNote;
                    }
                    else if ((int)f_ExistingCol == (int)f_HeldCol + 1) { //held colour is to the left of existing note
                        f_NewNote.SetHalfColour(Half.Left, f_HeldNote);
                        f_NewNote.SetHalfColour(Half.Right, f_ExistingNote);
                        return f_NewNote;
                    }
                    else {  //held & existing colours are not next to each other
                        return new NoteTemplate(f_HeldNote);
                    }
                }
            }
            else {
                return new NoteTemplate(f_HeldNote);
            }
        }

        private void CreatePreviewImage() {
            NoteImage = new Canvas() {
                Height = 16,
                Width = 12,
                Opacity = 0.5
            };

            for (var i = 0; i < 16; i++) {
                int f_num = 6 * i / 8;
                var f_img = new Image() {
                    Height = 2,
                    Width = 6,
                    Source = BitImages.GetBitImg(PreviewTemplate.Positions[i], PreviewTemplate.Colours[i], NoteType.White),
                    Margin = new Thickness(6 * (i / 8), i % 8 * 2, 0, 0)
                    
                };
                NoteImage.Children.Add(f_img);
            }

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
            var f_preview = NewNotes.Peek();
            var f_previous = f_existingnote.FindNoteAbove();
            var f_next = f_existingnote.FindNoteBelow();

            if (f_preview.Note == f_previous) {
                PushNote(f_existingnote);
            }
            else if (f_preview.Note == f_next){
                PopNote();
            }
        }
        public void PushNote(NoteViewModal f_existingnote) {
            var f_startpoint = (NewNotes.Count == 0);
            NewNotes.Push(new NotePreview(f_existingnote, HeldNote, Override, f_startpoint));
        }
        public void PopNote() {
            if (NewNotes.Count > 1) {
                var f_preview = NewNotes.Pop();
                f_preview.Cancel();
            }
        }
        public void Activate() {
            while(NewNotes.Count != 0) {
                var f_preview = NewNotes.Pop();
                f_preview.Activate();
            }
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
