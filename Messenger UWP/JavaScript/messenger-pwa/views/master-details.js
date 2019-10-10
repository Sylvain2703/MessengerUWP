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


MessengerPWA.Views.MasterDetails = (function () {
    var self = {};

    var isDetailsView = false;
    var container = DOM.getByClass(MessengerPWA.Selectors.MASTER_DETAILS_CONTAINER);
    var root = DOM.getByClass(MessengerPWA.Selectors.ROOT_CONTAINER);
    var rootComponent = null;

    /**
     * Update the displayed views/panels.
     */
    function updateViews() {
        if (window.innerWidth < 700) {
            if (isDetailsView) {
                container.classList.remove("master");
                container.classList.add("details");
            }
            else {
                container.classList.add("master");
                container.classList.remove("details");
            }
        }
        else {
            container.classList.remove("master");
            container.classList.remove("details");
        }
    }

    /**
     * Clear the details view content.
     * Based on a similar code for removing a conversation.
     */
    function clearDetailsView() {
        rootComponent.setState(function (prevState) {
            prevState.reasonState.activeThreadID = null;
            prevState.reasonState.detailView = null;
            prevState.reasonState.serverThreadID = null;
            return prevState;
        }, function () {
            window.history.pushState(null, "", "/");
        });
    }

    /**
     * Show the master view as the main one.
     */
    self.showMasterAsMainView = function () {
        isDetailsView = false;
        clearDetailsView();
        updateViews();
    };

    /**
     * Show the details view as the main one.
     */
    self.showDetailsAsMainView = function () {
        isDetailsView = true;
        updateViews();
    };

    /**
     * Gets if the details view is the main one or not.
     */
    self.isDetailsTheMainView = function () {
        return isDetailsView;
    };


    if (root && container) {
        rootComponent = ReactInternal.findClosestComponent(root);
        self.showMasterAsMainView();

        window.addEventListener("resize", updateViews, false);
    }

    return self;
})();