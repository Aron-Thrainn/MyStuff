using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Interop;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Input;
using CPSM.ViewModals;
using CPSM.Forms;
using System.Threading;
using System.Windows.Threading;
using CommonClasses.Images;

namespace CPSM
{
    #region Main UI Elements
    public class GUI
    {
        public MainWindow _Window { get; set; }
        public Canvas Can { get; set; }
        public SongCanvas _SongCan { get; set; }
        public NoteColourForm _FormNoteColour { get; set; }
        public MeasureCreatorForm _FormMeasureCreator { get; set; }
        public SaveAndLoadForm _SaveLoadForm { get; set; }
        public PagesForm _FormPages { get; set; }
        public VersionForm _VersionForm { get; set; }

        public GUI(MainWindow f_wind, Canvas f_can, SongCanvas f_songcan) {
            _Window = f_wind;
            Can = f_can;
            _SongCan = f_songcan;
        }

        public void initNoteColourForm(NoteColourForm f_form, List<Canvas> f_cans, ComboBox f_NoteSizeBox) {
            _FormNoteColour = f_form;
            _FormNoteColour.Init(f_cans, f_NoteSizeBox);
        }
        public void initMeasureCreatorForm(MeasureCreatorForm f_form) {
            _FormMeasureCreator = f_form;
            _FormMeasureCreator._Song = _SongCan;
        }
        public void initPagesForm(Canvas f_Next, Canvas f_prev) {
            _FormPages = new PagesForm(_SongCan._Creator, f_Next, f_prev);
        }
        public void initVersionForm(Label f_Label, Version f_Version) {
            _VersionForm = new VersionForm(f_Label, f_Version);
        }
    }

    public class SongCanvas
    {
        public Canvas SongCan { get; set; }
        public Canvas MeasuresCan { get; set; }
        public MainWindow _Window { get; set; }
        public List<MeasureViewModal> Measures { get; set; }
        public SongViewModalCreator _Creator { get; set; }
        public NoteDisplay _NoteDisp { get; set; }
        public SongData ActiveSongData { get; set; }

        public SongCanvas(MainWindow f_wind, Canvas f_songcan, SongViewModalCreator f_creator) {
            _Window = f_wind;
            SongCan = f_songcan;
            _Creator = f_creator;
        }


        public void LoadSong(string f_title, string f_source) {
            var f_loader = new SongLoader(@"C:\\Users\\Notandi\\Desktop\\temptxt.txt");
            var f_song = f_loader.LoadSong(f_title, f_source);
            if (f_song != null) {
                LoadSongFromData(f_song);
            }
        }
        public void LoadSongFromData(SongData f_song) {
            ActiveSongData = f_song;
            _Creator.LoadSong(f_song);
        }
        public void SaveSong() {
            try {
                var saver = new SongSaver(ActiveSongData);
            }
            catch {
                _Window._GUI._SaveLoadForm.SaveConfirm(false);
                return;
            }
            _Window._GUI._SaveLoadForm.SaveConfirm(true);
        }
        public void CreateNoteDisplay(Canvas f_can) {
            _NoteDisp = new NoteDisplay(f_can);
        }
        public void AddNewMeasure(MeasureSize f_size) {
            //ActiveSongData.AddMeasure(f_size);
            var f_tempmes = new MeasureData(ActiveSongData, f_size);
            ActiveSongData.AddMeasure(f_tempmes);
            _Creator.CreateMeasure(f_tempmes, true);
        }
        public void DeleteMeasure() {
            ActiveSongData.DeleteMeasure();
            _Creator.DeleteMeasure();
        }
        public void InitializeNoteDisplay(Canvas f_can) {
            _NoteDisp = new NoteDisplay(f_can);
        }


        public class NoteDisplay
        {
            public NoteDisplay(Canvas f_candisplay) {
                for (int i = 1; i < 8; i++) {
                    //create image & set position
                    var f_NoteImage = new Image() {
                        Height = 16,
                        Width = 12,
                        Stretch = Stretch.Fill,
                        Source = ImageControl.NoteImg((OctaveColour)i, NoteType.White),
                        Margin = new Thickness(2 + i * 14, 2, 0, 0)
                    };
                    f_candisplay.Children.Add(f_NoteImage);

                }
            }
        }

    }

    public class SongSaver {

        public int i { get; set; }
        public int o { get; set; }
        public int count { get; set; }
        public int currMesSize { get; set; }
        public int state { get; set; }
        public NoteTemplate prevNote { get; set; }
        public StringBuilder _Line { get; set; }

        public static string PCPath = @"C:\\Users\\Notandi\\Desktop\\temptxt.txt";
        public static string LaptopPath = @"C:\\Users\\Arthas Menethil\\Desktop\\temptxt.txt";


        #region static chars
        private static string TITLE = "T";
        private static string SOURCE = "S";
        private static string MEASCOUNT = "C";
        private static string PAGECOUNT = "V";
        private static string DATE = "D";
        private static string MEASURE = "M";
        private static string NOTE = "N";
        private static string BIT = "B";
        private static string EXTENSION = "X";
        private static string OPEN = "(";
        private static string CLOSE = ")";
        private static string END = ";";



        private static string O = "o";
        private static string Q = "q";
        private static string W = "w";
        private static string E = "e";
        private static string R = "r";
        private static string T = "t";
        private static string Y = "y";
        private static string U = "u";
         
        //a & b reserved for note bit pos
        #endregion

