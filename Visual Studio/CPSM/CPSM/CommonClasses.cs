using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CPSM;

namespace CommonClasses
{
    using CPSM;

    
    namespace CustomButtons
    {
        using Images;
        public enum ButtonState
        {
            Idle,
            Toggled,
            Pressed,
            PressedFromIdle
        }

        public class ParentButton : Canvas
        {
            #region Standard variables
            //images and offsets
            protected Image Icon { get; set; }
            protected Image Frame { get; set; }
            protected Label TagLabel { get; set; }
            protected Canvas Highlight { get; set; }

            //non-changing elements
            protected Canvas Can { get; set; }

            //variables
            protected ButtonState State { get; set; }
            protected bool MouseLock { get; set; }
            protected bool MouseLeft { get; set; }
            protected bool MouseRight { get; set; }
            static protected double SizeIcon = 30;
            static protected double SizeFrame = 40;
            static protected double SizeOffset1 = 5;
            static protected double SizeOffset2 = 6;
            static protected double SizeOffset3 = 7;
            static protected int SizeText = 28;
            static protected double SizeTextOffsety = 8;
            static protected double SizeTextOffsetx = 1;
            protected double SizeScale { get; set; }

            //functions
            protected Action<object, MouseButtonEventArgs> Function { get; set; }
            protected Action<object, MouseButtonEventArgs> FunctionRight { get; set; }

            #endregion

            public ParentButton(Canvas f_can)
            {
                Init_shared(f_can);
            }
            protected void Init_shared(Canvas f_can)
            {
                MouseLock = false;
                MouseLeft = false;
                MouseRight = false;
                SizeScale = 1;

                Icon = new Image();
                Frame = new Image();
                TagLabel = new Label();
                Highlight = new Canvas();
                Icon.Stretch = Stretch.Fill;
                Frame.Stretch = Stretch.Fill;
                Icon.Tag = this;
                Frame.Tag = this;
                Highlight.Tag = this;
                TagLabel.FontWeight = FontWeights.DemiBold;

                Highlight.Height = SizeIcon;
                Highlight.Width = SizeIcon;
                Highlight.Opacity = 0;
                Highlight.Background = Brushes.AliceBlue;
                Can = f_can;
                Can.Children.Add(Frame);
                Can.Children.Add(Icon);
                Can.Children.Add(TagLabel);
                Can.Children.Add(Highlight);
                Can.Background = null;
                
                Frame.Margin = new Thickness(0, 0, 0, 0);
                setButtonState(ButtonState.Idle);

                Frame.MouseEnter += ButtonEnter;
                Frame.MouseLeave += ButtonLeave;
                Highlight.MouseEnter += ButtonEnter;
                Highlight.MouseLeave += ButtonLeave;
            }
            protected void ButtonEvent(object sender, MouseButtonEventArgs e)
            {
                try
                {
                    Function(sender, e);
                }
                catch (Exception ex)
                {

                }
            }
            protected void ButtonEventRight(object sender, MouseButtonEventArgs e)
            {
                try
                {
                    FunctionRight(sender, e);
                }
                catch (Exception ex)
                {

                }
            }
            protected void ButtonEnter(object sender, MouseEventArgs e)
            {
                Highlight.Opacity = 0.65;
            }
            protected void ButtonLeave(object sender, MouseEventArgs e)
            {
                Highlight.Opacity = 0;
            }
            #region Set functions
            public void SetImg(BitmapImage f_img)
            {
                Icon.Source = f_img;
            }
            public void setButtonState(ButtonState f_state)
            {
                Frame.Source = ImageControl.IconFrame(f_state);
                State = f_state;
                switch (f_state)
                {
                    case ButtonState.Idle:
                        {
                            var f_margin = SizeOffset1 * SizeScale;
                            Icon.Margin = new Thickness(f_margin, f_margin, 0, 0);
                            TagLabel.Margin = new Thickness(f_margin - SizeTextOffsetx, f_margin - SizeTextOffsety, 0, 0);
                            Highlight.Margin = new Thickness(f_margin, f_margin, 0, 0);
                            break;
                        }
                    case ButtonState.Toggled:
                        {
                            var f_margin = SizeOffset2 * SizeScale;
                            Icon.Margin = new Thickness(f_margin, f_margin, 0, 0);
                            TagLabel.Margin = new Thickness(f_margin - SizeTextOffsetx, f_margin - SizeTextOffsety, 0, 0);
                            Highlight.Margin = new Thickness(f_margin, f_margin, 0, 0);
                            break;
                        }
                    case ButtonState.Pressed:
                        {
                            var f_margin = SizeOffset3 * SizeScale;
                            Icon.Margin = new Thickness(f_margin, f_margin, 0, 0);
                            TagLabel.Margin = new Thickness(f_margin - SizeTextOffsetx, f_margin - SizeTextOffsety, 0, 0);
                            Highlight.Margin = new Thickness(f_margin, f_margin, 0, 0);
                            break;
                        }
                    case ButtonState.PressedFromIdle:
                        {
                            var f_margin = SizeOffset3 * SizeScale;
                            Icon.Margin = new Thickness(f_margin, f_margin, 0, 0);
                            TagLabel.Margin = new Thickness(f_margin - SizeTextOffsetx, f_margin - SizeTextOffsety, 0, 0);
                            Highlight.Margin = new Thickness(f_margin, f_margin, 0, 0);
                            break;
                        }
                }
            }
            public void SetTooltip(string f_tooltip)
            {
                Icon.ToolTip = f_tooltip;
                Frame.ToolTip = f_tooltip;
            }
            public void SetSize(double f_scale)
            {
                Icon.Height = (SizeIcon * f_scale);
                Frame.Height = (SizeFrame * f_scale);
                Highlight.Height = (SizeIcon * f_scale);
                Highlight.Width = (SizeIcon * f_scale);
                TagLabel.FontSize = (SizeText * f_scale);
                SizeScale = f_scale;
                setButtonState(State);
            }
            public void SetButtonClickEvent(Action<object, MouseButtonEventArgs> f_func)
            {
                Function = f_func;
            }
            public void SetButtonRightClickEvent(Action<object, MouseButtonEventArgs> f_func)
            {
                FunctionRight = f_func;
            }
            public void SetTagLabel(string f_taglabel)
            {
                TagLabel.Content = f_taglabel;
            }
            #endregion
        }

