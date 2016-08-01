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

namespace CountdownCalendar
{
    using Timers;
    using CommonClasses.IniSpace;
    using CommonClasses.Images;
    using CommonClasses.CustomButtons;
    using Timers.Catagories;
    using System.Windows.Threading;
    using System.Windows.Input;
    using System.Collections.Generic;

    #region Todo
    /*
    Get a .exe icon
    Indicator for when hidden timer finish

     Bugs
    set focus when opening a form
    multiple cats selected by default when opening newtimer form
    Hotkeys override all previous functionality
    not sorting properly
    current time shows MM/DD/YYYY
    reposition "time not set" button
    
    */
    #endregion

    #region Done
    /*
    Event started timers
    Event finished timers
    custom button
    custom checkbox
    custom radiobutton
    Timers with non-set dates
    Description
    Add all the catagories
    Counter for filter buttons
    filter display
    save/load filters
    Hotkeys
    Make timers clickable with no button
    */
    #endregion


    public partial class MainWindow : Window
    {
        #region Properties
        private TimerController _timerContr { get; set; }
        private CatagoryController _catCtrl { get; set; }
        private Hotkeycontrol _keyCtrl { get; set; }

        public StackPanel TimerBarHolder { get; set; }
        public NewTimerForm FormNew { get; set; }
        public EditTimerForm FormEdit { get; set; }
        public FilterForm FormFilter { get; set; }
        public EditCatForm FormCatEdit { get; set; }
        #endregion

