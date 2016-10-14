using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            public SongViewModalCreator(Canvas f_measurecan, MouseNoteControl f_mousectrl) {
                //MeasuresCan = f_measurecan;
                MeasureStack = new StackPanel() {
                    Margin = new Thickness(10, 10, 0, 0)
                };
                f_measurecan.Children.Add(MeasureStack);
                _Mouse = f_mousectrl;
                Measures = new List<MeasureViewModal>();
            }

            public void LoadSong(SongData f_song) {
                MeasureStack.Children.Clear();
                foreach (var mes in f_song.Measures) {
                    CreateMeasure(mes);
                }
            }
            public void CreateMeasure(MeasureData f_measure) {
                var modal = new MeasureViewModal(f_measure, _Mouse, this);
                Measures.Add(modal);
                MeasureStack.Children.Add(modal.Can);
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
        }

        public static class BitImages
        {
            public static CroppedBitmap GetBitImg(NoteBitPos f_pos, OctaveColour f_oct, NoteType f_type) {
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
                //default
                return new Int32Rect(0, 0, 6, 2);
            }
        }

        public class MeasureViewModal
        {
            public Canvas Can { get; set; }
            public Image ModalImg { get; set; }
            public WhiteNoteViewModal[,] WhiteNotes { get; set; }
            public BlackNoteViewModal[,] BlackNotes { get; set; }
            public MeasureSize Size { get; set; }
            public SongViewModalCreator Parent { get; set; }

            public MeasureViewModal(MeasureData f_measure, MouseNoteControl f_mouse, SongViewModalCreator f_parent) {
                Size = f_measure.Size;
                WhiteNotes = new WhiteNoteViewModal[14, (int)Size];
                BlackNotes = new BlackNoteViewModal[10, (int)Size];
                Parent = f_parent;
                Can = new Canvas() {
                    Height = ImageControl.MeasureImg(Size).Height-2, //-2 for the seperator
                    Width = 220
                };
                ModalImg = new Image() {
                    Source = ImageControl.MeasureImg(Size),
                    Width = 220,
                    Stretch = Stretch.None
                };
                Can.Children.Add(ModalImg);
                for (int i = 0; i < 14; i++) {
                    for (int o = 0; o < (int)Size; o++) {
                        var tempNote = new WhiteNoteViewModal(f_measure.WhiteNotes[i, o], Can, f_mouse, this, i, o);
                        var temptemplate = new NoteTemplate(f_measure.WhiteNotes[i, o]);
                        tempNote.SetColour(temptemplate);
                        WhiteNotes[i, o] = tempNote;
                    }
                }
                for (int i = 0; i < 10; i++) {
                    for (int o = 0; o < (int)Size; o++) {
                        var tempNote = new BlackNoteViewModal(f_measure.BlackNotes[i, o], Can, f_mouse, this);
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
                                    return f_previousmeasure.WhiteNotes[ii, (int)f_previousmeasure.Size-1];
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
                                    return f_previousmeasure.BlackNotes[ii, (int)f_previousmeasure.Size-1];
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
            public Tuple<WhiteNoteHalfViewModal, WhiteNoteHalfViewModal> Halves { get; set; }
            public MeasureViewModal Parent { get; set; }

            public NoteViewModal(NoteData f_note, Canvas f_measureCan, MouseNoteControl f_mouse, MeasureViewModal f_parent) {
                CounterPart = f_note;
                _Mouse = f_mouse;
                Parent = f_parent;
            }

            public void NoteLeftClickDown(object sender, MouseButtonEventArgs e) {
                Point MousePos = e.GetPosition(NoteCan);
                _Mouse.NoteLeftClickedDown(this, e, MousePos);
            }
            public void NoteLeftClickUp(object sender, MouseButtonEventArgs e) {
                Point MousePos = e.GetPosition(NoteCan);
                _Mouse.NoteLeftClickedUp(this, e, MousePos);
            }
            public void NoteRightClickDown(object sender, MouseButtonEventArgs e) {
                Point MousePos = e.GetPosition(NoteCan);
                _Mouse.NoteRightClickedDown(this, e, MousePos);
            }
            public void NoteRightClickUp(object sender, MouseButtonEventArgs e) {
                Point MousePos = e.GetPosition(NoteCan);
                _Mouse.NoteRightClickedUp(this, e, MousePos);
            }
            public void NoteMouseEnter(object sender, MouseEventArgs e) {
                Point MousePos = e.GetPosition(NoteCan);
                _Mouse.NoteMouseEnter(this, e, MousePos);
            }
            public void NoteMouseLeave(object sender, MouseEventArgs e) {
                Point MousePos = e.GetPosition(NoteCan);
                _Mouse.NoteMouseLeave(this, e, MousePos);
            }
            public virtual void SetColour(OctaveColour f_oct, PartialNote f_part) { }
            public virtual void SetColour(NoteTemplate f_template) { }

            public void ClearNote() {
                SetColour(new NoteTemplate());
                CounterPart.SetColour(new NoteTemplate());
            }
            protected virtual void SetColourHelper(OctaveColour f_oct) { }
            protected virtual void SetColourHelperHalf(OctaveColour f_oct, Half f_half) { }

            public virtual void SetPreview(NoteTemplate f_preview) { }
            public virtual void ClearPreview() { }

            public NoteViewModal FindNoteBelow() {
                return Parent.FindNoteBelow(this);
            }
            public NoteViewModal FindNoteAbove() {
                return Parent.FindNoteAbove(this);
            }
        }


        public class WhiteNoteViewModal : NoteViewModal
        {
            public new Tuple<WhiteNoteHalfViewModal, WhiteNoteHalfViewModal> Halves { get; set; }
            public int x { get; set; }
            public int y { get; set; }

            public WhiteNoteViewModal(NoteData f_note, Canvas f_measureCan, MouseNoteControl f_mouse, MeasureViewModal f_parent, int f_xpos, int f_ypos) : base(f_note, f_measureCan, f_mouse, f_parent) {
                NoteCan = new Canvas();
                Halves = new Tuple<WhiteNoteHalfViewModal, WhiteNoteHalfViewModal>(new WhiteNoteHalfViewModal(NoteCan, Half.Left, this), new WhiteNoteHalfViewModal(NoteCan, Half.Right, this));
                f_measureCan.Children.Add(NoteCan);
                SetPosition(f_xpos, f_ypos);
                x = f_xpos;
                y = f_ypos;
            }

            public void SetPosition(int f_xpos, int f_ypos) {
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

            public override void SetColour(NoteTemplate f_template) {
                //clear note and paste
                if (f_template == null) {
                    return;
                }
                int count = 0;
                foreach (var bit in Halves.Item1.Bits) {
                    bit.setOct(f_template.Colours[count], f_template.Positions[count]);
                    count++;
                }
                foreach (var bit in Halves.Item2.Bits) {
                    bit.setOct(f_template.Colours[count], f_template.Positions[count]);
                    count++;
                }
            }

            protected override void SetColourHelper(OctaveColour f_oct) {
                foreach (var bit in Halves.Item1.Bits) {
                    bit.setOct(f_oct);
                }
                foreach (var bit in Halves.Item2.Bits) {
                    bit.setOct(f_oct);
                }
                
                CounterPart.SetColour(f_oct);
            }
            protected override void SetColourHelperHalf(OctaveColour f_oct, Half f_half) {
                WhiteNoteHalfViewModal half;
                if (f_half == Half.Left) { half = Halves.Item1; }
                else { half = Halves.Item2; }

                foreach (var bit in half.Bits) {
                    bit.setOct(f_oct);
                }

                var template = new NoteTemplate(this);
                CounterPart.SetColour(template);
            }

            public override void SetPreview(NoteTemplate f_preview) {
                int counter = 0;
                foreach (var f_bit in Halves.Item1.Bits) {
                    if (f_bit.Oct == f_preview.Colours[counter] && f_bit.Pos == f_preview.Positions[counter]) {
                        f_bit.setOct(f_preview.Colours[counter], f_preview.Positions[counter]);
                        f_bit.NoteBitImg.Opacity = 0.5;


                    }
                    else {
                        f_bit.setOct(f_preview.Colours[counter], f_preview.Positions[counter]);
                        f_bit.NoteBitImg.Opacity = 0.5;
                    }
                    counter++;
                }
                foreach (var f_bit in Halves.Item2.Bits) {
                    if (f_bit.Oct == f_preview.Colours[counter] && f_bit.Pos == f_preview.Positions[counter]) {

                        f_bit.setOct(f_preview.Colours[counter], f_preview.Positions[counter]);
                        f_bit.NoteBitImg.Opacity = 0.5;
                    }
                    else {
                        f_bit.setOct(f_preview.Colours[counter], f_preview.Positions[counter]);
                        f_bit.NoteBitImg.Opacity = 0.5;
                    }
                    counter++;
                }
            }
            public override void ClearPreview() {
                SetColour(new NoteTemplate(CounterPart));
            }

        }
        public class WhiteNoteHalfViewModal
        {
            public List<WhiteNoteBitViewModal> Bits { get; set; }
            public Canvas NoteCan { get; set; }

            public WhiteNoteHalfViewModal(Canvas f_notecan, Half f_half, NoteViewModal f_parent) {
                NoteCan = f_notecan;
                Bits = new List<WhiteNoteBitViewModal>();

                for (int i = 0; i < 8; i++) {
                    Bits.Add(new WhiteNoteBitViewModal(NoteCan, ((NoteBitPos)i + 8 * (int)f_half), f_parent));
                }
            }
        }
        public class WhiteNoteBitViewModal
        {
            public Image NoteBitImg { get; set; }
            public Canvas NoteCan { get; set; }
            public NoteViewModal ParentNote { get; set; }
            public Grid ClickableGrid { get; set; }
            public NoteBitPos Pos { get; set; }
            public OctaveColour Oct { get; set; }

            public WhiteNoteBitViewModal(Canvas f_notecan, NoteBitPos f_pos, NoteViewModal f_parent) {
                NoteCan = f_notecan;
                ParentNote = f_parent;
                Pos = f_pos;
                Oct = OctaveColour.none;

                ClickableGrid = new Grid() {
                    Height = 2,
                    Width = 6,
                    Background = Brushes.Transparent
                };

                NoteBitImg = new Image() {
                    Height = 2,
                    Width = 6,
                    Stretch = Stretch.None
                };
                ClickableGrid.Children.Add(NoteBitImg);
                NoteCan.Children.Add(ClickableGrid);

                //debug:
                //setOct(OctaveColour.Blue, f_pos);

                ClickableGrid.MouseLeftButtonDown += new MouseButtonEventHandler(BitLeftClickDown);
                ClickableGrid.MouseLeftButtonUp += new MouseButtonEventHandler(BitLeftClickUp);
                ClickableGrid.MouseRightButtonDown += new MouseButtonEventHandler(BitRightClickDown);
                ClickableGrid.MouseRightButtonUp += new MouseButtonEventHandler(BitRightClickUp);
                ClickableGrid.MouseEnter += new MouseEventHandler(BitMouseEnter);
                ClickableGrid.MouseLeave += new MouseEventHandler(BitMouseLeave);
                setPos(f_pos);

            }

            public void setPos(NoteBitPos f_pos) {
                switch (f_pos) {
                    case NoteBitPos.a1: { ClickableGrid.Margin = new Thickness(0, 0, 0, 0); break; }
                    case NoteBitPos.a2: { ClickableGrid.Margin = new Thickness(0, 2, 0, 0); break; }
                    case NoteBitPos.a3: { ClickableGrid.Margin = new Thickness(0, 4, 0, 0); break; }
                    case NoteBitPos.a4: { ClickableGrid.Margin = new Thickness(0, 6, 0, 0); break; }
                    case NoteBitPos.a5: { ClickableGrid.Margin = new Thickness(0, 8, 0, 0); break; }
                    case NoteBitPos.a6: { ClickableGrid.Margin = new Thickness(0, 10, 0, 0); break; }
                    case NoteBitPos.a7: { ClickableGrid.Margin = new Thickness(0, 12, 0, 0); break; }
                    case NoteBitPos.a8: { ClickableGrid.Margin = new Thickness(0, 14, 0, 0); break; }
                    case NoteBitPos.b1: { ClickableGrid.Margin = new Thickness(6, 0, 0, 0); break; }
                    case NoteBitPos.b2: { ClickableGrid.Margin = new Thickness(6, 2, 0, 0); break; }
                    case NoteBitPos.b3: { ClickableGrid.Margin = new Thickness(6, 4, 0, 0); break; }
                    case NoteBitPos.b4: { ClickableGrid.Margin = new Thickness(6, 6, 0, 0); break; }
                    case NoteBitPos.b5: { ClickableGrid.Margin = new Thickness(6, 8, 0, 0); break; }
                    case NoteBitPos.b6: { ClickableGrid.Margin = new Thickness(6, 10, 0, 0); break; }
                    case NoteBitPos.b7: { ClickableGrid.Margin = new Thickness(6, 12, 0, 0); break; }
                    case NoteBitPos.b8: { ClickableGrid.Margin = new Thickness(6, 14, 0, 0); break; }
                }
            }
            public void setOct(OctaveColour f_oct, NoteBitPos f_pos) {
                NoteBitImg.Source = BitImages.GetBitImg(f_pos, f_oct, NoteType.White);
                Oct = f_oct;
                CheckForNote();
            }
            public void setOct(OctaveColour f_oct) {
                NoteBitImg.Source = BitImages.GetBitImg(Pos, f_oct, NoteType.White);
                Oct = f_oct;
                CheckForNote();
            }
            public void BitLeftClickDown(object sender, MouseButtonEventArgs e) {
                ParentNote.NoteLeftClickDown(sender, e);
            }
            public void BitLeftClickUp(object sender, MouseButtonEventArgs e) {
                ParentNote.NoteLeftClickUp(sender, e);
            }
            public void BitRightClickDown(object sender, MouseButtonEventArgs e) {
                ParentNote.NoteRightClickDown(sender, e);
            }
            public void BitRightClickUp(object sender, MouseButtonEventArgs e) {
                ParentNote.NoteRightClickUp(sender, e);
            }
            public void BitMouseEnter(object sender, MouseEventArgs e) {
                ParentNote.NoteMouseEnter(sender, e);
            }
            public void BitMouseLeave(object sender, MouseEventArgs e) {
                ParentNote.NoteMouseLeave(sender, e);
            }

            private void CheckForNote() {
                //make empty notes invisible
                if (Oct == OctaveColour.none) {
                    NoteBitImg.Opacity = 0;
                }
                else {
                    NoteBitImg.Opacity = 1;
                }
            }

        }


        public class BlackNoteViewModal : NoteViewModal
        {
            public new Tuple<BlackNoteHalfViewModal, BlackNoteHalfViewModal> Halves { get; set; }
            public bool Debugvar { get; set; }

            public BlackNoteViewModal(NoteData f_note, Canvas f_measureCan, MouseNoteControl f_mouse, MeasureViewModal f_parent) : base(f_note, f_measureCan, f_mouse, f_parent) {
                Debugvar = true;
                //create note halves
                //create canvas/stack panel
                //set position in MeasureData canvas
                //throw new NotImplementedException();
            }
        }
        public class BlackNoteHalfViewModal
        {
            public List<BlackNoteBitViewModal> Bits { get; set; }
        }
        public class BlackNoteBitViewModal
        {
            public OctaveColour Oct { get; set; }
            public NoteBitPos Pos { get; set; }
        }

        public class NoteTemplate
        {
            public OctaveColour[] Colours { get; set; }
            public NoteBitPos[] Positions { get; set; }

            public NoteTemplate() {
                Init();
                for (int i=0; i<16; i++) {
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
            public NoteTemplate(WhiteNoteViewModal f_note) {
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
            public NoteTemplate(BlackNoteViewModal f_note) {
                Colours = new OctaveColour[16];
                Positions = new NoteBitPos[16];

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
            }
            public NoteTemplate(MouseNoteColour f_mousecolour, Point f_mousepoint) {
                Init();

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
    }
}