        public SongSaver(SongData f_Song) {
            _Line = new StringBuilder();
            _Line.Append(TITLE + OPEN + f_Song.Title + CLOSE);
            _Line.Append(SOURCE + OPEN + f_Song.Source + CLOSE);
            _Line.Append(MEASCOUNT + OPEN + f_Song.Measures.Count + CLOSE);
            _Line.Append(PAGECOUNT + OPEN + f_Song.PageCount + CLOSE);
            _Line.Append(DATE + OPEN + DateTime.Now.ToShortDateString() + CLOSE);

            foreach (var measure in f_Song.Measures) {
                currMesSize = (int)measure.Size;
                
                _Line.Append(MEASURE);
                if ((int)measure.Size < 10) {
                    _Line.Append(0);
                    _Line.Append(currMesSize + OPEN);
                }
                else
                    _Line.Append(currMesSize + OPEN);

                i = 0;
                o = 0;
                count = 0;
                state = 0;
                // 0 = norm, 1 = counting 0s, 2 = counting non-0s
                while (true) {
                    if (i >= 14) { // reached end of this measure
                        _Line.Append(WriteNote(prevNote, count));
                        break;
                    }
                    var temp = new NoteTemplate(measure.WhiteNotes[i, o]);
                    var tempcol = temp.isUsniform();

                    switch (state) {
                        case 0: {
                            if (tempcol == OctaveColour.none) { // note is empty
                                state = 1;
                            }
                            else { // note is not empty
                                state = 2;
                            }
                            prevNote = temp;
                            count = 1;
                            IncrimentNote();
                            break;
                        }
                        case 1: { // note is empty
                            if (tempcol == OctaveColour.none) {
                                count++;
                                IncrimentNote();
                                break;
                            }
                            else {
                                _Line.Append(WriteNote(prevNote, count));
                                state = 0;
                            }
                            break;
                        }
                        case 2: { // note is not empty
                            var tempprev = prevNote.GetAsExtension();
                            if (CompareNotes(temp, tempprev)) {
                                count++;
                                IncrimentNote();
                                break;
                            }
                            else {
                                _Line.Append(WriteNote(prevNote, count));
                                state = 0;
                            }
                            break;
                        }
                    }
                }

                i = 0;
                o = 0;
                count = 0;
                state = 0;
                // 0 = norm, 1 = counting 0s, 2 = counting non-0s
                while (true) {
                    if (i >= 10) { // reached end of this measure
                        _Line.Append(WriteNote(prevNote, count));
                        break;
                    }
                    var temp = new NoteTemplate(measure.BlackNotes[i, o]);
                    var tempcol = temp.isUsniform();
                    if (tempcol != OctaveColour.none && i == 0 && o == 0) {

                    }

                    switch (state) {
                        case 0: {
                            if (tempcol == OctaveColour.none) { // note is empty
                                state = 1;
                            }
                            else { // note is not empty
                                state = 2;
                            }
                            prevNote = temp;
                            count = 1;
                            IncrimentNote();
                            break;
                        }
                        case 1: { // note is empty
                            if (tempcol == OctaveColour.none) {
                                count++;
                                IncrimentNote();
                                break;
                            }
                            else {
                                _Line.Append(WriteNote(prevNote, count));
                                state = 0;
                            }
                            break;
                        }
                        case 2: { // note is not empty
                            var tempprev = prevNote.GetAsExtension();
                            if (CompareNotes(temp, tempprev)) {
                                count++;
                                IncrimentNote();
                                break;
                            }
                            else {
                                _Line.Append(WriteNote(prevNote, count));
                                state = 0;
                            }
                            break;
                        }
                    }
                }
                _Line.Append(CLOSE);
            }
        _Line.Append(END);

            var f_Override = false;
            string[] f_lines = null;
            try {
                var f_Loader = new SongLoader(PCPath);
                var f_datas = f_Loader.LoadSongsSmall();
                var f_count = 0;
                foreach (var f_data in f_datas) {
                    if (f_data.Title == f_Song.Title && f_data.Source == f_Song.Source) {
                        f_lines = File.ReadAllLines(PCPath);
                        f_lines[f_count] = _Line.ToString();
                        f_Override = true;
                        break;
                    }
                    f_count++;
                }
            }
            catch { }
            if (!f_Override) {
                using (StreamWriter file = new StreamWriter(PCPath, true)) {
                    file.WriteLine(_Line.ToString());
                }
            }
            else {
                File.Delete(PCPath);
                foreach (var f_line in f_lines) {
                    using (StreamWriter file = new StreamWriter(PCPath, true)) {
                        file.WriteLine(f_line);
                    }
                }
            }
        }