        public MainWindow() {
            InitializeComponent();

            _catCtrl = new CatagoryController();
            _timerContr = new TimerController(this, _catCtrl);

            Init_Menu();
            Init_Timer();
            Init_NewTimerForm();
            Init_EditTimerForm();
            Init_EditCatForm();
            Init_FilterCatForm();
            Init_Window();
            Init_Hotkeys();
            UpdateCurrentTime();

            _timerContr.Inireload();

            TimerBarHolder = stc_TimeBarHolder;
            FormFilter.UpdateTimerDisplay();
            UpdateTimerBarHolder();
            cat_tooltip_reset();
        }
        #region Initialization
        private void Init_Menu() {
            //newtimer button
            var f_cmb = new CustomButton(cmb_NewTimer);
            f_cmb.SetButtonClickEvent(btn_newTimer_Click);
            f_cmb.SetTooltip("New Timer");
            f_cmb.SetImg(ImageControl.Icon3);

            //Formback
            cnv_FormBack.MouseLeftButtonUp += FormBack_click;

        }
        private void Init_Timer() {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        private void Init_NewTimerForm() {
            var f_cats = new List<CustomRadioButton>();
            var f_rdb_group = new CustomRadioButtonGroup();
            var f_rdb_1 = new CustomRadioButton(cmb_NewTimerForm_rdb1, f_rdb_group);
            var f_rdb_2 = new CustomRadioButton(cmb_NewTimerForm_rdb2, f_rdb_group);
            var f_rdb_3 = new CustomRadioButton(cmb_NewTimerForm_rdb3, f_rdb_group);
            var f_rdb_4 = new CustomRadioButton(cmb_NewTimerForm_rdb4, f_rdb_group);
            var f_rdb_5 = new CustomRadioButton(cmb_NewTimerForm_rdb5, f_rdb_group);
            var f_rdb_6 = new CustomRadioButton(cmb_NewTimerForm_rdb6, f_rdb_group);
            var f_rdb_7 = new CustomRadioButton(cmb_NewTimerForm_rdb7, f_rdb_group);
            var f_rdb_8 = new CustomRadioButton(cmb_NewTimerForm_rdb8, f_rdb_group);
            f_rdb_1.Tag = 1;
            f_rdb_2.Tag = 2;
            f_rdb_3.Tag = 3;
            f_rdb_4.Tag = 4;
            f_rdb_5.Tag = 5;
            f_rdb_6.Tag = 6;
            f_rdb_7.Tag = 7;
            f_rdb_8.Tag = 8;
            f_cats.Add(f_rdb_1);
            f_cats.Add(f_rdb_2);
            f_cats.Add(f_rdb_3);
            f_cats.Add(f_rdb_4);
            f_cats.Add(f_rdb_5);
            f_cats.Add(f_rdb_6);
            f_cats.Add(f_rdb_7);
            f_cats.Add(f_rdb_8);
            foreach (var cmb in f_cats) {
                cmb.SetSize(0.5);
                cmb.SetImg(ImageControl.IconCat((enumCat)cmb.Tag));
            }

            var f_annual = new CustomCheckbox(cmb_NewTimerForm_chb_1);
            f_annual.SetSize(0.5);
            f_annual.SetImg(ImageControl.IconAnnual);
            f_annual.SetTooltip("Annual");

            var f_dateset = new CustomCheckbox(cmb_NewTimerForm_chb_2);
            f_dateset.SetSize(0.5);
            f_dateset.SetImg(ImageControl.IconAnnual);
            f_dateset.setChecked(true);
            f_dateset.SetTooltip("Specific date");

            var f_btn1 = new CustomButton(cmb_NewTimerForm_btn_1);
            f_btn1.SetTooltip("Create");
            f_btn1.SetImg(ImageControl.Icon3);
            var f_btn2 = new CustomButton(cmb_NewTimerForm_btn_2);
            f_btn2.SetTooltip("Cancel");
            f_btn2.SetSize(0.5);
            f_btn2.SetImg(ImageControl.IconCancel);


            FormNew = new NewTimerForm(_timerContr, this);
            FormNew.SetButtons(f_cats, f_annual, f_dateset, f_btn1, f_btn2);
            FormNew.SetTextboxes(txt_newTimerName, txt_time, txt_duration);
            FormNew.SetMisc(dtp_newTimerDate, cnv_newTimerForm, cnv_FormBack, txt_newTimerDesc);
            FormNew.SetDefaultFormVariables();
            FormNew.Deactivate();
        }
        private void Init_EditTimerForm() {
            var f_cats = new List<CustomRadioButton>();
            var f_rdb_group = new CustomRadioButtonGroup();
            var f_rdb_1 = new CustomRadioButton(cmb_EditTimerForm_rdb1, f_rdb_group);
            var f_rdb_2 = new CustomRadioButton(cmb_EditTimerForm_rdb2, f_rdb_group);
            var f_rdb_3 = new CustomRadioButton(cmb_EditTimerForm_rdb3, f_rdb_group);
            var f_rdb_4 = new CustomRadioButton(cmb_EditTimerForm_rdb4, f_rdb_group);
            var f_rdb_5 = new CustomRadioButton(cmb_EditTimerForm_rdb5, f_rdb_group);
            var f_rdb_6 = new CustomRadioButton(cmb_EditTimerForm_rdb6, f_rdb_group);
            var f_rdb_7 = new CustomRadioButton(cmb_EditTimerForm_rdb7, f_rdb_group);
            var f_rdb_8 = new CustomRadioButton(cmb_EditTimerForm_rdb8, f_rdb_group);
            f_rdb_1.Tag = 1;
            f_rdb_2.Tag = 2;
            f_rdb_3.Tag = 3;
            f_rdb_4.Tag = 4;
            f_rdb_5.Tag = 5;
            f_rdb_6.Tag = 6;
            f_rdb_7.Tag = 7;
            f_rdb_8.Tag = 8;
            f_cats.Add(f_rdb_1);
            f_cats.Add(f_rdb_2);
            f_cats.Add(f_rdb_3);
            f_cats.Add(f_rdb_4);
            f_cats.Add(f_rdb_5);
            f_cats.Add(f_rdb_6);
            f_cats.Add(f_rdb_7);
            f_cats.Add(f_rdb_8);
            foreach (var cmb in f_cats) {
                cmb.SetSize(0.5);
                cmb.SetImg(ImageControl.IconCat((enumCat)cmb.Tag));
            }

            var f_annual = new CustomCheckbox(cmb_EditTimerForm_chb_1);
            f_annual.SetSize(0.5);
            f_annual.SetImg(ImageControl.IconAnnual);

            var f_dateset = new CustomCheckbox(cmb_EditTimerForm_chb_2);
            f_dateset.SetSize(0.5);
            f_dateset.SetImg(ImageControl.IconAnnual);
            f_dateset.setChecked(true);
            f_dateset.SetTooltip("Specific date");

            var f_btn1 = new CustomButton(cmb_EditTimerForm_btn_1);
            f_btn1.SetTooltip("Edit");
            f_btn1.SetImg(ImageControl.Icon1);
            var f_btn2 = new CustomButton(cmb_EditTimerForm_btn_2);
            f_btn2.SetTooltip("Delete");
            f_btn2.SetImg(ImageControl.IconDelete);
            var f_btn3 = new CustomButton(cmb_EditTimerForm_btn_3);
            f_btn3.SetTooltip("Cancel");
            f_btn3.SetSize(0.5);
            f_btn3.SetImg(ImageControl.IconCancel);

            FormEdit = new EditTimerForm(_timerContr, this, f_btn2);
            FormEdit.SetButtons(f_cats, f_annual, f_dateset, f_btn1, f_btn3);
            FormEdit.SetTextboxes(txt_editTimerName, txt_EditTimerTime, txt_EditTimerDuration);
            FormEdit.SetMisc(dtp_EditTimerDate, cnv_editTimerForm, cnv_FormBack, txt_editTimerDesc);
            FormEdit.Deactivate();
        }
        private void Init_FilterCatForm() {
            //filter cat checkboxes
            var f_list = new List<CustomCheckbox>();
            var f_cmb1 = new CustomCheckbox(cmb_FilterCat1);
            var f_cmb2 = new CustomCheckbox(cmb_FilterCat2);
            var f_cmb3 = new CustomCheckbox(cmb_FilterCat3);
            var f_cmb4 = new CustomCheckbox(cmb_FilterCat4);
            var f_cmb5 = new CustomCheckbox(cmb_FilterCat5);
            var f_cmb6 = new CustomCheckbox(cmb_FilterCat6);
            var f_cmb7 = new CustomCheckbox(cmb_FilterCat7);
            var f_cmb8 = new CustomCheckbox(cmb_FilterCat8);
            f_cmb1.Tag = 1;
            f_cmb2.Tag = 2;
            f_cmb3.Tag = 3;
            f_cmb4.Tag = 4;
            f_cmb5.Tag = 5;
            f_cmb6.Tag = 6;
            f_cmb7.Tag = 7;
            f_cmb8.Tag = 8;
            f_list.Add(f_cmb1);
            f_list.Add(f_cmb2);
            f_list.Add(f_cmb3);
            f_list.Add(f_cmb4);
            f_list.Add(f_cmb5);
            f_list.Add(f_cmb6);
            f_list.Add(f_cmb7);
            f_list.Add(f_cmb8);
            foreach (var cmb in f_list) {
                cmb.SetSize(0.75);
                cmb.SetImg(ImageControl.IconCat((enumCat)cmb.Tag));
            }
            f_cmb1.setChecked(true);
            f_cmb2.setChecked(true);

            FormFilter = new FilterForm(f_list, cnv_filterform, FormCatEdit, _timerContr, this);
        }
        private void Init_EditCatForm() {
            //rename cat button
            var f_renamecat = new CustomButton(cmb_RenameCat);
            f_renamecat.SetTooltip("Save");
            f_renamecat.SetSize(0.5);
            f_renamecat.SetImg(ImageControl.Icon2);

            FormCatEdit = new EditCatForm(_catCtrl, this, cnv_editCatForm, txt_catName, f_renamecat);
        }
        private void Init_Window() {
            cnv_Window.Height = 500;
            cnv_FormBack.Margin = new Thickness(0, 0, 0, 0);
            cnv_FormBack.Height = cnv_Window.Height;
            cnv_FormBack.Width = cnv_Window.Width;
            cnv_editTimerForm.Margin = new Thickness(150, 50, 0, 0);
            cnv_newTimerForm.Margin = new Thickness(150, 50, 0, 0);
        }
        private void Init_Hotkeys() {
            _keyCtrl = new Hotkeycontrol(this);
            
        }
        #endregion
        #region Timer functions
        void timer_Tick(object sender, EventArgs e) {
            UpdateCurrentTime();
            _timerContr.UpdateTimerCountdowns();
        }
        private void UpdateCurrentTime() {
            var today = DateTime.Now;
            lbl_WeekDay.Content = today.DayOfWeek;
            lbl_date.Content = today.Date;
            lbl_time.Content = today.ToLongTimeString();
        }
        #endregion
        public void UpdateTimerBarHolder() {
            var f_sortedTimers = _timerContr.Timers.Where(x => x.Display);
            f_sortedTimers.OrderBy(x => x.StartTime).ThenBy(x => x.Name);


            TimerBarHolder.Children.Clear();
            foreach (var timer in f_sortedTimers) {
                TimerBarHolder.Children.Add(timer.TimerBar);
            }

        }
        public void cat_tooltip_reset() {
            FormFilter.UpdateCatTooltips(_catCtrl);
            FormNew.UpdateCatTooltips(_catCtrl);
            FormEdit.UpdateCatTooltips(_catCtrl);
        }
        private void btn_newTimer_Click(object sender, RoutedEventArgs e) {
            if (FormNew.isActive()) {
                FormNew.Deactivate();
            }
            else {
                FormEdit.Deactivate();
                FormNew.Activate();
            }
        }
        private void FormBack_click(object sender, MouseButtonEventArgs e) {
            FormEdit.Deactivate();
            FormNew.Deactivate();
        }
        
    }

