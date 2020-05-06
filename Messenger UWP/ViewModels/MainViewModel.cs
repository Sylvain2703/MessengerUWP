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

namespace Messenger.UWP.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private const string MessengerLoginURL = "https://www.messenger.com/login";
        private const string MessengerPasswordURL = "messenger.com/login/password";

        private Action<Uri> navigate;

        public MainViewModel(Action<Uri> navigate)
        {
            this.navigate = navigate;
            this.navigate(new Uri(MessengerLoginURL));
        }

        #region Properties

        private NavigationState state = NavigationState.Loading;
        public NavigationState State
        {
            get { return state; }
            set
            {
                if (SetProperty(ref state, value))
                {
                    OnPropertyChanged("IsLoading");
                    OnPropertyChanged("HasFailed");
                    OnPropertyChanged("ShowIcon");
                }
            }
        }

        public bool IsLoading => State == NavigationState.Loading;

        public bool HasFailed => State == NavigationState.Failed;

        public bool ShowIcon => State != NavigationState.Succeeded;

        #endregion

        public void Reload(Uri currentUrl)
        {
            // If navigation fails after submitting the user credentials, we have to retry from the login page.
            if (currentUrl.OriginalString.Contains(MessengerPasswordURL))
                currentUrl = new Uri(MessengerLoginURL);

            navigate(currentUrl);
        }
    }

    public enum NavigationState
    {
        Loading,
        Failed,
        Succeeded
    }
}