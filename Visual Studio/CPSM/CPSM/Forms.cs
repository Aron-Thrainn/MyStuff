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

namespace CPSM
{
    namespace Forms
    {
        public class NoteColourForm
        {
            public MouseNoteColour _MouseCtrl { get; set; }
            public Canvas MainCan { get; set; }
            public List<CustomRadioButton> RadioButtons { get; set; }
            public CustomRadioButtonGroup RadioGroup { get; set; }

            public NoteColourForm(Canvas f_can, MouseNoteColour f_mousectrl) {
                MainCan = f_can;
                _MouseCtrl = f_mousectrl;
                RadioButtons = new List<CustomRadioButton>();
            }

            public void Init(List<Canvas> f_buttoncans) {
                RadioGroup = new CustomRadioButtonGroup();
                int count = 0;
                foreach (var can in f_buttoncans) {
                    var tempbtn = new CustomRadioButton(can, RadioGroup);
                    tempbtn.SetButtonClickEvent(ClickEvent);
                    tempbtn.SetImg(ImageControl.noteIcons((OctaveColour)count));
                    tempbtn.Tag = count;
                    count++;

                    RadioButtons.Add(tempbtn);
                }
            }
            public void ClickEvent(object sender, MouseButtonEventArgs e) {
                var btn = sender as CustomRadioButton;
                _MouseCtrl.ActiveColour = (OctaveColour)btn.Tag;
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



        public class FormSongSelect
        {
            public MainWindow _Window { get; set; }

            public FormSongSelect(MainWindow f_wind) {
                _Window = f_wind;
            }
        }
    }
}
