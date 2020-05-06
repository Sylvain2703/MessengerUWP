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


using Messenger.UWP.Converters;
using Messenger.UWP.Helpers;
using Messenger.UWP.ViewModels;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Messenger.UWP
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; }

        private WebView webView;
        private WebViewInjector webViewInjector;

        public MainPage()
        {
            InitializeComponent();
            InitializeBackground();
            InitializeWebView();

            Canvas.SizeChanged += UpdateSplashScreenImageLocation;

            var appView = ApplicationView.GetForCurrentView();
            appView.SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
            appView.VisibleBoundsChanged += UpdateContainerLocation;

            ViewModel = new MainViewModel(webView.Navigate);
            DataContext = ViewModel;
        }

        private void InitializeBackground()
        {
            if (App.IsAcrylicAvailable)
            {
                var acrylicBrush = new AcrylicBrush()
                {
                    BackgroundSource = AcrylicBackgroundSource.HostBackdrop,
                    TintColor = Colors.White,
                    FallbackColor = Colors.White,
                    TintOpacity = 0.8,
                };

                BindingOperations.SetBinding(acrylicBrush, AcrylicBrush.AlwaysUseFallbackProperty,
                    new Binding() { Path = new PropertyPath("ShowIcon") });

                Background = acrylicBrush;
            }
            else
            {
                Background = new SolidColorBrush(Colors.White);
            }
        }

        private void InitializeWebView()
        {
            webViewInjector = new WebViewInjector();
            webViewInjector.AddCss("/Web/bundle.css");
            webViewInjector.AddJavaScript("/Web/bundle.js");

            webView = new WebView(WebViewExecutionMode.SameThread);
            webView.DefaultBackgroundColor = Colors.Transparent;
            webView.NavigationStarting += WebViewNavigationStarting;
            webView.NavigationCompleted += WebViewNavigationCompleted;

            Grid.SetRowSpan(webView, 2);
            webView.SetBinding(WebView.VisibilityProperty, new Binding
            {
                Path = new PropertyPath("ShowIcon"),
                Converter = new BooleanToVisibilityConverter { Mode = BooleanToVisibilityMode.TrueIsCollapsed }
            });

            Container.Children.Insert(0, webView);
        }

        private void WebViewNavigationStarting(WebView sender, WebViewNavigationStartingEventArgs e)
        {
            if (ViewModel != null)
                ViewModel.State = NavigationState.Loading;
        }

        private async void WebViewNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
                await webViewInjector.InjectAsync(sender);

            if (ViewModel != null)
                ViewModel.State = e.IsSuccess ? NavigationState.Succeeded : NavigationState.Failed;
        }

        private void RetryClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Reload(webView.Source);
        }

        private void UpdateSplashScreenImageLocation(object sender, SizeChangedEventArgs e)
        {
            var imageLocation = App.SplashScreen.GetFixedImageLocation();

            Canvas.SetLeft(SplashScreenImage, imageLocation.X);
            Canvas.SetTop(SplashScreenImage, imageLocation.Y);
            SplashScreenImage.Height = imageLocation.Height;
            SplashScreenImage.Width = imageLocation.Width;

            UpdateContainerLocation(ApplicationView.GetForCurrentView());
        }

        private void UpdateContainerLocation(ApplicationView sender, object args = null)
        {
            var visibleBounds = sender.VisibleBounds;

            if (App.IsWindowsMobile)
            {
                var isNotContinuum = UIViewSettings.GetForCurrentView().UserInteractionMode != UserInteractionMode.Mouse;
                if (isNotContinuum)
                    Canvas.SetLeft(Container, visibleBounds.X);
                Canvas.SetTop(Container, visibleBounds.Y);
            }
            Container.Height = visibleBounds.Height;
            Container.Width = visibleBounds.Width;
        }
    }
}