        public class CustomButton : ParentButton
        {

            public CustomButton(Canvas f_can) : base(f_can)
            {
                SetButtonEvents();
            }
            #region Button events
            private void SetButtonEvents()
            {
                Highlight.MouseLeftButtonDown += new MouseButtonEventHandler(ButtonDown);
                Highlight.MouseLeftButtonUp += new MouseButtonEventHandler(ButtonUp);

                Frame.MouseLeftButtonDown += new MouseButtonEventHandler(ButtonDown);
                Frame.MouseLeftButtonUp += new MouseButtonEventHandler(ButtonUp);
                Frame.MouseLeave += new MouseEventHandler(MouseLeaveButton);
            }
            private void ButtonDown(object sender, MouseButtonEventArgs e)
            {
                MouseLock = true;
                switch (State)
                {
                    case ButtonState.Idle:
                        {
                            setButtonState(ButtonState.Pressed);
                            break;
                        }
                }
            }
            private void ButtonUp(object sender, MouseButtonEventArgs e)
            {
                if (MouseLock)
                {
                    switch (State)
                    {
                        case ButtonState.Pressed:
                            {
                                setButtonState(ButtonState.Idle);
                                ButtonEvent(this, e);
                                break;
                            }
                    }
                }
                MouseLock = false;
            }
            private void MouseLeaveButton(object sender, MouseEventArgs e)
            {
                if (State == ButtonState.Pressed)
                {
                    setButtonState(ButtonState.Idle);
                }
                MouseLock = false;
            }
            #endregion
        }

        public class CustomCheckbox : ParentButton
        {
            private bool Checked { get; set; }

