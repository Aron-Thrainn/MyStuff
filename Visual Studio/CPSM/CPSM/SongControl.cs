using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClasses.IniSpace;

namespace CPSM_class_diagram
{
    public class SongControl {
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
        public void SetName(string f_name) {
            ActiveSong.Name = f_name;
        }
        public void SetSource(string f_source) {
            ActiveSong.Source = f_source;
        }
        public int GetNewID() {
            throw new NotImplementedException();
        }
    }



    public enum NoteBitPos {
        a1,
        a2,
        a3,
        a4,
        a5,
        a6,
        a7,
        a8,
        b1,
        b2,
        b3,
        b4,
        b5,
        b6,
        b7,
        b8
    }
    public enum OctaveColour {
        none,
        Brown,
        Teal,
        Blue,
        Green,
        Red,
        Purple,
        Yellow
    }
    public enum Half {
        Left,
        Right
    }
    public enum MeasureSize {
        four = 4,
        six = 6
    }

    public class Song {
        public int ID { get; set; }
        public Stack<Measure> Measures { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }

        public Song(int f_ID) {
            ID = f_ID;
            Measures = new Stack<Measure>();
            Name = "";
            Source = "";
        }

        public void AddMeasure(MeasureSize f_size) {
            var NewMeasure = new Measure(this, f_size);
            Measures.Push(NewMeasure);
        }

    }

    public class Measure {
        public Note[,] WhiteNotes { get; set; }
        public Note[,] BlackNotes { get; set; }
        public Song Parent { get; set; }
        public MeasureSize Size { get; set; }

        public Measure(Song f_parent, MeasureSize f_size) {
            Parent = f_parent;
            Size = f_size;

            var f_sizeint = (int)Size;
            WhiteNotes = new Note[7, f_sizeint];
            for (int i = 0; i <= 7; i++) {
                for (int o = 0; o <= f_sizeint; o++) {
                    WhiteNotes[i, o] = new Note(this);
                }
            }
            BlackNotes = new Note[5, f_sizeint];
            for (int i = 0; i <= 5; i++) {
                for (int o = 0; o <= f_sizeint; o++) {
                    BlackNotes[i, o] = new Note(this);
                }
            }

        }
    }
    
    public class Note {
        public Tuple<NoteHalf, NoteHalf> NoteHalves { get; set; }
        public Measure Parent { get; set; }

        public Note(Measure f_parent) {
            Parent = f_parent;
            NoteHalves = new Tuple<NoteHalf, NoteHalf>(new NoteHalf(this), new NoteHalf(this));

        }

        public void SetColour(OctaveColour f_oct) {
            NoteHalves.Item1.SetColour(f_oct);
            NoteHalves.Item2.SetColour(f_oct);
        }
        public void SetColourHalf(OctaveColour f_oct, Half f_half) {
            if (f_half == Half.Left) {
                NoteHalves.Item1.SetColour(f_oct);
            }
            else {
                NoteHalves.Item2.SetColour(f_oct);
            }
        }
    }

    public class NoteHalf {
        public NoteBit[] NoteBits { get; set; }
        public Note Parent { get; set; }

        public NoteHalf(Note f_parent) {
            Parent = f_parent;
            NoteBits = new NoteBit[8];
            for (int i = 0; i <= 8; i++) {
                NoteBits[i] = new NoteBit(this);
            }
        }

        public void SetColour(OctaveColour f_oct) {
            foreach (var bit in NoteBits) {
                bit.SetColour(f_oct);
            }
        }
    }

    public class NoteBit {
        public NoteHalf Parent { get; set; }
        public NoteBitPos pos { get; set; }
        public OctaveColour Oct { get; set; }

        public NoteBit(NoteHalf f_parent) {
            Parent = f_parent;
        }

        public void SetColour(OctaveColour f_oct) {
            Oct = f_oct;
        }
    }
}