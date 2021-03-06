﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using OpenSimBot.OMVWrapper.Utility;
using OpenSimBot.OMVWrapper.Manager;

namespace OpenSimBot.BotFramework
{
    class ConfigAgent : Singleton<ConfigAgent>
    {
        /*Members**************************************************************/
        public const int BOT_CONFIG_MAXNUM = 100;
        public const string BOT_SCRIPTES_DIR = "./";
        private BotSessionMgr.BotSession m_currentSess = null;

        /*Functions************************************************************/
        public string GetScriptsDirectory()
        {
            return BOT_SCRIPTES_DIR;
        }

        public string[] GetScriptsNameList()
        {
            string dir = GetScriptsDirectory();
            if (!string.IsNullOrEmpty(dir))
            {
                return Directory.GetFiles(dir, "*.lua");
            }

            return null;
        }
    }
}
