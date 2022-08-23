﻿using Files.Uwp.Interacts;
using Microsoft.Toolkit.Uwp;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;

namespace Files.Uwp.Helpers
{
    public static class DriveHelpers
    {
        // Eject using Shell Verb (fixes #6072, #6439)
        public static async Task EjectDeviceAsync(string path)
        {
            var connection = await AppServiceConnectionHelper.Instance;
            if (connection != null)
            {
                await connection.SendMessageAsync(new ValueSet()
                {
                    { "Arguments", "InvokeVerb" },
                    { "FilePath", path },
                    { "Verb", "eject" }
                });
            }
        }

        // Eject using DeviceIoControl
        /*public static async Task EjectDeviceAsync(string path)
        {
            var removableDevice = new RemovableDevice(path);
            bool result = await removableDevice.EjectAsync();
            if (result)
            {
                Debug.WriteLine("Device successfully ejected");

                var toastContent = new ToastContent()
                {
                    Visual = new ToastVisual()
                    {
                        BindingGeneric = new ToastBindingGeneric()
                        {
                            Children =
                            {
                                new AdaptiveText()
                                {
                                    Text = "EjectNotificationHeader".GetLocalized()
                                },
                                new AdaptiveText()
                                {
                                    Text = "EjectNotificationBody".GetLocalized()
                                }
                            },
                            Attribution = new ToastGenericAttributionText()
                            {
                                Text = "SettingsAboutAppName".GetLocalized()
                            }
                        }
                    },
                    ActivationType = ToastActivationType.Protocol
                };

                // Create the toast notification
                var toastNotif = new ToastNotification(toastContent.GetXml());

                // And send the notification
                ToastNotificationManager.CreateToastNotifier().Show(toastNotif);
            }
            else
            {
                Debug.WriteLine("Can't eject device");

                await DialogDisplayHelper.ShowDialogAsync(
                    "EjectNotificationErrorDialogHeader".GetLocalized(),
                    "EjectNotificationErrorDialogBody".GetLocalized());
            }
        }*/
    }
}