        private void IncrimentNote() {
            o++;
            if (o >= currMesSize) {
                o = 0;
                i++;
            }
        }
        private string WrittenNote(NoteTemplate f_note) {
            if (f_note.isUsniform() == OctaveColour.none) { // note is empty
                return GetNoteKey(f_note.isUsniform().Value);
            }
            else if (f_note.IsSimple()) { // note is simple
                return GetNoteKey(f_note.isUsniform().Value);
            }
            else if (f_note.IsFull() && f_note.HalfColour(Half.Left) != null && f_note.HalfColour(Half.Right) != null &&
                (f_note.HalfColour(Half.Left) == f_note.HalfColour(Half.Right) + 1 || f_note.HalfColour(Half.Left) == f_note.HalfColour(Half.Right) - 1)) { // note is Dual-octival
                return NOTE + OPEN + GetNoteKey(f_note.HalfColour(Half.Left).Value) + GetNoteKey(f_note.HalfColour(Half.Right).Value) + CLOSE;
            }
            else { // note is complex
                var tempstring = new StringBuilder();
                tempstring.Append(NOTE + OPEN);

                OctaveColour f_tempcol = f_note.Colours[0];
                NoteBitPos f_temppos = f_note.Positions[0];
                tempstring.Append(AppendBit(f_tempcol, f_temppos));
                int count = 1;



                for (int i = 1; i < 16; i++) {

                    if (f_note.Colours[i] == f_tempcol && f_note.Positions[i] == (NoteBitPos)((int)f_temppos + 1)) {
                        //note bit is a continuation of previous one
                        count++;
                    }
                    else {
                        if (count != 0) {
                            // writing the duplicate notes
                            tempstring.Append(EXTENSION + count);
                            count = 1;
                        }
                        //note bit is not a continuation of previous one
                        tempstring.Append(AppendBit(f_note.Colours[i], f_note.Positions[i]));
                    }
                    f_temppos = f_note.Positions[i];
                    f_tempcol = f_note.Colours[i];
                }

                if (count != 0) {
                    // writing the duplicate notes
                    tempstring.Append(EXTENSION + count);
                    count = 1;
                }
                tempstring.Append(CLOSE);
                return tempstring.ToString();

            }
        }
        private string WriteNote(NoteTemplate f_note, int f_count) {
            if (f_count == 1) {
                return WrittenNote(f_note);
            }
            else if (f_count == 2) {
                string f_char = WrittenNote(f_note);
                return f_char + f_char;
            }
            else {
                return WrittenNote(f_note) + EXTENSION + count.ToString();
            }
        }
        private string GetNoteKey(OctaveColour f_col) {
            switch (f_col) {
                case OctaveColour.none: {
                    return O;
                }
                case OctaveColour.Brown: {
                    return Q;
                }
                case OctaveColour.Teal: {
                    return W;
                }
                case OctaveColour.Blue: {
                    return E;
                }
                case OctaveColour.Green: {
                    return R;
                }
                case OctaveColour.Red: {
                    return T;
                }
                case OctaveColour.Purple: {
                    return Y;
                }
                case OctaveColour.Yellow: {
                    return U;
                }
            }
            throw new Exception();
        }
        private string AppendBit(OctaveColour f_oct, NoteBitPos f_pos) {
            string pos;
            string oct = GetNoteKey(f_oct);
            pos = f_pos.ToString();
            return (BIT + OPEN + pos + oct + CLOSE);
        }
        private bool CompareNotes(NoteTemplate f_note1, NoteTemplate f_note2) {
            for (int i = 0; i < 16; i++) {
                if (f_note1.Colours[i] != f_note2.Colours[i] || f_note1.Positions[i] != f_note2.Positions[i]) {
                    return false;
                }
            }
            return true;
        }
    }

    public class SongLoader
    {

        public string Path { get; set; }
        public int i { get; set; }
        public int o { get; set; }
        public bool HandlingBlack { get; set; }
        public int count { get; set; }
        public bool LastHandled { get; set; }
        public NoteTemplate LastNote { get; set; }

        #region static chars
        private static string TITLE = "T";
        private static string SOURCE = "S";
        private static string MEASCOUNT = "C";
        private static string PAGECOUNT = "V";
        private static string DATE = "D";
        private static string MEASURE = "M";
        private static string NOTE = "N";
        private static string BIT = "B";
        private static string EXTENSION = "X";
        private static string OPEN = "(";
        private static string CLOSE = ")";
        private static string END = ";";


        private static string O = "o";
        private static string Q = "q";
        private static string W = "w";
        private static string E = "e";
        private static string R = "r";
        private static string T = "t";
        private static string Y = "y";
        private static string U = "u";

        //a & b reserved for note bit pos
        #endregion

        public SongLoader(string f_Path) {
            Path = f_Path;
        }

        public SongData LoadSong(string f_title, string f_source) {
            var f_Lines = File.ReadAllLines(Path);
            var f_Song = new SongData();
            string f_Line = "";

            foreach (var line in f_Lines) {
                //var f_temp1 = ReadName(line);
                //var f_temp2 = ReadSource(line);


                if (ReadName(line) == f_title && ReadSource(line) == f_source) {
                    f_Line = line;
                    break;
                }
            }
            if (f_Line == "") {
                return null; // song not found
            }

            f_Song.Title = f_title;
            f_Song.Source = f_source;
            count = 0;
            while (f_Line[count] != END[0]) {
                if (f_Line[count] == MEASURE[0] && f_Line[count + 3] == OPEN[0]) { //count+3 because measures are 3 characters long (m03, m12 ect)
                    string f_tempmes = MEASURE;
                    count++;
                    f_tempmes += f_Line[count].ToString();
                    count++;
                    f_tempmes += f_Line[count].ToString();
                    count++;
                    var f_messize = GetMeasureSize(f_tempmes);
                    var f_measure = new MeasureData(f_Song, f_messize);
                    count++;
                    i = 0;
                    o = 0;
                    HandlingBlack = false;
                    SetLast(new NoteTemplate(f_Line[count])); // first note
                    count++;
                    while (f_Line[count] != CLOSE[0]) { //read to end of measure
                        var f_char = f_Line[count];
                        if (f_Line[count] == EXTENSION[0]) {
                            HandleExt(f_measure, f_Line);
                        }
                        else if (f_Line[count] == NOTE[0]) {
                            HandleComplex(f_measure, f_Line);
                        }
                        else {  //simple note
                            HandleSimple(f_measure, f_Line);
                        }
                    }
                    AddNote(f_measure, 1);//add last note
                    f_Song.AddMeasure(f_measure);
                }
                count++;
            }
            return f_Song;
        }
        public List<SongDataSmall> LoadSongsSmall() {
            var f_Lines = File.ReadAllLines(Path);
            var f_SongDataSmall = new List<SongDataSmall>();
            foreach (var line in f_Lines) {
                var f_SmallData = new SongDataSmall();
                
                f_SmallData.Title = ReadName(line);
                f_SmallData.Source = ReadSource(line);
                f_SmallData.MeasureCount = ReadMeasureCount(line);
                f_SmallData.PageCount = ReadPageCount(line);
                f_SmallData.Date = DateTime.Parse(ReadDate(line));


                f_SongDataSmall.Add(f_SmallData);
            }
            return f_SongDataSmall;
        }

