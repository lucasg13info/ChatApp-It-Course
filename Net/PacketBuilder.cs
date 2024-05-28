using System;
using System.IO;
using System.Text;

namespace ChatClient.Net
{
    internal class PacketBuilder
    {
        private MemoryStream _stream;

        public PacketBuilder()
        {
            _stream = new MemoryStream();
        }

        internal void WriteOpCode(int v)
        {
            _stream.Write(BitConverter.GetBytes(v), 0, sizeof(int));
        }

        internal void WriteString(string str)
        {
            WriteOpCode(str.Length);
            byte[] stringBytes = Encoding.UTF8.GetBytes(str);
            _stream.Write(stringBytes, 0, stringBytes.Length);
        }

        internal void WriteDateTime(DateTime dateTime)
        {
            long ticks = dateTime.Ticks;
            _stream.Write(BitConverter.GetBytes(ticks), 0, sizeof(long));
        }

        internal byte[] GetPacketBytes()
        {
            return _stream.ToArray();
        }
    }
}