    public class Hotkeycontrol
    {
        public MainWindow _window { get; set; }


        #region Command Objects
        public class SpaceCommand : ICommand
        {
            public MainWindow _window { get; set; }
            public event EventHandler CanExecuteChanged;

            public SpaceCommand(MainWindow f_window) {
                _window = f_window;
            }

            public bool CanExecute(object parameter) {
                return true;
            }

            public void Execute(object parameter) {
                if (!_window.FormNew.isActive() && !_window.FormEdit.isActive()) {
                    _window.FormNew.Activate();
                }
            }
        }

        public class EnterCommand : ICommand
        {
            public MainWindow _window { get; set; }
            public event EventHandler CanExecuteChanged;

            public EnterCommand(MainWindow f_window) {
                _window = f_window;
            }
            public bool CanExecute(object parameter) {
                return true;
            }

            public void Execute(object parameter) {
                if (_window.FormNew.isActive()) {
                    _window.FormNew.Hotkey_submit();
                }
                else if (_window.FormEdit.isActive()) {
                    _window.FormEdit.Hotkey_submit();
                }
            }
        }

        public class EscCommand : ICommand
        {
            public MainWindow _window { get; set; }
            public event EventHandler CanExecuteChanged;

            public EscCommand(MainWindow f_window) {
                _window = f_window;
            }
            public bool CanExecute(object parameter) {
                return true;
            }

            public void Execute(object parameter) {
                if (_window.FormNew.isActive()) {
                    _window.FormNew.Deactivate();
                }
                else if (_window.FormEdit.isActive()) {
                    _window.FormEdit.Deactivate();
                }
                else if (_window.FormCatEdit.isActive()) {
                    _window.FormCatEdit.Deactivate();
                }
                else {
                    _window.Close();
                }
            }
        }
        #endregion
        /*  Uses
         * space to open Newtimer form
         * escape to close form or app
         * enter to submit form
         */

        public Hotkeycontrol(MainWindow f_wind) {
            _window = f_wind;
            SpaceFunc();
            EnterFunc();
            EscFunc();
            
        }
        

        private void SpaceFunc() {
            SpaceCommand Testcommand = new SpaceCommand(_window);

            KeyGesture OpenKeyGesture = new KeyGesture(
                Key.Space,
                ModifierKeys.None);

            KeyBinding OpenCmdKeybinding = new KeyBinding(
                Testcommand,
                OpenKeyGesture);

            _window.InputBindings.Add(OpenCmdKeybinding);
        }
        private void EscFunc() {
            EscCommand Testcommand = new EscCommand(_window);

            KeyGesture OpenKeyGesture = new KeyGesture(
                Key.Escape,
                ModifierKeys.None);

            KeyBinding OpenCmdKeybinding = new KeyBinding(
                Testcommand,
                OpenKeyGesture);

            _window.InputBindings.Add(OpenCmdKeybinding);
        }
        private void EnterFunc() {
            EnterCommand Testcommand = new EnterCommand(_window);

            KeyGesture OpenKeyGesture = new KeyGesture(
                Key.Enter,
                ModifierKeys.None);

            KeyBinding OpenCmdKeybinding = new KeyBinding(
                Testcommand,
                OpenKeyGesture);

            _window.InputBindings.Add(OpenCmdKeybinding);
        }
    }

    public class TimerForm
    {
        #region Form properties
        public string Name { get; set; }
        public string Description { get; set; }
        public enumCat Cat { get; set; }
        public string Time { get; set; }
        public string Duration { get; set; }
        public bool Annual { get; set; }
        public DateTime? StartTime { get; set; }

        protected TextBox InputName { get; set; }
        protected TextBox InputDesc { get; set; }
        protected List<CustomRadioButton> InputCats { get; set; }
        protected DatePicker InputDate { get; set; }
        protected TextBox InputTime { get; set; }
        protected TextBox InputDuration { get; set; }
        protected CustomCheckbox InputAnnual { get; set; }
        protected CustomCheckbox InputDateSet { get; set; }
        protected CustomButton InputSubmit { get; set; }
        protected CustomButton InputCancel { get; set; }
        protected Canvas InputForm { get; set; }
        protected Canvas FormBack { get; set; }

        protected TimerController _timerContr { get; set; }
        protected MainWindow _window { get; set; }
        #endregion

