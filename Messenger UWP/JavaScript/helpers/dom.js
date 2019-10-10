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


var DOM = {};

DOM.getById = function (id) {
    return document.getElementById(id);
};

DOM.getByClass = function (name) {
    return document.getElementsByClassName(name)[0];
};
DOM.getAllByClass = function (name) {
    return document.getElementsByClassName(name);
};

DOM.getByTag = function (tag) {
    return document.getElementsByTagName(tag)[0];
};
DOM.getAllByTag = function (tag) {
    return document.getElementsByTagName(tag);
};

DOM.getBySelector = function (selector) {
    return document.querySelector(selector);
};
DOM.getAllBySelector = function (selector) {
    return document.querySelectorAll(selector);
};