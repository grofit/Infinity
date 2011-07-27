using System.Reflection;
using System.Collections.Generic;
using Infinity.Plugins.TwoDA;
using netextender.extensions;
using NUnit.Framework;

namespace UnitTests.Plugins
{
    [TestFixture]
    public class TwoDAPlugin_Tests
    {
        [Test]
        public void should_correctly_read_file_signature ()
        {
            var expectedOutput = "2DA V1.0";
            var plugin = new TwoDAPlugin(null);

            var getSignatureMethod = plugin.GetType().GetMethod("GetSignature", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = getSignatureMethod.Invoke(plugin, new[] { expectedOutput.AsBytes() });

            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void should_correctly_convert_default_value()
        {
            var expectedValue = "12345";
            var plugin = new TwoDAPlugin(null);

            var convertDefaultValueMethod = plugin.GetType().GetMethod("ConvertToDefaultValue", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = convertDefaultValueMethod.Invoke(plugin, new[] { " 12345 ".AsBytes() });

            Assert.That(result, Is.EqualTo(expectedValue));
        }

        [Test]
        public void should_correctly_convert_columns()
        {
            var expectedList = new List<string> {"col1", "col2", "col3"};
            var plugin = new TwoDAPlugin(null);

            var convertToColumnMethod = plugin.GetType().GetMethod("ConvertToColumns", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = convertToColumnMethod.Invoke(plugin, new[] {"col1 col2 col3".AsBytes() });

            Assert.That(result, Is.EqualTo(expectedList));
        }

        [Test]
        public void should_correctly_convert_rows()
        {
            var expectedName = "row1";
            var expectedData = new List<string> { "1", "2", "3" };
            var plugin = new TwoDAPlugin(null);

            var convertToRowMethod = plugin.GetType().GetMethod("ConvertToRow", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = convertToRowMethod.Invoke(plugin, new[] { "row1 1 2 3 ".AsBytes() });

            Assert.That(result, Is.TypeOf(typeof(TwoDARow)));

            var castResult = (result as TwoDARow);
            Assert.That(castResult.RowName, Is.EqualTo(expectedName));
            Assert.That(castResult.RowData, Is.EqualTo(expectedData));
        }
    }
}