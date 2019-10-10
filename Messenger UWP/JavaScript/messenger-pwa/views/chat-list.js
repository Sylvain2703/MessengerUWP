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


MessengerPWA.Views.ChatList = (function () {
    var self = {};

    function onItemClicked() {
        // Check if we were already in details view.
        var isInDetails = MessengerPWA.Views.MasterDetails.isDetailsTheMainView();
        if (!isInDetails) {
            // Show details view and ensure the info panel is closed.
            MessengerPWA.Views.MasterDetails.showDetailsAsMainView();
            MessengerPWA.Views.InfoPanel.hide();

            // Update the navigation stack to update the back button.
            Navigation.pushToStack(MessengerPWA.Views.MasterDetails.showMasterAsMainView);
        }
    }

    // Subscribe to clicks on a chat item.
    Dispatcher.Click.subscribe("." + MessengerPWA.Selectors.CHAT_ITEM, onItemClicked);

    // Subscribe to clicks on a search result item.
    Dispatcher.Click.subscribe(MessengerPWA.Selectors.SEARCH_RESULT_ITEM, onItemClicked, true);

    return self;
})();