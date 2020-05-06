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
using Windows.UI.Xaml.Data;

namespace Messenger.UWP.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public BooleanToVisibilityMode Mode { get; set; } = BooleanToVisibilityMode.TrueIsVisible;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool boolValue)
            {
                if (Mode == BooleanToVisibilityMode.TrueIsVisible)
                    return boolValue ? Visibility.Visible : Visibility.Collapsed;
                else
                    return boolValue ? Visibility.Collapsed : Visibility.Visible;
            }

            throw new ArgumentException("Invalid value type in BooleanToVisibilityConverter: expects a boolean instead of a " + value.GetType());
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility visibilityValue)
            {
                if (Mode == BooleanToVisibilityMode.TrueIsVisible)
                    return visibilityValue == Visibility.Visible;
                else
                    return visibilityValue == Visibility.Collapsed;
            }

            throw new ArgumentException("Invalid value type in BooleanToVisibilityConverter: expects a Visibility instead of a " + value.GetType());
        }
    }

    public enum BooleanToVisibilityMode
    {
        TrueIsVisible,
        TrueIsCollapsed
    }
}