        public TimerForm(TimerController f_tmrctrl, MainWindow f_window) {
            _timerContr = f_tmrctrl;
            _window = f_window;
        }
        #region public functions
        public TimeSpan GetStartTime() {
            return ParseTime(Time);
        }
        public TimeSpan GetDuration() {
            return ParseTime(Duration);
        }
        public bool isActive() {

            return InputForm.IsEnabled;
        }
        public void Activate() {
            InputForm.IsEnabled = true;
            InputForm.Visibility = Visibility.Visible;
            FormBack.Visibility = Visibility.Visible;
        }
        public void Deactivate() {
            InputForm.IsEnabled = false;
            InputForm.Visibility = Visibility.Hidden;
            FormBack.Visibility = Visibility.Hidden;
        }
        public enumCat GetEnum() {
            foreach (var cat in InputCats) {
                if (cat.GetChecked()) {
                    Cat = (enumCat)int.Parse(cat.Tag.ToString());
                }
            }
            return Cat;
        }
        public void SetCategory(enumCat f_cat) {
            foreach (var f_formcat in InputCats) {
                if (f_formcat.Tag as int? == (int)f_cat) {
                    f_formcat.SetChecked(true);
                }
                else {
                    f_formcat.SetChecked(false);
                }
            }
            Cat = f_cat;
        }
        public void UpdateCatTooltips(CatagoryController f_catctrl) {
            foreach (var filter in InputCats) {
                var f_cat = f_catctrl.GetCatagoryName((int)(filter.Tag));

                filter.SetTooltip(f_cat);
            }
        }
        public void UpdateFormVariables() {
            Name = InputName.Text;
            Description = InputDesc.Text;
            Cat = GetEnum();
            Annual = getAnnual();
            Time = InputTime.Text;
            Duration = InputDuration.Text;


            if (InputDateSet.isChecked()) {
                StartTime = InputDate.SelectedDate.Value;
            }
            else {
                StartTime = null;
            }

        }
        #endregion
        #region set functions
        public void SetTextboxes(TextBox f_name, TextBox f_time, TextBox f_duration) {
            InputName = f_name;
            InputTime = f_time;
            InputDuration = f_duration;
        }
        public void SetButtons(List<CustomRadioButton> f_cats, CustomCheckbox f_annual, CustomCheckbox f_dateset, CustomButton f_submit, CustomButton f_cancel) {
            InputCats = f_cats;
            InputAnnual = f_annual;
            InputDateSet = f_dateset;
            InputSubmit = f_submit;
            InputCancel = f_cancel;

            SetButtonEvents();
        }
        public void SetMisc(DatePicker f_datepicker, Canvas f_form, Canvas f_back, TextBox f_desc) {
            InputDate = f_datepicker;
            InputForm = f_form;
            InputDesc = f_desc;
            FormBack = f_back;
        }
        virtual public void SetButtonEvents() { }
        #endregion
        #region private functions
        protected TimeSpan ParseTime(string f_strtime) {
            var f_input = f_strtime.ToCharArray();
            string f_format = FormatTime(f_input);
            int f_hours = 0, f_minutes = 0;

            switch (f_format) {
                case "xxlxx": {
                        f_hours = int.Parse(f_input.ElementAt(0).ToString() + f_input.ElementAt(1).ToString());
                        f_minutes = int.Parse(f_input.ElementAt(3).ToString() + f_input.ElementAt(4).ToString());
                        break;
                    }
                case "xlxxo": {
                        f_hours = int.Parse(f_input.ElementAt(0).ToString());
                        f_minutes = int.Parse(f_input.ElementAt(2).ToString() + f_input.ElementAt(3).ToString());
                        break;
                    }
                case "xxlxo": {
                        f_hours = int.Parse(f_input.ElementAt(0).ToString() + f_input.ElementAt(1).ToString());
                        f_minutes = int.Parse(f_input.ElementAt(3).ToString());
                        break;
                    }
                case "xlxoo": {
                        f_hours = int.Parse(f_input.ElementAt(0).ToString());
                        f_minutes = int.Parse(f_input.ElementAt(2).ToString());
                        break;
                    }
                case "xxooo": {
                        f_hours = int.Parse(f_input.ElementAt(0).ToString() + f_input.ElementAt(1).ToString());
                        break;
                    }
                case "xoooo": {
                        f_hours = int.Parse(f_input.ElementAt(0).ToString());
                        break;
                    }

                default: {
                        //error message
                        throw new Exception();
                    }

            }
            var f_time = TimeSpan.FromHours(f_hours) + TimeSpan.FromMinutes(f_minutes);

            return f_time;
        }
        protected string FormatTime(char[] f_input) {
            int f_counter = 0;
            char[] f_format = new char[] { 'o', 'o', 'o', 'o', 'o' };
            foreach (var ch in f_input) {
                if (char.IsDigit(ch)) {
                    f_format[f_counter] = 'x';
                }
                else if (char.IsPunctuation(ch) || ch == ' ') {
                    f_format[f_counter] = 'l';
                }
                f_counter++;
            }
            return new string(f_format);
        }
        protected string ParseTime(DateTime f_startdate) {
            string f_time = "";
            var f_hours = f_startdate.Hour.ToString();
            var f_minutes = f_startdate.Minute.ToString();

            f_time += (f_hours.Length == 1 ? ("0" + f_hours) : f_hours);
            f_time += ":";
            f_time += (f_minutes.Length == 1 ? ("0" + f_minutes) : f_minutes);

            return f_time;
        }
        protected string ParseTime(TimeSpan f_duration) {
            string f_time = "";
            var f_hours = f_duration.Hours.ToString();
            var f_minutes = f_duration.Minutes.ToString();

            f_time += (f_hours.Length == 1 ? ("0" + f_hours) : f_hours);
            f_time += ":";
            f_time += (f_minutes.Length == 1 ? ("0" + f_minutes) : f_minutes);

            return f_time;
        }
        protected void SetAnnual() {
            InputAnnual.setChecked(Annual);
        }
        protected bool getAnnual() {
            return (InputAnnual.isChecked());
        }
        protected void Btn_dateset(object sender, RoutedEventArgs e) {
            var f_sender = sender as CustomCheckbox;
            if (f_sender.isChecked()) {
                InputDate.IsEnabled = false;
                StartTime = null;
            }
            else {
                InputDate.IsEnabled = true;
                StartTime = InputDate.SelectedDate;
            }
        }
        protected void Btn_Cancel(object sender, RoutedEventArgs e) {
            Deactivate();
        }
        #endregion
    }

    public class NewTimerForm : TimerForm
    {
        public NewTimerForm(TimerController f_tmrctrl, MainWindow f_window) : base(f_tmrctrl, f_window) {
            
        }
        public void SetDefaultFormVariables() {
            InputDate.SelectedDate = (DateTime.Today.AddDays(1));

            InputName.Text = "";
            InputDesc.Text = "";
            InputTime.Text = "08:00";
            InputDuration.Text = "01:00";
            var f_firstCat = InputCats.First();
            f_firstCat.SetChecked(true);
        }
        public override void SetButtonEvents() {
            InputDateSet.SetButtonClickEvent(Btn_dateset);
            InputCancel.SetButtonClickEvent(btn_newTimerCancel_Click);
            InputSubmit.SetButtonClickEvent(btn_newTimerSubmit_Click);
        }

        private void btn_newTimerCancel_Click(object sender, RoutedEventArgs e) {
            Btn_Cancel(sender, e);
            SetDefaultFormVariables();
        }
        private void btn_newTimerSubmit_Click(object sender, RoutedEventArgs e) {
            UpdateFormVariables();
            var f_timer = _timerContr.CreateTimer(Name, Description, Annual,
                                      StartTime, GetEnum(), GetStartTime(),
                                      GetDuration(), null);

            _timerContr.IniWrite();
            _window.UpdateTimerBarHolder();
            SetDefaultFormVariables();

            Deactivate();
        }
        public void Hotkey_submit() {
            btn_newTimerSubmit_Click(new object(), new RoutedEventArgs());
        }
    }

    public class EditTimerForm : TimerForm
    {
        private CustomButton InputDelete { get; set; }

