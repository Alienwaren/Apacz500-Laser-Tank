using System;
using System.Diagnostics;
using System.Timers;
using Android.OS;
using Xamarin.Forms;
using Debug = System.Diagnostics.Debug;

namespace ApaczTank
{
    public partial class RootPage
    {
        private Timer _timer = new Timer();

        private string _actualAction = "";
        public RootPage()
        {
            InitializeComponent();
            _timer.Elapsed += SendMsgOnElasped;
            _timer.Interval = 10;
        }

        private void SendMsgOnElasped(object sender, ElapsedEventArgs e)
        {
            App.BluetoothService.WriteToDevice(_actualAction);
            Debug.Write(_actualAction);
        }

        private void TankControlOnClick(object sender, EventArgs e)
        {
            _timer.Start();
            if (sender is Button clickedButton)
            {
                string clickedButtonName = clickedButton.ClassId;
                if (clickedButtonName == "ForwardButton")
                {
                    _actualAction = "UP";
                }else if (clickedButtonName == "BackButton")
                {
                    _actualAction = "DOWN";
                }
                else if (clickedButtonName == "RightButton")
                {
                    _actualAction = "RIGHT";
                }
                else if (clickedButtonName == "LeftButton")
                {
                    _actualAction = "LEFT";
                }
                else if (clickedButtonName == "FireButton")
                {
                    _actualAction = "SHOOT";
                }
            }
        }

        private void NavigateToBluetoothSettings(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BluetoothDevicesPage());
        }

        private void StopSendingCommandsOnButtonRelease(object sender, EventArgs e)
        {
            _timer.Stop();
        }
    }

}
