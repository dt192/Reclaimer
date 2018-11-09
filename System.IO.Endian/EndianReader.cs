﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace System.IO
{
    public class EndianReader : BinaryReader
    {
        public ByteOrder ByteOrder { get; set; }

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <seealso cref="EndianReader"/> class
        /// based on the specified stream with the system byte order and using UTF-8 encoding.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentNullException" />
        public EndianReader(Stream input) : this(input, BitConverter.IsLittleEndian ? ByteOrder.LittleEndian : ByteOrder.BigEndian, new UTF8Encoding(), false)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <seealso cref="EndianReader"/> class
        /// based on the specified stream with the specified byte order and using UTF-8 encoding.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <param name="byteOrder">The byte order of the stream.</param>
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentNullException" />
        public EndianReader(Stream input, ByteOrder byteOrder) : this(input, byteOrder, new UTF8Encoding(), false)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <seealso cref="EndianReader"/> class
        /// based on the specified stream with the specified byte order and character encoding.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <param name="byteOrder">The byte order of the stream.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentNullException" />
        public EndianReader(Stream input, ByteOrder byteOrder, Encoding encoding) : this(input, byteOrder, encoding, false)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <seealso cref="EndianReader"/> class
        /// based on the specified stream with the specified byte order and character encoding, and optionally leaves the stream open.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <param name="byteOrder">The byte order of the stream.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="leaveOpen">true to leave the stream open after the EndianReader object is disposed; otherwise, false.</param>
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentNullException" />
        public EndianReader(Stream input, ByteOrder byteOrder, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen)
        {
            ByteOrder = byteOrder;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Reads a 4-byte floating point value from the current stream using the current byte order 
        /// and advances the current position of the stream by four bytes.
        /// </summary>
        /// <param name="byteOrder">The byte order to use.</param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException" />
        /// <exception cref="IOException" />
        /// <exception cref="ObjectDisposedException" />
        public override float ReadSingle()
        {
            return ReadSingle(ByteOrder);
        }

        /// <summary>
        /// Reads an 8-byte floating point value from the current stream using the current byte order 
        /// and advances the current position of the stream by eight bytes.
        /// </summary>
        /// <param name="byteOrder">The byte order to use.</param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException" />
        /// <exception cref="IOException" />
        /// <exception cref="ObjectDisposedException" />
        public override double ReadDouble()
        {
            return ReadDouble(ByteOrder);
        }

        /// <summary>
        /// Reads a decimal value from the current stream using the current byte order 
        /// and advances the current position of the stream by sixteen bytes.
        /// </summary>
        /// <param name="byteOrder">The byte order to use.</param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException" />
        /// <exception cref="IOException" />
        /// <exception cref="ObjectDisposedException" />
        public override decimal ReadDecimal()
        {
            return ReadDecimal(ByteOrder);
        }

        /// <summary>
        /// Reads a 2-byte signed integer from the current stream using the current byte order 
        /// and advances the current position of the stream by two bytes.
        /// </summary>
        /// <param name="byteOrder">The byte order to use.</param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException" />
        /// <exception cref="IOException" />
        /// <exception cref="ObjectDisposedException" />
        public override short ReadInt16()
        {
            return ReadInt16(ByteOrder);
        }

        /// <summary>
        /// Reads a 4-byte signed integer from the current stream using the current byte order 
        /// and advances the current position of the stream by four bytes.
        /// </summary>
        /// <param name="byteOrder">The byte order to use.</param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException" />
        /// <exception cref="IOException" />
        /// <exception cref="ObjectDisposedException" />
        public override int ReadInt32()
        {
            return ReadInt32(ByteOrder);
        }

        /// <summary>
        /// Reads an 8-byte signed integer from the current stream using the current byte order 
        /// and advances the current position of the stream by eight bytes.
        /// </summary>
        /// <param name="byteOrder">The byte order to use.</param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException" />
        /// <exception cref="IOException" />
        /// <exception cref="ObjectDisposedException" />
        public override long ReadInt64()
        {
            return ReadInt64(ByteOrder);
        }

        /// <summary>
        /// Reads a 2-byte unsigned integer from the current stream using the current byte order 
        /// and advances the current position of the stream by two bytes.
        /// </summary>
        /// <param name="byteOrder">The byte order to use.</param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException" />
        /// <exception cref="IOException" />
        /// <exception cref="ObjectDisposedException" />
        public override ushort ReadUInt16()
        {
            return ReadUInt16(ByteOrder);
        }

        /// <summary>
        /// Reads a 4-byte unsigned integer from the current stream using the current byte order
        /// and advances the current position of the stream by four bytes.
        /// </summary>
        /// <param name="byteOrder">The byte order to use.</param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException" />
        /// <exception cref="IOException" />
        /// <exception cref="ObjectDisposedException" />
        public override uint ReadUInt32()
        {
            return ReadUInt32(ByteOrder);
        }

        /// <summary>
        /// Reads an 8-byte unsigned integer from the current stream using the current byte order
        /// and advances the current position of the stream by eight bytes.
        /// </summary>
        /// <param name="byteOrder">The byte order to use.</param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException" />
        /// <exception cref="IOException" />
        /// <exception cref="ObjectDisposedException" />
        public override ulong ReadUInt64()
        {
            return ReadUInt64(ByteOrder);
        }

        #endregion

        #region ByteOrder Read

        /// <summary>
        /// Reads a 4-byte floating-point value from the current stream using the specified byte order 
        /// and advances the current position of the stream by four bytes.
        /// </summary>
        /// <param name="byteOrder">The byte order to use.</param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException" />
        /// <exception cref="IOException" />
        /// <exception cref="ObjectDisposedException" />
        public float ReadSingle(ByteOrder byteOrder)
        {
            if (byteOrder == ByteOrder.LittleEndian)
                return base.ReadSingle();

            var bytes = base.ReadBytes(4);
            Array.Reverse(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }

        /// <summary>
        /// Reads an 8-byte floating-point value from the current stream using the specified byte order 
        /// and advances the current position of the stream by eight bytes.
        /// </summary>
        /// <param name="byteOrder">The byte order to use.</param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException" />
        /// <exception cref="IOException" />
        /// <exception cref="ObjectDisposedException" />
        public double ReadDouble(ByteOrder byteOrder)
        {
            if (byteOrder == ByteOrder.LittleEndian)
                return base.ReadDouble();

            var bytes = base.ReadBytes(8);
            Array.Reverse(bytes);
            return BitConverter.ToDouble(bytes, 0);
        }

        /// <summary>
        /// Reads a decimal value from the current stream using the specified byte order 
        /// and advances the current position of the stream by sixteen bytes.
        /// </summary>
        /// <param name="byteOrder">The byte order to use.</param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException" />
        /// <exception cref="IOException" />
        /// <exception cref="ObjectDisposedException" />
        public decimal ReadDecimal(ByteOrder byteOrder)
        {
            if (byteOrder == ByteOrder.LittleEndian)
                return base.ReadDecimal();

            var bits = new int[4];
            var bytes = base.ReadBytes(16);
            Array.Reverse(bytes);
            for (int i = 0; i < 4; i++)
                bits[i] = BitConverter.ToInt32(bytes, i * 4);
            return new Decimal(bits);
        }

        /// <summary>
        /// Reads a 2-byte signed integer from the current stream using the specified byte order 
        /// and advances the current position of the stream by two bytes.
        /// </summary>
        /// <param name="byteOrder">The byte order to use.</param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException" />
        /// <exception cref="IOException" />
        /// <exception cref="ObjectDisposedException" />
        public short ReadInt16(ByteOrder byteOrder)
        {
            if (byteOrder == ByteOrder.LittleEndian)
                return base.ReadInt16();

            var bytes = base.ReadBytes(2);
            Array.Reverse(bytes);
            return BitConverter.ToInt16(bytes, 0);
        }

        /// <summary>
        /// Reads a 4-byte signed integer from the current stream using the specified byte order 
        /// and advances the current position of the stream by four bytes.
        /// </summary>
        /// <param name="byteOrder">The byte order to use.</param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException" />
        /// <exception cref="IOException" />
        /// <exception cref="ObjectDisposedException" />
        public int ReadInt32(ByteOrder byteOrder)
        {
            if (byteOrder == ByteOrder.LittleEndian)
                return base.ReadInt32();

            var bytes = base.ReadBytes(4);
            Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// Reads an 8-byte signed integer from the current stream using the specified byte order 
        /// and advances the current position of the stream by eight bytes.
        /// </summary>
        /// <param name="byteOrder">The byte order to use.</param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException" />
        /// <exception cref="IOException" />
        /// <exception cref="ObjectDisposedException" />
        public long ReadInt64(ByteOrder byteOrder)
        {
            if (byteOrder == ByteOrder.LittleEndian)
                return base.ReadInt64();

            var bytes = base.ReadBytes(8);
            Array.Reverse(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }

        /// <summary>
        /// Reads a 2-byte unsigned integer from the current stream using the specified byte order 
        /// and advances the current position of the stream by two bytes.
        /// </summary>
        /// <param name="byteOrder">The byte order to use.</param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException" />
        /// <exception cref="IOException" />
        /// <exception cref="ObjectDisposedException" />
        public ushort ReadUInt16(ByteOrder byteOrder)
        {
            if (byteOrder == ByteOrder.LittleEndian)
                return base.ReadUInt16();

            var bytes = base.ReadBytes(2);
            Array.Reverse(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }

        /// <summary>
        /// Reads a 4-byte unsigned integer from the current stream using the specified byte order 
        /// and advances the current position of the stream by four bytes.
        /// </summary>
        /// <param name="byteOrder">The byte order to use.</param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException" />
        /// <exception cref="IOException" />
        /// <exception cref="ObjectDisposedException" />
        public uint ReadUInt32(ByteOrder byteOrder)
        {
            if (byteOrder == ByteOrder.LittleEndian)
                return base.ReadUInt32();

            var bytes = base.ReadBytes(4);
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }

        /// <summary>
        /// Reads an 8-byte unsigned integer from the current stream using the specified byte order 
        /// and advances the current position of the stream by eight bytes.
        /// </summary>
        /// <param name="byteOrder">The byte order to use.</param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException" />
        /// <exception cref="IOException" />
        /// <exception cref="ObjectDisposedException" />
        public ulong ReadUInt64(ByteOrder byteOrder)
        {
            if (byteOrder == ByteOrder.LittleEndian)
                return base.ReadUInt64();

            var bytes = base.ReadBytes(8);
            Array.Reverse(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }

        #endregion
    }
}