        private void HandleSimple(MeasureData f_measure, string f_Line) {
            AddNote(f_measure, 1);
            SetLast(new NoteTemplate(f_Line[count]));
            count++;
        }
        private void HandleComplex(MeasureData f_measure, string f_Line) {
            AddNote(f_measure, 1);
            var f_temptemp = new NoteTemplate();
            if (f_Line[count] == NOTE[0] && f_Line[count + 2] != BIT[0]) { 
                count++;
                count++;
                f_temptemp.SetHalfColour(Half.Left, f_Line[count]);
                count++;
                f_temptemp.SetHalfColour(Half.Right, f_Line[count]);
                count++;
                SetLast(f_temptemp);
                count++;
            }
            else {
                var f_CurrBit = 0;
                count++;
                count++;
                while(f_Line[count] == BIT[0]) {
                    count++;
                    count++;
                    var f_PosString = new StringBuilder();
                    f_PosString.Append(f_Line[count]);
                    f_PosString.Append(f_Line[count+1]);
                    count++;
                    count++;
                    var f_ColString = f_Line[count];
                    count++;
                    count++;
                    var f_ExtCount = new StringBuilder();
                    f_ExtCount.Append("0");
                    if (f_Line[count] == EXTENSION[0]) {
                        f_ExtCount = new StringBuilder();
                        count++;
                        while(f_Line[count] != BIT[0] && f_Line[count] != CLOSE[0]) {
                            f_ExtCount.Append(f_Line[count]);
                            count++;
                        }
                    }
                    var f_TempPos = GetPosFromString(f_PosString.ToString());
                    var f_TempCol = GetColFromString(f_ColString.ToString());
                    var f_tempExtCount = int.Parse(f_ExtCount.ToString());
                    var f_TempCounter = 0;

                    for (int i = 0; i < f_tempExtCount; i++) {
                        f_temptemp.Positions[f_CurrBit] = (NoteBitPos)(f_TempPos + f_TempCounter);
                        f_temptemp.Colours[f_CurrBit] = f_TempCol;
                        f_CurrBit++;
                        f_TempCounter++;
                    }
                }
                SetLast(f_temptemp);
                count++;
            }
        }
        private void HandleExt(MeasureData f_measure, string f_Line) {
            count++;
            var f_tempstring = new StringBuilder();
            while (char.IsDigit(f_Line[count])) {
                f_tempstring.Append(f_Line[count]);
                count++;
            }
            AddNote(f_measure, int.Parse(f_tempstring.ToString()));

            
        }

        private string ReadName(string f_line) {
            int count = 0;
            StringBuilder f_name = new StringBuilder();
            while (!(f_line[count] == TITLE[0] && f_line[count + 1] == OPEN[0])) {
                count++;
            }
            count++;
            count++;
            while (f_line[count] != CLOSE[0]) {
                f_name.Append(f_line[count]);
                count++;
            }
            return f_name.ToString();
        }
        private string ReadSource(string f_line) {
            int count = 0;
            StringBuilder f_Source = new StringBuilder();
            while (!(f_line[count] == SOURCE[0] && f_line[count + 1] == OPEN[0])) {
                count++;
            }
            count++;
            count++;
            while (f_line[count] != CLOSE[0]) {
                f_Source.Append(f_line[count]);
                count++;
            }
            return f_Source.ToString();
        }
        private int ReadMeasureCount(string f_line) {
            int count = 0;
            StringBuilder f_MesCount = new StringBuilder();
            while (!(f_line[count] == MEASCOUNT[0] && f_line[count + 1] == OPEN[0])) {
                count++;
            }
            count++;
            count++;
            while (f_line[count] != CLOSE[0]) {
                f_MesCount.Append(f_line[count]);
                count++;
            }
            return int.Parse(f_MesCount.ToString());
        }
        private int ReadPageCount(string f_line) {
            int count = 0;
            StringBuilder f_PageCount = new StringBuilder();
            while (!(f_line[count] == PAGECOUNT[0] && f_line[count + 1] == OPEN[0])) {
                count++;
            }
            count++;
            count++;
            while (f_line[count] != CLOSE[0]) {
                f_PageCount.Append(f_line[count]);
                count++;
            }
            return int.Parse(f_PageCount.ToString());
        }
        private string ReadDate(string f_line) {
            int count = 0;
            StringBuilder f_name = new StringBuilder();
            while (!(f_line[count] == DATE[0] && f_line[count + 1] == OPEN[0])) {
                count++;
            }
            count++;
            count++;
            while (f_line[count] != CLOSE[0]) {
                f_name.Append(f_line[count]);
                count++;
            }
            return f_name.ToString();
        }
        private MeasureSize GetMeasureSize(string f_str) {
            switch (f_str) {
                case "M04":
                return MeasureSize.four;
                case "M06":
                return MeasureSize.six;
                case "M08":
                return MeasureSize.eight;
                case "M10":
                return MeasureSize.ten;
                case "M12":
                return MeasureSize.twelve;
            }
            throw new Exception();
        }
        private void AddNote(MeasureData f_measure, int f_notecount) {
            if (LastHandled) { return; }
            for (int c = 0; c < f_notecount; c++) {
                try {
                    if (LastNote.isUsniform() != OctaveColour.none) {
                        if (!HandlingBlack) {
                            if (c == 0) {
                                f_measure.WhiteNotes[i, o].SetNote(LastNote);
                            }
                            else {
                                f_measure.WhiteNotes[i, o].SetNote(LastNote.GetAsExtension());
                            }
                        }
                        else {
                            if (c == 0) {
                                f_measure.BlackNotes[i, o].SetNote(LastNote);
                            }
                            else {
                                f_measure.BlackNotes[i, o].SetNote(LastNote.GetAsExtension());
                            }

                        }
                    }
                }
                catch { }
                o++;
                if (!HandlingBlack && o >= (int)f_measure.Size && i >= 13) {
                    HandlingBlack = true;
                    o = 0;
                    i = 0;
                }
                if (o >= (int)f_measure.Size) {
                    o = 0;
                    i++;
                }
            }
            LastHandled = true;
        }
        private void SetLast(NoteTemplate f_note) {
            LastNote = f_note;
            LastHandled = false;
        }
        private NoteBitPos GetPosFromString(string f_str) {
            switch (f_str) {
                case "a1": { return NoteBitPos.a1; }
                case "a2": { return NoteBitPos.a2; }
                case "a3": { return NoteBitPos.a3; }
                case "a4": { return NoteBitPos.a4; }
                case "a5": { return NoteBitPos.a5; }
                case "a6": { return NoteBitPos.a6; }
                case "a7": { return NoteBitPos.a7; }
                case "a8": { return NoteBitPos.a8; }
                case "b1": { return NoteBitPos.b1; }
                case "b2": { return NoteBitPos.b2; }
                case "b3": { return NoteBitPos.b3; }
                case "b4": { return NoteBitPos.b4; }
                case "b5": { return NoteBitPos.b5; }
                case "b6": { return NoteBitPos.b6; }
                case "b7": { return NoteBitPos.b7; }
                case "b8": { return NoteBitPos.b8; }
            }
            return NoteBitPos.a1;
        }
        private OctaveColour GetColFromString(string f_str) {
            switch (f_str) {
                case "o": { return OctaveColour.none; }
                case "q": { return OctaveColour.Brown; }
                case "w": { return OctaveColour.Teal; }
                case "e": { return OctaveColour.Blue; }
                case "r": { return OctaveColour.Green; }
                case "t": { return OctaveColour.Red; }
                case "y": { return OctaveColour.Purple; }
                case "u": { return OctaveColour.Yellow; }
            }
            return OctaveColour.none;
        }
    }
    #endregion
    #region Mouse Control
    public class MouseControl
    {
        public MainWindow _Window { get; set; }
        public MouseNoteControl _noteCtrl { get; set; }