            public CustomCheckbox(Canvas f_can) : base(f_can)
            {
                SetButtonEvents();
                Checked = false;
            }
            public bool isChecked()
            {
                return Checked;
            }
            public void setChecked(bool f_checked)
            {
                if (f_checked)
                {
                    Checked = true;
                    setButtonState(ButtonState.Toggled);
                }
                else
                {
                    Checked = false;
                    setButtonState(ButtonState.Idle);
                }
            }
            #region Button events
            private void SetButtonEvents()
            {
                Highlight.MouseLeftButtonDown += new MouseButtonEventHandler(ButtonDown);
                Highlight.MouseLeftButtonUp += new MouseButtonEventHandler(ButtonUp);
                Highlight.MouseRightButtonDown += new MouseButtonEventHandler(ButtonDownRight);
                Highlight.MouseRightButtonUp += new MouseButtonEventHandler(ButtonUpRight);

                Frame.MouseLeftButtonDown += new MouseButtonEventHandler(ButtonDown);
                Frame.MouseLeftButtonUp += new MouseButtonEventHandler(ButtonUp);
                Frame.MouseLeave += new MouseEventHandler(MouseLeaveButton);
            }
            private void ButtonDown(object sender, MouseButtonEventArgs e)
            {
                MouseLock = true;
                switch (State)
                {
                    case ButtonState.Idle:
                        {
                            setButtonState(ButtonState.PressedFromIdle);
                            break;
                        }
                    case ButtonState.Toggled:
                        {
                            setButtonState(ButtonState.Pressed);
                            break;
                        }
                }
            }
            private void ButtonUp(object sender, MouseButtonEventArgs e)
            {
                if (MouseLock)
                {
                    switch (State)
                    {
                        case ButtonState.PressedFromIdle:
                            {
                                setButtonState(ButtonState.Toggled);
                                ButtonEvent(this, e);
                                Checked = true;
                                break;
                            }
                        case ButtonState.Pressed:
                            {
                                setButtonState(ButtonState.Idle);
                                ButtonEvent(this, e);
                                Checked = false;
                                break;
                            }
                    }
                }
                MouseLock = false;
            }
            private void ButtonDownRight(object sender, MouseButtonEventArgs e)
            {
                MouseLock = true;
                switch (State)
                {
                    case ButtonState.Idle:
                        {
                            setButtonState(ButtonState.PressedFromIdle);
                            break;
                        }
                    case ButtonState.Toggled:
                        {
                            setButtonState(ButtonState.Pressed);
                            break;
                        }
                }
            }
            private void ButtonUpRight(object sender, MouseButtonEventArgs e)
            {
                if (MouseLock)
                {
                    switch (State)
                    {
                        case ButtonState.PressedFromIdle:
                            {
                                setButtonState(ButtonState.Idle);
                                FunctionRight(this, e);
                                break;
                            }
                        case ButtonState.Pressed:
                            {
                                setButtonState(ButtonState.Toggled);
                                FunctionRight(this, e);
                                break;
                            }
                    }
                }
                MouseLock = false;
            }
            private void MouseLeaveButton(object sender, MouseEventArgs e)
            {
                if (State == ButtonState.Pressed)
                {
                    setButtonState(ButtonState.Toggled);
                }
                else if (State == ButtonState.PressedFromIdle)
                {
                    setButtonState(ButtonState.Idle);
                }
                MouseLock = false;
            }
            #endregion
        }
        #region Custom radiobutton group
        public class CustomRadioButtonGroup
        {
            public List<CustomRadioButton> RadioButtons { get; set; }
            public string GroupName { get; set; }

            public CustomRadioButtonGroup()
            {
                RadioButtons = new List<CustomRadioButton>();
            }
            public void SetActiveButton(int f_id)
            {
                foreach (var btn in RadioButtons)
                {
                    if (btn.Id == f_id)
                    {
                        btn.SetChecked(true);
                    }
                    else
                    {
                        btn.SetChecked(false);
                    }
                }
            }
            public void AddButton(CustomRadioButton f_btn)
            {
                f_btn.Group = this;
                f_btn.Id = GenerateNewId();
                RadioButtons.Add(f_btn);
            }
            private int GenerateNewId()
            {
                int f_id = 0;
                bool f_taken;
                while (true)
                {
                    f_taken = false;
                    foreach (var f_btn in RadioButtons)
                    {
                        if (f_id == f_btn.Id)
                        {
                            f_taken = true;
                            break;
                        }
                    }
                    if (!f_taken)
                    {
                        return f_id;
                    }
                    f_id++;
                }
            }
        }
        #endregion
        public class CustomRadioButton : ParentButton
        {
            public CustomRadioButtonGroup Group { get; set; }
            public int Id { get; set; }
            private bool Checked { get; set; }

