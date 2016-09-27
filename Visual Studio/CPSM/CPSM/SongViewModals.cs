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
                var modal = new MeasureViewModal(f_measure, _Mouse);
                Measures.Add(modal);
                MeasureStack.Children.Add(modal.Can);
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

            public MeasureViewModal(MeasureData f_measure, MouseNoteControl f_mouse) {
                var MeasureSize = f_measure.Size;
                Can = new Canvas() {
                    Background = Brushes.AliceBlue,
                    Height = 300,
                    Width = 300
                };
                ModalImg = new Image() {
                    Source = ImageControl.MeasureImg(MeasureSize),
                    Width = 220,
                    Stretch = Stretch.None
                };
                Can.Children.Add(ModalImg);
                for (int i = 0; i < 14; i++) {
                    for (int o = 0; o < (int)MeasureSize; o++) {
                        var tempNote = new WhiteNoteViewModal(f_measure.WhiteNotes[i, o], Can, f_mouse, i, o);
                        var temptemplate = new NoteTemplate(f_measure.WhiteNotes[i, o]);
                        tempNote.SetColour(temptemplate);
                    }
                }
                for (int i = 0; i < 10; i++) {
                    for (int o = 0; o < (int)MeasureSize; o++) {
                        var tempNote = new BlackNoteViewModal(f_measure.BlackNotes[i, o], Can, f_mouse);
                    }
                }
            }
        }


        public class NoteViewModal
        {
            public NoteData CounterPart { get; set; }
            public MouseNoteControl _Mouse { get; set; }
            public Canvas NoteCan { get; set; }
            public Tuple<WhiteNoteHalfViewModal, WhiteNoteHalfViewModal> Halves { get; set; }

            

            public NoteViewModal(NoteData f_note, Canvas f_measureCan, MouseNoteControl f_mouse) {
                CounterPart = f_note;
                _Mouse = f_mouse;
            }

            public void NoteLeftClickDown(object sender, MouseButtonEventArgs e) {
                Point MousePos = e.GetPosition(NoteCan);
                _Mouse.NoteLeftClickedDown(sender, e, MousePos, this);
            }
            public void NoteLeftClickUp(object sender, MouseButtonEventArgs e) {
                Point MousePos = e.GetPosition(NoteCan);
                _Mouse.NoteLeftClickedUp(sender, e, MousePos, this);
            }
            public void NoteRightClickDown(object sender, MouseButtonEventArgs e) {
                Point MousePos = e.GetPosition(NoteCan);
                _Mouse.NoteRightClickedDown(sender, e, MousePos, this);
            }
            public void NoteRightClickUp(object sender, MouseButtonEventArgs e) {
                Point MousePos = e.GetPosition(NoteCan);
                _Mouse.NoteRightClickedUp(sender, e, MousePos, this);
            }
            public void NoteMouseEnter(object sender, MouseEventArgs e) {
                Point MousePos = e.GetPosition(NoteCan);
                _Mouse.NoteMouseEnter(sender, e, MousePos, this);
            }
            public void NoteMouseLeave(object sender, MouseEventArgs e) {
                Point MousePos = e.GetPosition(NoteCan);
                _Mouse.NoteMouseLeave(sender, e, MousePos, this);
            }
            public virtual void SetColour(OctaveColour f_oct, PartialNote f_part) { }
            public virtual void SetColour(NoteTemplate f_template) { }


            public void ClearNote() {
                SetColourHelper(OctaveColour.none);
            }
            protected virtual void SetColourHelper(OctaveColour f_oct) { }
            protected virtual void SetColourHelperHalf(OctaveColour f_oct, Half f_half) { }
        }


        public class WhiteNoteViewModal : NoteViewModal
        {
            public new Tuple<WhiteNoteHalfViewModal, WhiteNoteHalfViewModal> Halves { get; set; }

            public WhiteNoteViewModal(NoteData f_note, Canvas f_measureCan, MouseNoteControl f_mouse, int f_xpos, int f_ypos) : base(f_note, f_measureCan, f_mouse) {
                NoteCan = new Canvas();
                Halves = new Tuple<WhiteNoteHalfViewModal, WhiteNoteHalfViewModal>(new WhiteNoteHalfViewModal(NoteCan, Half.Left, this), new WhiteNoteHalfViewModal(NoteCan, Half.Right, this));
                f_measureCan.Children.Add(NoteCan);
                SetPosition(f_xpos, f_ypos);
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

            public override void SetColour(OctaveColour f_oct, PartialNote f_part) {
                switch(f_part) {
                    case PartialNote.Full: { SetColourFull(f_oct); break; }
                        //todo: other noteparts
                }
            }
            public override void SetColour(NoteTemplate f_template) {
                //clear note and paste
                int count = 0;
                foreach (var bit in Halves.Item1.Bits) {
                    bit.setOct(f_template.Colours[count], f_template.Positions[count]);
                    count++;
                }
                foreach (var bit in Halves.Item2.Bits) {
                    bit.setOct(f_template.Colours[count], f_template.Positions[count]);
                    count++;
                }


                //throw new NotImplementedException();
            }


            private void SetColourFull(OctaveColour f_oct) {
                if (f_oct == OctaveColour.none) {
                    //do nothing
                }
                else {
                    var template = new NoteTemplate(this);
                    var tempCol = template.isUsniform();


                    if (tempCol != null) {
                        if (tempCol == OctaveColour.none) {
                            //note not set
                            SetColourHelper(f_oct);
                        }
                        else {
                            //note is uniform
                            if (f_oct == tempCol) {
                                //do nothing
                            }
                            else if ((int)f_oct == (int)tempCol - 1) {// new colour is to the left
                                SetColourHelperHalf(f_oct, Half.Left);
                            }
                            else if ((int)f_oct == (int)tempCol + 1) {// new colour is to the right
                                SetColourHelperHalf(f_oct, Half.Right);
                            }
                            else {
                                SetColourHelper(f_oct);
                            }
                        }
                    }
                    else {
                        //note is dual-octival
                        if (template.HalfColour(Half.Left) == f_oct) { // making it uniform
                            SetColourHelperHalf(f_oct, Half.Right);
                        }
                        else if (template.HalfColour(Half.Right) == f_oct) { // making it uniform
                            SetColourHelperHalf(f_oct, Half.Left);
                        }

                    }

                }
            }

            protected override void SetColourHelper(OctaveColour f_oct) {
                foreach (var bit in Halves.Item1.Bits) {
                    bit.setOct(f_oct);
                }
                foreach (var bit in Halves.Item2.Bits) {
                    bit.setOct(f_oct);
                }

                var template = new NoteTemplate(this);
                CounterPart.SetColour(template);
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
                //throw new NotImplementedException();
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

            public BlackNoteViewModal(NoteData f_note, Canvas f_measureCan, MouseNoteControl f_mouse) : base(f_note, f_measureCan, f_mouse) {
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
                            for (int i=0; i<16; i++) {
                                Colours[i] = f_mousecolour.ActiveColour;
                                Positions[i] = (NoteBitPos)i;

                            }
                            break;
                        }
                        //todo:  cases for non-full notes

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
                int count = (int)f_half * 8;
                int end = 8 + ((int)f_half * 8);
                while (count < end) {
                    if (Colours[count] != OctaveColour.none) {
                        return Colours[count];
                    }
                    count++;
                }
                return null;

            }

            private void Init() {
                Colours = new OctaveColour[16];
                Positions = new NoteBitPos[16];

            }
        }
        public static class NoteTemplates
        {
            //probably wont be used
        }





    }
}
