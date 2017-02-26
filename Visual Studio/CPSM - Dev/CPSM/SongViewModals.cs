using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using CommonClasses.Images;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using System.Media;

namespace CPSM
{
    namespace ViewModals
    {
        public class SongViewModalCreator
        {
            public MouseNoteControl _Mouse { get; set; }
            public Canvas MeasuresCan { get; set; }
            public Stack<StackPanel> MeasureStack { get; set; }
            public List<StackPanel> Pages { get; set; }
            public StackPanel ActivePage { get; set; }
            public StackPanel ActiveStackPanel { get; set; }
            public List<MeasureViewModal> Measures { get; set; }
            public Label TitleBox { get; set; }
            public Label SourceBox { get; set; }
            public Label VersionBox { get; set; }
            public Label PageNum { get; set; }
            private NoteInitializer _Initializer { get; set; }
            public int CurrentPage { get; set; }
            public int TotalPages { get; set; }
            public SongData _ActiveSong { get; set; }
            private PageTurner _PageTurner { get; set; }

            public SongViewModalCreator(Canvas f_measurecan, MouseNoteControl f_mousectrl, Label f_titlebox, Label f_sourcebox, Label f_versionbox, Label f_Pagenumber, NoteImageControl f_NoteImageControl) {
                MeasuresCan = f_measurecan;
                MeasureStack = new Stack<StackPanel>();
                Pages = new List<StackPanel>();
                _Mouse = f_mousectrl;
                Measures = new List<MeasureViewModal>();

                PageNum = f_Pagenumber;
                TitleBox = f_titlebox;
                SourceBox = f_sourcebox;


                _Initializer = new NoteInitializer(f_NoteImageControl);
            }

            public void LoadSong(SongData f_song) {
                ClearSong();
                _ActiveSong = f_song;
                MeasureStack = new Stack<StackPanel>();
                foreach (var mes in f_song.Measures) {
                    CreateMeasure(mes, false);
                }
                TitleBox.Content = f_song.Title;
                SourceBox.Content = f_song.Source;
            }
            public void CreateMeasure(MeasureData f_measure, bool f_empty) {
                var f_modal = new MeasureViewModal(f_measure, _Mouse, this, f_empty, Measures.Count + 1);
                GoToPage(TotalPages);

                if (MeasureStack.Count == 0) {
                    AddColumn();
                }
                double f_ASPHeight = 0;
                foreach (FrameworkElement mes in ActiveStackPanel.Children) {
                    f_ASPHeight += mes.Height;
                }

                var f_DistFromBottom = MeasuresCan.Height - f_ASPHeight;

                if (f_DistFromBottom < f_modal.Can.Height + 20) {
                    AddColumn();
                }
                AddMeasure(f_modal);

            }
            public void DeleteMeasure() {

                GoToPage(TotalPages);
                RemoveMeasure();

                if (ActiveStackPanel.Children.Count == 0) {
                    RemoveColumn();
                }
            }
            private void AddColumn() {
                if (ActivePage == null || ActivePage.Children.Count == 4) {
                    AddPage();
                }
                var f_newstack = new StackPanel() {
                    Margin = new Thickness(35, 0, 0, 0)
                };
                MeasureStack.Push(f_newstack);
                ActiveStackPanel = f_newstack;
                ActivePage.Children.Add(f_newstack);
            }
            private void AddMeasure(MeasureViewModal f_modal) {
                ActiveStackPanel.Children.Add(f_modal.Can);
                Measures.Add(f_modal);

            }
            private void RemoveColumn() {
                var f_OldStack = MeasureStack.Pop();
                ActivePage.Children.Remove(f_OldStack);
                if (MeasureStack.Count != 0) {
                    ActiveStackPanel = MeasureStack.Peek();
                }
                else {
                    ActiveStackPanel = null;
                }

                if (ActivePage.Children.Count == 0) {
                    RemovePage();
                }
            }
            private void RemoveMeasure() {
                var f_TempMeasure = Measures.ElementAt(Measures.Count - 1);
                ActiveStackPanel.Children.Remove(f_TempMeasure.Can);
                Measures.Remove(f_TempMeasure);
            }
            private void AddPage() {
                if (Pages.Count != 0) // if there is a page, take it off the canvas
                {
                    var f_OldPage = Pages.ElementAt(Pages.Count - 1);
                    MeasuresCan.Children.Remove(f_OldPage);
                }

                var f_NewPage = new StackPanel() {
                    Margin = new Thickness(0, 10, 0, 0),
                    Orientation = Orientation.Horizontal
                };
                MeasuresCan.Children.Add(f_NewPage);
                Pages.Add(f_NewPage);
                ActivePage = f_NewPage;
                IncrementPageNumber();
            }
            private void RemovePage() {
                var f_OldPage = Pages.ElementAt(Pages.Count - 1);
                MeasuresCan.Children.Remove(f_OldPage);
                Pages.Remove(f_OldPage);
                if (Pages.Count != 0) {
                    ActivePage = Pages.ElementAt(Pages.Count - 1);
                    MeasuresCan.Children.Add(ActivePage);
                    DeIncrementPageNumber();
                }
                else {
                    ActivePage = null;
                    DeIncrementPageNumber();
                }
            }
            private void IncrementPageNumber() {
                if (_ActiveSong != null) {
                    TotalPages++;
                    CurrentPage = TotalPages;
                    PageNum.Content = "Page " + CurrentPage.ToString() + " / " + TotalPages.ToString();
                    _ActiveSong.PageCount = TotalPages;
                }
            }
            private void DeIncrementPageNumber() {
                if (_ActiveSong != null) {
                    TotalPages--;
                    CurrentPage = TotalPages;
                    PageNum.Content = "Page " + CurrentPage.ToString() + " / " + TotalPages.ToString();
                    _ActiveSong.PageCount = TotalPages;
                }
            }
            public void NextPage() {
                PageTimerActivate(CurrentPage + 1);
            }
            public void PrevPage() {
                PageTimerActivate(CurrentPage - 1);
            }
            public void GoToPage(int f_page) {
                if (f_page == CurrentPage) {
                    return;
                }
                PageTimerActivate(f_page);

            }
            public void ClearSong() {
                while (true) {
                    if (Measures.Count == 0) { break; }
                    DeleteMeasure();
                }
            }
            private void PageTimerActivate(int f_page) {
                if (_PageTurner == null) {
                    _PageTurner = new PageTurner(f_page, this);
                }
                else {
                    try {
                        _PageTurner.FinalPageNumber = f_page;
                    }
                    catch {
                        PageTimerActivate(f_page);
                    }
                }
            }
            private void PageTimerDeactivate() {
                _PageTurner = null;
            }
            private void CheckForEmptyPage() {
                double f_ASPHeight = 0;
                foreach (FrameworkElement mes in Pages.ElementAt(Pages.Count-1).Children) {
                    f_ASPHeight += mes.ActualHeight;
                }

                if (f_ASPHeight == 0) {
                    RemoveColumn();
                }
            }


