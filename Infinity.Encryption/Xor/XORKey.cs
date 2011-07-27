using System.Collections.Generic;

namespace Infinity.Encryption.Xor
{
    public class XORKey
    {
        private int m_Position;
        private IList<byte> m_KeyBytes;

        public XORKey(IEnumerable<byte> keyBytes)
        {
            if(keyBytes is IList<byte>)
            { m_KeyBytes = keyBytes as IList<byte>; }
            else
            { m_KeyBytes = new List<byte>(keyBytes); }

            m_Position = 0;
        }

        public byte GetNextByte()
        {
            if(m_Position >= m_KeyBytes.Count)
            { m_Position = 0; }
            return m_KeyBytes[m_Position++];
        }

        public void MoveKeyPosition(int position)
        { m_Position = position; }
    }
}
