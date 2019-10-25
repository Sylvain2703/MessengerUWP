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


(function () {
    // Set the viewport for mobile devices.
    var meta = document.createElement("meta");
    meta.name = "viewport";
    meta.content = "width=device-width, initial-scale=1";
    DOM.getByTag("head").appendChild(meta);

    // As we change the user agent, we need to ensure that the CSS fixes for Edge are applied.
    var bodyClassList = document.body.classList;
    if (!bodyClassList.contains("edge"))
        bodyClassList.add("edge");
})();

var MessengerPWA = {};
MessengerPWA.Views = {};