            public MeasureViewModal GetNextMeasure(MeasureViewModal f_measure) {
                bool f_found = false;
                foreach (var measure in Measures) {
                    if (f_found) {
                        return measure;
                    }
                    else {
                        if (measure == f_measure) {
                            f_found = true;
                        }
                    }
                }
                return null; //no next measure exists
            }
            public MeasureViewModal GetpreviousMeasure(MeasureViewModal f_measure) {
                MeasureViewModal f_prev = null;
                foreach (var measure in Measures) {
                    if (measure == f_measure) {
                        return f_prev;
                    }
                    else {
                        f_prev = measure;
                    }
                }
                throw new Exception();
            }
            public void EnqueueNote(NoteViewModal f_note) {
                _Initializer.AddNote(f_note);
            }

            public class PageTurner
            {
                public DispatcherTimer _Timer { get; set; }
                public SongViewModalCreator _Parent { get; set; }
                public StackPanel NewPage { get; set; }
                public int PageNumber { get; set; }
                public int Stage { get; set; }
                public int? FinalPageNumber { get; set; }

                public PageTurner(int f_NewPage, SongViewModalCreator f_Parent) {
                    _Parent = f_Parent;
                    PageNumber = f_NewPage;
                    Stage = 0;

                    _Timer = new DispatcherTimer();
                    _Timer.Interval = TimeSpan.FromSeconds(0.1);
                    _Timer.Tick += Timer_Tick;
                    _Timer.Start();

                    if (PageNumber < _Parent.TotalPages) {
                        _Parent.PageTimerDeactivate();
                    }
                }
                private void Timer_Tick(object sender, EventArgs e) {
                    try {
                        switch (Stage) {
                            case 0: {
                                NewPage = _Parent.Pages.ElementAt(PageNumber - 1);
                                _Parent.MeasuresCan.Children.Remove(_Parent.ActivePage);
                                Stage = 1;
                                break;
                            }
                            case 1: {
                                _Parent.MeasuresCan.Children.Add(NewPage);
                                _Parent.ActivePage = NewPage;
                                Stage = 2;
                                break;
                            }
                            case 2: {
                                _Parent.CurrentPage = PageNumber;
                                _Parent.PageNum.Content = "Page " + _Parent.CurrentPage.ToString() + " / " + _Parent.TotalPages.ToString();
                                Stage = 3;
                                break;
                            }
                            case 3: {
                                _Parent.CheckForEmptyPage();
                                if (PageNumber == FinalPageNumber) {
                                    _Timer.Stop();
                                    _Parent.PageTimerDeactivate();
                                }
                                else if (FinalPageNumber != null) {
                                    PageNumber = FinalPageNumber.Value;
                                    Stage = 0;
                                }
                                else {
                                    _Timer.Stop();
                                    _Parent.PageTimerDeactivate();
                                }
                                break;
                            }
                        }
                    }
                    catch {
                        _Parent.PageTimerDeactivate();
                    }
                    
                }
            }

            public class NoteInitializer
            {
                private readonly double INTERVAL = 0.0001;
                public Queue<NoteViewModal> NoteQueue { get; set; }
                public DispatcherTimer Timer { get; set; }
                public bool SleepMode { get; set; }
                public NoteImageControl _NoteImageControl { get; set; }

                public NoteInitializer(NoteImageControl f_NoteImageControl) {
                    _NoteImageControl = f_NoteImageControl;
                    NoteQueue = new Queue<NoteViewModal>();
                    Timer = new DispatcherTimer();
                    Timer.Interval = TimeSpan.FromSeconds(INTERVAL);
                    Timer.Tick += Timer_Tick;
                    SleepMode = true;
                }

                private void Timer_Tick(object sender, EventArgs e) {
                    //debug

                    if (NoteQueue.Count == 0) {
                        SleepMode = true;
                        Timer.Stop();
                        return;
                    }
                    var f_note = NoteQueue.Dequeue();
                    if (f_note.Parent.Exists && !f_note.Initialized) {
                        if (!f_note._TempVars.empty) {
                            //SetColour handles Initialization
                            f_note.SetColour(new NoteTemplate(f_note), _NoteImageControl.GetImage(new NoteTemplate(f_note)));
                        }
                        else {
                            f_note.Initialize();
                        }
                    }
                    else {
                        Timer_Tick(null, null);
                    }
                }
                public void AddNote(NoteViewModal f_note) {
                    if (SleepMode) {
                        SleepMode = false;
                        Timer.Start();
                    }
                    if (f_note.GetType() == NoteType.Black) { }
                    NoteQueue.Enqueue(f_note);
                }
            }
        }

        public class NoteImageControl
        {
            private Queue<NoteCache> Cache { get; set; }
            private List<NoteCache> CommonNotes { get; set; }
            private readonly int MAXCACHE = 12;
            private bool CommonNotesInitialized { get; set; }

            public NoteImageControl() {
                CommonNotesInitialized = false;
                Cache = new Queue<NoteCache>();
            }

            private void InitializeCommonNotes() {
                CommonNotes = new List<NoteCache>();
                for (int i = 0; i < 2; i++) {
                    for (int o = 1; o < 8; o++) {
                        var f_Template = new NoteTemplate();
                        f_Template.SetColour((OctaveColour)o);
                        f_Template.SetAsExtension();
                        f_Template.Type = (NoteType)i;
                        var f_Image = SetImageBits(f_Template);

                        var f_TempCache = new NoteCache(f_Template, f_Image);
                        CommonNotes.Add(f_TempCache);
                    }
                };
                CommonNotesInitialized = true;
            }

            public BitmapSource GetImage(NoteTemplate f_template) {
                if (f_template.IsSimple()) { return GetCommonImage(f_template); }
                var f_CacheResult = SearchCache(f_template);
                if (f_CacheResult != null) {
                    return f_CacheResult;
                }
                else {

                    // Draws the images into a DrawingVisual component
                    var f_Image = SetImageBits(f_template);

                    CacheAdd(f_template, f_Image);
                    return f_Image;
                }
            }
            public BitmapSource GetCommonImage(NoteTemplate f_template) {
                if (f_template.IsStart()) {
                    return ImageControl.NoteImg(f_template.isUsniform().Value, f_template.Type);
                }
                else if (f_template.IsExtension()) {
                    if (!CommonNotesInitialized) { InitializeCommonNotes(); }
                    return SearchCommonCache(f_template);
                }
                else {
                    throw new Exception();
                }

            }

