using System;
using System.Collections.Generic;
using System.Text;

namespace ApaczTank.Interfaces
{
    public interface IMessageService
    {
        void DisplayMessage(string msg);

        void DisplayMessageAsync(string msg);
    }
}
