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

namespace CPSM
{
    namespace ViewModals
    {
        public class SongViewModalCreator
        {
            public MouseNoteControl _Mouse { get; set; }
            public Canvas MeasuresCan { get; set; }
            public StackPanel MeasureStack { get; set; }
            public List<MeasureViewModal> Measures { get; set; }
            public Label TitleBox { get; set; }
            public Label SourceBox { get; set; }
            public Label VersionBox { get; set; }
            private NoteInitializer _Initializer { get; set; }

            public SongViewModalCreator(Canvas f_measurecan, MouseNoteControl f_mousectrl, Label f_titlebox, Label f_sourcebox, Label f_versionbox) {
                //MeasuresCan = f_measurecan;
                MeasureStack = new StackPanel() {
                    Margin = new Thickness(20, 10, 0, 0)
                };
                f_measurecan.Children.Add(MeasureStack);
                _Mouse = f_mousectrl;
                Measures = new List<MeasureViewModal>();

                TitleBox = f_titlebox;
                SourceBox = f_sourcebox;

                _Initializer = new NoteInitializer();
            }

            public void LoadSong(SongData f_song) {
                MeasureStack.Children.Clear();
                foreach (var mes in f_song.Measures) {
                    CreateMeasure(mes, false);
                }
                TitleBox.Content = f_song.Name;
                SourceBox.Content = f_song.Source;
            }
            public void CreateMeasure(MeasureData f_measure, bool f_empty) {
                var modal = new MeasureViewModal(f_measure, _Mouse, this, f_empty, Measures.Count + 1);
                Measures.Add(modal);
                MeasureStack.Children.Add(modal.Can);
            }
            public void DeleteMeasure() {
                var f_mes = Measures.ElementAt(Measures.Count - 1);
                f_mes.Exists = false;
                MeasureStack.Children.RemoveAt(Measures.Count - 1);
                Measures.RemoveAt(Measures.Count - 1);
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

            public class NoteInitializer
            {
                private readonly double INTERVAL = 0.0001;
                public Queue<NoteViewModal> NoteQueue { get; set; }
                public DispatcherTimer Timer { get; set; }
                public bool SleepMode { get; set; }


                public NoteInitializer() {
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
                        f_note.Initialize();
                    }else {
                        Timer_Tick(null, null);
                    }
                }
                public void AddNote(NoteViewModal f_note) {
                    if (SleepMode) {
                        SleepMode = false;
                        Timer.Start();
                    }

                    NoteQueue.Enqueue(f_note);
                }
            }
        }

        public static class BitImages
        {
            public static CroppedBitmap GetBitImg(NoteBitPos f_pos, OctaveColour f_oct, NoteType f_type) {
                var ImageBit = ImageControl.NoteImg(f_oct, f_type);

                var crpimg = new CroppedBitmap(ImageBit, CreateRect(f_pos, f_type));
                return crpimg;
            }

