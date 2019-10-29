# Messenger UWP (alpha)

[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](/COPYING)
[![Build Status](https://dev.azure.com/ssoft-org/MessengerUWP/_apis/build/status/Messenger%20UWP%20CI?branchName=develop)](https://dev.azure.com/ssoft-org/MessengerUWP/_build/latest?branchName=develop)

Messenger UWP is a Universal Windows Platform app that lets you access **Facebook Messenger on Windows 10 PCs, tablets and phones**.

This app is based on the Messenger website ([www.messenger.com](https://www.messenger.com)) by using a WebView and injecting some JavaScript and CSS code.

Because Facebook provides at least two versions of [www.messenger.com](https://www.messenger.com), we also have two know versions. Currently, we don't know why some users have access to Messenger v1 and some others to Messenger v2 (probably AB testing).

|  Messenger iOS style  | Messenger Android style |
|:---------------------:|:-----------------------:|
| Version 1 in this app |  Version 2 in this app  |
| ![MessengerV1](/Docs/MessengerV1.gif) | ![MessengerV2](/Docs/MessengerV2.gif) |


## Join the preview program 🐱‍💻

You can join the preview program and **easily install Messenger UWP** from the Microsoft Store.  
Use this [**form**](https://forms.gle/pnGc9xBpZ8snPG6d9) to apply for the preview program and you will soon be able to download the app. Please note that you need at least Windows 10 Anniversary Update (1607) to run Messenger UWP.  

Don't forget to **report bugs and send feedbacks** on the GitHub issues page.  
We really need feedback as there is probably some difference with [www.messenger.com](https://www.messenger.com) between countries.


## Features

All the following features are adapted to run on Windows 10 Desktop and Windows 10 Mobile. Other features can also works but aren't fully tested.

- Send and receive text messages, attachments, stickers, reactions and GIFs
- Group and bot conversations
- See who is online and which messages have been read
- Customize chat colors and nicknames
- Native back button support
- Continuum support


## Roadmap

- Dark theme
- Acrylic effect (part of Fluent Design)
- Notifications + Live Tiles + Badges (complicated)
- Send voice messages (to test)
- Calls and video chats + in group (to test)
- Take photos and record videos (to test)
- Play games with friends (to test)
- Add filters, masks and effects to your video chats (complicated)
- Watch Stories and add your own (complicated)
- Contact anyone without Facebook account
- Send payments
- Share your location
- Workplace Chat support
- Xbox support


## Known issues

- Reactions, reply and forward to messages are not touch-friendly.
- In a thread, when a picture is too large, its preview isn't clipped away nicely.
- When the informations panel of a thread is opened, keyboard navigation can cause problems.


## Installation

For all users, join the preview program with the link above.

For developers:
- Install [Visual Studio 2019](https://developer.microsoft.com/en-us/windows/downloads) (VS 2017 should works too).
  - Install the "Universal Windows Platform Development" workload.
  - Install the latest Windows 10 SDK.
- Install the [Bundler & Minifier](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.BundlerMinifier) Visual Studio extension.
- Clone the code repository:  
`git clone https://github.com/Sylvain2703/MessengerUWP.git`
- Open [Messenger UWP.sln](/Messenger%20UWP.sln) with Visual Studio.


## Changelog

- Version 0.2.0:
  - Improve dialogs, settings and chatbot cards for small screens.
  - Improve navigation between the master and detail view: navigation should work in all cases.
  - Fix an issue where, on Messenger v2, a part of the back button was unexpectedly displayed (on devices with EdgeHTML < 16).
- Version 0.1.0: Initial alpha version.


## License

Copyright (C) 2019 Sylvain Bruyère

This repository is licensed with the [GNU General Public License v3.0 only (GPL-3.0-only)](/COPYING).