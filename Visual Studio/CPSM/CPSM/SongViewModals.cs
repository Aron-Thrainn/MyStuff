using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CPSM_class_diagram
{
    namespace ViewModals
    {
        public class SongViewModalCreator
        {
        }

        public class MeasureViewModal
        {
            public Canvas Can { get; set; }

            public MeasureViewModal(Measure f_measure) {
                Can = new Canvas();

                var MeasureSize = f_measure.Size;
                for (int i = 0; i <= 7; i++) {
                    for (int o = 0; o <= (int)MeasureSize; o++) {
                        var tempNote = new WhiteNoteViewModal(f_measure.WhiteNotes[i,o], Can);
                    }
                }
                for (int i = 0; i <= 5; i++) {
                    for (int o = 0; o <= (int)MeasureSize; o++) {
                        var tempNote = new BlackNoteViewModal(f_measure.BlackNotes[i, o], Can);
                    }
                }

            }
        }

        public class NoteViewModal
        {
            public Note CounterPart { get; set; }

            public NoteViewModal(Note f_note, Canvas f_measureCan) {
                CounterPart = f_note;

            }
        }


        public class WhiteNoteViewModal : NoteViewModal
        {
            public Tuple<WhiteNoteHalfViewModal,WhiteNoteHalfViewModal> Halves { get; set; }

            public WhiteNoteViewModal(Note f_note, Canvas f_measureCan) : base(f_note, f_measureCan) {
                //create note halves
                //create canvas/stack panel
                //set position in measure canvas
                throw new NotImplementedException();
            }
        }
        public class WhiteNoteHalfViewModal
        {
            public List<WhiteNoteBitViewModal> Bits { get; set; }
        }
        public class WhiteNoteBitViewModal
        {
            public OctaveColour Oct { get; set; }
            public NoteBitPos pos { get; set; }
        }


        public class BlackNoteViewModal : NoteViewModal
        {
            public Tuple<BlackNoteHalfViewModal, BlackNoteHalfViewModal> Halves { get; set; }

            public BlackNoteViewModal(Note f_note, Canvas f_measureCan) : base(f_note, f_measureCan) {
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