            internal static ImageSource GetBitImg(object p1, object p2, NoteType white) {
                throw new NotImplementedException();
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
                        if (!f_empty && !f_measure.WhiteNotes[i, o].IsEmpty()) {
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
        }


        public class NoteViewModal
        {
            public NoteData CounterPart { get; set; }
            public MouseNoteControl _Mouse { get; set; }
            public Canvas NoteCan { get; set; }
            public List<Canvas> ClickableGrid { get; set; }
            public MeasureViewModal Parent { get; set; }
            public NoteVars _TempVars { get; set; }
            public bool Initialized { get; set; }

            public NoteViewModal(NoteData f_note, Canvas f_measureCan, MouseNoteControl f_mouse, MeasureViewModal f_parent, int f_xpos, int f_ypos) {
                CounterPart = f_note;
                _Mouse = f_mouse;
                Parent = f_parent;
                Initialized = false;

                _TempVars = new NoteVars(f_measureCan, f_xpos, f_ypos);
            }

            public virtual void NoteLeftClickDown(object sender, MouseButtonEventArgs e) {
                Point MousePos = e.GetPosition(NoteCan);
                _Mouse.NoteLeftClickedDown(this, e, MousePos);
            }
            public virtual void NoteLeftClickUp(object sender, MouseButtonEventArgs e) {
                Point MousePos = e.GetPosition(NoteCan);
                _Mouse.NoteLeftClickedUp(this, e, MousePos);
            }
            public virtual void NoteRightClickDown(object sender, MouseButtonEventArgs e) {
                Point MousePos = e.GetPosition(NoteCan);
                _Mouse.NoteRightClickedDown(this, e, MousePos);
            }
            public virtual void NoteRightClickUp(object sender, MouseButtonEventArgs e) {
                Point MousePos = e.GetPosition(NoteCan);
                _Mouse.NoteRightClickedUp(this, e, MousePos);
            }
            public virtual void NoteMouseEnter(object sender, MouseEventArgs e) {
                Point MousePos = e.GetPosition(NoteCan);
                _Mouse.NoteMouseEnter(this, e, MousePos);
            }
            public virtual void NoteMouseLeave(object sender, MouseEventArgs e) {
                //Point MousePos = e.GetPosition(NoteCan);
                //_Mouse.NoteMouseLeave(this, e, MousePos);
            }
            public void ClearNote() {
                SetColour(new NoteTemplate());
                CounterPart.SetColour(new NoteTemplate());
            }

            public virtual void Initialize() { }
            public virtual void InitializeClickableGrid() { }
            public virtual void InitializeImages() { }
            public virtual void SetPosition(int f_xpos, int f_ypos) { }
            
            public virtual void SetColour(OctaveColour f_oct, PartialNote f_part) { }

            public virtual void SetColour(NoteTemplate f_template) { }
            public virtual void ClearPreview() { }
            protected virtual void SetColourHelper(OctaveColour f_oct) { }
            protected virtual void SetColourHelperHalf(OctaveColour f_oct, Half f_half) { }
            
            public NoteViewModal FindNoteBelow() {
                return Parent.FindNoteBelow(this);
            }
            public NoteViewModal FindNoteAbove() {
                return Parent.FindNoteAbove(this);
            }
        }

        public class NoteBitViewModal
        {
            public Image NoteBitImg { get; set; }
            public NoteViewModal ParentNote { get; set; }
            public NoteBitPos Pos { get; set; }
            public OctaveColour Oct { get; set; }
            public NoteBitPos TruePos { get; set; }

            public NoteBitViewModal(Canvas f_NoteCan, NoteBitPos f_pos, NoteViewModal f_parent, NoteBitPos f_truepos) {
                ParentNote = f_parent;
                Pos = f_pos;
                Oct = OctaveColour.none;
                TruePos = f_truepos;
            }

            public virtual void InitializeImage() { }
            public virtual Thickness setTruePos(NoteBitPos f_truepos) { return new Thickness(); }
            public virtual void setPos(NoteBitPos f_pos) { }
            public virtual void setOct(OctaveColour f_oct, NoteBitPos f_pos) { }
            public virtual void setOct(OctaveColour f_oct) { }
            
            protected void CheckForNote() {
                //make empty notes invisible
                if (Oct == OctaveColour.none) {
                    NoteBitImg.Opacity = 0;
                }
                else {
                    NoteBitImg.Opacity = 1;
                }
            }

        }

        public class WhiteNoteViewModal : NoteViewModal
        {
            public List<WhiteNoteBitViewModal> Bits { get; set; }

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
                    grid.MouseLeftButtonDown += new MouseButtonEventHandler(NoteLeftClickDown);
                    grid.MouseLeftButtonUp += new MouseButtonEventHandler(NoteLeftClickUp);
                    grid.MouseRightButtonDown += new MouseButtonEventHandler(NoteRightClickDown);
                    grid.MouseRightButtonUp += new MouseButtonEventHandler(NoteRightClickUp);
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

                Bits = new List<WhiteNoteBitViewModal>();

                for (int i = 0; i < 16; i++) {
                    Bits.Add(new WhiteNoteBitViewModal(NoteCan, ((NoteBitPos)i), this, (NoteBitPos)i));
                }


                /*
                //_TempVars.MeasureCan.Children.Add(NoteCan);
                if (_TempVars.xpos.HasValue && _TempVars.ypos.HasValue) {
                    var f_x = _TempVars.xpos.Value;
                    var f_y = _TempVars.ypos.Value;
                    SetPosition(f_x, f_y);
                }
                else throw new Exception();
                */

                InitializeImages();

                ClickableGrid = null;
                InitializeClickableGrid();

                //debug
                //SetColourHelper(OctaveColour.Blue);

                Initialized = true;
                
                if (!_TempVars.empty) SetColour(new NoteTemplate(CounterPart));
                _TempVars = null;
            }

            public override void InitializeImages() {
                foreach (var bit in Bits) {
                    bit.InitializeImage();
                }
                //debug
                //NoteCan.Background = Brushes.Pink;
            }

            public override void SetColour(NoteTemplate f_template) {
                if (!Initialized) { Initialize();}

                //clear note and paste
                if (f_template == null) {
                    return;
                }
                int count = 0;
                foreach (var bit in Bits) {
                    bit.setOct(f_template.Colours[count], f_template.Positions[count]);
                    count++;
                }
                CounterPart.SetColour(f_template);
            }
            public override void ClearPreview() {
                SetColour(new NoteTemplate(CounterPart));
            }
            protected override void SetColourHelper(OctaveColour f_oct) {
                foreach (var bit in Bits) {
                    bit.setOct(f_oct);
                }

                CounterPart.SetColour(f_oct);
            }
            protected override void SetColourHelperHalf(OctaveColour f_oct, Half f_half) {
                int count;

                if (f_half == Half.Left) {
                    count = 0;
                }
                else {
                    count = -8;
                }

                foreach (var bit in Bits) {
                    if (count >= 0 && count <= 8) {
                        bit.setOct(f_oct);
                    }
                    count++;
                }

                var template = new NoteTemplate(this);
                CounterPart.SetColour(template);
            }

            public override void NoteLeftClickDown(object sender, MouseButtonEventArgs e) {
                base.NoteLeftClickDown(sender, e);
            }
            public override void NoteLeftClickUp(object sender, MouseButtonEventArgs e) {
                base.NoteLeftClickUp(sender, e);
            }
            public override void NoteRightClickDown(object sender, MouseButtonEventArgs e) {
                base.NoteRightClickDown(sender, e);
            }
            public override void NoteRightClickUp(object sender, MouseButtonEventArgs e) {
                base.NoteRightClickUp(sender, e);
            }
            public override void NoteMouseLeave(object sender, MouseEventArgs e) {
                base.NoteMouseLeave(sender, e);
            }
            public override void NoteMouseEnter(object sender, MouseEventArgs e) {
                base.NoteMouseEnter(sender, e);
            }
        }