        public EditTimerForm(TimerController f_tmrctrl, MainWindow f_window, CustomButton f_delete)
            : base(f_tmrctrl, f_window) {
            InputDelete = f_delete;
        }
        public override void SetButtonEvents() {
            InputDateSet.SetButtonClickEvent(Btn_dateset);
            InputCancel.SetButtonClickEvent(Btn_Cancel);
            InputSubmit.SetButtonClickEvent(btn_editTimerSubmit_Click);
            InputDelete.SetButtonClickEvent(btn_editTimerDelete_Click);
        }
        public void SetFormVariables(Timer f_timer) {
            //time & duration
            var f_time = "08:00";
            var f_dur = "01:00";

            if (f_timer.StartTime.HasValue) {
                f_time = ParseTime(f_timer.StartTime.Value);
                f_dur = ParseTime(f_timer.Duration.Value);
            }
            InputTime.Text = f_time;
            InputDuration.Text = f_dur;
            Time = f_time;
            Duration = f_dur;

            //category
            SetCategory(f_timer.Cat);

            //datetime
            DateTime? f_datetime = DateTime.Today;
            f_datetime = f_datetime.Value.AddDays(1);

            if (f_timer.StartTime.HasValue) {
                f_datetime = f_timer.StartTime.Value;
                var f_timespan = TimeSpan.FromHours(f_datetime.Value.Hour) + TimeSpan.FromMinutes(f_datetime.Value.Minute);
                f_datetime = f_datetime.Value.Subtract(f_timespan);
                InputDate.SelectedDate = f_datetime;
                InputDate.IsEnabled = true;
                InputDateSet.setChecked(true);

            }
            else {
                InputDate.IsEnabled = false;
                InputDateSet.setChecked(false);
                InputDate.SelectedDate = f_datetime.Value;
            }

            //name
            InputName.Text = f_timer.Name;
            InputDesc.Text = f_timer.Description;

            //annual
            Annual = f_timer.Annual;
            SetAnnual();

        }

        private void btn_editTimerSubmit_Click(object sender, RoutedEventArgs e) {
            var f_timerid = _timerContr.EditTimer.Id;
            _timerContr.RemoveTimer(f_timerid);
            UpdateFormVariables();

            var f_timer = _timerContr.CreateTimer(Name, Description, Annual,
                                      StartTime, GetEnum(), GetStartTime(),
                                      GetDuration(), f_timerid);

            _timerContr.IniWrite();
            _window.UpdateTimerBarHolder();

            Deactivate();
        }
        private void btn_editTimerDelete_Click(object sender, RoutedEventArgs e) {

            _timerContr.RemoveTimer(_timerContr.EditTimer.Id);
            _window.UpdateTimerBarHolder();

            Deactivate();
        }
        public void Hotkey_submit() {
            btn_editTimerSubmit_Click(new object(), new RoutedEventArgs());
        }
    }

    public class EditCatForm
    {
        #region Properties
        private enumCat EditCat { get; set; }
        private TextBox InputName { get; set; }
        private Canvas InputForm { get; set; }
        private CustomButton InputButton { get; set; }
        private CatagoryController _catctrl { get; set; }
        private MainWindow _window { get; set; }
        #endregion

        public EditCatForm(CatagoryController f_catctrl, MainWindow f_window, Canvas f_form, TextBox f_textbox, CustomButton f_btn) {
            InputForm = f_form;
            InputName = f_textbox;
            InputButton = f_btn;
            _catctrl = f_catctrl;
            _window = f_window;

            InputButton.SetButtonClickEvent(Button_Click);
            Deactivate();
        }
        public void Activate(enumCat f_cat) {
            EditCat = f_cat;
            InputName.Text = _catctrl.GetCatagoryName(f_cat);
            InputForm.Visibility = Visibility.Visible;
            InputForm.IsEnabled = true;
        }
        public void Deactivate() {
            InputForm.Visibility = Visibility.Hidden;
            InputForm.IsEnabled = false;
        }
        public bool isActive() {
            return InputForm.IsEnabled;
        }
        public void Hotkey_Submit()
        {
            Rename();
        }
        private void Button_Click(object sender, MouseEventArgs e) {
            Rename();
        }
        private void Rename()
        {
            var f_newname = InputName.Text;

            _catctrl.SetCatagoruName(EditCat, f_newname);
            _window.cat_tooltip_reset();
            _catctrl.IniCatSave();
            Deactivate();
        }
    }

    public class FilterForm
    {
        #region Properties
        private List<CustomCheckbox> InputFilters { get; set; }
        private Canvas InputForm { get; set; }
        public bool[] Filters { get; set; }
        private readonly string IniSection = "Filters";
        private IniFile IniSave = new IniFile("Inisave.ini");
        private EditCatForm _EditForm { get; set; }
        private TimerController _TimerCtrl { get; set; }
        private MainWindow _Window { get; set; }
        #endregion

        public FilterForm(List<CustomCheckbox> f_list, Canvas f_form, EditCatForm f_editform, TimerController f_timerctrl, MainWindow f_window) {
            _EditForm = f_editform;
            _TimerCtrl = f_timerctrl;
            _Window = f_window;
            InputFilters = f_list;
            InputForm = f_form;

            Filters = new bool[9];
            if (IniFilterload()) {

            }
            else {
                Filters = new bool[9] { false, true, true, false, false, false, false, false, false };
            }

            //first element in Filters in not used.
            foreach (var chb in InputFilters) {
                chb.SetButtonRightClickEvent(ActivateRenameForm);
                chb.SetButtonClickEvent(ButtonClick);
            }
        }
        private void ButtonClick(object sender, MouseButtonEventArgs e) {
            var f_sender = sender as CustomCheckbox;
            var f_id = (int)f_sender.Tag;

            Filters[f_id] = !(f_sender.isChecked());
            UpdateTimerDisplay();
            _Window.UpdateTimerBarHolder();
            IniFilterSave();
        }
        public void ActivateRenameForm(object sender, MouseButtonEventArgs e) {
            var f_sender = sender as CustomCheckbox;
            var f_cat = (enumCat)f_sender.Tag;

            _EditForm.Activate(f_cat);
        }
        public void UpdateCatTooltips(CatagoryController f_catctrl) {
            foreach (var filter in InputFilters) {
                var f_cat = f_catctrl.GetCatagoryName((int)(filter.Tag));

                filter.SetTooltip(f_cat);
            }
        }
        public void UpdateCatNumbers() {
            foreach (var chb in InputFilters) {
                var f_text = _TimerCtrl.GetCatAmount((enumCat)chb.Tag).ToString();
                chb.SetTagLabel(f_text);
            }
        }
        public void UpdateTimerDisplay() {
            var f_list = _TimerCtrl.Timers;

            foreach (var tmr in f_list) {
                tmr.Display = Filters[(int)tmr.Cat];
            }
        }

