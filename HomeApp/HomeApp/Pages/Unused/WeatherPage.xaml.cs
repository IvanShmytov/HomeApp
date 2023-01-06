using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherPage : ContentPage
    {
        public WeatherPage()
        {
            InitializeComponent();
        }
        private void Weather_Click(object sender, EventArgs e)
        {
            Grid grid = new Grid
            {
                // Набор строк 
                RowDefinitions =
               {
                   new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                   new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                   new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
               }
            };
            grid.BackgroundColor = Color.Black;
            grid.Children.Add(new BoxView { Color = Color.LightBlue, }, 0, 0);
            grid.Children.Add(new BoxView { Color = Color.LightGoldenrodYellow, }, 0, 1);
            grid.Children.Add(new BoxView { Color = Color.LightGreen, }, 0, 2);
            grid.Children.Add(new Label {Text = "Outside", FontSize = 25, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Start }, 0, 0);
            grid.Children.Add(new Label { Text = "- 5 C", FontSize = 55, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center }, 0, 0);
            grid.Children.Add(new Label { Text = "Inside", FontSize = 25, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Start }, 0, 1);
            grid.Children.Add(new Label { Text = "+ 25 C", FontSize = 55, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center }, 0, 1);
            grid.Children.Add(new Label { Text = "Pressure", FontSize = 25, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Start }, 0, 2);
            grid.Children.Add(new Label { Text = "760 mm", FontSize = 55, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center }, 0, 2);
            this.Content = grid;
        }
        private void SetAlarm(object sender, EventArgs eventArgs)
        {
            // Основной контейнер компоновки
            var layout = new StackLayout() { Margin = new Thickness(20) };

            // Заголовок
            var header = new Label { Text = "Установить будильник", Margin = new Thickness(0, 20, 0, 0), FontSize = 20, HorizontalTextAlignment = TextAlignment.Center };
            layout.Children.Add(header);

            // Виджет выбора даты с описанием
            var datePickerText = new Label { Text = "Дата запуска", Margin = new Thickness(0, 20, 0, 0) };
            layout.Children.Add(datePickerText);
            var datePicker = new DatePicker
            {
                Format = "D",
                MaximumDate = DateTime.Now.AddDays(7),
                MinimumDate = DateTime.Now.AddDays(-7),
            };
            layout.Children.Add(datePicker);

            // Виджет выбора времени с описанием
            var timePickerText = new Label { Text = "Время запуска ", Margin = new Thickness(0, 20, 0, 0) };
            layout.Children.Add(timePickerText);
            var timePicker = new TimePicker
            {
                Time = new TimeSpan(13, 0, 0)
            };
            layout.Children.Add(timePicker);

            // Переключатель громкости с описанием
            Slider slider = new Slider
            {
                Minimum = 0,
                Maximum = 30,
                Value = 5.0,
                ThumbColor = Color.DodgerBlue,
                MinimumTrackColor = Color.DodgerBlue,
                MaximumTrackColor = Color.Gray
            };
            var sliderText = new Label { Text = $"Громкость: {slider.Value}", HorizontalOptions = LayoutOptions.Center, Margin = new Thickness(0, 30, 0, 0) };
            // Регистрируем обработчик события изменения громкости
            slider.ValueChanged += (s, e) => VolumeHandler(s, e, sliderText);
            layout.Children.Add(sliderText);
            layout.Children.Add(slider);

            // Переключатель и заголовок для него
            var switchHeader = new Label { Text = "Повторять каждый день", HorizontalOptions = LayoutOptions.Center, Margin = new Thickness(0, 5, 0, 0) };
            layout.Children.Add(switchHeader);
            Switch switchControl = new Switch
            {
                IsToggled = false,
                HorizontalOptions = LayoutOptions.Center,
                ThumbColor = Color.DodgerBlue,
                OnColor = Color.LightSteelBlue,
            };
            layout.Children.Add(switchControl);

            // Кнопка сохранения и обработчик для неё
            var saveAlarmButton = new Button { Text = "Сохранить", BackgroundColor = Color.Silver, Margin = new Thickness(0, 5, 0, 0) };
            saveAlarmButton.Clicked += (s, e) => SaveAlarmHandler(s, e, datePicker.Date + timePicker.Time);
            layout.Children.Add(saveAlarmButton);

            // Инициализация леаута
            this.Content = layout;
        }
        private void VolumeHandler(object sender, ValueChangedEventArgs e, Label header)
        {
            header.Text = String.Format("Громкость: {0:F1}", e.NewValue);
        }

        /// <summary>
        /// Обработчик сохранения будильника
        /// </summary>
        void SaveAlarmHandler(object sender, EventArgs e, DateTime alarmDate)
        {
            var layout = new StackLayout() { Margin = new Thickness(20), VerticalOptions = LayoutOptions.Center };
            var dateHeader = new Label { Text = $"Будильник сработает:", FontSize = 20, HorizontalTextAlignment = TextAlignment.Center };
            var dateText = new Label { Text = $"{alarmDate.Day}.{alarmDate.Month} в {alarmDate.Hour}:{alarmDate.Minute}", FontSize = 20, HorizontalTextAlignment = TextAlignment.Center };

            layout.Children.Add(dateHeader);
            layout.Children.Add(dateText);
            this.Content = layout;
        }
    }
}