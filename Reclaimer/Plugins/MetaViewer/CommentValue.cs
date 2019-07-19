﻿using Adjutant.Blam.Common;
using Reclaimer.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Endian;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Reclaimer.Plugins.MetaViewer
{
    public class CommentValue : MetaValue
    {
        public string Title { get; }
        public string Body { get; }

        public CommentValue(XmlNode node, ICacheFile cache, long baseAddress, EndianReader reader)
            : base(node, cache, baseAddress, reader)
        {
            Title = GetStringAttribute(node, "title", "name");
            Body = node.InnerText;
        }

        public override void RefreshValue(EndianReader reader)
        {

        }
    }
}
