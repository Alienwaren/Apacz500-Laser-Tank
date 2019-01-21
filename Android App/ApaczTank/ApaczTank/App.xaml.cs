using System;
using ApaczTank.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ApaczTank
{
    public partial class App : Application
    {
        public static IBluetoothService BluetoothService { get; private set; }

        public static IMessageService MessageService { get; private set; }

        public static void InitApp(IBluetoothService implementedBluetoothService, IMessageService implementedMessageService)
        {
            BluetoothService = implementedBluetoothService;
            MessageService = implementedMessageService;
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new RootPage());
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
