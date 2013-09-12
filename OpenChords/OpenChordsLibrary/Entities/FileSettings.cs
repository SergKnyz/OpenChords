﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace OpenChords.Entities
{
    [XmlRoot("settings")]
    public class FileAndFolderSettings
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string AppsFolder {get; set;}
        //public bool isLocalOpenSong { get; set; }
        public string OpenSongExecutable { get; set; }
        public string OpenSongSetsAndSongs { get; set; }
        public bool OpenSongUseWelcomeSlide { get; set; }
        public string OpenSongWelcomeSlide { get; set; }
        public bool PortableMode { get; set; }
        public string ApplicationDataFolder { get; set; }
        public string FileSync { get; set; }
        public bool CheckForUpdates { get; set; }

        public string _SettingsFolder;

        //we need to be able to cator for non portable mode
        [XmlIgnore]
        public string SettingsFolder
        {
            get
            {
                if (!PortableMode)
                    return Application.UserAppDataPath + "\\";
                else
                    return _SettingsFolder;
            }
            set 
            {
                _SettingsFolder = value;
            }
        }


        
        public static FileAndFolderSettings loadSettings()
        {
            var settings = IO.XmlReaderWriter.readFileAndFolderSettings();
            var dir = new DirectoryInfo(@".\");
            settings.CurrentPath = dir.FullName;
            return settings;
        }

        /// <summary>
        /// load settings with specified path
        /// </summary>
        /// <returns></returns>
        public static FileAndFolderSettings loadSettings(string path)
        {        
            var settings = IO.XmlReaderWriter.readFileAndFolderSettings(path);
            var dir = new DirectoryInfo(path);    
            settings.CurrentPath = dir.Parent.FullName + "\\";
            return settings;
        }


        public string CurrentPath { get; protected set; }

        public void saveSettings()
        {
            IO.XmlReaderWriter.writeFileAndFolderSettings(this);

        }

        public FileAndFolderSettings()
        {
            AppsFolder =  "..\\..\\OpenChordsApps\\";
            _SettingsFolder = "..\\..\\OpenChordsSettings\\";
            OpenSongExecutable = "c:\\Program Files\\OpenSong\\OpenSong.exe";
            OpenSongSetsAndSongs = "c:\\OpenSong";
            OpenSongUseWelcomeSlide = false;
            OpenSongWelcomeSlide = "Welcome";
            PortableMode = true;
            ApplicationDataFolder = "..\\Data";
            FileSync = "";
            CheckForUpdates = true;
        }





        internal void refresh()
        {
            throw new NotImplementedException();
        }
    }
}