        public class WhiteNoteBitViewModal : NoteBitViewModal
        {
            public WhiteNoteBitViewModal(Canvas f_notecan, NoteBitPos f_pos, NoteViewModal f_parent, NoteBitPos f_truepos) : base(f_notecan, f_pos, f_parent, f_truepos) {
                
            }

            public override void InitializeImage() {
                NoteBitImg = new Image() {
                    Height = 2,
                    Width = 8
                };
                NoteBitImg.Margin = setTruePos(TruePos);
                ParentNote.NoteCan.Children.Add(NoteBitImg);
            }
            public override Thickness setTruePos(NoteBitPos f_truepos) {
                int xx = -1;
                switch (f_truepos) {
                    case NoteBitPos.a1: { return new Thickness(xx, 0, 0, 0); }
                    case NoteBitPos.a2: { return new Thickness(xx, 2, 0, 0); }
                    case NoteBitPos.a3: { return new Thickness(xx, 4, 0, 0); }
                    case NoteBitPos.a4: { return new Thickness(xx, 6, 0, 0); }
                    case NoteBitPos.a5: { return new Thickness(xx, 8, 0, 0); }
                    case NoteBitPos.a6: { return new Thickness(xx, 10, 0, 0); }
                    case NoteBitPos.a7: { return new Thickness(xx, 12, 0, 0); }
                    case NoteBitPos.a8: { return new Thickness(xx, 14, 0, 0); }
                    case NoteBitPos.b1: { return new Thickness(xx + 6, 0, 0, 0); }
                    case NoteBitPos.b2: { return new Thickness(xx + 6, 2, 0, 0); }
                    case NoteBitPos.b3: { return new Thickness(xx + 6, 4, 0, 0); }
                    case NoteBitPos.b4: { return new Thickness(xx + 6, 6, 0, 0); }
                    case NoteBitPos.b5: { return new Thickness(xx + 6, 8, 0, 0); }
                    case NoteBitPos.b6: { return new Thickness(xx + 6, 10, 0, 0); }
                    case NoteBitPos.b7: { return new Thickness(xx + 6, 12, 0, 0); }
                    case NoteBitPos.b8: { return new Thickness(xx + 6, 14, 0, 0); }
                }
                throw new Exception();
            }
            public override void setOct(OctaveColour f_oct, NoteBitPos f_pos) {
                NoteBitImg.Source = BitImages.GetBitImg(f_pos, f_oct, NoteType.White);
                Oct = f_oct;
                CheckForNote();
            }
            public override void setOct(OctaveColour f_oct) {
                NoteBitImg.Source = BitImages.GetBitImg(Pos, f_oct, NoteType.White);
                Oct = f_oct;
                CheckForNote();
            }
        }
        

