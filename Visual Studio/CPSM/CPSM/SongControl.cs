using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClasses.IniSpace;
using CPSM.ViewModals;

namespace CPSM
{
    /*
    public class SongControl {
        private IniFile SaveFile { get; set; }
        public SongData ActiveSong { get; set; }

        public SongData LoadSong() {
            throw new NotImplementedException();
        }
        public void SaveSong() {


            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\\Users\\Notandi\\Desktop\\temptxt.txt")) {

                file.Write("N(" + ActiveSong.Name + ")");
                file.Write("S(" + ActiveSong.Source + ")");

                foreach (var measure in ActiveSong.Measures) {
                    file.Write('M');
                    if ((int)measure.Size >= 10) {
                        file.Write(0);
                        file.Write(((int)measure.Size));
                    }
                    else file.Write((int)measure.Size);

                    file.Write('(');
                    foreach (var note in measure.WhiteNotes) {
                        var temp = new NoteTemplate(note);
                        if (temp.isUsniform() != null) {
                            file.Write((int)temp.isUsniform().Value);
                        }
                    }
                    foreach (var note in measure.BlackNotes) {

                    }
                    file.Write(')');

                }
            }
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
    */


    public enum NoteBitPos {
        a1 = 0,
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
        none = 0,
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
    public enum NoteType
    {
        White,
        Black
    }
    public enum MeasureSize {
        four = 4,
        six = 6,
        eight = 8,
        ten = 10,
        twelve = 12
    }




    public class SongData {
        public int ID { get; set; } // might use name as unique primary key
        public List<MeasureData> Measures { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }

        //meta data about the song
        public int PageCount { get; set; }
        public List<int> MeasuresPerPage { get; set; }

        public SongData(int f_ID) {
            ID = f_ID;
            Measures = new List<MeasureData>();
            Name = "";
            Source = "";
        }

        public void AddMeasure(MeasureSize f_size) {
            var NewMeasure = new MeasureData(this, f_size);
            Measures.Add(NewMeasure);
        }
        public void DeleteMeasure() {
            Measures.RemoveAt(Measures.Count-1);
        }
    }
    public class MeasureData {
        public NoteData[,] WhiteNotes { get; set; }
        public NoteData[,] BlackNotes { get; set; }
        public SongData Parent { get; set; }
        public MeasureSize Size { get; set; }

        public MeasureData(SongData f_parent, MeasureSize f_size) {
            Parent = f_parent;
            Size = f_size;

            var f_sizeint = (int)Size;
            WhiteNotes = new NoteData[14, f_sizeint];
            for (int i = 0; i < 14; i++) {
                for (int o = 0; o < f_sizeint; o++) {
                    WhiteNotes[i, o] = new NoteData(this);
                }
            }
            BlackNotes = new NoteData[10, f_sizeint];
            for (int i = 0; i < 10; i++) {
                for (int o = 0; o < f_sizeint; o++) {
                    BlackNotes[i, o] = new NoteData(this);
                }
            }

        }

    }
    
    public class NoteData {
        public MeasureData Parent { get; set; }
        public OctaveColour[] Colours { get; set; }
        public NoteBitPos[] Positions { get; set; }

        public NoteData(MeasureData f_parent) {
            Parent = f_parent;
            Colours = new OctaveColour[16];
            Positions = new NoteBitPos[16] {NoteBitPos.a1, NoteBitPos.a2, NoteBitPos.a3, NoteBitPos.a4, NoteBitPos.a5,
                NoteBitPos.a6, NoteBitPos.a7, NoteBitPos.a8, NoteBitPos.b1, NoteBitPos.b2, NoteBitPos.b3, NoteBitPos.b4,
                NoteBitPos.b5, NoteBitPos.b6, NoteBitPos.b7, NoteBitPos.b8 };

            
            SetColour(OctaveColour.none);
        }

        public void SetNote(NoteTemplate f_template) {
            for (int i = 0; i < 16; i++) {
                Colours[i] = f_template.Colours[i];
                Positions[i] = f_template.Positions[i];
            }
        }
        public void SetColour(OctaveColour f_oct) {
            for (int i=0; i<16; i++) {
                Colours[i] = f_oct;
            }
        }
        public void SetColourHalf(OctaveColour f_oct, Half f_half) {
            if (f_half == Half.Left) {
                for (int i = 0; i < 8; i++) {
                    Colours[i] = f_oct;
                }
            }
            else {
                for (int i = 8; i < 16; i++) {
                    Colours[i] = f_oct;
                }
            }
        }
        public void SetColour(NoteTemplate f_template) {
            for (int i = 0; i < 16; i++) {
                Colours[i] = f_template.Colours[i];
            }
        }

    }
    /*

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
            WhiteNotes = new Note[14, f_sizeint];
            for (int i = 0; i < 14; i++) {
                for (int o = 0; o < f_sizeint; o++) {
                    WhiteNotes[i, o] = new Note(this);
                }
            }
            BlackNotes = new Note[10, f_sizeint];
            for (int i = 0; i < 10; i++) {
                for (int o = 0; o < f_sizeint; o++) {
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
            NoteHalves = new Tuple<NoteHalf, NoteHalf>(new NoteHalf(this, Half.Left), new NoteHalf(this, Half.Right));

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
        public void SetColour(NoteTemplate f_template) {
            int count = 0;
            foreach (var bit in NoteHalves.Item1.NoteBits) {
                bit.SetColour(f_template.Colours[count], f_template.Positions[count]);
                count++;
            }
            foreach (var bit in NoteHalves.Item2.NoteBits) {
                bit.SetColour(f_template.Colours[count], f_template.Positions[count]);
                count++;
            }
        }
    }

    public class NoteHalf {
        public NoteBit[] NoteBits { get; set; }
        public Note Parent { get; set; }
        public Half Half { get; set; }

        public NoteHalf(Note f_parent, Half f_half) {
            Parent = f_parent;
            Half = f_half;
            NoteBits = new NoteBit[8];
            for (int i = 0; i < 8; i++) {
                var poscount = ((int)f_half * 8) + i;
                NoteBits[i] = new NoteBit(this, (NoteBitPos)poscount);
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

        public NoteBit(NoteHalf f_parent, NoteBitPos f_pos) {
            Parent = f_parent;
            SetPosition(f_pos);
            pos = f_pos;
        }

        public void SetColour(OctaveColour f_oct) {
            Oct = f_oct;
            SetPosition(pos);
        }
        public void SetColour(OctaveColour f_oct, NoteBitPos f_pos) {
            Oct = f_oct;
            SetPosition(f_pos);
        }
        public void SetPosition(NoteBitPos f_pos) {
            pos = f_pos;
        }
    }*/
}