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


MessengerPWA.Views.MasterDetail = (function () {
    var self = {};

    var container = DOM.getByClass(MessengerPWA.Selectors.MASTER_DETAIL_CONTAINER);
    var root = DOM.getByClass(MessengerPWA.Selectors.ROOT_CONTAINER);
    var rootComponent = null;

    /**
     * Gets if the detail view has content or not.
     */
    self.hasDetailView = function () {
        return rootComponent.state.reasonState.detailView != null;
    };

    /**
     * Clears the detail view content.
     * Based on a similar code for removing a conversation.
     */
    function clearDetailView() {
        rootComponent.setState(function (prevState) {
            prevState.reasonState.activeThreadID = null;
            prevState.reasonState.detailView = null;
            prevState.reasonState.serverThreadID = null;
            return prevState;
        }, function () {
            var url = getUrlWithoutThread();
            window.history.pushState(url, "", url);
            updateViews();
        });
    }

    /**
     * Gets the current URL without the thread name, if existing.
     */
    function getUrlWithoutThread() {
        var url = window.location.href;
        var index = url.indexOf("/t/");
        if (index > -1)
            url = url.substring(0, index) + "/" + window.location.search;
        return url;
    }

    /**
     * Updates the displayed views.
     */
    function updateViews() {
        if (window.innerWidth < 700) {
            if (self.hasDetailView()) {
                container.classList.remove("master");
                container.classList.add("detail");
            }
            else {
                container.classList.add("master");
                container.classList.remove("detail");
            }
        }
        else {
            container.classList.remove("master");
            container.classList.remove("detail");
        }
    }

    /**
     * Triggered after the "MessengerReact" component update.
     */
    function componentDidUpdate(prevProps, prevState) {
        // If the detail view content appears.
        if (prevState.reasonState.detailView == null && this.state.reasonState.detailView != null) {
            // Ensures the info panel is closed and updates views.
            MessengerPWA.Views.InfoPanel.hide();
            updateViews();

            Navigation.pushToStack(clearDetailView);
        }
        // If the detail view content disappears.
        else if (prevState.reasonState.detailView != null && this.state.reasonState.detailView == null) {
            Navigation.popFromStack();
        }
    }

    if (root && container) {
        // Gets the root "MessengerReact" component.
        rootComponent = ReactInternal.findClosestComponent(root);

        // Intercepts the componentDidUpdate calls to know when the state changes.
        var originalComponentDidUpdate = rootComponent.componentDidUpdate.bind(rootComponent);
        rootComponent.componentDidUpdate = function (prevProps, prevState, snapshot) {
            originalComponentDidUpdate(prevProps, prevState, snapshot);
            componentDidUpdate.call(rootComponent, prevProps, prevState);
        };

        clearDetailView();

        window.addEventListener("resize", updateViews, false);
    }

    return self;
})();