        public class BlackNoteViewModal : NoteViewModal
        {
            public List<BlackNoteBitViewModal> Bits { get; set; }

            public BlackNoteViewModal(NoteData f_note, Canvas f_measureCan, MouseNoteControl f_mouse, MeasureViewModal f_parent, int f_xpos, int f_ypos) : base(f_note, f_measureCan, f_mouse, f_parent, f_xpos, f_ypos) {

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
                    grid.MouseLeftButtonDown += new MouseButtonEventHandler(NoteLeftClickDown);
                    grid.MouseLeftButtonUp += new MouseButtonEventHandler(NoteLeftClickUp);
                    grid.MouseRightButtonDown += new MouseButtonEventHandler(NoteRightClickDown);
                    grid.MouseRightButtonUp += new MouseButtonEventHandler(NoteRightClickUp);
                    grid.MouseEnter += new MouseEventHandler(NoteMouseEnter);
                    grid.MouseLeave += new MouseEventHandler(NoteMouseLeave);
                    NoteCan.Children.Add(grid);
                }
            }
            private Canvas InitGridHelper(int f_ypos) {
                return new Canvas() {
                    Width = 8,
                    Height = 2,
                    Margin = new Thickness(2, f_ypos, 0, 0)
                };
            }

            public override void Initialize() {
                NoteCan = new Canvas() {
                    Height = 16,
                    Width = 10
                };

                Bits = new List<BlackNoteBitViewModal>();

                for (int i = 0; i < 16; i++) {
                    Bits.Add(new BlackNoteBitViewModal(NoteCan, ((NoteBitPos)i), this, (NoteBitPos)i));
                }

                _TempVars.MeasureCan.Children.Add(NoteCan);
                //debug
                //SetColourHelper(OctaveColour.Blue);
                if (_TempVars.xpos.HasValue && _TempVars.ypos.HasValue) {
                    var f_x = _TempVars.xpos.Value;
                    var f_y = _TempVars.ypos.Value;
                    SetPosition(f_x, f_y);
                }
                else throw new Exception();

                InitializeImages();
                Initialized = true;

                if (!_TempVars.empty) SetColour(new NoteTemplate(CounterPart));
                _TempVars = null;
            }
            public override void InitializeImages() {
                foreach (var bit in Bits) {
                    bit.InitializeImage();
                }
            }

