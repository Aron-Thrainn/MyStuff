using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CPSM.ViewModals;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Interop;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Input;

namespace CPSM
{

    public class GUI {
        public MainWindow _Window { get; set; }
        public Canvas Can { get; set; }
        public SongCanvas _SongCan { get; set; }

        public GUI(MainWindow f_wind, Canvas f_can, SongCanvas f_songcan) {
            _Window = f_wind;
            Can = f_can;
        }

    }
    public class SongCanvas {
        public MainWindow _Window { get; set; }
        public List<MeasureViewModal> Measures { get; set; }
        public SongViewModalCreator _Creator { get; set; }


        public SongCanvas(MainWindow f_wind) {
            _Window = f_wind;
            _Creator = new SongViewModalCreator();
        }
    }

    public class FormSongSelect {
        public MainWindow _Window { get; set; }

        public FormSongSelect(MainWindow f_wind) {
            _Window = f_wind;
        }
    }

    public class MouseControl {
        public MainWindow _Window { get; set; }

        public MouseControl(MainWindow f_wind) {
            _Window = f_wind;
        }
    }
    public class MouseNoteColour { }

    public class MouseNoteControl
    {

        public void NoteLeftClickedDown(object sender, MouseButtonEventArgs e, Point f_mousepos, NoteViewModal f_modal) {
            throw new NotImplementedException();
        }
        public void NoteLeftClickedUp(object sender, MouseButtonEventArgs e, Point f_mousepos, NoteViewModal f_modal) {
            throw new NotImplementedException();
        }
        public void NoteRightClickedDown(object sender, MouseButtonEventArgs e, Point f_mousepos, NoteViewModal f_modal) {
            throw new NotImplementedException();
        }
        public void NoteRightClickedUp(object sender, MouseButtonEventArgs e, Point f_mousepos, NoteViewModal f_modal) {
            throw new NotImplementedException();
        }



    }

    public class HotkeyControl {
        public MainWindow _Window { get; set; }

        public HotkeyControl(MainWindow f_wind) {
            _Window = f_wind;
        }
    }

    public class ScreenCapturer {
        public Canvas SongCan { get; set; }
        public string FilePath { get; set; }

        
        public ScreenCapturer(Canvas f_SongCan) {
            //Saves in project folder
            //FilePath = new FileInfo("TempImage.png").FullName.ToString();
            //Saves to desktop
            FilePath = "C:\\Users\\Notandi\\Desktop\\TempImage.png";
            SongCan = f_SongCan;
        }

        public void SaveScreenShot() {
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
