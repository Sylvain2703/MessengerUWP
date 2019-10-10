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


MessengerPWA.Version = (function () {
    var masterView = DOM.getByClass(MessengerPWA.Selectors.MASTER_VIEW);
    if (masterView) {
        var messengerV2 = masterView.classList.contains(MessengerPWA.Selectors.MASTER_VIEW_V2);
        return messengerV2 ? 2 : 1;
    }

    return null;
})();