using PushNotification.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PushNotification.Plugin.Abstractions;

namespace ALFCConnect.Helpers
{
    public class CrossPushNotificationListner : IPushNotificationListener
    {
        public void OnError(string message, DeviceType deviceType)
        {
            throw new NotImplementedException();
        }

        public void OnMessage(JObject values, DeviceType deviceType)
        {
            throw new NotImplementedException();
        }

        public void OnRegistered(string token, DeviceType deviceType)
        {
            throw new NotImplementedException();
        }

        public void OnUnregistered(DeviceType deviceType)
        {
            throw new NotImplementedException();
        }

        public bool ShouldShowNotification()
        {
            throw new NotImplementedException();
        }
    }
}