            private BitmapSource SearchCache(NoteTemplate f_template) {
                foreach (var img in Cache) {
                    if (img.Template.IsEqual(f_template)) {
                        return img.NoteImage;
                    }
                }
                return null;
            }
            private BitmapSource SearchCommonCache(NoteTemplate f_template) {
                foreach (var img in CommonNotes) {
                    if (img.Template.IsEqual(f_template)) {
                        return img.NoteImage;
                    }
                }
                return null;
            }
            private void CacheAdd(NoteTemplate f_template, BitmapSource f_image) {
                if (SearchCache(f_template) != null) { return; }
                if (Cache.Count >= MAXCACHE)
                    Cache.Dequeue();
                var f_CacheElement = new NoteCache(f_template, f_image);
                Cache.Enqueue(f_CacheElement);

            }
            private BitmapFrame[] GetImageBits(NoteTemplate f_template) {
                var f_BitImages = new BitmapFrame[16];
                for (int i = 0; i < 16; i++) {
                    try {
                        if (f_template.Colours[i] != OctaveColour.none) {
                            f_BitImages[i] = BitmapFrame.Create(BitImages.GetBitImg(f_template.Positions[i], f_template.Colours[i], f_template.Type));
                        }
                    }
                    catch(Exception e) {

                    }
                }
                return f_BitImages;
            }
            private BitmapSource SetImageBits(NoteTemplate f_template) {
                BitmapFrame[] f_bits = GetImageBits(f_template);
                DrawingVisual drawingVisual = new DrawingVisual();
                int f_width = 5;
                if (f_template.Type == NoteType.White) {
                    f_width = 6;
                }

                using (DrawingContext drawingContext = drawingVisual.RenderOpen()) {
                    drawingContext.DrawImage(f_bits[0], new Rect(0, 0, f_width, 2));
                    drawingContext.DrawImage(f_bits[1], new Rect(0, 2, f_width, 2));
                    drawingContext.DrawImage(f_bits[2], new Rect(0, 4, f_width, 2));
                    drawingContext.DrawImage(f_bits[3], new Rect(0, 6, f_width, 2));
                    drawingContext.DrawImage(f_bits[4], new Rect(0, 8, f_width, 2));
                    drawingContext.DrawImage(f_bits[5], new Rect(0, 10, f_width, 2));
                    drawingContext.DrawImage(f_bits[6], new Rect(0, 12, f_width, 2));
                    drawingContext.DrawImage(f_bits[7], new Rect(0, 14, f_width, 2));
                    drawingContext.DrawImage(f_bits[8], new Rect(f_width, 0, f_width, 2));
                    drawingContext.DrawImage(f_bits[9], new Rect(f_width, 2, f_width, 2));
                    drawingContext.DrawImage(f_bits[10], new Rect(f_width, 4, f_width, 2));
                    drawingContext.DrawImage(f_bits[11], new Rect(f_width, 6, f_width, 2));
                    drawingContext.DrawImage(f_bits[12], new Rect(f_width, 8, f_width, 2));
                    drawingContext.DrawImage(f_bits[13], new Rect(f_width, 10, f_width, 2));
                    drawingContext.DrawImage(f_bits[14], new Rect(f_width, 12, f_width, 2));
                    drawingContext.DrawImage(f_bits[15], new Rect(f_width, 14, f_width, 2));
                }
                RenderTargetBitmap rtb = new RenderTargetBitmap(f_width*2, 16, 96, 96, PixelFormats.Pbgra32);
                rtb.Render(drawingVisual);
                var f_Image = rtb as BitmapSource;
                return f_Image;
            }

            private class NoteCache
            {
                public NoteTemplate Template { get; set; }
                public BitmapSource NoteImage { get; set; }

                public NoteCache(NoteTemplate f_template, BitmapSource f_image) {
                    Template = f_template;
                    NoteImage = f_image;
                }
            }
        }

        public static class BitImages
        {
            public static CroppedBitmap GetBitImg(NoteBitPos f_pos, OctaveColour f_oct, NoteType f_type) {
                if (f_oct == OctaveColour.none) {
                    return new CroppedBitmap();
                }

                var ImageBit = ImageControl.NoteImg(f_oct, f_type);

                var crpimg = new CroppedBitmap(ImageBit, CreateRect(f_pos, f_type));
                return crpimg;
            }
            private static Int32Rect CreateRect(NoteBitPos f_pos, NoteType f_type) {
                if (f_type == NoteType.White) {
                    switch (f_pos) {
                        case NoteBitPos.a1: { return new Int32Rect(0, 0, 6, 2); }
                        case NoteBitPos.a2: { return new Int32Rect(0, 2, 6, 2); }
                        case NoteBitPos.a3: { return new Int32Rect(0, 4, 6, 2); }
                        case NoteBitPos.a4: { return new Int32Rect(0, 6, 6, 2); }
                        case NoteBitPos.a5: { return new Int32Rect(0, 8, 6, 2); }
                        case NoteBitPos.a6: { return new Int32Rect(0, 10, 6, 2); }
                        case NoteBitPos.a7: { return new Int32Rect(0, 12, 6, 2); }
                        case NoteBitPos.a8: { return new Int32Rect(0, 4, 6, 2); }
                        case NoteBitPos.b1: { return new Int32Rect(6, 0, 6, 2); }
                        case NoteBitPos.b2: { return new Int32Rect(6, 2, 6, 2); }
                        case NoteBitPos.b3: { return new Int32Rect(6, 4, 6, 2); }
                        case NoteBitPos.b4: { return new Int32Rect(6, 6, 6, 2); }
                        case NoteBitPos.b5: { return new Int32Rect(6, 8, 6, 2); }
                        case NoteBitPos.b6: { return new Int32Rect(6, 10, 6, 2); }
                        case NoteBitPos.b7: { return new Int32Rect(6, 12, 6, 2); }
                        case NoteBitPos.b8: { return new Int32Rect(6, 14, 6, 2); }
                    }
                }
                else {
                     switch (f_pos) {
                        case NoteBitPos.a1: { return new Int32Rect(0, 0, 5, 2); }
                        case NoteBitPos.a2: { return new Int32Rect(0, 2, 5, 2); }
                        case NoteBitPos.a3: { return new Int32Rect(0, 4, 5, 2); }
                        case NoteBitPos.a4: { return new Int32Rect(0, 6, 5, 2); }
                        case NoteBitPos.a5: { return new Int32Rect(0, 8, 5, 2); }
                        case NoteBitPos.a6: { return new Int32Rect(0, 10, 5, 2); }
                        case NoteBitPos.a7: { return new Int32Rect(0, 12, 5, 2); }
                        case NoteBitPos.a8: { return new Int32Rect(0, 4, 5, 2); }
                        case NoteBitPos.b1: { return new Int32Rect(5, 0, 5, 2); }
                        case NoteBitPos.b2: { return new Int32Rect(5, 2, 5, 2); }
                        case NoteBitPos.b3: { return new Int32Rect(5, 4, 5, 2); }
                        case NoteBitPos.b4: { return new Int32Rect(5, 6, 5, 2); }
                        case NoteBitPos.b5: { return new Int32Rect(5, 8, 5, 2); }
                        case NoteBitPos.b6: { return new Int32Rect(5, 10, 5, 2); }
                        case NoteBitPos.b7: { return new Int32Rect(5, 12, 5, 2); }
                        case NoteBitPos.b8: { return new Int32Rect(5, 14, 5, 2); }
                    }

                }
                throw new Exception();
            }
        }

        public class MeasureViewModal
        {
            public Canvas Can { get; set; }
            public Image ModalImg { get; set; }
            public Label MeasureCount { get; set; }
            public WhiteNoteViewModal[,] WhiteNotes { get; set; }
            public BlackNoteViewModal[,] BlackNotes { get; set; }
            public MeasureSize Size { get; set; }
            public SongViewModalCreator Parent { get; set; }
            public bool Exists { get; set; } // for use by note initializer

