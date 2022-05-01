﻿using Adjutant.Spatial;
using Reclaimer.Blam.Common;
using Reclaimer.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reclaimer.Blam.HaloReach
{
    public class shader
    {
        [Offset(0)]
        public TagReference BaseShaderReference { get; set; }

        [Offset(56)]
        public BlockCollection<ShaderPropertiesBlock> ShaderProperties { get; set; }
    }

    [FixedSize(176)]
    public class ShaderPropertiesBlock
    {
        [Offset(0)]
        public TagReference TemplateReference { get; set; }

        [Offset(16)]
        public BlockCollection<ShaderMapBlock> ShaderMaps { get; set; }

        [Offset(28)]
        public BlockCollection<RealVector4D> TilingData { get; set; }
    }

    [FixedSize(24)]
    public class ShaderMapBlock
    {
        [Offset(0)]
        public TagReference BitmapReference { get; set; }

        [Offset(21)]
        public byte TilingIndex { get; set; }
    }
}