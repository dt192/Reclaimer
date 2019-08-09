﻿using Studio.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Reclaimer.Plugins
{
    public class MapViewerPlugin : Plugin
    {
        const string OpenKey = "MapViewer.OpenMap";
        const string OpenPath = "File\\Open Map";

        internal static MapViewerSettings Settings;

        public override string Name => "Map Viewer";

        public override void Initialise()
        {
            Settings = LoadSettings<MapViewerSettings>();
        }

        public override void Suspend()
        {
            SaveSettings(Settings);
        }

        public override IEnumerable<PluginMenuItem> GetMenuItems()
        {
            yield return new PluginMenuItem(OpenKey, OpenPath, OnMenuItemClick);
        }

        private void OnMenuItemClick(string key)
        {
            if (key != OpenKey) return;

            var ofd = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Halo Map Files|*.map",
                Multiselect = true,
                CheckFileExists = true
            };

            if (!string.IsNullOrEmpty(Settings.MapFolder))
                ofd.InitialDirectory = Settings.MapFolder;

            if (ofd.ShowDialog() != true)
                return;

            foreach (var fileName in ofd.FileNames)
                OpenPhysicalFile(fileName);
        }

        public override bool SupportsFileExtension(string extension)
        {
            return extension.ToLower() == "map";
        }

        public override void OpenPhysicalFile(string fileName)
        {
            LogOutput($"Loading map file: {fileName}");

            var mv = new Controls.MapViewer();
            mv.LoadMap(fileName);
            Substrate.AddUtility(mv, Substrate.GetHostWindow(), Dock.Left, new GridLength(400));

            LogOutput($"Loaded map file: {fileName}");
        }
    }

    internal class MapViewerSettings
    {
        public string MapFolder { get; set; }
        public bool HierarchyView { get; set; }
    }
}