        public MouseControl(MainWindow f_wind) {
            _Window = f_wind;
            _noteCtrl = new MouseNoteControl();
        }
    }
    public enum PartialNote
    {
        Full = 0,
        Half = 1,
        Quarter = 2,
        Eighth = 3
    }
    public class MouseNote
    {
        public NoteTemplate Template { get; set; }
        public OctaveColour ActiveColour { get; set; }
        public PartialNote ActivePatrial { get; set; }
        public MouseNoteControl _Parent { get; set; }
        public bool IsTemplate { get; set; }


        public MouseNote(MouseNoteControl f_parent) {
            ActiveColour = OctaveColour.none;
            ActivePatrial = PartialNote.Full;
            Template = null;
            IsTemplate = false;
            _Parent = f_parent;
        }
        public void SetTemplate(NoteTemplate f_template) {
            Template = f_template;
            IsTemplate = true;
        }
        public void SetColour(OctaveColour f_col) {
            ActiveColour = f_col;
            IsTemplate = false;
        }

        public void UpdatePreview(OctaveColour f_oct) {
            _Parent.UpdatePreview();
        }

    }

    public class MouseNoteControl
    {
        public MouseNote _colourctrl { get; set; }
        public NotePreview _Preview { get; set; }
        public NoteCreator _Creator { get; set; }
        public bool Creating { get; set; }
        public bool Suppressed { get; set; }
        public NoteColourForm _Form { get; set; }
        public NoteImageControl _NoteImageConrtol { get; set; }


        public MouseNoteControl() {
            _colourctrl = new MouseNote(this);
            _NoteImageConrtol = new NoteImageControl();
            Suppressed = false;
        }

        public void NoteClickUp(NoteViewModal sender, MouseButtonEventArgs e, Point f_mousepos) {
            switch (e.ChangedButton) {
                case MouseButton.Right: {
                    if (!Creating)
                        NoteRightClickedUp(sender, e, f_mousepos);
                    break;
                }
                case MouseButton.Left: {
                    NoteLeftClickedUp(sender, e, f_mousepos);
                    break;
                }
                case MouseButton.Middle: {
                    if (!Creating && !sender.CounterPart.IsEmpty()) {
                        _Form.SimulateClickDown(OctaveColour.none);
                        _Form.SimulateClickUp(OctaveColour.none);
                        NoteMiddleClickedUp(sender, e, f_mousepos);
                        _Form.SetFirstButtonIcon(_Preview.NoteImage);
                    }
                    break;
                }
            }
        }
        public void NoteClickDown(NoteViewModal sender, MouseButtonEventArgs e, Point f_mousepos) {
            switch (e.ChangedButton) {
                case MouseButton.Right: {
                    if (!Creating)
                        NoteRightClickedDown(sender, e, f_mousepos);
                    break;
                }
                case MouseButton.Left: {
                    NoteLeftClickedDown(sender, e, f_mousepos);
                    break;
                }
                case MouseButton.Middle: {

                    break;
                }
            }
        }

