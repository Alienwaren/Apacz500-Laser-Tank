using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ApaczTank.Interfaces;
using Java.Util;

namespace ApaczTank.Droid
{
    public class BluetoothService : IBluetoothService
    {
        public bool Connected => _bluetoothSocket.IsConnected;

        private BluetoothAdapter _bluetoothAdapter;

        private BluetoothSocket _bluetoothSocket;

        public bool HasBluetooth { get; } = true;

        public BluetoothService()
        {
            _bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            if (_bluetoothAdapter == null)
            {
                HasBluetooth = false;
            }
        }

        public bool IsEnabled => _bluetoothAdapter.IsEnabled;

        public List<string> GetBondedDevices()
        {
            List<string> devices = new List<string>();
            if (HasBluetooth)
            {
                foreach (BluetoothDevice dev in _bluetoothAdapter.BondedDevices)
                {
                    devices.Add(dev.Name);
                } 
            }
            return devices;
        }

        public bool Connect(string deviceName)
        {
            if (!string.IsNullOrEmpty(deviceName) && IsEnabled)
            {
                BluetoothDevice dev = _bluetoothAdapter.BondedDevices.FirstOrDefault(x => x.Name == deviceName);
                _bluetoothSocket =
                    dev?.CreateInsecureRfcommSocketToServiceRecord(
                        UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));
                try
                {
                    _bluetoothSocket?.Connect();

                }
                catch (Java.IO.IOException)
                {
                    App.MessageService.DisplayMessage("Could not connect to specified device. Please try again");
                }
                
            }
            return _bluetoothSocket != null && _bluetoothSocket.IsConnected;
        }

        public bool Disconnect()
        {
            throw new NotImplementedException();
        }

        public string ReadFromDevice(int count)
        {
            string receivedMessage = "";
            if (_bluetoothSocket.IsConnected)
            {
                byte[] buffer = new byte[count];
                _bluetoothSocket?.InputStream?.Read(buffer, 0, count);
                receivedMessage = Encoding.UTF8.GetString(buffer);
            }
            return receivedMessage;
        }

        public void WriteToDevice(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                _bluetoothSocket?.OutputStream?.Write(Encoding.UTF8.GetBytes(message), 0, message.Length);
            }

        }
    }
}