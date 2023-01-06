using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace HomeApp.Pages
{
    public partial class DeviceControlPage : ContentPage
    {
        public DeviceControlPage()
        {
            InitializeComponent();
            GetContent();
        }

        public void GetContent()
        {
            // Создаем виджет выбора даты
            var datePicker = new DatePicker
            {
                Format = "D",
                // Диапазон дат: +/- неделя
                MaximumDate = DateTime.Now.AddDays(7),
                MinimumDate = DateTime.Now.AddDays(-7),
            };

            var datePickerText = new Label { Text = "Дата запуска ", Margin = new Thickness(0, 20, 0, 0) };

            // Добавляем всё на страницу
            //stackLayout.Children.Add(new Label { Text = "Устройство" });
            //stackLayout.Children.Add(new Entry { BackgroundColor = Color.AliceBlue, Text = "Холодильник" });
            stackLayout.Children.Add(datePickerText);
            stackLayout.Children.Add(datePicker);

            // Виджет выбора времени.
            var timePickerText = new Label { Text = "Время запуска ", Margin = new Thickness(0, 20, 0, 0) };
            var timePicker = new TimePicker
            {
                Time = new TimeSpan(13, 0, 0)
            };

            stackLayout.Children.Add(timePickerText);
            stackLayout.Children.Add(timePicker);

            // Создаем меню выбора в виде выпадающего списка с текстовым заголовком
            var pickerText = new Label { Text = "Напряжение сети, В", Margin = new Thickness(0, 20, 0, 0) };
            var picker = new Picker { Title = "Выберите напряжение сети" };
            // Добавляем значения выпадающего списка для пользовательского выбора
            picker.Items.Add("220");
            picker.Items.Add("120");
            // Добавляем элементы на страницу
            stackLayout.Children.Add(pickerText);
            stackLayout.Children.Add(picker);

            var saveButton = new Button
            {
                Text = "Сохранить",
                BackgroundColor = Color.Silver,
                Margin = new Thickness(0, 5, 0, 0)
            };
            saveButton.Clicked += SaveButtonClick;
            stackLayout.Children.Add(saveButton);

            // Регистрируем обработчик события выбора даты
            datePicker.DateSelected += (sender, e) => DateSelectedHandler(sender, e, datePickerText);
            // Регистрируем обработчик события выбора времени
            timePicker.PropertyChanged += (sender, e) => TimeChangedHandler(sender, e, timePickerText, timePicker);

            var stepperText = new Label
            {
                Text = "Температура: 5.0 °C",
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 30, 0, 0)
            };
            // Установим сам переключатель
            //Stepper stepper = new Stepper
            //{
            //    Minimum = -30,
            //    Maximum = 30,
            //    Increment = 1,
            //    Value = 5,
            //    HorizontalOptions = LayoutOptions.Center,
            //    VerticalOptions = LayoutOptions.CenterAndExpand
            //};
            Slider slider = new Slider
            {
                Minimum = -30,
                Maximum = 30,
                Value = 5.0,
                ThumbColor = Color.Red,
                MinimumTrackColor = Color.DodgerBlue,
                MaximumTrackColor = Color.Gray
            };
            var sliderText = new Label { Text = $"Температура: {slider.Value} °C", HorizontalOptions = LayoutOptions.Center, Margin = new Thickness(0, 30, 0, 0) };
            stackLayout.Children.Add(sliderText);
            stackLayout.Children.Add(slider);
            slider.ValueChanged += (sender, e) => TempChangedHandler(sender, e, sliderText);
           

        }
        private void SaveButtonClick(object sender, EventArgs e)
        {
            // После сохранения параметров отключаем пользователю возможность редактирования
            deviceEntry.IsEnabled = false;
        }
        private void TempChangedHandler(object sender, ValueChangedEventArgs e, Label header)
        {
            header.Text = String.Format("Температура: {0:F1}°C", e.NewValue);
        }
        public void TimeChangedHandler(object sender, PropertyChangedEventArgs e, Label timePickerText, TimePicker timePicker)
        {
            // Обновляем текст сообщения, когда появляется новое значение времени
            if (e.PropertyName == "Time")
                timePickerText.Text = "В " + timePicker.Time;
        }
        public void DateSelectedHandler(object sender, DateChangedEventArgs e, Label datePickerText)
        {
            // При срабатывании выбора - будет меняться информационное сообщение.
            datePickerText.Text = "Запустится " + e.NewDate.ToString("dd/MM/yyyy");
        }
    }
}