            public MeasureViewModal(MeasureData f_measure, MouseNoteControl f_mouse, SongViewModalCreator f_parent, bool f_empty, int f_measurecount) {
                Exists = true;
                Size = f_measure.Size;
                WhiteNotes = new WhiteNoteViewModal[14, (int)Size];
                BlackNotes = new BlackNoteViewModal[10, (int)Size];
                Parent = f_parent;
                Can = new Canvas() {
                    Height = ImageControl.MeasureImg(Size).Height - 2, //-2 for the seperator
                    Width = 220
                };
                ModalImg = new Image() {
                    Source = ImageControl.MeasureImg(Size),
                    Width = 220,
                    Stretch = Stretch.None
                };
                MeasureCount = new Label() {
                    Content = f_measurecount.ToString(),
                    Height = 40,
                    Width = 30,
                    Margin = new Thickness(-20, 5, 0, 0),

                };
                Can.Children.Add(ModalImg);
                Can.Children.Add(MeasureCount);
                for (int i = 0; i < 14; i++) {
                    for (int o = 0; o < (int)Size; o++) {
                        var tempNote = new WhiteNoteViewModal(f_measure.WhiteNotes[i, o], Can, f_mouse, this, i, o);
                        if (!f_empty && !f_measure.WhiteNotes[i, o].IsEmpty()) {
                            tempNote._TempVars.empty = false;
                            Parent.EnqueueNote(tempNote);
                        }
                        WhiteNotes[i, o] = tempNote;
                    }
                }
                for (int i = 0; i < 10; i++) {
                    for (int o = 0; o < (int)Size; o++) {
                        var tempNote = new BlackNoteViewModal(f_measure.BlackNotes[i, o], Can, f_mouse, this, i, o);
                        if (!f_empty && !f_measure.BlackNotes[i, o].IsEmpty()) {
                            tempNote._TempVars.empty = false;
                            Parent.EnqueueNote(tempNote);
                        }
                        BlackNotes[i, o] = tempNote;
                    }
                }
            }

            public NoteViewModal FindNoteBelow(NoteViewModal f_note) {
                int ii = 0;
                int oo = 0;
                for (int i = 0; i < 14; i++) {
                    for (int o = 0; o < (int)Size; o++) {
                        var f_whitenote = f_note as WhiteNoteViewModal;
                        if (WhiteNotes[i, o] == f_whitenote) {
                            ii = i;
                            oo = o + 1;
                            if (oo == (int)Size) {
                                //note below is in next measure
                                var f_nextmeasure = Parent.GetNextMeasure(this);
                                if (f_nextmeasure != null) {
                                    return f_nextmeasure.WhiteNotes[ii, 0];
                                }
                                else {
                                    return null;
                                }
                            }
                            else {
                                return WhiteNotes[ii, oo];
                            }
                        }
                    }
                }
                for (int i = 0; i < 10; i++) {
                    for (int o = 0; o < (int)Size; o++) {
                        var f_blacknote = f_note as BlackNoteViewModal;
                        if (BlackNotes[i, o] == f_blacknote) {
                            ii = i;
                            oo = o + 1;
                            if (oo == (int)Size) {
                                //note below is in next measure
                                var f_nextmeasure = Parent.GetNextMeasure(this);
                                if (f_nextmeasure != null) {
                                    return f_nextmeasure.BlackNotes[ii, 0];
                                }
                                else {
                                    return null;
                                }
                            }
                            else {
                                return BlackNotes[ii, oo];
                            }

                        }
                    }
                }
                return null;
            }
            public NoteViewModal FindNoteAbove(NoteViewModal f_note) {
                int ii = 0;
                int oo = 0;
                for (int i = 0; i < 14; i++) {
                    for (int o = 0; o < (int)Size; o++) {
                        var f_whitenote = f_note as WhiteNoteViewModal;
                        if (WhiteNotes[i, o] == f_whitenote) {
                            ii = i;
                            oo = o - 1;
                            if (oo < 0) {
                                //note above is in previous measure
                                var f_previousmeasure = Parent.GetpreviousMeasure(this);
                                if (f_previousmeasure != null) {
                                    return f_previousmeasure.WhiteNotes[ii, (int)f_previousmeasure.Size - 1];
                                }
                                else {
                                    return null;
                                }
                            }
                            else {
                                return WhiteNotes[ii, oo];
                            }
                        }

                    }
                }
                for (int i = 0; i < 10; i++) {
                    for (int o = 0; o < (int)Size; o++) {
                        var f_blacknote = f_note as BlackNoteViewModal;
                        if (BlackNotes[i, o] == f_blacknote) {
                            ii = i;
                            oo = o - 1;
                            if (oo < 0) {
                                //note above is in previous measure
                                var f_previousmeasure = Parent.GetpreviousMeasure(this);
                                if (f_previousmeasure != null) {
                                    return f_previousmeasure.BlackNotes[ii, (int)f_previousmeasure.Size - 1];
                                }
                                else {
                                    return null;
                                }
                            }
                            else {
                                return BlackNotes[ii, oo];
                            }
                        }
                    }
                }
                throw new Exception();
            }
            public NoteViewModal FindNoteToLeft(NoteViewModal f_note) {
                int ii = 0;
                int oo = 0;
                for (int i = 0; i < 14; i++) {
                    for (int o = 0; o < (int)Size; o++) {
                        var f_whitenote = f_note as WhiteNoteViewModal;
                        if (WhiteNotes[i, o] == f_whitenote) {
                            ii = i;
                            oo = o;
                            switch (ii) {
                                case 0:
                                return new NoteViewModal();
                                case 1:
                                case 2:
                                return BlackNotes[ii - 1, oo];
                                case 3:
                                return WhiteNotes[ii - 1, oo];
                                case 4:
                                case 5:
                                case 6:
                                return BlackNotes[ii - 2, oo];
                                case 7:
                                return new NoteViewModal();
                                case 8:
                                case 9:
                                return BlackNotes[ii - 3, oo];
                                case 10:
                                return WhiteNotes[ii - 1, oo];
                                case 11:
                                case 12:
                                case 13:
                                return BlackNotes[ii - 4, oo];
                            }
                        }
                    }
                }
                for (int i = 0; i < 10; i++) {
                    for (int o = 0; o < (int)Size; o++) {
                        var f_blacknote = f_note as BlackNoteViewModal;
                        if (BlackNotes[i, o] == f_blacknote) {
                            ii = i;
                            oo = o;
                            switch (ii) {
                                case 0:
                                case 1:
                                return WhiteNotes[ii, oo];
                                case 2:
                                case 3:
                                case 4:
                                return WhiteNotes[ii + 1, oo];
                                case 5:
                                case 6:
                                return WhiteNotes[ii + 2, oo];
                                case 7:
                                case 8:
                                case 9:
                                return WhiteNotes[ii + 3, oo];
                            }
                        }
                    }
                }
                throw new Exception();
            }
            public NoteViewModal FindNoteToRight(NoteViewModal f_note) {
                int ii = 0;
                int oo = 0;
                for (int i = 0; i < 14; i++) {
                    for (int o = 0; o < (int)Size; o++) {
                        var f_whitenote = f_note as WhiteNoteViewModal;
                        if (WhiteNotes[i, o] == f_whitenote) {
                            ii = i;
                            oo = o;
                            switch (ii) {
                                case 0:
                                case 1:
                                return BlackNotes[ii, oo];
                                case 2:
                                return WhiteNotes[ii + 1, oo];
                                case 3:
                                case 4:
                                case 5:
                                return BlackNotes[ii - 1, oo];
                                case 6:
                                return new NoteViewModal();
                                case 7:
                                case 8:
                                return BlackNotes[ii - 2, oo];
                                case 9:
                                return WhiteNotes[ii + 1, oo];
                                case 10:
                                case 11:
                                case 12:
                                return BlackNotes[ii - 3, oo];
                                case 13:
                                return new NoteViewModal();
                            }
                        }
                    }
                }
                for (int i = 0; i < 10; i++) {
                    for (int o = 0; o < (int)Size; o++) {
                        var f_blacknote = f_note as BlackNoteViewModal;
                        if (BlackNotes[i, o] == f_blacknote) {
                            ii = i;
                            oo = o;
                            switch (ii) {
                                case 0:
                                case 1:
                                return WhiteNotes[ii + 1, oo];
                                case 2:
                                case 3:
                                case 4:
                                return WhiteNotes[ii + 2, oo];
                                case 5:
                                case 6:
                                return WhiteNotes[ii + 3, oo];
                                case 7:
                                case 8:
                                case 9:
                                return WhiteNotes[ii + 4, oo];
                            }
                        }
                    }
                }
                throw new Exception();
            }
        }