        public void NoteLeftClickedDown(NoteViewModal sender, MouseButtonEventArgs e, Point f_mousepos) {
            ResetPreview();
            var f_HeldNote = new NoteTemplate(_colourctrl, f_mousepos);
            _Creator = new NoteCreator(f_HeldNote, sender, false, this);
            Creating = true;
        }
        public void NoteLeftClickedUp(NoteViewModal sender, MouseButtonEventArgs e, Point f_mousepos) {
            MakePreview(sender, f_mousepos);
        }
        public void NoteRightClickedDown(NoteViewModal sender, MouseButtonEventArgs e, Point f_mousepos) {
            sender.ClearNote();
            UpdatePreview();
        }
        public void NoteRightClickedUp(NoteViewModal sender, MouseButtonEventArgs e, Point f_mousepos) {

        }
        public void NoteMiddleClickedUp(NoteViewModal sender, MouseButtonEventArgs e, Point f_mousepos) {
            _colourctrl.SetTemplate(new NoteTemplate(sender));
            MakePreview(sender, f_mousepos);
        }
        public void NoteMouseEnter(NoteViewModal sender, MouseEventArgs e, Point f_mousepos) {
            //leave triggers before enter when going from one to another
            if (Creating) {
                _Creator.AddNote(sender);
            }
            else if (e.RightButton == MouseButtonState.Pressed) {
                sender.ClearNote();
            }
            else {
                MakePreview(sender, f_mousepos);
            }
        }
        public void NoteMouseLeave(NoteViewModal sender, MouseEventArgs e, Point f_mousepos) {
            //leave triggers before enter when going from one to another

            if (Creating == false && _Preview != null && _Preview.Initialized == true) { // checks weather the preveiew has only just been created, else it loops
                ResetPreview();
            }
        }
        //need global mouse enevt handler for _creator to work properly

        public void MakePreview(NoteViewModal sender, Point f_mousepos) {
            if (!Suppressed) {
                var f_HeldNote = new NoteTemplate(_colourctrl, f_mousepos);
                ResetPreview();
                _Preview = new NotePreview(sender, f_HeldNote, false, true, this);
            }
        }
        public void UpdatePreview() {
            if (_Preview != null) {
                var f_tempnote = _Preview.Note;
                NoteTemplate f_temptemplate;
                if (_colourctrl.IsTemplate) {
                    f_temptemplate = new NoteTemplate(_colourctrl.Template);
                }
                else {
                    f_temptemplate = new NoteTemplate();
                    f_temptemplate.SetColour(_colourctrl.ActiveColour);
                }
                // sets the heldnote to f_oct colour full note, does not currently work for partial notes

                ResetPreview();
                _Preview = new NotePreview(f_tempnote, f_temptemplate, false, true, this);
            }
        }
        public void UpdatePreview(NoteTemplate f_Note) {
            var f_tempnote = _Preview.Note;
            var f_temptemplate = new NoteTemplate(f_Note);
            // sets the heldnote to f_oct colour full note, does not currently work for partial notes

            ResetPreview();
            _Preview = new NotePreview(f_tempnote, f_temptemplate, false, true, this);
        }
        public void GlobalMouseUp() {
            if (Creating) {
                _Creator.Activate();
                _Creator = null;
                Creating = false;
            }
            else {
            }
        }
        public void ResetPreview() {
            if (_Preview != null) {
                _Preview.Cancel();
            }
        }

        public void SuppressPreview() {
            Suppressed = true;
            ResetPreview();
        }
        public void UnSuppressPreview() {
            Suppressed = false;
        }
    }

    public class NotePreview
    {
        public NoteTemplate PreviewTemplate { get; set; }
        public NoteViewModal Note { get; set; }
        public bool NoteOverride { get; set; }
        public bool StartPoint { get; set; }
        public Canvas NoteImage { get; set; }
        public MouseNoteControl _Control { get; set; }
        public bool Initialized { get; set; }

        public NotePreview(NoteViewModal f_note, NoteTemplate f_HeldNote, bool f_override, bool f_startpoint, MouseNoteControl f_control) {
            _Control = f_control;
            Note = f_note;
            NoteOverride = f_override;
            StartPoint = f_startpoint;
            Initialized = false;
            var existingnote = new NoteTemplate(f_note as NoteViewModal);

            if (!f_startpoint) {
                f_HeldNote.SetAsExtension();
            }

            //PreviewTemplate = CreatePreview(f_override, f_HeldNote, existingnote);
            PreviewTemplate = CreatePreview(f_HeldNote, existingnote);
            PreviewTemplate.Type = Note.GetType();
            CreatePreviewImage();
            if (PreviewTemplate.isUsniform() != OctaveColour.none) {
                Display();
            }
            else {
                ForceDisplay();
            }
        }

        public void Display() {
            if (PreviewTemplate.IsEqual(new NoteTemplate(Note))) {
                ForceDisplay(); // note must still be loaded for middle click function
            }
            else {
                var f_xx = Note.NoteCan.Margin.Left;
                var f_yy = Note.NoteCan.Margin.Top;

                NoteImage.Margin = new Thickness(f_xx, f_yy, 0, 0);
                Note.Parent.Can.Children.Add(NoteImage);
                Note.NoteCan.Opacity = 0; //suppresses the existing note in favor of the preview
            }
        }
        public void Activate() {
            if (PreviewTemplate != null) {
                Cancel();
                Note.SetColour(PreviewTemplate, _Control._NoteImageConrtol.GetImage(PreviewTemplate));
            }
        }
        public void Cancel() {
            Note.Parent.Can.Children.Remove(NoteImage);
            Note.NoteCan.Opacity = 1;
            _Control._Preview = null;
        }
        public void ForceDisplay() {
            Note.Parent.Can.Children.Add(NoteImage);
            NoteImage.Loaded += delegate {
                Note.Parent.Can.Children.Remove(NoteImage);
            };

        }

