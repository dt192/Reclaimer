﻿using Adjutant.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Endian;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adjutant.Blam.Halo5
{
    public class Module
    {
        internal const int ModuleHeader = 0x64686f6d;

        public string FileName { get; }

        public ModuleType ModuleType => Header.Version;
        public ModuleHeader Header { get; }

        public List<ModuleItem> Items { get; }
        public Dictionary<string, string> Classes { get; }
        public Dictionary<int, ModuleItem> ItemsById { get; }
        public Dictionary<int, string> Strings { get; }
        public List<int> Resources { get; }
        public List<Block> Blocks { get; }

        public long DataAddress { get; }

        public Module(string fileName)
        {
            FileName = fileName;

            using (var reader = CreateReader())
            {
                Header = reader.ReadObject<ModuleHeader>();

                Items = new List<ModuleItem>(Header.ItemCount);
                ItemsById = new Dictionary<int, ModuleItem>();
                for (int i = 0; i < Header.ItemCount; i++)
                {
                    var item = reader.ReadObject<ModuleItem>((int)Header.Version);
                    Items.Add(item);
                    if (item.GlobalTagId != -1)
                        ItemsById.Add(item.GlobalTagId, item);
                }

                var origin = reader.BaseStream.Position;
                Strings = new Dictionary<int, string>();
                while (reader.BaseStream.Position < origin + Header.StringsSize)
                    Strings.Add((int)(reader.BaseStream.Position - origin), reader.ReadNullTerminatedString());

                Resources = new List<int>(Header.ResourceCount);
                for (int i = 0; i < Header.ResourceCount; i++)
                    Resources.Add(reader.ReadInt32());

                Blocks = new List<Block>(Header.BlockCount);
                for (int i = 0; i < Header.BlockCount; i++)
                    Blocks.Add(reader.ReadObject<Block>((int)Header.Version));

                DataAddress = reader.BaseStream.Position;

                Classes = Items.Where(i => i.ClassCode != null)
                    .GroupBy(i => i.ClassCode)
                    .ToDictionary(g => g.Key, g => g.First().ClassName);
            }
        }

        public DependencyReader CreateReader()
        {
            var fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            return CreateReader(fs);
        }

        internal DependencyReader CreateReader(Stream stream)
        {
            var reader = new DependencyReader(stream, ByteOrder.LittleEndian);

            //verify header when reading a module file
            if (stream is FileStream)
            {
                var header = reader.PeekInt32();

                if (header != ModuleHeader)
                    throw Exceptions.NotAValidMapFile(FileName);
            }

            reader.RegisterInstance(this);

            return reader;
        }
    }

    [FixedSize(48, MaxVersion = (int)ModuleType.Halo5Forge)]
    [FixedSize(56, MinVersion = (int)ModuleType.Halo5Forge)]
    public class ModuleHeader
    {
        [Offset(0)]
        public int Head { get; set; }

        [Offset(4)]
        [VersionNumber]
        public ModuleType Version { get; set; }

        [Offset(8)]
        public long ModuleId { get; set; }

        [Offset(16)]
        public int ItemCount { get; set; }

        [Offset(20)]
        public int ManifestCount { get; set; }

        [Offset(24)]
        public int ResourceIndex { get; set; }

        [Offset(28)]
        public int StringsSize { get; set; }

        [Offset(32)]
        public int ResourceCount { get; set; }

        [Offset(36)]
        public int BlockCount { get; set; }
    }

    [FixedSize(20, MaxVersion = (int)ModuleType.Halo5Forge)]
    [FixedSize(32, MinVersion = (int)ModuleType.Halo5Forge)]
    public class Block
    {
        [StoreType(typeof(uint))]
        [Offset(0, MaxVersion = (int)ModuleType.Halo5Forge)]
        [Offset(8, MinVersion = (int)ModuleType.Halo5Forge)]
        public long CompressedOffset { get; set; }

        [StoreType(typeof(uint))]
        [Offset(4, MaxVersion = (int)ModuleType.Halo5Forge)]
        [Offset(12, MinVersion = (int)ModuleType.Halo5Forge)]
        public long CompressedSize { get; set; }

        [StoreType(typeof(uint))]
        [Offset(8, MaxVersion = (int)ModuleType.Halo5Forge)]
        [Offset(16, MinVersion = (int)ModuleType.Halo5Forge)]
        public long UncompressedOffset { get; set; }

        [StoreType(typeof(uint))]
        [Offset(12, MaxVersion = (int)ModuleType.Halo5Forge)]
        [Offset(20, MinVersion = (int)ModuleType.Halo5Forge)]
        public long UncompressedSize { get; set; }

        [Offset(16, MaxVersion = (int)ModuleType.Halo5Forge)]
        [Offset(24, MinVersion = (int)ModuleType.Halo5Forge)]
        public int Compressed { get; set; }
    }
}