        public class NoteViewModal
        {
            public NoteData CounterPart { get; set; }
            public MouseNoteControl _Mouse { get; set; }
            public Canvas NoteCan { get; set; }
            public List<Canvas> ClickableGrid { get; set; }
            public Image NoteImage { get; set; }
            public MeasureViewModal Parent { get; set; }
            public NoteVars _TempVars { get; set; }
            public bool Initialized { get; set; }

            public NoteViewModal() { /* Dummy note for, baisically a null when searching for adjacent notes*/}
            public NoteViewModal(NoteData f_note, Canvas f_measureCan, MouseNoteControl f_mouse, MeasureViewModal f_parent, int f_xpos, int f_ypos) {
                CounterPart = f_note;
                _Mouse = f_mouse;
                Parent = f_parent;
                Initialized = false;

                _TempVars = new NoteVars(f_measureCan, f_xpos, f_ypos);
            }

            public virtual void NoteClickDown(object sender, MouseButtonEventArgs e) {
                Point MousePos = e.GetPosition(NoteCan);
                _Mouse.NoteClickDown(this, e, MousePos);
            }
            public virtual void NoteClickUp(object sender, MouseButtonEventArgs e) {
                Point MousePos = e.GetPosition(NoteCan);
                _Mouse.NoteClickUp(this, e, MousePos);
            }
            public virtual void NoteMouseEnter(object sender, MouseEventArgs e) {
                Point MousePos = e.GetPosition(NoteCan);
                _Mouse.NoteMouseEnter(this, e, MousePos);
            }
            public virtual void NoteMouseLeave(object sender, MouseEventArgs e) {
                Point MousePos = e.GetPosition(NoteCan);
                _Mouse.NoteMouseLeave(this, e, MousePos);
            }
            public void ClearNote() {
                NoteCan.Children.Remove(NoteImage);
                NoteImage = null;
                Initialized = false;
                CounterPart.SetNote(new NoteTemplate());
            }
            public virtual new NoteType GetType() { return NoteType.White; }

            public virtual void Initialize() { }
            public virtual void InitializeClickableGrid() { }
            public virtual void InitializeImage() { }
            public virtual void SetPosition(int f_xpos, int f_ypos) { }

            public void SetColour(NoteTemplate f_template, BitmapSource f_source) {
                if (f_template == null || f_template.isUsniform() == OctaveColour.none) {
                    return;
                }

                if (!Initialized) { Initialize(); }

                NoteImage.Source = f_source;
                CounterPart.SetNote(f_template);
                NoteImage.Opacity = 1;
            }

            public NoteViewModal FindNoteBelow() {
                return Parent.FindNoteBelow(this);
            }
            public NoteViewModal FindNoteAbove() {
                return Parent.FindNoteAbove(this);
            }
            public NoteViewModal FindNoteToLeft() {
                if (Parent != null) {
                    return Parent.FindNoteToLeft(this);
                }
                else
                    return new NoteViewModal();
            }
            public NoteViewModal FindNoteToRight() {
                if (Parent != null) {
                    return Parent.FindNoteToRight(this);
                }
                else
                    return new NoteViewModal();
            }
        }

        public class WhiteNoteViewModal : NoteViewModal
        {

            public WhiteNoteViewModal(NoteData f_note, Canvas f_measureCan, MouseNoteControl f_mouse, MeasureViewModal f_parent, int f_xpos, int f_ypos) : base(f_note, f_measureCan, f_mouse, f_parent, f_xpos, f_ypos) {
                NoteCan = new Canvas() {
                    Height = 16,
                    Width = 12
                };
                f_parent.Can.Children.Add(NoteCan);
                SetPosition(f_xpos, f_ypos);
                InitializeClickableGrid();
            }

            public override void SetPosition(int f_xpos, int f_ypos) {
                int xx = 0, yy;

                switch (f_xpos) {
                    case 0: { xx = 0; break; }
                    case 1: { xx = 16; break; }
                    case 2: { xx = 32; break; }
                    case 3: { xx = 44; break; }
                    case 4: { xx = 60; break; }
                    case 5: { xx = 76; break; }
                    case 6: { xx = 92; break; }
                    case 7: { xx = 116; break; }
                    case 8: { xx = 132; break; }
                    case 9: { xx = 148; break; }
                    case 10: { xx = 160; break; }
                    case 11: { xx = 176; break; }
                    case 12: { xx = 192; break; }
                    case 13: { xx = 208; break; }
                }
                yy = (1 + (f_ypos * 16));

                NoteCan.Margin = new Thickness(xx, yy, 0, 0);
            }

            public override void InitializeClickableGrid() {

                ClickableGrid = new List<Canvas>();

                ClickableGrid.Add(InitGridHelper(0));
                ClickableGrid.Add(InitGridHelper(2));
                ClickableGrid.Add(InitGridHelper(4));
                ClickableGrid.Add(InitGridHelper(6));
                ClickableGrid.Add(InitGridHelper(8));
                ClickableGrid.Add(InitGridHelper(10));
                ClickableGrid.Add(InitGridHelper(12));
                ClickableGrid.Add(InitGridHelper(14));

                foreach (var grid in ClickableGrid) {
                    grid.MouseDown += new MouseButtonEventHandler(NoteClickDown);
                    grid.MouseUp += new MouseButtonEventHandler(NoteClickUp);
                    grid.MouseEnter += new MouseEventHandler(NoteMouseEnter);
                    grid.MouseLeave += new MouseEventHandler(NoteMouseLeave);
                    NoteCan.Children.Add(grid);
                }

            }
            private Canvas InitGridHelper(int f_ypos) {
                return new Canvas() {
                    Width = 12,
                    Height = 2,
                    Margin = new Thickness(0, f_ypos, 0, 0),
                    Background = Brushes.Transparent
                };
            }
            public override void Initialize() {
                InitializeImage();
                ClickableGrid = null;
                InitializeClickableGrid();
                Initialized = true;
                _TempVars = null;
            }
            public override void InitializeImage() {
                NoteImage = new Image() {
                    Height = 16,
                    Width = 12
                };
                NoteCan.Children.Add(NoteImage);
            }
            public override NoteType GetType() {
                return NoteType.White;
            }

        }

        public class BlackNoteViewModal : NoteViewModal
        {

            public BlackNoteViewModal(NoteData f_note, Canvas f_measureCan, MouseNoteControl f_mouse, MeasureViewModal f_parent, int f_xpos, int f_ypos) : base(f_note, f_measureCan, f_mouse, f_parent, f_xpos, f_ypos) {
                NoteCan = new Canvas() {
                    Height = 16,
                    Width = 8
                };
                f_parent.Can.Children.Add(NoteCan);
                SetPosition(f_xpos, f_ypos);
                InitializeClickableGrid();
            }

