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


using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Messenger.UWP
{
    public sealed partial class MainPage : Page
    {
        private WebView webView;
        private WebViewInjector webViewInjector;

        public MainPage()
        {
            InitializeComponent();
            InitializeWebView();
        }

        private void InitializeWebView()
        {
            webViewInjector = new WebViewInjector();
            webViewInjector.AddCss("/Web/bundle.css");
            webViewInjector.AddJavaScript("/Web/bundle.js");

            webView = new WebView(WebViewExecutionMode.SameThread);
            webView.NavigationStarting += WebViewNavigationStarting;
            webView.NavigationFailed += WebViewNavigationFailed;
            webView.NavigationCompleted += WebViewNavigationCompleted;
            webView.Source = new Uri("https://www.messenger.com/login");
            Container.Children.Insert(0, webView);
        }

        private void WebViewNavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            Progress.IsActive = true;
            webView.Visibility = Visibility.Collapsed;
        }

        private void WebViewNavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            // TODO: handle navigation failed
        }

        private async void WebViewNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (args.IsSuccess)
            {
                await webViewInjector.InjectAsync(sender);
                Progress.IsActive = false;
                webView.Visibility = Visibility.Visible;
            }
        }
    }
}