            public CustomRadioButton(Canvas f_can, CustomRadioButtonGroup f_group) : base(f_can)
            {
                SetButtonEvents();
                f_group.AddButton(this);
            }
            public bool GetChecked()
            {
                return Checked;
            }
            public void SetChecked(bool f_checked)
            {
                if (f_checked)
                {
                    Checked = true;
                    setButtonState(ButtonState.Toggled);
                }
                else
                {
                    Checked = false;
                    setButtonState(ButtonState.Idle);
                }
            }
            public void Check()
            {
                Group.SetActiveButton(Id);
            }
            #region Button events
            private void SetButtonEvents()
            {
                Highlight.MouseLeftButtonDown += new MouseButtonEventHandler(ButtonDown);
                Highlight.MouseLeftButtonUp += new MouseButtonEventHandler(ButtonUp);

                Frame.MouseLeftButtonDown += new MouseButtonEventHandler(ButtonDown);
                Frame.MouseLeftButtonUp += new MouseButtonEventHandler(ButtonUp);
                Frame.MouseLeave += new MouseEventHandler(MouseLeaveButton);
            }
            private void ButtonDown(object sender, MouseButtonEventArgs e)
            {
                MouseLock = true;
                switch (State)
                {
                    case ButtonState.Idle:
                        {
                            setButtonState(ButtonState.PressedFromIdle);
                            break;
                        }
                    case ButtonState.Toggled:
                        {
                            setButtonState(ButtonState.Pressed);
                            break;
                        }
                }
            }
            private void ButtonUp(object sender, MouseButtonEventArgs e)
            {
                if (MouseLock)
                {
                    switch (State)
                    {
                        case ButtonState.PressedFromIdle:
                            {
                                Check();
                                ButtonEvent(this, e);
                                break;
                            }
                        case ButtonState.Pressed:
                            {
                                setButtonState(ButtonState.Toggled);
                                ButtonEvent(this, e);
                                break;
                            }
                    }
                }
                MouseLock = false;
            }
            private void ButtonDownRight(object sender, MouseButtonEventArgs e)
            {
                MouseLock = true;
                switch (State)
                {
                    case ButtonState.Idle:
                        {
                            setButtonState(ButtonState.PressedFromIdle);
                            break;
                        }
                    case ButtonState.Toggled:
                        {
                            setButtonState(ButtonState.Pressed);
                            break;
                        }
                }
            }
            private void ButtonUpRight(object sender, MouseButtonEventArgs e)
            {
                if (MouseLock)
                {
                    switch (State)
                    {
                        case ButtonState.PressedFromIdle:
                            {
                                setButtonState(ButtonState.Idle);
                                FunctionRight(this, e);
                                break;
                            }
                        case ButtonState.Pressed:
                            {
                                setButtonState(ButtonState.Toggled);
                                FunctionRight(this, e);
                                break;
                            }
                    }
                }
                MouseLock = false;
            }
            private void MouseLeaveButton(object sender, MouseEventArgs e)
            {
                if (State == ButtonState.PressedFromIdle)
                {
                    setButtonState(ButtonState.Idle);
                }
                else if (State == ButtonState.Pressed)
                {
                    setButtonState(ButtonState.Toggled);
                }
                MouseLock = false;
            }
            #endregion
        }
    }
    

   
    namespace Images
    {
        using CustomButtons;
        public static class ImageControl
        {
            #region images
            private static readonly BitmapImage DefaultIcon = new BitmapImage(new Uri("pack://application:,,,/Resources/icons_0.png"));
            private static readonly BitmapImage IconFrameIdle = new BitmapImage(new Uri("pack://application:,,,/Resources/frame_0.png"));
            private static readonly BitmapImage IconFrameToggle = new BitmapImage(new Uri("pack://application:,,,/Resources/frame_1.png"));
            private static readonly BitmapImage IconFramePressed = new BitmapImage(new Uri("pack://application:,,,/Resources/frame_2.png"));
            private static readonly BitmapImage IconBlue = new BitmapImage(new Uri("pack://application:,,,/Resources/icons_0.png"));
            private static readonly BitmapImage IconGreen = new BitmapImage(new Uri("pack://application:,,,/Resources/icons_1.png"));
            private static readonly BitmapImage IconRed = new BitmapImage(new Uri("pack://application:,,,/Resources/icons_2.png"));
            private static readonly BitmapImage IconOrange = new BitmapImage(new Uri("pack://application:,,,/Resources/icons_3.png"));
            private static readonly BitmapImage IconPink = new BitmapImage(new Uri("pack://application:,,,/Resources/icons_4.png"));
            private static readonly BitmapImage IconYellow = new BitmapImage(new Uri("pack://application:,,,/Resources/icons_5.png"));
            private static readonly BitmapImage IconTeal = new BitmapImage(new Uri("pack://application:,,,/Resources/icons_6.png"));
            private static readonly BitmapImage IconPurple = new BitmapImage(new Uri("pack://application:,,,/Resources/icons_7.png"));
            public static readonly BitmapImage Icon1 = new BitmapImage(new Uri("pack://application:,,,/Resources/icons_8.png"));
            public static readonly BitmapImage Icon2 = new BitmapImage(new Uri("pack://application:,,,/Resources/icons_9.png"));
            public static readonly BitmapImage Icon3 = new BitmapImage(new Uri("pack://application:,,,/Resources/icons_10.png"));
            public static readonly BitmapImage IconDelete = new BitmapImage(new Uri("pack://application:,,,/Resources/icons_11.png"));
            public static readonly BitmapImage IconCancel = new BitmapImage(new Uri("pack://application:,,,/Resources/icons_12.png"));
            public static readonly BitmapImage IconAnnual = new BitmapImage(new Uri("pack://application:,,,/Resources/icons_13.png"));
            public static readonly BitmapImage IconTest = new BitmapImage(new Uri("pack://application:,,,/Resources/img1.png"));
            #endregion

