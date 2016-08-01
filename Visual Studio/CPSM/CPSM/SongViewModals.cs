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

        public class WhiteNoteViewModal
        {
            public WhiteNoteViewModal(WhiteNote f_note, Canvas f_measureCan) {
                //create note halves
                //create canvas/stack panel
                //set position in measure canvas
                throw new NotImplementedException();
            }
        }
        public class WhiteNoteHalfViewModal
        {

        }
        public class WhiteNoteBitViewModal { }


        public class BlackNoteViewModal
        {
            public BlackNoteViewModal(BlackNote f_note, Canvas f_measureCan) {
                //create note halves
                //create canvas/stack panel
                //set position in measure canvas
                throw new NotImplementedException();
            }
        }
        public class BlackNoteHalfViewModal { }
        public class BlackNoteBitViewModal { }

    }

}
