using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPSM;
using System.Windows.Controls;
using CommonClasses.CustomButtons;
using CommonClasses.Images;
using System.Windows.Input;
using CPSM.ViewModals;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace CPSM
{
    namespace Forms
    {
        public class NoteColourForm
        {
            public MouseNote _MouseCtrl { get; set; }
            public Canvas MainCan { get; set; }
            public List<CustomRadioButton> RadioButtons { get; set; }
            public CustomRadioButtonGroup RadioGroup { get; set; }
            public CustomRadioButton FirstButton { get; set; }

            public NoteColourForm(Canvas f_can, MouseNote f_mousectrl) {
                MainCan = f_can;
                _MouseCtrl = f_mousectrl;
                RadioButtons = new List<CustomRadioButton>();
            }

            public void Init(List<Canvas> f_buttoncans) {
                RadioGroup = new CustomRadioButtonGroup();
                int count = 0;
                foreach (var can in f_buttoncans) {
                    var tempbtn = new CustomRadioButton(can, RadioGroup);
                    tempbtn.SetImg(ImageControl.noteIcons((OctaveColour)count));
                    tempbtn.Tag = count;
                    if (count == 0) {
                        tempbtn.SetButtonClickEvent(FirstButtonClickEvent);
                        FirstButton = tempbtn;
                    }
                    else {
                        tempbtn.SetButtonClickEvent(ClickEvent);
                    }
                    count++;

                    RadioButtons.Add(tempbtn);
                }
            }
            public void ClickEvent(object sender, MouseButtonEventArgs e) {
                var btn = sender as CustomRadioButton;
                _MouseCtrl.SetColour((OctaveColour)btn.Tag);
                //_MouseCtrl.UpdatePreview((OctaveColour)btn.Tag);
            }
            public void FirstButtonClickEvent(object sender, MouseButtonEventArgs e) {
                var btn = sender as CustomRadioButton;
                btn.SetImg(ImageControl.noteIcons(OctaveColour.none));
                _MouseCtrl.SetColour((OctaveColour)btn.Tag);
                _MouseCtrl.UpdatePreview((OctaveColour)btn.Tag);
            }
            public void SimulateClickDown(OctaveColour f_col) {
                CustomRadioButton tempbtn = null;
                foreach (var btn in RadioButtons) {
                    if ((OctaveColour)btn.Tag  == f_col) {
                        tempbtn = btn;
                        break;
                    }
                }

                tempbtn.SimulateButtonDown();


            }
            public void SimulateClickUp(OctaveColour f_col) {
                CustomRadioButton tempbtn = null;
                foreach (var btn in RadioButtons) {
                    if ((OctaveColour)btn.Tag == f_col) {
                        tempbtn = btn;
                        break;
                    }
                }

                tempbtn.SimulateButtonUp();


            }

            public void SetFirstButtonIcon(Canvas f_note) {
                var f_tempop = f_note.Opacity;
                f_note.Opacity = 1;
                f_note.Loaded += delegate {
                    PresentationSource source = PresentationSource.FromVisual(f_note);
                    RenderTargetBitmap rtb = new RenderTargetBitmap((int)f_note.RenderSize.Width,
                          (int)f_note.RenderSize.Height, 96, 96, PixelFormats.Default);

                    VisualBrush sourceBrush = new VisualBrush(f_note);
                    DrawingVisual drawingVisual = new DrawingVisual();
                    DrawingContext drawingContext = drawingVisual.RenderOpen();
                    using (drawingContext) {
                        drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0),
                              new Point(f_note.RenderSize.Width, f_note.RenderSize.Height)));
                    }
                    rtb.Render(drawingVisual);
                    var f_image = rtb as BitmapSource;
                    FirstButton.SetImg(f_image);
                    f_note.Opacity = f_tempop;
                };
                //also reset the image when button is pressed
            }
        }

        public class MeasureCreatorForm
        {
            public Canvas MainCan { get; set; }
            public CustomButton CreateButton { get; set; }
            public CustomButton DeleteButton { get; set; }
            public ComboBox ComboList { get; set; }
            public SongCanvas _Song { get; set; }

            public MeasureCreatorForm(Canvas f_can, Canvas f_cbtn_create, Canvas f_cbtn_delete, ComboBox f_combo) {
                MainCan = f_can;
                ComboList = f_combo;

                CreateButton = new CustomButton(f_cbtn_create);
                CreateButton.SetButtonClickEvent(ClickEvent_create);
                CreateButton.SetImg(ImageControl.IconAnnual);

                DeleteButton = new CustomButton(f_cbtn_delete);
                DeleteButton.SetButtonClickEvent(ClickEvent_delete);
                DeleteButton.SetImg(ImageControl.IconDelete);
            }

            public void ClickEvent_create(object sender, MouseButtonEventArgs e) {
                var btn = sender as CustomButton;

                MeasureSize f_selectedSize = (MeasureSize)((ComboList.SelectedIndex * 2) + 4);
                _Song.AddNewMeasure(f_selectedSize);
            }
            public void ClickEvent_delete(object sender, MouseButtonEventArgs e) {
                _Song.DeleteMeasure();
            }
        }

        public class PagesForm
        {
            public CustomButton NextPageBtn { get; set; }
            public CustomButton PrevPageBtn { get; set; }
            public SongViewModalCreator _Creator { get; set; }

            public PagesForm(SongViewModalCreator f_creator,Canvas f_Next, Canvas f_prev)
            {
                _Creator = f_creator;
                NextPageBtn = new CustomButton(f_Next);
                NextPageBtn.SetButtonClickEvent(NextPage);
                NextPageBtn.SetImg(ImageControl.Icon1);

                PrevPageBtn = new CustomButton(f_prev);
                PrevPageBtn.SetButtonClickEvent(PrevPage);
                PrevPageBtn.SetImg(ImageControl.Icon2);
            }

            public void NextPage(object sender, MouseEventArgs e)
            {
                _Creator.NextPage();
            }
            public void PrevPage(object sender, MouseEventArgs e)
            {
                _Creator.PrevPage();
            }
        }

        public class FormSongSelect
        {
            public MainWindow _Window { get; set; }

            public FormSongSelect(MainWindow f_wind) {
                _Window = f_wind;
            }
        }
    }
}