        #region Ini functions
        public void IniFilterSave() {
            IniFilterClear();
            for (int i = 1; i < 9; i++) {
                IniSave.Write(i.ToString(), Filters[i].ToString(), IniSection);
            }
        }
        public bool IniFilterload() {
            if (IniSave.KeyExists("1",IniSection)) {
                for (int i = 1; i < 9; i++) {
                    bool f_bool = bool.Parse((IniSave.Read(i.ToString(), IniSection)));
                    Filters[i] = f_bool;
                }

                int f_filters = 1;
                foreach (var btn in InputFilters) {
                    btn.setChecked(Filters[f_filters]);
                    f_filters++;
                }
                return true;
            }
            else {
                return false;
            }
        }
        public void IniFilterClear() {
            IniSave.DeleteSection(IniSection);
        }
        #endregion
    }

    namespace Timers
    {
        using CountdownCalendar;

        public enum TimerState
        {
            Counting,
            InProgress,
            Finished,
            Unknown
        }

        public class TimerController
        {
            #region Properties
            public MainWindow _window { get; set; }
            private IniFile IniSave = new IniFile("Inisave.ini");
            private CatagoryController _catCtrl { get; set; }
            public List<Timer> Timers { get; set; }
            public Timer EditTimer { get; set; }
            #endregion

            public TimerController(MainWindow f_window, CatagoryController f_carCtrl) {
                Timers = new List<Timer>();
                EditTimer = null;
                _catCtrl = f_carCtrl;
                _window = f_window;
            }
            public void TimerButton_click(object sender, RoutedEventArgs e) {
                var f_btn = sender as Canvas;
                int? f_id = f_btn.Tag as int?;
                if (f_id.HasValue) {
                    var f_timer = GetTimerById(f_id.Value);
                    if (f_timer != null) {
                        EditTimer = f_timer;

                        _window.FormNew.Deactivate();
                        _window.FormEdit.Activate();
                        _window.FormEdit.SetFormVariables(f_timer);
                    }
                }
            }

