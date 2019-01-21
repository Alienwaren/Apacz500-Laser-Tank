using System;
using System.Collections.Generic;
using System.Text;

namespace ApaczTank.Interfaces
{
    public interface IBluetoothService
    {

        bool IsEnabled { get; }

        bool HasBluetooth { get; }

        List<string> GetBondedDevices();

        bool Connect(string deviceName);

        bool Disconnect();

        string ReadFromDevice(int count);

        void WriteToDevice(string message);

        bool Connected { get; }
    }
}
