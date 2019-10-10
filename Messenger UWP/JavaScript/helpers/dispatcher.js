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


/**
 * Dispatcher helps to handle events from the global (window) context applied to specific DOM elements.
 * Use it when we can't safely add listeners on DOM elements (like with React, Angular or Vue projects,
 * as the DOM can drastically change).
 */
var Dispatcher = {};

/**
 * Listen and dispatch click events to subscribers.
 */
Dispatcher.Click = (function () {
    var self = {};

    var bubblingSubscribers = [];
    var capturingSubscribers = [];

    /**
     * Subscribes to clicks on elements that matches to a selector (and don't matches the
     * exclude selector, if specified).
     * @param useCapture refers to the dispatch order as described here :
     * https://developer.mozilla.org/en-US/docs/Web/API/EventTarget/addEventListener#Syntax
     */
    self.subscribe = function (selector, callback, useCapture, excludeSelector) {
        var obj = { selector: selector, excludeSelector: excludeSelector, callback: callback };
        if (useCapture)
            capturingSubscribers.push(obj);
        else
            bubblingSubscribers.push(obj);
    };

    /**
     * Checks if the click matches a subscribed selector.
     */
    function checkSubscribers(event, subscribers) {
        if (!event.isTrusted)
            return;

        for (var i = 0; i < subscribers.length; i++) {
            var subscriber = subscribers[i];
            if (event.target.closest(subscriber.selector)) {
                var exclude = subscriber.excludeSelector && event.target.closest(subscriber.excludeSelector);
                if (!exclude)
                    subscriber.callback();
            }
        }
    }

    // Listen every clicks on the window by bubbling.
    window.addEventListener("click", function (event) {
        checkSubscribers(event, bubblingSubscribers);
    }, false);

    // Listen every clicks on the window by capturing.
    window.addEventListener("click", function (event) {
        checkSubscribers(event, capturingSubscribers);
    }, true);

    return self;
})();