            public void UpdateTimerCountdowns() {
                foreach (var timer in Timers) {
                    timer.UpdateTimerCountdown();
                }
            }
            #region Basic functions
            public Timer CreateTimer(string f_name, string f_desc, bool f_annual, DateTime? f_starttime, enumCat f_cat, TimeSpan? f_hoursminutes, TimeSpan? f_dur, int? f_newid) {
                var f_timer = new Timer() {
                    Name = f_name,
                    Description = f_desc,
                    Annual = f_annual,
                    StartTime = f_starttime,
                    Cat = f_cat,
                    Duration = f_dur
                };
                if (f_newid.HasValue) {
                    f_timer.Id = f_newid.Value;
                }
                else {
                    f_timer.Id = GenerateNewId();
                }
                f_timer.Display = _window.FormFilter.Filters[(int)f_timer.Cat];
                f_timer.StartTime += f_hoursminutes;
                f_timer.TimerBar = Timerbar_Create(f_timer);

                AddTimer(f_timer);

                return f_timer;
            }
            public Timer CreateTimer(TimerForm f_form) {
                //todo
                return new Timer();
            }
            public StackPanel Timerbar_Create(Timer f_timer) {
                var f_holder = new StackPanel();
                f_holder.Width = 383;
                f_holder.Height = 60;
                f_holder.HorizontalAlignment = HorizontalAlignment.Left;
                f_holder.Background = _catCtrl.GetCatagoryColour(f_timer.Cat);


                var f_borderBot = new Border()
                {
                    BorderThickness = new Thickness(0, 1, 0, 0),
                    Margin = new Thickness(0, 0, 0, 0),
                    BorderBrush = Brushes.Black,
                    Width = f_holder.Width
                };
                var f_borderTop = new Border()
                {
                    BorderThickness = new Thickness(0, 1, 0, 0),
                    Margin = new Thickness(0, 0, 0, 0),
                    BorderBrush = Brushes.Black,
                    Width = f_holder.Width
                };
                
                

                var f_lbl1 = new Label {
                    Content = f_timer.Name,
                    FontWeight = FontWeights.DemiBold,
                    FontSize = 16
                };
                var f_lbl2 = new Label();
                var f_lbl3 = new Label {
                    Content = "",
                    MaxWidth = 200,
                    MinWidth = 200
                };

                var f_lbl4 = new Label();
                var f_lbl5 = new Label();
                if (f_timer.StartTime.HasValue) {
                    f_lbl2.Content = f_timer.StartTime.Value.ToShortDateString();
                    f_lbl4.Content = f_timer.StartTime.Value.ToShortTimeString();
                    f_lbl5.Content = f_timer.Duration.ToString().Substring(0, 5);
                }

                

                var f_img1 = new Image {
                    Stretch = Stretch.Fill,
                    MaxHeight = 20,
                    MaxWidth = 20
                };
                if (f_timer.Annual) {
                    f_img1.Source = ImageControl.IconAnnual;
                }

                int f_Id = f_timer.Id;
                var f_highlight = new Canvas()
                {
                    Opacity = 0,
                    Background = Brushes.AliceBlue,
                    Margin = new Thickness(0, -56, 0, 0),
                    Height = f_holder.Height,
                    Width = f_holder.Width,
                    Tag = f_Id
                };
                f_highlight.MouseLeftButtonUp += new MouseButtonEventHandler(TimerButton_click);
                f_highlight.MouseEnter += new MouseEventHandler(EnableHighlight);
                f_highlight.MouseLeave += new MouseEventHandler(DisableHighlight);

                var f_parentstack = new StackPanel();
                var f_stack1 = new StackPanel();
                var f_stack2 = new StackPanel();
                var f_stack3 = new StackPanel();
                f_stack1.Orientation = Orientation.Horizontal;
                f_stack2.Orientation = Orientation.Horizontal;
                f_stack3.Orientation = Orientation.Horizontal;
                f_stack2.Height = 24;

                f_stack1.Children.Add(new Canvas() { Width = 6 });
                f_stack1.Children.Add(f_lbl1);

                f_stack2.Children.Add(new Canvas() { Width = 6 });
                f_stack2.Children.Add(f_lbl2);
                f_stack2.Children.Add(f_lbl4);
                f_stack2.Children.Add(f_lbl5);
                f_stack2.Children.Add(new Canvas() { Width = 170 });
                f_stack2.Children.Add(f_img1);

                f_stack3.Children.Add(new Canvas() { Width = 6 });
                f_stack3.Children.Add(f_lbl3);

                f_parentstack.Children.Add(f_stack1);
                f_parentstack.Children.Add(f_stack2);
                f_parentstack.Children.Add(f_stack3);

                f_stack2.Margin = new Thickness(-3, -9, -3, -6);
                f_lbl1.Margin = new Thickness(-3);
                f_lbl3.Margin = new Thickness(-3);

                //f_border.Child = f_parentstack
                f_holder.Children.Add(f_borderTop);
                f_holder.Children.Add(f_parentstack);
                f_holder.Children.Add(f_highlight);
                f_holder.Children.Add(f_borderBot);


                if (f_timer.Description != "") {
                    f_highlight.ToolTip = f_timer.Description;
                }
                
                f_timer.CountdownLabel = f_lbl3;
                

                return f_holder;
            }
            public void AddTimer(Timer f_timer) {
                if (f_timer != null) {
                    Timers.Add(f_timer);
                }
                _window.FormFilter.UpdateCatNumbers();
            }
            public void RemoveTimer(int f_timerId) {
                var f_timer = GetTimerById(f_timerId);
                if (f_timer != null) {
                    Timers.Remove(f_timer);
                    IniWrite();
                }
                _window.FormFilter.UpdateCatNumbers();
            }
            public Timer GetTimerById(int f_timerId) {
                foreach (var f_timer in Timers) {
                    if (f_timer.Id == f_timerId) {
                        return f_timer;
                    }
                }
                return null;
            }
            private int GenerateNewId() {
                int f_id = 0;
                bool f_taken;
                while (true) {
                    f_taken = false;
                    foreach (var f_timer in Timers) {
                        if (f_id == f_timer.Id) {
                            f_taken = true;
                            break;
                        }
                    }
                    if (!f_taken) {
                        return f_id;
                    }
                    f_id++;
                }
            }
            public int GetCatAmount(enumCat f_cat) {
                int f_result = 0;

                foreach (var tmr in Timers) {
                    if (tmr.Cat == f_cat) {
                        f_result++;
                    }
                }


                return f_result;
            }
            private void EnableHighlight(object sender, MouseEventArgs e)
            {
                var can = sender as Canvas;
                can.Opacity = 0.3;
            }
            private void DisableHighlight(object sender, MouseEventArgs e)
            {
                var can = sender as Canvas;
                can.Opacity = 0;
            }
            #endregion
            #region Ini functions
            public void IniWrite() {
                int f_section = 0;
                IniClear();
                foreach (var f_timer in Timers) {
                    IniSave.Write("id", f_timer.Id.ToString(), f_section.ToString());
                    IniSave.Write("name", f_timer.Name, f_section.ToString());
                    IniSave.Write("description", f_timer.Description, f_section.ToString());
                    IniSave.Write("annual", f_timer.Annual.ToString(), f_section.ToString());
                    IniSave.Write("catagory", f_timer.Cat.ToString(), f_section.ToString());

                    if (f_timer.StartTime.HasValue) {
                        IniSave.Write("dateticks", f_timer.StartTime.Value.Ticks.ToString(), f_section.ToString());
                        IniSave.Write("durationticks", f_timer.Duration.Value.Ticks.ToString(), f_section.ToString());
                    }
                    else {
                        IniSave.Write("dateticks", "", f_section.ToString());
                        IniSave.Write("durationticks", "", f_section.ToString());
                    }

                    f_section++;
                }
            }
            public void IniClear() {
                int f_section = 0;
                string f_key = "name";
                while (IniSave.KeyExists(f_key, f_section.ToString())) {
                    IniSave.DeleteSection(f_section.ToString());
                    f_section++;
                }
            }
            public void Inireload() {
                string f_key = "name";
                int f_section = 0;
                while (IniSave.KeyExists(f_key, f_section.ToString())) {

                    DateTime? f_datetime = new DateTime();
                    TimeSpan? f_duration = new TimeSpan();
                    if (IniSave.Read("dateticks", f_section.ToString()) == "") {
                        f_datetime = null;
                        f_duration = null;
                    }
                    else {
                        f_datetime = f_datetime.Value.AddTicks(Int64.Parse(IniSave.Read("dateticks", f_section.ToString())));
                        f_duration = TimeSpan.FromTicks(Int64.Parse(IniSave.Read("durationticks", f_section.ToString())));
                    }

                    var f_timer = CreateTimer(IniSave.Read("name", f_section.ToString()),
                                             IniSave.Read("description", f_section.ToString()),
                                             bool.Parse(IniSave.Read("annual", f_section.ToString())),
                                             f_datetime,
                                             (enumCat)Enum.Parse(typeof(enumCat), IniSave.Read("catagory", f_section.ToString())),
                                             new TimeSpan(),
                                             f_duration,
                                             int.Parse(IniSave.Read("id", f_section.ToString()))
                                             );
                    f_section += 1;
                }
            }
            #endregion

        }

        public class Timer
        {
            #region timer properties
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime? StartTime { get; set; }
            public TimeSpan? Duration { get; set; }
            public bool Annual { get; set; }
            public enumCat Cat { get; set; }
            public bool Display { get; set; }
            public StackPanel TimerBar { get; set; }
            public Label CountdownLabel { get; set; }
            #endregion