            public override void SetPosition(int f_xpos, int f_ypos) {
                int xx = 0, yy;

                switch (f_xpos) {
                    case 0: { xx = 8; break; }
                    case 1: { xx = 24; break; }
                    case 2: { xx = 52; break; }
                    case 3: { xx = 68; break; }
                    case 4: { xx = 84; break; }
                    case 5: { xx = 124; break; }
                    case 6: { xx = 140; break; }
                    case 7: { xx = 168; break; }
                    case 8: { xx = 184; break; }
                    case 9: { xx = 200; break; }
                }
                xx += 1; // offset shortcut

                yy = (1 + (f_ypos * 16));

                NoteCan.Margin = new Thickness(xx, yy, 0, 0);
            }

            public override void InitializeClickableGrid() {

                ClickableGrid = new List<Canvas>();

                ClickableGrid.Add(InitGridHelper(0));
                ClickableGrid.Add(InitGridHelper(2));
                ClickableGrid.Add(InitGridHelper(4));
                ClickableGrid.Add(InitGridHelper(6));
                ClickableGrid.Add(InitGridHelper(8));
                ClickableGrid.Add(InitGridHelper(10));
                ClickableGrid.Add(InitGridHelper(12));
                ClickableGrid.Add(InitGridHelper(14));

                foreach (var grid in ClickableGrid) {
                    grid.MouseDown += new MouseButtonEventHandler(NoteClickDown);
                    grid.MouseUp += new MouseButtonEventHandler(NoteClickUp);
                    grid.MouseEnter += new MouseEventHandler(NoteMouseEnter);
                    grid.MouseLeave += new MouseEventHandler(NoteMouseLeave);
                    NoteCan.Children.Add(grid);
                }
            }
            private Canvas InitGridHelper(int f_ypos) {
                return new Canvas() {
                    Width = 8,
                    Height = 2,
                    Margin = new Thickness(2, f_ypos, 0, 0),
                    Background = Brushes.Transparent
                };
            }
            public override void Initialize() {
                InitializeImage();
                ClickableGrid = null;
                InitializeClickableGrid();
                Initialized = true;
                _TempVars = null;
            }
            public override void InitializeImage() {
                NoteImage = new Image() {
                    Height = 16,
                    Width = 10
                };
                NoteCan.Children.Add(NoteImage);
            }
            public override NoteType GetType() {
                return NoteType.Black;
            }

        }




        public class NoteTemplate
        {
            public OctaveColour[] Colours { get; set; }
            public NoteBitPos[] Positions { get; set; }
            public NoteType Type { get; set; }

