using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeApp.Pages
{
    public partial class LoginPage : ContentPage
    {
        public const string BUTTON_TEXT = "Войти";
        // Переменная счетчика
        public static int loginCouner = 0;
        IDeviceDetector detector = DependencyService.Get<IDeviceDetector>();

        public LoginPage()
        {
            InitializeComponent();
            if (Device.Idiom == TargetIdiom.Desktop)
                loginButton.CornerRadius = 0;
            //runningDevice.Text = detector.GetDevice();
            infoMessage.SetDynamicResource(Label.TextColorProperty, "errorColor");
            infoMessage.SetDynamicResource(Label.FontSizeProperty, "errorFont");
        }
        private async void Login_Click(object sender, EventArgs e)
        {
            loginButton.Text = $"Выполняется вход..";
            // Имитация задержки (приложение загружает данные с сервера)
            await Task.Delay(150);

            // Переход на следующую страницу - страницу списка устройств
            await Navigation.PushAsync(new DeviceListPage());
            // Восстановим первоначальный текст на кнопке (на случай, если пользователь вернется на этот экран чтобы выполнить вход снова)
            loginButton.Text = BUTTON_TEXT;
        }
    }
}
