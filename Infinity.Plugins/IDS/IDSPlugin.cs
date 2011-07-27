using System;
using System.Collections.Generic;
using System.IO;
using Infinity.Encryption;
using Infinity.Encryption.Streams;
using Infinity.Lookups;
using netextender.extensions;
using netextender.streams;

namespace Infinity.Plugins.IDS
{
    public class IDSPlugin : IPlugin
    {
        private static readonly int CommentFlag = '#'.AsByte();

        public int PluginSignature
        {
            get { return ResourceTypes.IDS; }
        }

        public string TargetVersion
        {
            get { return "1.0"; }
        }

        private IEncryption m_Encryption;

        public IDSPlugin(IEncryption encryption)
        {
            m_Encryption = encryption;
        }

        public IDSResource Import(Stream stream)
        {
            var byteStream = CreateByteStream(stream);

            if (byteStream is EncryptedByteStream)
            { stream.Seek(2, SeekOrigin.Current); }
          
            var createdIDS = new IDSResource();
            createdIDS.DefaultValue = ConvertToDefaultValue(byteStream.ReadUntilLineFound());

            var line = byteStream.ReadUntilLineFound();
            while (line.Length > 0)
            {
                if (line[0] == CommentFlag)
                { continue; }

                createdIDS.Mappings.Add(ConvertToKeyValuePair(line));
                line = byteStream.ReadUntilLineFound();
            }

            return createdIDS;
        }

        private ByteStream CreateByteStream(Stream stream)
        {
            if (m_Encryption != null && m_Encryption.IsResourceEncrypted(stream))
            { return new EncryptedByteStream(stream, m_Encryption); }
            return new ByteStream(stream);
        }

        private int ConvertToDefaultValue(byte[] bytes)
        {
            var valueString = bytes.AsString().Trim();
            return Convert.ToInt32(valueString);
        }

        private KeyValuePair<int, string> ConvertToKeyValuePair(byte[] bytes)
        {
            var rowString = bytes.AsString();
            var rowElements = rowString.Split(' ');

            int key = 0;
            string value = string.Empty;

            var isKey = true;
            for (int i = 0; i < rowElements.Length; i++)
            {
                if (string.IsNullOrEmpty(rowElements[i]))
                { continue; }

                if (isKey)
                {
                    key = Convert.ToInt32(rowElements[i]);
                    isKey = false;
                    continue;
                }
                
                value = rowElements[i];
                break;
            }

            return new KeyValuePair<int, string>(key, value);
        }
    }
}