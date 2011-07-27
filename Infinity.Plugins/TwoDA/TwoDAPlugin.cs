using System;
using System.Collections.Generic;
using System.IO;
using Infinity.Configuration;
using Infinity.Encryption;
using Infinity.Encryption.Streams;
using Infinity.Lookups;
using netextender.extensions;
using netextender.streams;

namespace Infinity.Plugins.TwoDA
{
    public class TwoDAPlugin : IPlugin
    {
        private static readonly string Signature2DA = "2DA V1.0";
        private static readonly int CommentFlag = '#'.AsByte();

        public int PluginSignature
        {
            get { return ResourceTypes.TwoDA; }
        }

        public string TargetVersion
        {
            get { return "1.0"; }
        }

        private IEncryption m_Encryption;

        public TwoDAPlugin(IEncryption encryption)
        { m_Encryption = encryption; }

        public TwoDAResource Import(Stream stream)
        {
            var byteStream = CreateByteStream(stream);

            if(byteStream is EncryptedByteStream)
            { stream.Seek(2, SeekOrigin.Current); }

            var signature = GetSignature(byteStream.ReadUntilLineFound());
            if(!signature.Equals(Signature2DA, StringComparison.CurrentCultureIgnoreCase))
            { LoggingConfiguration.LogAndThrowError(new Exception("Invalid signature for 2DA file")); }

            var Created2DA = new TwoDAResource();
            Created2DA.DefaultValue = ConvertToDefaultValue(byteStream.ReadUntilLineFound());
            Created2DA.Columns = ConvertToColumns(byteStream.ReadUntilLineFound());

            var line = byteStream.ReadUntilLineFound();
            while(line.Length > 0)
            {
                if(line[0] == CommentFlag)
                { continue; }

                Created2DA.Rows.Add(ConvertToRow(line));
                line = byteStream.ReadUntilLineFound();
            }

            return Created2DA;
        }

        private ByteStream CreateByteStream(Stream stream)
        {
            if (m_Encryption != null && m_Encryption.IsResourceEncrypted(stream))
            { return new EncryptedByteStream(stream, m_Encryption); }
            return new ByteStream(stream);
        }

        private string GetSignature(byte[] bytes)
        {
            var signatureString = bytes.AsString();
            return signatureString.Trim();
        }

        private IList<string> ConvertToColumns(byte[] bytes)
        {
            var columnString = bytes.AsString();
            var columnNames = columnString.Split(' ');

            var actualColumns = new List<string>();
            for (int i = 0; i < columnNames.Length; i++)
            { 
                if(string.IsNullOrEmpty(columnNames[i]))
                { continue; }

                actualColumns.Add(columnNames[i]);
            }

            return actualColumns;
        }

        private TwoDARow ConvertToRow(byte[] bytes)
        {
            var rowString = bytes.AsString();
            var rowElements = rowString.Split(' ');

            var twoDARow = new TwoDARow();
            var isRowName = true;
            for(int i=0;i<rowElements.Length;i++)
            {
                if (string.IsNullOrEmpty(rowElements[i]))
                { continue; }

                if (isRowName)
                {
                    twoDARow.RowName = rowElements[i];
                    isRowName = false;
                    continue;
                }

                twoDARow.RowData.Add(rowElements[i]);
            }

            return twoDARow;
        }

        private string ConvertToDefaultValue(byte[] bytes)
        {
            var rowString = bytes.AsString();
            return rowString.Replace(" ", string.Empty);
        }
    }
}