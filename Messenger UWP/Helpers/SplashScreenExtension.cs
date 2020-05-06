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


using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace Messenger.UWP.Helpers
{
    public static class SplashScreenExtension
    {
        public static Rect GetFixedImageLocation(this SplashScreen splashScreen)
        {
            if (App.IsWindowsMobile)
            {
                const double splashScreenRatio = 620d / 300d;
                var screenWidth = Window.Current.Bounds.Width;
                var screenHeight = Window.Current.Bounds.Height;

                double width;
                double height;

                bool isContinuum = UIViewSettings.GetForCurrentView().UserInteractionMode == UserInteractionMode.Mouse;
                if (isContinuum)
                {
                    height = 200;
                    width = height * splashScreenRatio;
                }
                else
                {
                    var orientations = DisplayInformation.GetForCurrentView().CurrentOrientation;
                    var isPortrait = orientations == DisplayOrientations.Portrait || orientations == DisplayOrientations.PortraitFlipped;

                    width = isPortrait ? screenWidth : screenHeight;
                    height = width / splashScreenRatio;
                }

                return new Rect(
                    (screenWidth - width) / 2,
                    (screenHeight - height) / 2,
                    width, height);
            }

            return splashScreen.ImageLocation;
        }
    }
}
