using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace HomeApp.Pages
{
    public partial class AlarmPage : ContentPage
    {
        public AlarmPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик события изменения громкости
        /// </summary>
        private void VolumeHandler(object sender, ValueChangedEventArgs e)
        {
            volumeHeader.Text = String.Format("Громкость: {0:F1}", e.NewValue);
        }

        /// <summary>
        /// Обработчик сохранения будильника
        /// </summary>
        void SaveAlarmHandler(object sender, EventArgs e)
        {
            if (datePicker.Date + timePicker.Time <= DateTime.Now)
                return;

            datePicker.IsEnabled = false;
            timePicker.IsEnabled = false;
            volumeSlider.IsEnabled = false;
            recurringSwitch.IsEnabled = false;

            string minutesString = timePicker.Time.Minutes.ToString();
            string minutes = minutesString.Length == 1 ? "0" + minutesString : minutesString;
            resultMessage.Text = $"Будильник установлен на {datePicker.Date.Day}.{datePicker.Date.Month}, {timePicker.Time.Hours}:{minutes}";
        }

        /// <summary>
        ///  Отслеживаем и валидируем дату
        /// </summary>
        private void DateChangeHandler(object sender, PropertyChangedEventArgs e)
        {
            VisualStateManager.GoToState(datePicker, datePicker.Date < DateTime.Today ? "Invalid" : "Valid");
        }

        /// <summary>
        /// Отслеживаем и валидируем время
        /// </summary>
        private void TimeChangeHandler(object sender, PropertyChangedEventArgs e)
        {
            VisualStateManager.GoToState(timePicker, datePicker.Date + timePicker.Time < DateTime.Now ? "Invalid" : "Valid");
        }
    }
}