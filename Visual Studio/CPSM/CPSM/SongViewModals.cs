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

            public SongViewModalCreator() {

            }

            public BitmapImage GetBitImg(NoteBitPos f_pos, OctaveColour f_oct, NoteType f_type) {
                var ImageBit = ImageControl.NoteImg(f_oct, f_type);
                ImageBit.SourceRect = CreateRect(f_pos, f_type);
                return ImageBit;
            }
            public void CreateMeasure(Measure f_measure) {
                var MeasureSize = f_measure.Size;
                //create measureviewmodal
                //set modal pos on page
                //
                throw new NotImplementedException();
            }

            private Int32Rect CreateRect(NoteBitPos f_pos, NoteType f_type) {
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
            public Image MeasureImg { get; set; }

            public MeasureViewModal(Measure f_measure, MouseNoteControl f_mouse) {
                Can = new Canvas();

                var MeasureSize = f_measure.Size;
                for (int i = 0; i <= 7; i++) {
                    for (int o = 0; o <= (int)MeasureSize; o++) {
                        var tempNote = new WhiteNoteViewModal(f_measure.WhiteNotes[i, o], Can, f_mouse, i, o);
                    }
                }
                for (int i = 0; i <= 5; i++) {
                    for (int o = 0; o <= (int)MeasureSize; o++) {
                        var tempNote = new BlackNoteViewModal(f_measure.BlackNotes[i, o], Can, f_mouse);
                    }
                }
            }
        }

        public class NoteViewModal
        {
            public Note CounterPart { get; set; }
            public MouseNoteControl _Mouse { get; set; }
            public Canvas NoteCan { get; set; }

            public NoteViewModal(Note f_note, Canvas f_measureCan, MouseNoteControl f_mouse) {
                CounterPart = f_note;

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
        }


        public class WhiteNoteViewModal : NoteViewModal
        {
            public Tuple<WhiteNoteHalfViewModal,WhiteNoteHalfViewModal> Halves { get; set; }

            public WhiteNoteViewModal(Note f_note, Canvas f_measureCan, MouseNoteControl f_mouse, int f_xpos, int f_ypos) : base(f_note, f_measureCan, f_mouse) {
                NoteCan = new Canvas();
                Halves = new Tuple<WhiteNoteHalfViewModal, WhiteNoteHalfViewModal>(new WhiteNoteHalfViewModal(NoteCan, Half.Left), new WhiteNoteHalfViewModal(NoteCan, Half.Right));
                f_measureCan.Children.Add(NoteCan);
                SetPosition(f_xpos, f_ypos);
            }

            public void SetPosition(int f_xpos, int f_ypos) {
                NoteCan.Margin = new Thickness(f_xpos*12, f_ypos*16, 0,0);
            }

            

        }
        public class WhiteNoteHalfViewModal
        {
            public List<WhiteNoteBitViewModal> Bits { get; set; }
            public Canvas NoteCan { get; set; }

            public WhiteNoteHalfViewModal(Canvas f_notecan, Half f_half) {
                NoteCan = f_notecan;
                Bits = new List<WhiteNoteBitViewModal>();

                for (int i = 1; i <= 8; i++) {
                    Bits.Add(new WhiteNoteBitViewModal(NoteCan, ((NoteBitPos)i+i*(int)f_half)));
                }
            }
        }
        public class WhiteNoteBitViewModal
        {
            public Image NoteBitImg { get; set; }
            public Canvas NoteCan { get; set; }
            public NoteViewModal ParentNote { get; set; }
            
            public WhiteNoteBitViewModal(Canvas f_notecan, NoteBitPos f_pos) {
                NoteCan = f_notecan;

                NoteBitImg = new Image() {
                    Height = 2,
                    Width = 12,
                    Stretch = Stretch.None
                };
                NoteBitImg.MouseLeftButtonDown += new MouseButtonEventHandler(BitLeftClickDown);
                NoteBitImg.MouseLeftButtonUp += new MouseButtonEventHandler(BitLeftClickUp);
                NoteBitImg.MouseRightButtonDown += new MouseButtonEventHandler(BitRightClickDown);
                NoteBitImg.MouseRightButtonUp += new MouseButtonEventHandler(BitRightClickUp);
                setPos(f_pos);
                
            }

            public void setPos(NoteBitPos f_pos) {
                switch (f_pos) {
                    case NoteBitPos.a1: {
                            NoteBitImg.Margin = new Thickness(0,0,0,0);
                            break;
                        }
                }
                throw new NotImplementedException();
            }
            public void setOct(OctaveColour f_oct) {
                switch (f_oct) {
                    case OctaveColour.Blue: { break; }
                }
                throw new NotImplementedException();
            }
            public void BitLeftClickDown(object sender, MouseButtonEventArgs e) {
                ParentNote.NoteLeftClickDown(sender, e);
            }
            public void BitLeftClickUp(object sender, MouseButtonEventArgs e) {
                throw new NotImplementedException();
            }
            public void BitRightClickDown(object sender, MouseButtonEventArgs e) {
                throw new NotImplementedException();
            }
            public void BitRightClickUp(object sender, MouseButtonEventArgs e) {
                throw new NotImplementedException();
            }
        }


        public class BlackNoteViewModal : NoteViewModal
        {
            public Tuple<BlackNoteHalfViewModal, BlackNoteHalfViewModal> Halves { get; set; }

            public BlackNoteViewModal(Note f_note, Canvas f_measureCan, MouseNoteControl f_mouse) : base(f_note, f_measureCan, f_mouse) {
                //create note halves
                //create canvas/stack panel
                //set position in measure canvas
                throw new NotImplementedException();
            }
        }
        public class BlackNoteHalfViewModal
        {
            public List<BlackNoteBitViewModal> Bits { get; set; }
        }
        public class BlackNoteBitViewModal
        {
            public OctaveColour Oct { get; set; }
            public NoteBitPos pos { get; set; }
        }

    }

}
