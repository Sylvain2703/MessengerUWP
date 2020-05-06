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


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace Messenger.UWP.Helpers
{
    public class WebViewInjector
    {
        // JavaScript code used to import a CSS file
        private const string cssInjector = @"
var head = document.getElementsByTagName('head')[0];
function addCss(href) {
    var link = document.createElement('link');
    link.rel = 'stylesheet';
    link.type = 'text/css';
    link.href = href;
    head.appendChild(link);
}";

        // CSS and JavaScript files names
        private List<string> cssFiles = new List<string>();
        private List<string> javascriptFiles = new List<string>();

        // JavaScript files names and their contents (to avoid unnecessary reloading)
        private Dictionary<string, string> javascriptCache = new Dictionary<string, string>();

        public void AddCss(string source)
        {
            cssFiles.Add("ms-appx-web://" + source);
        }
        public void AddJavaScript(string source)
        {
            javascriptFiles.Add("ms-appx://" + source);
        }

        public async Task InjectAsync(WebView webView)
        {
            if (cssFiles.Count < 1 && javascriptFiles.Count < 1)
                return;

            string js = cssInjector;
            foreach (var cssFile in cssFiles)
                js += "\naddCss('" + cssFile + "');";

            await GetAllJavaScriptAsync();
            foreach (var javascriptContent in javascriptCache)
                js += "\n" + javascriptContent.Value;

            await webView.InvokeScriptAsync("eval", new string[] { js });
        }

        private async Task GetAllJavaScriptAsync()
        {
            foreach (string javascriptFile in javascriptFiles)
            {
                if (!javascriptCache.ContainsKey(javascriptFile))
                    await GetFileContentAsync(javascriptFile);
            }
        }

        private async Task GetFileContentAsync(string source)
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(source));
            var content = await FileIO.ReadTextAsync(file);
            javascriptCache.Add(source, content);
        }
    }
}
