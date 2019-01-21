using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ApaczTank.Interfaces;

namespace ApaczTank.Droid
{
    public class MessageService : IMessageService
    {
        public void DisplayMessage(string msg)
        {
            Toast.MakeText(Application.Context, msg, ToastLength.Short).Show();
        }

        public void DisplayMessageAsync(string msg)
        {
            throw new NotImplementedException();
        }
    }
}