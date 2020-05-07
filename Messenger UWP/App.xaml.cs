// This file is part of Messenger UWP.
// Copyright (C) 2019 Sylvain Bruyère
//
// Messenger UWP is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, version 3.
//
// Messenger UWP is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Messenger UWP.  If not, see <https://www.gnu.org/licenses/>.


using Messenger.UWP.Helpers;
using Messenger.UWP.Views;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.System.Profile;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Messenger.UWP
{
    sealed partial class App : Application
    {
        public static readonly bool IsWindowsMobile = AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile";
        public static readonly bool IsAcrylicAvailable = ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.AcrylicBrush");
        public static readonly bool IsStatusBarAvailable = ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar");

        public static SplashScreen SplashScreen = null;

        /// <summary>
        /// Initializes the singleton application object. This is the first line of authored code executed.
        /// </summary>
        public App()
        {
            UserAgentHelper.SetUserAgent(UserAgentHelper.Chrome);

            InitializeComponent();
            Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user. Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(360, 360));
            SetStatusBarColor();

            SplashScreen = e.SplashScreen;

            Frame rootFrame = Window.Current.Content as Frame;

            // Don't repeat app initialization.
            if (rootFrame == null)
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;

                Window.Current.Content = rootFrame;
            }

            if (!e.PrelaunchActivated)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restaured, navigate to the main page with parameters.
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }

                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when navigation to a certain page fails.
        /// </summary>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended. Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: save the app state and stop background tasks
            deferral.Complete();
        }

        private void SetStatusBarColor()
        {
            if (IsStatusBarAvailable)
            {
                var statusBar = StatusBar.GetForCurrentView();
                statusBar.BackgroundOpacity = 1;
                statusBar.BackgroundColor = (Color)Resources["MessengerColor"];
                statusBar.ForegroundColor = Colors.White;
            }
        }
    }
}