            public bool isStarted() {
                return (StartTime < DateTime.Now);
            }
            public bool isDone() {
                return (StartTime + Duration < DateTime.Now);
            }
            public void UpdateTimerCountdown() {
                var f_state = GetState();

                switch (f_state) {
                    case TimerState.Counting: SetCountdown(); break;
                    case TimerState.InProgress: SetInProgress(); break;
                    case TimerState.Finished: SetFinished(); break;
                    case TimerState.Unknown: SetUnknown(); break;
                }

            }
            public TimerState GetState() {
                var f_now = DateTime.Now;
                if (!StartTime.HasValue) {
                    return TimerState.Unknown;
                }
                else if (StartTime > f_now) {
                    return TimerState.Counting;
                }
                else if ((StartTime + Duration) > f_now) {
                    return TimerState.InProgress;
                }
                else {
                    return TimerState.Finished;
                }
            }
            private void SetCountdown() {
                var f_timeleft = StartTime - DateTime.Now;
                var f_str = GetTimeString(f_timeleft.Value);

                CountdownLabel.Content = f_str;
            }
            private void SetInProgress() {
                var f_timeleft = (StartTime + Duration) - DateTime.Now;
                var f_str = GetTimeString(f_timeleft.Value);

                f_str = "In progress: " + f_str;

                CountdownLabel.Content = f_str;
            }
            private void SetFinished() {
                CountdownLabel.Content = "Finished";
            }
            private void SetUnknown() {
                CountdownLabel.Content = "Time Not Set";
            }
            private string GetTimeString(TimeSpan f_time) {
                if (f_time == null) return "";
                var f_str = "";
                int f_int1, f_int2;
                if (f_time.Days > 364) {
                    //years
                    f_int1 = f_time.Days / 364;
                    //months
                    f_int2 = (f_time.Days % 364) / 30;

                    f_str += f_int1;
                    f_str += (f_int1 == 1 ? " year, " : " years, ");
                    f_str += f_int2;
                    f_str += (f_int2 == 1 ? " month" : " months");
                    f_str += " left.";
                }
                else if (f_time.Days > 30) {
                    //months
                    f_int1 = f_time.Days / 30;
                    //days
                    f_int2 = f_time.Days % 30;

                    f_str += f_int1;
                    f_str += (f_int1 == 1 ? " month, " : " months, ");
                    f_str += f_int2;
                    f_str += (f_int2 == 1 ? " day" : " days");
                    f_str += " left.";
                }
                else if (f_time.Days > 1) {
                    //days
                    f_int1 = f_time.Days;
                    //hours
                    f_int2 = f_time.Hours;

                    f_str += f_int1;
                    f_str += (f_int1 == 1 ? " day, " : " days, ");
                    f_str += f_int2;
                    f_str += (f_int2 == 1 ? " hour" : " hours");
                    f_str += " left.";
                }
                else if (f_time.Hours > 1) {
                    //hours
                    f_int1 = f_time.Hours;
                    //minutes
                    f_int2 = f_time.Minutes;

                    f_str += f_int1;
                    f_str += (f_int1 == 1 ? " hour, " : " hours, ");
                    f_str += f_int2;
                    f_str += (f_int2 == 1 ? " minute" : " minutes");
                    f_str += " left.";
                }
                else if (f_time.Minutes > 1) {
                    //minutes
                    f_int1 = f_time.Minutes;
                    //seconds
                    f_int2 = f_time.Seconds;

                    f_str += f_int1;
                    f_str += (f_int1 == 1 ? " minute, " : " minutes, ");
                    f_str += f_int2;
                    f_str += (f_int2 == 1 ? " second" : " seconds");
                    f_str += " left.";
                }
                else {
                    //seconds
                    f_int1 = f_time.Seconds;

                    f_str += f_int1;
                    f_str += (f_int1 == 1 ? " second " : " seconds ");
                    f_str += " left.";
                }

                return f_str;
            }
        }

        namespace Catagories
        {
            using System.Windows.Media;
            using CommonClasses.Images;
            public enum enumCat
            {
                blue = 1,
                green,
                red,
                orange,
                pink,
                yellow,
                teal,
                purple

            }

            public class CatagoryController
            {
                #region Properties
                private readonly static int MaxCatagories = 8;
                private readonly string IniSection = "Catagory";
                private IniFile IniSave = new IniFile("Inisave.ini");
                public List<Catagory> Cats { get; set; }
                #endregion

                public CatagoryController() {
                    Cats = new List<Catagory>();
                    var f_load = IniCatLoad();

                    if (!f_load) {

                        var f_cat1 = new Catagory(1, "Blue");
                        var f_cat2 = new Catagory(2, "Green");
                        var f_cat3 = new Catagory(3, "Red");
                        var f_cat4 = new Catagory(4, "Orange");
                        var f_cat5 = new Catagory(5, "Pink");
                        var f_cat6 = new Catagory(6, "Yellow");
                        var f_cat7 = new Catagory(7, "Teal");
                        var f_cat8 = new Catagory(8, "Purple");

                        Cats.Add(f_cat1);
                        Cats.Add(f_cat2);
                        Cats.Add(f_cat3);
                        Cats.Add(f_cat4);
                        Cats.Add(f_cat5);
                        Cats.Add(f_cat6);
                        Cats.Add(f_cat7);
                        Cats.Add(f_cat8);

                    }
                }
                #region Ini functions
                public void IniCatSave() {
                    IniCatClear();
                    foreach (var cat in Cats) {
                        IniSave.Write(cat.Id.ToString(), cat.Name, IniSection);
                    }
                }
                public bool IniCatLoad() {
                    if (IniSave.KeyExists("1", IniSection)) {
                        for (int i = 1; i <= MaxCatagories; i++) {
                            string f_name = IniSave.Read(i.ToString(), IniSection);
                            var f_cat = new Catagory(i, f_name);
                            Cats.Add(f_cat);
                        }

                        return true;
                    }
                    else return false;
                }
                private void IniCatClear() {
                    IniSave.DeleteSection(IniSection);
                }
                #endregion
                #region Get functions
                public SolidColorBrush GetCatagoryColour(enumCat f_enum) {
                    var f_cat = GetCatagory(f_enum);
                    return f_cat.Colour;

                }
                public string GetCatagoryName(int f_id) {
                    return GetCatagoryName((enumCat)f_id);
                }
                public string GetCatagoryName(enumCat f_enum) {
                    var f_catagory = GetCatagory(f_enum);

                    return f_catagory.Name;
                }
                public Catagory GetCatagory(int f_id) {
                    foreach (var cat in Cats) {
                        if (cat.Id == f_id) {
                            return cat;
                        }
                    }
                    return null;
                }
                public Catagory GetCatagory(enumCat f_enum) {
                    return GetCatagory((int)f_enum);
                }
                public void SetCatagoruName(enumCat f_enum, string f_name) {
                    var f_cat = GetCatagory(f_enum);
                    f_cat.Name = f_name;
                }
                #endregion

                public class Catagory
                {
                    public int Id { get; set; }
                    public string Name { get; set; }
                    public SolidColorBrush Colour { get; set; }

                    public Catagory(int f_id, string f_name) {
                        Id = f_id;
                        Name = f_name;

                        Colour = Helper_getColour(Id);
                    }
                    private SolidColorBrush Helper_getColour(int f_id) {
                        switch (f_id) {
                            case 1: return Brushes.CornflowerBlue;
                            case 2: return Brushes.PaleGreen;
                            case 3: return Brushes.Red;
                            case 4: return Brushes.Orange;
                            case 5: return Brushes.HotPink;
                            case 6: return Brushes.LightYellow;
                            case 7: return Brushes.SkyBlue;
                            case 8: return Brushes.BlueViolet;

                            default: return Brushes.AliceBlue;
                        }
                    }
                }
            }
        }
    }
}
