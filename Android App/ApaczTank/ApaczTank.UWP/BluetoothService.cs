using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using ApaczTank.Interfaces;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Enumeration;
using Windows.Devices.Radios;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace ApaczTank.UWP
{
    public class BluetoothService : IBluetoothService
    {
        private RfcommDeviceService _rfcommDeviceService;

        private StreamSocket _btStreamSocket = new StreamSocket();

        private DeviceInformationCollection _services;

        public bool IsEnabled { get;  }
        public bool HasBluetooth { get; private set; } = false;

        public bool IsConnected { get; set; }

        public BluetoothService()
        {
            Initialize();
        }

        public async void Initialize()
        {
            var radios = await Radio.GetRadiosAsync();
            var btRadio = radios.FirstOrDefault(radio => radio.Kind == RadioKind.Bluetooth);
            if (btRadio != null)
            {
                HasBluetooth = true;
            }
            var serviceSelector = RfcommDeviceService.GetDeviceSelector(RfcommServiceId.SerialPort);
            _services = await DeviceInformation.FindAllAsync(serviceSelector);
            
        }

        public List<string> GetBondedDevices()
        {
            List<string> foundDevices = new List<string>();
            foreach (var device in _services)
            {
                string devName = device.Name;
                foundDevices.Add(devName);
            }
            return foundDevices;
            
        }

        private async void PrepareRfCommService(string deviceName)
        {
            if (!string.IsNullOrEmpty(deviceName))
            {
                foreach (var item in _services)
                {
                    if (item.Name == deviceName)
                    {
                        _rfcommDeviceService = await RfcommDeviceService.FromIdAsync(item.Id);
                        break;
                    }
                }
                if (_rfcommDeviceService != null)
                {
                    await _btStreamSocket.ConnectAsync(_rfcommDeviceService.ConnectionHostName, _rfcommDeviceService.ConnectionServiceName);
                    IsConnected = true;
                }
            }
        }

        public bool Connect(string deviceName)
        {
            bool connected = false;

            PrepareRfCommService(deviceName);
            
            return IsConnected;
        }

        public bool Disconnect()
        {
            throw new NotImplementedException();
        }

        public string ReadFromDevice(int count)
        {
            throw new NotImplementedException();
        }

        private async void SendString(string message)
        {
            DataWriter dwriter = new DataWriter(_btStreamSocket.OutputStream);
            uint len = dwriter.MeasureString(message);
            dwriter.WriteUInt32(len);
            dwriter.WriteString(message);
            await dwriter.StoreAsync();
            await dwriter.FlushAsync();
        }
        public void WriteToDevice(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                SendString(message); 
            }
        }

        public bool Connected { get; }
    }
}