            public static BitmapImage IconFrame(ButtonState f_state)
            {
                switch (f_state)
                {
                    case ButtonState.Idle: return IconFrameIdle;
                    case ButtonState.Toggled: return IconFrameToggle;
                    case ButtonState.Pressed: return IconFramePressed;
                    case ButtonState.PressedFromIdle: return IconFramePressed;

                    default: return DefaultIcon;
                }
            }

            public static BitmapImage NoteImg(OctaveColour f_oct, NoteType f_type) {


                return IconTest;
            }
        }
    }
    

    #region Inispace
    namespace IniSpace
    {
        using System.IO;
        using System.Reflection;
        using System.Runtime.InteropServices;
        using System.Text;

        public class IniFile   // revision 10
        {
            string Path;
            string EXE = Assembly.GetExecutingAssembly().GetName().Name;

            [DllImport("kernel32")]
            static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

            [DllImport("kernel32")]
            static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

            public IniFile(string IniPath = null)
            {
                Path = new FileInfo(IniPath ?? EXE + ".ini").FullName.ToString();
            }

            public string Read(string Key, string Section = null)
            {
                var RetVal = new StringBuilder(255);
                GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
                return RetVal.ToString();
            }

            public void Write(string Key, string Value, string Section = null)
            {
                WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
            }

            public void DeleteKey(string Key, string Section = null)
            {
                Write(Key, null, Section ?? EXE);
            }

            public void DeleteSection(string Section = null)
            {
                Write(null, null, Section ?? EXE);
            }

            public bool KeyExists(string Key, string Section = null)
            {
                return Read(Key, Section).Length > 0;
            }
        }
    }
    #endregion



   
    
}
