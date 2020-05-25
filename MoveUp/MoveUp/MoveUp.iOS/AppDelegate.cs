using System;
using System.Collections.Generic;
using System.Linq;
using AppSearch;
using Foundation;
using MoveUp.Installers;
using MoveUp.iOS.Installers;
using UIKit;

namespace MoveUp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public UIApplicationShortcutItem LaunchedShortcutItem { get; set; }
        public int StartupPage { get; set; } = 0;

        public bool HandleShortcutItem(UIApplicationShortcutItem shortcutItem)
        {
            var handled = false;

            // Anything to process?
            if (shortcutItem == null) return false;

            // Take action based on the shortcut type
            switch (shortcutItem.Type)
            {
                case ShortcutIdentifier.First:
                    StartupPage = 1;
                    handled = true;
                    break;
                case ShortcutIdentifier.Second:
                    StartupPage = 2;
                    handled = true;
                    break;
                case ShortcutIdentifier.Third:
                    StartupPage = 3;
                    handled = true;
                    break;
            }

            // Return results
            return handled;
        }

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            var shouldPerformAdditionalDelegateHandling = true;

            // Get possible shortcut item
            if (options != null)
            {
                LaunchedShortcutItem = options[UIApplication.LaunchOptionsShortcutItemKey] as UIApplicationShortcutItem;
                shouldPerformAdditionalDelegateHandling = (LaunchedShortcutItem == null);
            }
            if (LaunchedShortcutItem != null)
            {
                HandleShortcutItem(LaunchedShortcutItem);

                // Clear shortcut after it's been handled
                LaunchedShortcutItem = null;
            }

            Xamarin.FormsMaps.Init();
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App(new List<IInstaller>() { new iOSInstaller() }, StartupPage));

            var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
                                       UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null);

            app.RegisterUserNotificationSettings(notificationSettings);

            base.FinishedLaunching(app, options);

            return shouldPerformAdditionalDelegateHandling;
        }

        public override void OnActivated(UIApplication application)
        {
            // Handle any shortcut item being selected
            HandleShortcutItem(LaunchedShortcutItem);

            // Clear shortcut after it's been handled
            LaunchedShortcutItem = null;
        }

        public override void PerformActionForShortcutItem(UIApplication application, UIApplicationShortcutItem shortcutItem, UIOperationHandler completionHandler)
        {
            // Perform action
            completionHandler(HandleShortcutItem(shortcutItem));
        }
    }
}
