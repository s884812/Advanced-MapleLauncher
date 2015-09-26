using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Launcher Created By: Torban
    RaGEZONE Profile: https://forum.ragezone.com/members/2000184932.html

    Please do not remove the credits.
*/

namespace Launcher
{
    class Settings
    {
        private static string _xmlURL = "http://linktoyourxml.com/launcher.xml"; // The URL to the XML file you uploaded.

        // --- DO NOT EDIT THE BOTTOM CODE ---

        public static string xmlURL { get { return _xmlURL; } set { _xmlURL = value; } }

        private static string _clientName = "";
        public static string clientName { get { return _clientName; } set { _clientName = value; } }

        private static string _websiteURL = "";
        public static string websiteURL { get { return _websiteURL; } set { _websiteURL = value; } }

        private static string _forumURL = "";
        public static string forumURL { get { return _forumURL; } set { _forumURL = value; } }

        private static string _voteURL = "";
        public static string voteURL { get { return _voteURL; } set { _voteURL = value; } }
    }
}
