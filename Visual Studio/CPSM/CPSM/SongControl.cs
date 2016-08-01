using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClasses.IniSpace;

namespace CPSM_class_diagram
{
    public class SongControl
    {
        private IniFile SaveFile { get; set; }
        public Song ActiveSong { get; set; }

        public Song LoadSong() {
            throw new NotImplementedException();
        }
        public void SaveSong() {
            throw new NotImplementedException();
        }
        public void RemoveSong() {
            throw new NotImplementedException();
        }

        public void AddMeasure(MeasureSize f_size) {
            ActiveSong.AddMeasure(f_size);
        }

    }




    public enum MeasureSize
    {
        four = 4,
        six = 6
    }
    public class Song
    {
        public Stack<Measure> Measures { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }

        public Song() {
            Measures = new Stack<Measure>();
            Name = "";
            Source = "";
        }

        public void AddMeasure(MeasureSize f_size) {
            var NewMeasure = new Measure(this, f_size);
            Measures.Push(NewMeasure);
        }

    }
    
    public class Measure
    {
        public WhiteNote[,] WhiteNotes { get; set; }
        public BlackNote[,] BlackNotes { get; set; }
        public Song Parent { get; set; }
        public MeasureSize Size { get; set; }

        public Measure(Song f_parent, MeasureSize f_size) {
            Parent = f_parent;
            Size = f_size;

            var f_sizeint = (int)Size;
            WhiteNotes = new WhiteNote[7, f_sizeint];
            for (int i = 0; i <= 7; i++) {
                for (int o = 0; o <= f_sizeint; o++) {
                    WhiteNotes[i, o] = new WhiteNote(this);
                }
            }
            BlackNotes = new BlackNote[5, f_sizeint];
            for (int i = 0; i <= 5; i++) {
                for (int o = 0; o <= f_sizeint; o++) {
                    BlackNotes[i, o] = new BlackNote(this);
                }
            }

        }
    }

    public class WhiteNote
    {
        public Tuple<WhiteNoteHalf, WhiteNoteHalf> WhiteNoteHalves { get; set; }
        public Measure Parent { get; set; }

        public WhiteNote(Measure f_parent) {
            Parent = f_parent;
            for (int i = 0; i <= 12; i++) {
                WhiteNoteHalves = new Tuple<WhiteNoteHalf, WhiteNoteHalf>(new WhiteNoteHalf(this), new WhiteNoteHalf(this));
            }
        }
    }


    public class WhiteNoteHalf
    {
        public WhiteNoteBit[] WhiteNoteBits { get; set; }
        public WhiteNote Parent { get; set; }

        public WhiteNoteHalf(WhiteNote f_parent) {
            Parent = f_parent;
            WhiteNoteBits = new WhiteNoteBit[12];
            for (int i = 0; i <= 12; i++) {
                WhiteNoteBits[i] = new WhiteNoteBit(this);
            }
        }
    }

    public class WhiteNoteBit
    {
        public WhiteNoteHalf Parent { get; set; }

        public WhiteNoteBit(WhiteNoteHalf f_parent) {
            Parent = f_parent;
        }
    }


    public class BlackNote
    {
        public Tuple<BlackNoteHalf, BlackNoteHalf> BlackNoteHalves { get; set; }
        public Measure Parent { get; set; }

        public BlackNote(Measure f_parent) {
            Parent = f_parent;
            for (int i = 0; i <= 12; i++) {
                BlackNoteHalves = new Tuple<BlackNoteHalf, BlackNoteHalf>(new BlackNoteHalf(this), new BlackNoteHalf(this));
            }
        }
    }

    public class BlackNoteHalf
    {
        public BlackNoteBit[] BlackNoteBits { get; set; }
        public BlackNote Parent { get; set; }

        public BlackNoteHalf(BlackNote f_parent) {
            Parent = f_parent;
            BlackNoteBits = new BlackNoteBit[12];
            for (int i = 0; i <= 12; i++) {
                BlackNoteBits[i] = new BlackNoteBit(this);
            }
        }
    }

    public class BlackNoteBit
    {
        public BlackNoteHalf Parent { get; set; }

        public BlackNoteBit(BlackNoteHalf f_parent) {
            Parent = f_parent;
        }
    }
}