            public NoteTemplate() {
                Init();
                for (int i = 0; i < 16; i++) {
                    Colours[i] = OctaveColour.none;
                    Positions[i] = (NoteBitPos)i;
                }
            }
            public NoteTemplate(NoteData f_note) {
                Init();

                for (int i = 0; i < 16; i++) {
                    Colours[i] = f_note.Colours[i];
                    Positions[i] = f_note.Positions[i];
                }
            }
            public NoteTemplate(NoteViewModal f_note) {
                Init();

                for (int i = 0; i < 16; i++) {
                    Colours[i] = f_note.CounterPart.Colours[i];
                    Positions[i] = f_note.CounterPart.Positions[i];
                }
                if (f_note is WhiteNoteViewModal) {
                    Type = NoteType.White;
                }
                else {
                    Type = NoteType.Black;
                }
            }
            public NoteTemplate(MouseNote f_mousecolour, Point f_mousepoint) {
                Init();
                if (f_mousecolour.IsTemplate) {
                    var f_note = f_mousecolour.Template;
                    for (int i = 0; i < 16; i++) {
                        Colours[i] = f_note.Colours[i];
                        Positions[i] = f_note.Positions[i];
                    }
                }
                else {
                    switch (f_mousecolour.ActivePatrial) {
                        case PartialNote.Full: {
                            for (int i = 0; i < 16; i++) {
                                Colours[i] = f_mousecolour.ActiveColour;
                                Positions[i] = (NoteBitPos)i;

                            }
                            break;
                        }
                        case PartialNote.Half: {
                            int count = 0;
                            for (int i = 0; i < 16; i++) {
                                Colours[i] = OctaveColour.none;
                                Positions[i] = (NoteBitPos)i;

                            }
                            if (f_mousepoint.Y <= 8) {
                                for (int i = 0; i < 4; i++) {
                                    Colours[i] = f_mousecolour.ActiveColour;
                                    Positions[i] = (NoteBitPos)count;
                                    Colours[i + 8] = f_mousecolour.ActiveColour;
                                    Positions[i + 8] = (NoteBitPos)count + 8;
                                    count++;
                                }
                            }
                            else if (f_mousepoint.Y > 8) {
                                for (int i = 4; i < 8; i++) {
                                    Colours[i] = f_mousecolour.ActiveColour;
                                    Positions[i] = (NoteBitPos)count;
                                    Colours[i + 8] = f_mousecolour.ActiveColour;
                                    Positions[i + 8] = (NoteBitPos)count + 8;
                                    count++;
                                }

                            }
                            break;
                        }
                            case PartialNote.Quarter: {
                            int count = 0;
                            for (int i = 0; i < 16; i++) {
                                Colours[i] = OctaveColour.none;
                                Positions[i] = (NoteBitPos)i;

                            }
                            if (f_mousepoint.Y <= 4) {
                                for (int i = 0; i < 2; i++) {
                                    Colours[i] = f_mousecolour.ActiveColour;
                                    Positions[i] = (NoteBitPos)count;
                                    Colours[i + 8] = f_mousecolour.ActiveColour;
                                    Positions[i + 8] = (NoteBitPos)count + 8;
                                    count++;
                                }
                            }
                            else if (f_mousepoint.Y <= 8) {
                                for (int i = 2; i < 4; i++) {
                                    Colours[i] = f_mousecolour.ActiveColour;
                                    Positions[i] = (NoteBitPos)count;
                                    Colours[i + 8] = f_mousecolour.ActiveColour;
                                    Positions[i + 8] = (NoteBitPos)count + 8;
                                    count++;
                                }
                            }
                            else if (f_mousepoint.Y <= 12) {
                                for (int i = 4; i < 6; i++) {
                                    Colours[i] = f_mousecolour.ActiveColour;
                                    Positions[i] = (NoteBitPos)count;
                                    Colours[i + 8] = f_mousecolour.ActiveColour;
                                    Positions[i + 8] = (NoteBitPos)count + 8;
                                    count++;
                                }
                            }
                            else if (f_mousepoint.Y <= 16) {
                                for (int i = 6; i < 8; i++) {
                                    Colours[i] = f_mousecolour.ActiveColour;
                                    Positions[i] = (NoteBitPos)count;
                                    Colours[i + 8] = f_mousecolour.ActiveColour;
                                    Positions[i + 8] = (NoteBitPos)count + 8;
                                    count++;
                                }
                            }
                            break;
                        }

                        case PartialNote.Eighth: {
                            int count = 0;
                            for (int i = 0; i < 16; i++) {
                                Colours[i] = OctaveColour.none;
                                Positions[i] = (NoteBitPos)i;

                            }
                            if (f_mousepoint.Y <= 2) {
                                for (int i = 0; i < 1; i++) {
                                    Colours[i] = f_mousecolour.ActiveColour;
                                    Positions[i] = (NoteBitPos)count;
                                    Colours[i + 8] = f_mousecolour.ActiveColour;
                                    Positions[i + 8] = (NoteBitPos)count + 8;
                                    count++;
                                }
                            }
                            else if (f_mousepoint.Y <= 4) {
                                for (int i = 1; i < 2; i++) {
                                    Colours[i] = f_mousecolour.ActiveColour;
                                    Positions[i] = (NoteBitPos)count;
                                    Colours[i + 8] = f_mousecolour.ActiveColour;
                                    Positions[i + 8] = (NoteBitPos)count + 8;
                                    count++;
                                }
                            }
                            else if (f_mousepoint.Y <= 6) {
                                for (int i = 2; i < 3; i++) {
                                    Colours[i] = f_mousecolour.ActiveColour;
                                    Positions[i] = (NoteBitPos)count;
                                    Colours[i + 8] = f_mousecolour.ActiveColour;
                                    Positions[i + 8] = (NoteBitPos)count + 8;
                                    count++;
                                }
                            }
                            else if (f_mousepoint.Y <= 8) {
                                for (int i = 3; i < 4; i++) {
                                    Colours[i] = f_mousecolour.ActiveColour;
                                    Positions[i] = (NoteBitPos)count;
                                    Colours[i + 8] = f_mousecolour.ActiveColour;
                                    Positions[i + 8] = (NoteBitPos)count + 8;
                                    count++;
                                }
                            }
                            else if (f_mousepoint.Y <= 10) {
                                for (int i = 4; i < 5; i++) {
                                    Colours[i] = f_mousecolour.ActiveColour;
                                    Positions[i] = (NoteBitPos)count;
                                    Colours[i + 8] = f_mousecolour.ActiveColour;
                                    Positions[i + 8] = (NoteBitPos)count + 8;
                                    count++;
                                }
                            }
                            else if (f_mousepoint.Y <= 12) {
                                for (int i = 5; i < 6; i++) {
                                    Colours[i] = f_mousecolour.ActiveColour;
                                    Positions[i] = (NoteBitPos)count;
                                    Colours[i + 8] = f_mousecolour.ActiveColour;
                                    Positions[i + 8] = (NoteBitPos)count + 8;
                                    count++;
                                }
                            }
                            else if (f_mousepoint.Y <= 14) {
                                for (int i = 6; i < 7; i++) {
                                    Colours[i] = f_mousecolour.ActiveColour;
                                    Positions[i] = (NoteBitPos)count;
                                    Colours[i + 8] = f_mousecolour.ActiveColour;
                                    Positions[i + 8] = (NoteBitPos)count + 8;
                                    count++;
                                }
                            }
                            else if (f_mousepoint.Y <= 16) {
                                for (int i = 7; i < 8; i++) {
                                    Colours[i] = f_mousecolour.ActiveColour;
                                    Positions[i] = (NoteBitPos)count;
                                    Colours[i + 8] = f_mousecolour.ActiveColour;
                                    Positions[i + 8] = (NoteBitPos)count + 8;
                                    count++;
                                }
                            }
                            break;
                        }
                        //todo:  cases for non-full notes
                    }
                }
            }
            public NoteTemplate(NoteTemplate f_note) {
                Init();

                for (int i = 0; i < 16; i++) {
                    Colours[i] = f_note.Colours[i];
                    Positions[i] = f_note.Positions[i];
                }
            }
            public NoteTemplate(NoteTemplate f_NewNote, NoteTemplate f_ExistingNote) {
                Init();
                for (int i = 0; i < 8; i++) {
                    if (f_ExistingNote.Colours[i] == OctaveColour.none) { // if existingnote is transparent here
                        Colours[i] = f_NewNote.Colours[i];
                        Colours[i + 8] = f_NewNote.Colours[i + 8];
                        Positions[i] = f_NewNote.Positions[i];
                        Positions[i + 8] = f_NewNote.Positions[i + 8];

                    }
                    else if (f_NewNote.Colours[i] == OctaveColour.none) { // if newnote is transparent here
                        Colours[i] = f_ExistingNote.Colours[i];
                        Colours[i + 8] = f_ExistingNote.Colours[i + 8];
                        Positions[i] = f_ExistingNote.Positions[i];
                        Positions[i+8] = f_ExistingNote.Positions[i+8];
                    }
                    else if (f_ExistingNote.Colours[i] != f_ExistingNote.Colours[i+8]) { // catch and override dual-octival note
                        Colours[i] = f_NewNote.Colours[i];
                        Colours[i+8] = f_NewNote.Colours[i+8];
                        Positions[i] = f_NewNote.Positions[i];
                        Positions[i + 8] = f_NewNote.Positions[i + 8];
                    }
                    else if ((int)f_ExistingNote.Colours[i] == (int)f_NewNote.Colours[i] + 1) { // new note is to the left
                        Colours[i] = f_NewNote.Colours[i];
                        Colours[i + 8] = f_ExistingNote.Colours[i + 8];
                        Positions[i] = f_NewNote.Positions[i];
                        Positions[i + 8] = f_ExistingNote.Positions[i + 8];
                    }
                    else if ((int)f_ExistingNote.Colours[i] == (int)f_NewNote.Colours[i] - 1){// new note is to the right
                        Colours[i] = f_ExistingNote.Colours[i];
                        Colours[i + 8] = f_NewNote.Colours[i + 8];
                        Positions[i] = f_ExistingNote.Positions[i];
                        Positions[i + 8] = f_NewNote.Positions[i + 8];
                    }
                    else { // override
                        Colours[i] = f_NewNote.Colours[i];
                        Colours[i + 8] = f_NewNote.Colours[i + 8];
                        Positions[i] = f_NewNote.Positions[i];
                        Positions[i + 8] = f_NewNote.Positions[i + 8];
                    }
                }
            }
            public NoteTemplate(char f_char) {
                Init();
                var f_col = GetColourFromChar(f_char);
                SetColour(f_col);
            }
            public NoteTemplate(PartialNote f_partial, OctaveColour f_col) {
                Init();
                for (int i = 0; i < (int)f_partial; i++) {
                    Colours[i] = f_col;
                    Colours[i + 8] = f_col;

                }
            }
            public NoteTemplate(PartialNote f_partial, OctaveColour f_col, int f_end, int f_start, bool f_IsExtension) {
                Init();
                var f_startpoint = f_start * (int)f_partial;
                var f_EndPoint = f_end * (int)f_partial;

                //debug
                if (f_start != 0) {
                    SystemSounds.Beep.Play();
                }

                for (int i = f_startpoint; i < f_EndPoint; i++) {
                    Colours[i] = f_col;
                    Colours[i + 8] = f_col;
                    if (!f_IsExtension) {
                        Positions[i] = (NoteBitPos)i;
                        Positions[i + 8] = (NoteBitPos)i + 8;
                    }
                }
            }

