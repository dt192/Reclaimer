﻿using Adjutant.Geometry;
using Reclaimer.Blam.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adjutant.Spatial
{
    public struct Int16N2 : IXMVector
    {
        private Int16N x, y;

        public float X
        {
            get => x.Value;
            set => x = new Int16N(value);
        }

        public float Y
        {
            get => y.Value;
            set => y = new Int16N(value);
        }

        public Int16N2(short x, short y)
        {
            this.x = new Int16N(x);
            this.y = new Int16N(y);
        }

        public Int16N2(float x, float y)
        {
            this.x = new Int16N(x);
            this.y = new Int16N(y);
        }

        public float Length => (float)Math.Sqrt(X * X + Y * Y);

        public override string ToString() => Utils.CurrentCulture($"[{X:F6}, {Y:F6}]");

        #region IXMVector

        float IXMVector.Z
        {
            get => float.NaN;
            set { }
        }

        float IXMVector.W
        {
            get => float.NaN;
            set { }
        }

        VectorType IXMVector.VectorType => VectorType.Int16_N2;

        #endregion

        #region Equality Operators

        public static bool operator ==(Int16N2 value1, Int16N2 value2) => value1.x == value2.x && value1.y == value2.y;
        public static bool operator !=(Int16N2 value1, Int16N2 value2) => !(value1 == value2);

        public static bool Equals(Int16N2 value1, Int16N2 value2) => value1.x.Equals(value2.x) && value1.y.Equals(value2.y);
        public override bool Equals(object obj)=> obj is Int16N2 value && Int16N2.Equals(this, value);
        public bool Equals(Int16N2 value) => Int16N2.Equals(this, value);

        public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();

        #endregion
    }
}