        public bool HasTemplate() {
            return PreviewTemplate != null;
        }
        private NoteTemplate CreatePreview(bool f_override, NoteTemplate f_HeldNote, NoteTemplate f_ExistingNote) {
            var f_NewNote = new NoteTemplate();
            var f_overrideNote = f_override;
            var f_HeldCol = f_HeldNote.isUsniform();
            if (f_HeldCol == null) {
                return new NoteTemplate(f_HeldNote);
            }
            if (f_HeldCol == OctaveColour.none) { //do nothing
                return new NoteTemplate();
            }
            if (f_ExistingNote.isUsniform() == OctaveColour.none) {
                return new NoteTemplate(f_HeldNote);
            }

            if (f_ExistingNote.isUsniform() == f_HeldNote.isUsniform()) {
                if (!f_ExistingNote.IsExtension() && !f_HeldNote.IsExtension()) { //both are simple and not extensions
                    return new NoteTemplate(f_HeldNote, f_ExistingNote);
                }
                else if (f_ExistingNote.IsExtension() && f_HeldNote.IsExtension()) { //both are simple and extensions
                    return new NoteTemplate(f_HeldNote);
                }
                else
                    return new NoteTemplate(f_HeldNote);

            }

            if (f_overrideNote == false) { //Combine notes, heldnote is simple
                var f_ExistingCol = f_ExistingNote.isUsniform();
                if (f_ExistingCol == null) {
                    if (f_ExistingNote.HalfColour(Half.Left) != null && f_ExistingNote.HalfColour(Half.Right) != null) { //note is dual-octival
                        if (f_ExistingNote.HalfColour(Half.Left) == f_HeldCol) { //making it uniform
                            return new NoteTemplate(f_HeldNote);
                        }
                        else if (f_ExistingNote.HalfColour(Half.Right) == f_HeldCol) { //making it uniform
                            return new NoteTemplate(f_HeldNote);
                        }
                        else { //override existing note
                            return new NoteTemplate(f_HeldNote);
                        }

                    }
                    else { //Existing note is complex
                        return new NoteTemplate(f_HeldNote);

                        //simply replacing existing note with held note, 
                        //todo: complex combinations
                    }
                }
                else { //Held note & Existing note are simple
                    if ((int)f_ExistingCol == (int)f_HeldCol - 1) { //held colour is to the right of existing note
                        f_NewNote.SetHalfColour(Half.Left, f_ExistingNote);
                        f_NewNote.SetHalfColour(Half.Right, f_HeldNote);
                        return f_NewNote;
                    }
                    else if ((int)f_ExistingCol == (int)f_HeldCol + 1) { //held colour is to the left of existing note
                        f_NewNote.SetHalfColour(Half.Left, f_HeldNote);
                        f_NewNote.SetHalfColour(Half.Right, f_ExistingNote);
                        return f_NewNote;
                    }
                    else {  //held & existing colours are not next to each other
                        return new NoteTemplate(f_HeldNote);
                    }
                }
            }
            else {
                return new NoteTemplate(f_HeldNote);
            }
        }

        private NoteTemplate CreatePreview(NoteTemplate f_heldNote, NoteTemplate f_existingNote) {
            return new NoteTemplate(f_heldNote, f_existingNote);
        }

        private void CreatePreviewImage() {

            NoteImage = new Canvas() {
                Height = 16,
                Width = 12,
                Opacity = 0.5
            };
            if (PreviewTemplate.Type == NoteType.White) {
                for (var i = 0; i < 16; i++) {
                    var f_img = new Image() {
                        Height = 2,
                        Width = 6,
                        Source = BitImages.GetBitImg(PreviewTemplate.Positions[i], PreviewTemplate.Colours[i], PreviewTemplate.Type),
                        Margin = new Thickness(6 * (i / 8), i % 8 * 2, 0, 0),
                        
                };
                    NoteImage.Children.Add(f_img);
                    f_img.MouseLeave += new MouseEventHandler(NoteMouseLeave);
                    f_img.MouseEnter += new MouseEventHandler(NoteMouseBit);
                }
            }
            else {
                for (var i = 0; i < 16; i++) {
                    var f_img = new Image() {
                        Height = 2,
                        Width = 6,
                        Source = BitImages.GetBitImg(PreviewTemplate.Positions[i], PreviewTemplate.Colours[i], PreviewTemplate.Type),
                        Margin = new Thickness(4 * (i / 8), i % 8 * 2, 0, 0)
                    };
                    NoteImage.Children.Add(f_img);
                    f_img.MouseLeave += new MouseEventHandler(NoteMouseLeave);
                    f_img.MouseEnter += new MouseEventHandler(NoteMouseBit);
                }
            }
            
            //NoteImage.MouseLeave += new MouseEventHandler(NoteMouseLeave);
            //NoteImage.MouseEnter += new MouseEventHandler(NoteMouseEnter);
            NoteImage.MouseDown += new MouseButtonEventHandler(NoteClickDown);
            NoteImage.MouseUp += new MouseButtonEventHandler(NoteClickUp);

        }
        public void NoteClickDown(object sender, MouseButtonEventArgs e) {
            Point MousePos = e.GetPosition(NoteImage);
            _Control.NoteClickDown(Note, e, MousePos);
        }
        public void NoteClickUp(object sender, MouseButtonEventArgs e) {
            Point MousePos = e.GetPosition(NoteImage);
            _Control.NoteClickUp(Note, e, MousePos);
        }

        public void NoteMouseLeave(object sender, MouseEventArgs e) {
            Point MousePos = e.GetPosition(NoteImage);
            _Control.NoteMouseLeave(Note, e, MousePos);
        }
        public void NoteMouseEnter(object sender, MouseEventArgs e) {
            if (Initialized) { // avoids infinite loops when mouseEnter triggers on a new preview
                Point MousePos = e.GetPosition(null);
                _Control.NoteMouseEnter(Note, e, MousePos);
            }
            else
                Initialized = true;
        }
        public void NoteMouseBit(object sender, MouseEventArgs e) {
            NoteMouseEnter(NoteImage, e);
        }

    }

