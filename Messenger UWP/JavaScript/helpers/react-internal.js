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
 * ReactInternal provides functions to interact with React components from pure JavaScript at the runtime.
 */
var ReactInternal = (function () {
    var self = {};

    /**
     * Gets the internal React instance of a DOM node, if it exists.
     */
    function getInternalInstance(domNode) {
        var keys = Object.keys(domNode);
        for (var keyIndex in keys) {
            var key = keys[keyIndex];
            if (key.startsWith("__reactInternalInstance$")) {
                return domNode[key];
            }
        }
        return null;
    }

    /**
     * Find the associated Component instance of a DOM node, if it exists, else return null.
     */
    self.findComponent = function (domNode) {
        var instance = getInternalInstance(domNode);
        if (instance && instance.return && !(instance.return.stateNode instanceof Node))
            return instance.return.stateNode;

        return null;
    };

    /**
     * Find the associated Component instance of a DOM node, if it exists.
     * Else, try to find a parent Component instance.
     * If it reaches the root without finding anything, return null.
     */
    self.findClosestComponent = function (domNode) {
        var instance = getInternalInstance(domNode);
        if (instance) {
            while (instance.return && (!instance.return.stateNode || instance.return.stateNode instanceof Node)) {
                instance = instance.return;
            }
            if (instance.return)
                return instance.return.stateNode;
        }

        return null;
    };

    return self;
})();