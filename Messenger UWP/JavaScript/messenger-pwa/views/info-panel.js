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


MessengerPWA.Views.InfoPanel = (function () {
    var self = {};

    /**
     * Gets if the info panel is visible or not.
     */
    self.isVisible = function () {
        var panel = DOM.getByClass(MessengerPWA.Selectors.INFO_PANEL);
        return Boolean(panel && !panel.classList.contains("hidden_elem"));
    };

    /**
     * Change the info panel visibily. Show it if hidden and hide it if shown.
     */
    self.toggleVisibility = function () {
        var btn = DOM.getBySelector(MessengerPWA.Selectors.INFO_PANEL_BUTTON);
        if (btn)
            btn.click();
    };

    /**
     * Show the info panel.
     */
    self.show = function () {
        if (!self.isVisible())
            self.toggleVisibility();
    };

    /**
     * Hide the info panel.
     */
    self.hide = function () {
        if (self.isVisible())
            self.toggleVisibility();
    };

    // Subscribe to clicks on info panel button.
    Dispatcher.Click.subscribe(MessengerPWA.Selectors.INFO_PANEL_BUTTON, function () {
        Navigation.pushToStack(self.hide);
    }, true);

    // Subscribe to clicks outside the info panel content.
    Dispatcher.Click.subscribe("." + MessengerPWA.Selectors.INFO_PANEL, function () {
        Navigation.popFromStack();
    }, false, "." + MessengerPWA.Selectors.INFO_PANEL_CONTENT);

    // Subscribe to clicks on search button.
    Dispatcher.Click.subscribe(MessengerPWA.Selectors.INFO_PANEL_SEARCH_BUTTON, function () {
        Navigation.popFromStack();
    });

    return self;
})();