            public OctaveColour? isUsniform() {
                bool colourfound = false;
                OctaveColour testcol = OctaveColour.none;
                foreach (var col in Colours) {
                    if (!colourfound) {
                        if (col != OctaveColour.none) {
                            testcol = col;
                            colourfound = true;
                        }
                    }
                    else {
                        if (col != testcol && col != OctaveColour.none) {
                            return null;
                        }
                    }
                }
                return testcol;
            }
            public OctaveColour? isUsniform(PartialNote f_partial, int f_offset) {
                int f_length = 0;
                var f_testcol = OctaveColour.none;
                switch (f_partial) {
                    case PartialNote.Half: {
                        f_length = 4;
                        break;
                    }
                    case PartialNote.Quarter: {
                        f_length = 2;
                        break;
                    }
                    case PartialNote.Eighth: {
                        f_length = 1;
                        break;
                    }
                }
                int f_start = f_offset * f_length;
                int f_end = f_offset * f_length + f_length;

                for (int i = f_start; i < f_end; i++) {
                    if (f_testcol == OctaveColour.none && Colours[i] != OctaveColour.none) {
                        f_testcol = Colours[i];
                    }
                    if (!(Colours[i] == OctaveColour.none || Colours[i] == f_testcol)) {
                        return null;
                    }
                }
                return f_testcol;

                throw new NotImplementedException();
            }
            public OctaveColour? HalfColour(Half f_half) {
                OctaveColour testcol = OctaveColour.none;
                bool colourfound = false;
                int count = (int)f_half * 8;
                int end = 8 + ((int)f_half * 8);
                while (count < end) {
                    if (!colourfound) {
                        if (Colours[count] != OctaveColour.none) {
                            testcol = Colours[count];
                            colourfound = true;
                        }
                    }
                    else {
                        if (Colours[count] != testcol && Colours[count] != OctaveColour.none) {
                            return null;
                        }
                    }
                    count++;
                }
                return testcol;

            }
            public void SetHalfColour(Half f_half, OctaveColour f_col) {
                switch (f_half) {
                    case Half.Left: {
                        for (int i = 0; i < 8; i++) {
                            Colours[i] = f_col;
                            Positions[i] = (NoteBitPos)i;
                        }
                        break;
                    }
                    case Half.Right: {
                        for (int i = 8; i < 16; i++) {
                            Colours[i] = f_col;
                            Positions[i] = (NoteBitPos)i;
                        }
                        break;
                    }
                }
            }
            public void SetHalfColour(Half f_half, char f_char) {
                var f_col = GetColourFromChar(f_char);
                SetHalfColour(f_half, f_col);
            }
            public void SetHalfColour(Half f_half, NoteTemplate f_note) {
                switch (f_half) {
                    case Half.Left: {
                        for (int i = 0; i < 8; i++) {
                            Colours[i] = f_note.Colours[i];
                            Positions[i] = f_note.Positions[i];
                        }
                        break;
                    }
                    case Half.Right: {
                        for (int i = 8; i < 16; i++) {
                            Colours[i] = f_note.Colours[i];
                            Positions[i] = f_note.Positions[i];
                        }
                        break;
                    }
                }
            }
            public void SetColour(OctaveColour f_col) {
                for (int i = 0; i < 16; i++) {
                    Colours[i] = f_col;
                    Positions[i] = (NoteBitPos)i;
                }

            }
            private void Init() {
                Colours = new OctaveColour[16];
                Positions = new NoteBitPos[16];
                Type = NoteType.White;
            }
            public void SetAsExtension() {
                for (int i = 0; i < 8; i++) {
                    Positions[i] = NoteBitPos.a8;
                }
                for (int i = 8; i < 16; i++) {
                    Positions[i] = NoteBitPos.b8;
                }
            }
            public NoteTemplate GetAsExtension() {
                //returns a extension equivalent of this note
                var f_col1 = Colours[7];
                var f_col2 = Colours[15];
                var f_newnote = new NoteTemplate(this);
                for (int i = 0; i < 8; i++) {
                    f_newnote.Positions[i] = NoteBitPos.a8;
                    f_newnote.Colours[i] = f_col1;
                }
                for (int i = 8; i < 16; i++) {
                    f_newnote.Positions[i] = NoteBitPos.b8;
                    f_newnote.Colours[i] = f_col2;
                }
                return f_newnote;
            }
            public bool IsExtension() {
                for (int i = 0; i < 8; i++) {
                    if (Positions[i] != NoteBitPos.a8) {
                        return false;
                    }
                }
                for (int i = 8; i < 16; i++) {
                    if (Positions[i] != NoteBitPos.b8) {
                        return false;
                    }
                }
                return true;
            }
            public bool IsEqual(NoteTemplate f_other) {
                for (int i = 0; i < 16; i++) {
                    if (Colours[i] != f_other.Colours[i] || Positions[i] != f_other.Positions[i]) {
                        return false;
                    }
                }
                if (Type == f_other.Type) {
                    return true;
                }
                return false;
            }
            public bool IsStart() {
                int count = 0;
                foreach (var pos in Positions) {
                    if (pos != (NoteBitPos)count) { return false; }
                    count++;
                }
                return true;
            }
            public bool IsFull() {
                foreach (var col in Colours) {
                    if (col == OctaveColour.none) {
                        return false;
                    }
                }
                return true;
            }
            public bool IsSimple() {
                return ((IsStart() || IsExtension()) && IsFull() && isUsniform() != null);
            }
            public NoteTemplate GetWithOffset(PartialNote f_partial, int f_times) {
                int f_offset = (int)f_partial;
                var f_newnote = new NoteTemplate();
                for (int i = 7; i > 0; i--) {
                    try {
                        f_newnote.Colours[i] = Colours[i - (f_offset * f_times)];
                    }
                    catch { }
                    f_newnote.Colours[i+8] = Colours[i + 8 - (f_offset * f_times)];
                }
                return f_newnote;
            }
            private OctaveColour GetColourFromChar(char f_char) {
                switch (f_char) {
                    case 'o':
                    return OctaveColour.none;
                    case 'q':
                    return OctaveColour.Brown;
                    case 'w':
                    return OctaveColour.Teal;
                    case 'e':
                    return OctaveColour.Blue;
                    case 'r':
                    return OctaveColour.Green;
                    case 't':
                    return OctaveColour.Red;
                    case 'y':
                    return OctaveColour.Purple;
                    case 'u':
                    return OctaveColour.Yellow;
                }
                throw new Exception();
            }
            public NoteTemplate GetFull() {
                //returns a full equivalent of this note
                var f_newnote = new NoteTemplate(this);
                OctaveColour f_col = OctaveColour.none;
                int o = 0;
                for (int i = 0; i < 8; i++) {
                    if (Colours[i] == OctaveColour.none) {
                        try {
                            f_col = Colours[i - 1];
                            o = i;
                            break;
                        }
                        catch { }
                    }
                }
                if (f_col == OctaveColour.none) {
                    return f_newnote;
                }
                for (int i = o; i < 8; i++) {
                    f_newnote.Colours[i] = f_col;
                    f_newnote.Colours[i + 8] = f_col;
                }

                return f_newnote;
            }

            public static class NoteTemplates
            {
                //probably wont use
            }
        }

        public class NoteVars
        {
            public Canvas MeasureCan { get; set; }
            public int? xpos { get; set; }
            public int? ypos { get; set; }
            public bool empty { get; set; }

            public NoteVars(Canvas f_measureCan, int? f_xpos, int? f_ypos) {
                MeasureCan = f_measureCan;
                xpos = f_xpos;
                ypos = f_ypos;
                empty = true;
            }
        }
    }
}

