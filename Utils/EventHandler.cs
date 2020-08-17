﻿using ToasterApi.Models;

namespace ToasterApi.Utils
{
    public static class EventHandler
    {
        public static void HandleToasterStateChange(Toaster oldToaster, Toaster newToaster)
        {
            if (oldToaster.On != newToaster.On) {
                if (oldToaster.On == true)
                {
                    EventLogger.Log($"Toaster turned off.");
                }
                else {
                    EventLogger.Log($"Toaster turned on, Heat Level is {newToaster.Heat}");
                }
            }
        }

        public static void HandleFrontEndRequest(string toaster) {

            EventLogger.Log($"heat/time requested.");
        }

    }
}
