using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApaczTank.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApaczTank
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BluetoothDevicesPage : ContentPage
	{
		public BluetoothDevicesPage ()
		{
			InitializeComponent ();
		    if (!DesignMode.IsDesignModeEnabled)
		    {
		        List<string> btDevices = App.BluetoothService.GetBondedDevices();
		        if (btDevices.Count == 0)
		        {
		            btDevices.Add("No paired devices or bluetooth is not supported.");
		        }
		        else if (!App.BluetoothService.HasBluetooth)
		        {
		            btDevices.Add("Bluetooth not supported.");
		        }
		        BluetoothItems.ItemsSource = btDevices;
            }
		}


	    private void ConnectOnSelectionChanged(object sender, EventArgs e)
	    {
	        string elementName = (sender as Picker)?.SelectedItem as string;
	        if (App.BluetoothService.Connect(elementName))
	        {
                 //todo: make toasts
                Debug.Write("Connected!");
	        }
	    }
	}
}