using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClasses.IniSpace;
using CPSM.ViewModals;

namespace CPSM
{
    public enum NoteBitPos
    {
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
    public enum OctaveColour
    {
        none = 0,
        Brown,
        Teal,
        Blue,
        Green,
        Red,
        Purple,
        Yellow
    }
    public enum Half
    {
        Left,
        Right
    }
    public enum NoteType
    {
        White,
        Black
    }
    public enum MeasureSize
    {
        four = 4,
        six = 6,
        eight = 8,
        ten = 10,
        twelve = 12
    }



    public class SongDataSmall
    {
        public string Title { get; set; }
        public string Source { get; set; }
        public int MeasureCount { get; set; }
        public int PageCount { get; set; }
        public DateTime Date { get; set; }

        public SongDataSmall() {

        }
    }

    public class SongData
    {
        // name + Source is unique primary key
        public List<MeasureData> Measures { get; set; }
        public string Title { get; set; }
        public string Source { get; set; }

        //meta data about the song
        public int PageCount { get; set; }
        public List<int> MeasuresPerPage { get; set; }

        public SongData() {
            Measures = new List<MeasureData>();
            Title = "";
            Source = "";
            PageCount = 0;
            MeasuresPerPage = new List<int>();
        }
        /*public void AddMeasure(MeasureSize f_size) {
            var f_NewMeasure = new MeasureData(this, f_size);
            Measures.Add(f_NewMeasure);
        }*/
        public void AddMeasure(MeasureData f_measure) {
            Measures.Add(f_measure);
        }
        public void DeleteMeasure() {
            Measures.RemoveAt(Measures.Count - 1);
        }
    }
    public class MeasureData
    {
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

    public class NoteData
    {
        public MeasureData Parent { get; set; }
        public OctaveColour[] Colours { get; set; }
        public NoteBitPos[] Positions { get; set; }

        public NoteData(MeasureData f_parent) {
            Parent = f_parent;
            Colours = new OctaveColour[16];
            Positions = new NoteBitPos[16] {NoteBitPos.a1, NoteBitPos.a2, NoteBitPos.a3, NoteBitPos.a4, NoteBitPos.a5,
                NoteBitPos.a6, NoteBitPos.a7, NoteBitPos.a8, NoteBitPos.b1, NoteBitPos.b2, NoteBitPos.b3, NoteBitPos.b4,
                NoteBitPos.b5, NoteBitPos.b6, NoteBitPos.b7, NoteBitPos.b8 };


            SetNote(new NoteTemplate());
        }

        public void SetNote(NoteTemplate f_template) {
            for (int i = 0; i < 16; i++) {
                Colours[i] = f_template.Colours[i];
                Positions[i] = f_template.Positions[i];
            }
        }
        public void SetNote(OctaveColour f_oct) {
            for (int i = 0; i < 16; i++) {
                Colours[i] = f_oct;
            }
        }
        public bool IsEmpty() {
            foreach (var col in Colours) {
                if (col != OctaveColour.none) {
                    return false;
                }
            }
            return true;
        }

    }
}