    public class NoteCreator
    {
        public Stack<NotePreview> NewNotes { get; set; }
        public NoteTemplate HeldNote { get; set; }
        public bool Override { get; set; }
        public MouseNoteControl _Control { get; set; }

        public NoteCreator(NoteTemplate f_heldNote, NoteViewModal f_startingnote, bool f_override, MouseNoteControl f_control) {
            NewNotes = new Stack<NotePreview>();
            HeldNote = f_heldNote;
            Override = f_override;
            _Control = f_control;

            PushNote(f_startingnote);
        }

        public void AddNote(NoteViewModal f_existingnote) {
            var f_preview = NewNotes.Peek();
            var f_previous = f_existingnote.FindNoteAbove();
            var f_next = f_existingnote.FindNoteBelow();
            if (f_previous != null) {
                if (f_preview.Note == f_previous) { PushNote(f_existingnote); }
                else if (f_preview.Note == f_previous.FindNoteToLeft()) { PushNote(f_existingnote.FindNoteToLeft()); }
                else if (f_preview.Note == f_previous.FindNoteToRight()) { PushNote(f_existingnote.FindNoteToRight()); }
                else if (f_preview.Note == f_previous.FindNoteToLeft().FindNoteToLeft()) { PushNote(f_existingnote.FindNoteToLeft().FindNoteToLeft()); }
                else if (f_preview.Note == f_previous.FindNoteToRight().FindNoteToRight()) { PushNote(f_existingnote.FindNoteToRight().FindNoteToRight()); }

            }
            if (f_next != null) {
                if (f_preview.Note == f_next
                || f_preview.Note == f_next.FindNoteToLeft()
                || f_preview.Note == f_next.FindNoteToRight()
                || f_preview.Note == f_next.FindNoteToLeft().FindNoteToLeft()
                || f_preview.Note == f_next.FindNoteToRight().FindNoteToRight()) {
                    PopNote();
                }
            }
        }
        public void PushNote(NoteViewModal f_existingnote) {
            var f_startpoint = (NewNotes.Count == 0);
            NewNotes.Push(new NotePreview(f_existingnote, HeldNote, Override, f_startpoint, _Control));
        }
        public void PopNote() {
            if (NewNotes.Count > 1) {
                var f_preview = NewNotes.Pop();
                f_preview.Cancel();
            }
        }
        public void Activate() {
            while (NewNotes.Count != 0) {
                var f_preview = NewNotes.Pop();
                f_preview.Activate();
            }
        }
    }

    #endregion

    #region Misc
    public class ScreenCapturer
    {
        public Canvas SongCan { get; set; }
        public string FilePath { get; set; }
        public MouseControl _MouseCtrl { get; set; }
        public SongViewModalCreator _Creator { get; set; }
        public DispatcherTimer _Timer { get; set; }
        private readonly double INTERVAL = 0.001;
        public int Stage { get; set; }
        public int PagesExported { get; set; }

        public ScreenCapturer(Canvas f_SongCan, MouseControl f_mousecontr, SongViewModalCreator f_Creator) {
            //Saves in project folder
            //FilePath = new FileInfo("TempImage.png").FullName.ToString();
            //Saves to desktop
            FilePath = "C:\\Users\\Notandi\\Desktop\\TempImage.png";
            //FilePath = "C:\\Users\\Arthas Menethil\\Desktop\\TempImage.png";
            SongCan = f_SongCan;
            _MouseCtrl = f_mousecontr;
            _Creator = f_Creator;
        }

        public void ExportAllPages() {
            Stage = 0;
            PagesExported = 0;

            _Timer = new DispatcherTimer();
            _Timer.Interval = TimeSpan.FromSeconds(INTERVAL);
            _Timer.Tick += Timer_Tick;
            _Timer.Start();
        }
        public void SaveScreenShot() {

            _MouseCtrl._noteCtrl.SuppressPreview();

            PresentationSource source = PresentationSource.FromVisual(SongCan);
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)SongCan.RenderSize.Width,
                  (int)SongCan.RenderSize.Height, 96, 96, PixelFormats.Default);

            VisualBrush sourceBrush = new VisualBrush(SongCan);
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            using (drawingContext) {
                drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0),
                      new Point(SongCan.RenderSize.Width, SongCan.RenderSize.Height)));
            }
            rtb.Render(drawingVisual);


            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(rtb));
            using (Stream fileStream = File.Create(FilePath)) {
                pngImage.Save(fileStream);
            }

            _MouseCtrl._noteCtrl.UnSuppressPreview();
        }
        private void Timer_Tick(object sender, EventArgs e) {
            switch (Stage) {
                case 0: {
                    var f_NextPage = PagesExported + 1;
                    if (_Creator.TotalPages < f_NextPage) {
                        Stage = 2;
                    }
                    else {
                        _Creator.GoToPage(f_NextPage);
                        FilePath = "C:\\Users\\Notandi\\Desktop\\TempImage" + f_NextPage.ToString() + ".png";
                        Stage = 1;
                    }
                    break;
                }
                case 1: {
                    SaveScreenShot();
                    PagesExported++;
                    Stage = 0;
                    break;
                }
                case 2: {
                    Timer_Destroy();
                    break;
                }
            }
        }
        private void Timer_Destroy() {
            _Timer.Stop();
            _Timer = null;
        }
    }


    public class Version
    {
        public string CurrentVersion { get; set; }

        public Version() {
            CurrentVersion = "Pre-Alpha";
        }
    }
}
    #endregion