            public override void SetColour(NoteTemplate f_template) {
                if (!Initialized) { Initialize(); }

                //clear note and paste
                if (f_template == null) {
                    return;
                }
                int count = 0;
                foreach (var bit in Bits) {
                    bit.setOct(f_template.Colours[count], f_template.Positions[count]);
                    count++;
                }
            }
            public override void ClearPreview() {
                SetColour(new NoteTemplate(CounterPart));
            }
            protected override void SetColourHelper(OctaveColour f_oct) {
                foreach (var bit in Bits) {
                    bit.setOct(f_oct);
                }

                CounterPart.SetColour(f_oct);
            }
            protected override void SetColourHelperHalf(OctaveColour f_oct, Half f_half) {
                int count;

                if (f_half == Half.Left) {
                    count = 0;
                }
                else {
                    count = -8;
                }

                foreach (var bit in Bits) {
                    if (count >= 0 && count <= 8) {
                        bit.setOct(f_oct);
                    }
                    count++;
                }
            
                var template = new NoteTemplate(this);
                CounterPart.SetColour(template);
            }
        }

        public class BlackNoteBitViewModal : NoteBitViewModal
        {
            public BlackNoteBitViewModal(Canvas f_notecan, NoteBitPos f_pos, NoteViewModal f_parent, NoteBitPos f_truepos) : base(f_notecan, f_pos, f_parent, f_truepos) {
                
            }

            public override void InitializeImage() {
                NoteBitImg = new Image() {
                    Height = 2,
                    Width = 6
                };
                NoteBitImg.Margin = setTruePos(TruePos);
            }
            public override Thickness setTruePos(NoteBitPos f_truepos) {
                switch (f_truepos) {
                    case NoteBitPos.a1: { return new Thickness(-2, 0, 0, 0); }
                    case NoteBitPos.a2: { return new Thickness(-2, 2, 0, 0); }
                    case NoteBitPos.a3: { return new Thickness(-2, 4, 0, 0); }
                    case NoteBitPos.a4: { return new Thickness(-2, 6, 0, 0); }
                    case NoteBitPos.a5: { return new Thickness(-2, 8, 0, 0); }
                    case NoteBitPos.a6: { return new Thickness(-2, 10, 0, 0); }
                    case NoteBitPos.a7: { return new Thickness(-2, 12, 0, 0); }
                    case NoteBitPos.a8: { return new Thickness(-2, 14, 0, 0); }
                    case NoteBitPos.b1: { return new Thickness(0, 0, -2, 0); }
                    case NoteBitPos.b2: { return new Thickness(0, 2, -2, 0); }
                    case NoteBitPos.b3: { return new Thickness(0, 4, -2, 0); }
                    case NoteBitPos.b4: { return new Thickness(0, 6, -2, 0); }
                    case NoteBitPos.b5: { return new Thickness(0, 8, -2, 0); }
                    case NoteBitPos.b6: { return new Thickness(0, 10, -2, 0); }
                    case NoteBitPos.b7: { return new Thickness(0, 12, -2, 0); }
                    case NoteBitPos.b8: { return new Thickness(0, 14, -2, 0); }
                }
                throw new Exception();
            }
            public override void setOct(OctaveColour f_oct, NoteBitPos f_pos) {
                NoteBitImg.Source = BitImages.GetBitImg(f_pos, f_oct, NoteType.Black);
                Oct = f_oct;
                CheckForNote();
            }
            public override void setOct(OctaveColour f_oct) {
                NoteBitImg.Source = BitImages.GetBitImg(Pos, f_oct, NoteType.Black);
                Oct = f_oct;
                CheckForNote();
            }
        }




        public class NoteTemplate
        {
            public OctaveColour[] Colours { get; set; }
            public NoteBitPos[] Positions { get; set; }

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

                /*
                int count = 0;
                foreach (var bit in f_note.Halves.Item1.Bits) {
                    Colours[count] = bit.Oct;
                    Positions[count] = bit.Pos;
                    count++;
                }
                foreach (var bit in f_note.Halves.Item2.Bits) {
                    Colours[count] = bit.Oct;
                    Positions[count] = bit.Pos;
                    count++;
                }
                */
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
                var f_newnote = new NoteTemplate(this);
                for (int i = 0; i < 8; i++) {
                    f_newnote.Positions[i] = NoteBitPos.a8;
                }
                for (int i = 8; i < 16; i++) {
                    f_newnote.Positions[i] = NoteBitPos.b8;
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

