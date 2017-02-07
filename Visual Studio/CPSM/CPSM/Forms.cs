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
using System.Windows.Threading;

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
            public ComboBox NoteSizeBox { get; set; }

            public NoteColourForm(Canvas f_can, MouseNote f_mousectrl) {
                MainCan = f_can;
                _MouseCtrl = f_mousectrl;
                RadioButtons = new List<CustomRadioButton>();
            }

            public void Init(List<Canvas> f_buttoncans, ComboBox f_NoteSizeBox) {
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

                NoteSizeBox = f_NoteSizeBox;
                NoteSizeBox.DropDownClosed += new EventHandler(NoteSizeBoxEvent);
            }
            public void ClickEvent(object sender, MouseButtonEventArgs e) {
                var btn = sender as CustomRadioButton;
                _MouseCtrl.SetColour((OctaveColour)btn.Tag);
                //_MouseCtrl.UpdatePreview((OctaveColour)btn.Tag);
            }
            public void NoteSizeBoxEvent(object sender, EventArgs e) {
                _MouseCtrl.ActivePatrial = (PartialNote)NoteSizeBox.SelectedIndex;
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
                    if ((OctaveColour)btn.Tag == f_col) {
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
                //todo: fix size of icons
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
                CreateButton.SetTooltip("Create Measure");

                DeleteButton = new CustomButton(f_cbtn_delete);
                DeleteButton.SetButtonClickEvent(ClickEvent_delete);
                DeleteButton.SetImg(ImageControl.IconDelete);
                DeleteButton.SetTooltip("Delete Measure");
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

            public PagesForm(SongViewModalCreator f_creator, Canvas f_Next, Canvas f_prev) {
                _Creator = f_creator;
                NextPageBtn = new CustomButton(f_Next);
                NextPageBtn.SetButtonClickEvent(NextPage);
                NextPageBtn.SetImg(ImageControl.Icon1);
                NextPageBtn.SetTooltip("Next Page");

                PrevPageBtn = new CustomButton(f_prev);
                PrevPageBtn.SetButtonClickEvent(PrevPage);
                PrevPageBtn.SetImg(ImageControl.Icon2);
                PrevPageBtn.SetTooltip("Previous Page");
            }

            public void NextPage(object sender, MouseEventArgs e) {
                _Creator.NextPage();
            }
            public void PrevPage(object sender, MouseEventArgs e) {
                _Creator.PrevPage();
            }
        }


        public class SaveAndLoadForm
        {
            public CustomButton SaveButton { get; set; }
            public CustomButton LoadButton { get; set; }
            public LoadSongSelect SongSelectForm { get; set; }
            public SongCanvas _SongCan { get; set; }
            public SaveConfirmed _SaveConfirm { get; set; }


            public SaveAndLoadForm(Canvas f_SaveBtn, Canvas f_LoadBtn, LoadSongSelect f_SongSelect, SongCanvas f_SongCan) {
                SaveButton = new CustomButton(f_SaveBtn);
                LoadButton = new CustomButton(f_LoadBtn);
                _SongCan = f_SongCan;
                SongSelectForm = f_SongSelect;


                SaveButton.SetImg(ImageControl.Icon1);
                SaveButton.SetTooltip("Save Song");
                SaveButton.SetButtonClickEvent(SaveButtonClickEvent);

                LoadButton.SetImg(ImageControl.Icon2);
                LoadButton.SetTooltip("Load Song");
                LoadButton.SetButtonClickEvent(LoadButtonClickEvent);

            }

            public void LoadButtonClickEvent(object sender, MouseButtonEventArgs e) {
                if (SongSelectForm.IsHidden()) {
                    SongSelectForm.ShowForm();
                }
                else {
                    SongSelectForm.HideForm();
                }
            }
            public void SaveButtonClickEvent(object sender, MouseButtonEventArgs e) {
                _SongCan.SaveSong();
            }
            public void LoadSong(SongDataSmall f_data) {
                _SongCan.LoadSong(f_data.Title, f_data.Source);
            }
            public void SaveConfirm(bool f_DidSave) {
                //todo: Implement
                if (!f_DidSave) {
                    throw new Exception();
                }
            }

            public class SaveConfirmed
            {
                public DispatcherTimer _Timer { get; set; }
                public Canvas _VisualConfirmation { get; set; }
                public bool DidSave { get; set; }

                //todo: Implement
            }

        }
        
        public class LoadSongSelect
        {
            public SaveAndLoadForm _Parent { get; set; }
            public Canvas _Form { get; set; }
            public StackPanel VertiStack { get; set; }

            public LoadSongSelect(Canvas f_Form, Canvas f_cbtn) {
                _Form = f_Form;
                VertiStack = new StackPanel();
                _Form.Children.Add(VertiStack);
                
                HideForm();
            }

            public void LinkParent(SaveAndLoadForm f_parent) {
                _Parent = f_parent;
            }
            public bool IsHidden() {
                return _Form.IsEnabled ? false : true;
            }
            public void ShowForm() {
                _Form.Visibility = Visibility.Visible;
                _Form.IsEnabled = true;
                LoadSongList();
            }
            public void HideForm() {
                _Form.Visibility = Visibility.Hidden;
                _Form.IsEnabled = false;

                ClearStacks();
            }
            public void LoadSongList() {
                var f_songloader = new SongLoader(@"C:\\Users\\Notandi\\Desktop\\temptxt.txt");
                var f_songlist = f_songloader.LoadSongsSmall();

                foreach (var data in f_songlist) {
                    CreateListItem(data);
                }
            }
            public void CreateListItem(SongDataSmall f_data) {
                var f_item = new LoadSongItem(this, f_data);
                VertiStack.Children.Add(f_item);
                //todo: add stackpanels to vertistack
            }
            public void ClearStacks() {
                while (VertiStack.Children.Count > 0) {
                    VertiStack.Children.RemoveAt(0);
                }
            }
            public void LoadSong(SongDataSmall f_data) {
                _Parent.LoadSong(f_data);
                HideForm();
            }
        }

        public class LoadSongItem : Canvas
        {
            public LoadSongSelect _Parent { get; set; }
            public SongDataSmall _Data { get; set; }
            public CustomButton _btn { get; set; }

            public LoadSongItem(LoadSongSelect f_parent, SongDataSmall f_data) {
                _Parent = f_parent;
                _Data = f_data;
                Margin = new Thickness(0, -2, 0, 0);
                Height = 60;
                Width = 180;
                Background = Brushes.AliceBlue;
                Children.Add(new Label() //Title
                {
                    Content = _Data.Title,
                    Margin = new Thickness(0, 2, 0, 0)
                });
                Children.Add(new Label() // Source
                {
                    Content = _Data.Source,
                    Margin = new Thickness(0, 18, 0, 0)
                });
                Children.Add(new Label() // Measures & date
                {
                    Content = _Data.MeasureCount.ToString() + " Measures"  + "  " + _Data.Date.ToShortDateString(),
                    Margin = new Thickness(0, 34, 0, 0)
                });
                Children.Add(new Border() {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(2),
                    Height = Height,
                    Width = Width
                });
                var f_BtnCan = new Canvas() {
                    Margin = new Thickness(140, 10, 0, 0),
                    Background = Brushes.Black
                };
                Children.Add(f_BtnCan);
                _btn = new CustomButton(f_BtnCan);
                _btn.MouseUp += ItemClickEvent;
                _btn.SetImg(ImageControl.IconAnnual);
                _btn.SetSize(0.75);
                _btn.SetButtonClickEvent(ItemClickEvent);
            }

            public void ItemClickEvent(object sender, MouseButtonEventArgs e) {
                _Parent.LoadSong(_Data);
            }
        }  

        public class VersionForm
        {
            public Label _Label { get; set; }
            public Version _Version { get; set; }

            public VersionForm(Label f_Label, Version f_Version) {
                _Label = f_Label;
                _Version = f_Version;

                _Label.Content = _Version.CurrentVersion;
            }
        }
    }
}
