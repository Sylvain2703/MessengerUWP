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


var Navigation = (function () {
    var self = {};

    var navManager = null;
    var stack = [];

    /**
     * Notify that we navigate to another page and specify an action to execute on pop.
     * The shouldShowBackButtonFunc is an optional function that should return if the back button need to be visible.
     */
    self.pushToStack = function (popCallback, shouldShowBackButtonFunc) {
        stack.push({ popCallback: popCallback, shouldShowBackButtonFunc: shouldShowBackButtonFunc });
        self.updateBackButtonVisibility();
    };

    /**
     * Trigger the action to execute on pop.
     */
    self.popFromStack = function () {
        var popped = stack.pop();
        if (popped) {
            popped.popCallback();
            self.updateBackButtonVisibility();
            return true;
        }
        return false;
    };

    /**
     * Update the Windows back button visibility.
     */
    self.updateBackButtonVisibility = function () {
        if (navManager) {
            var total = stack.length;
            for (var i = 0; i < stack.length; i++) {
                var item = stack[i];
                if (item.shouldShowBackButtonFunc && item.shouldShowBackButtonFunc() === false) {
                    total--;
                }
            }

            navManager.appViewBackButtonVisibility = total > 0 ?
                Windows.UI.Core.AppViewBackButtonVisibility.visible :
                Windows.UI.Core.AppViewBackButtonVisibility.collapsed;
        }
    };

    // Listen Windows back events
    if (window.Windows && Windows.UI.Core) {
        navManager = Windows.UI.Core.SystemNavigationManager.getForCurrentView();
        navManager.addEventListener("backrequested", function (event) {
            event.handled = self.popFromStack();
        });
    }

    return self;
}());