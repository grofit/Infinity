using System.Collections.Generic;
using System.Reflection;
using Infinity.Plugins.IDS;
using netextender.extensions;
using NUnit.Framework;

namespace UnitTests.Plugins
{
    [TestFixture]
    public class IDSPlugin_Tests
    {
        [Test]
        public void should_correctly_convert_default_value()
        {
            var expectedValue = 100;
            var plugin = new IDSPlugin(null);

            var convertToDefaultValueMethod = plugin.GetType().GetMethod("ConvertToDefaultValue", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = convertToDefaultValueMethod.Invoke(plugin, new[] { " 100 ".AsBytes() });

            Assert.That(result, Is.TypeOf(typeof(int)));

            var castResult = (int)result;
            Assert.That(castResult, Is.EqualTo(expectedValue));
        }

        [Test]
        public void should_correctly_convert_to_key_value_pair()
        {
            var expectedKey = 1;
            var expectedValue = "SomeValue";
            var plugin = new IDSPlugin(null);

            var convertToKeyValuePairMethod = plugin.GetType().GetMethod("ConvertToKeyValuePair", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = convertToKeyValuePairMethod.Invoke(plugin, new[] { "1 SomeValue ".AsBytes() });

            Assert.That(result, Is.TypeOf(typeof(KeyValuePair<int, string>)));

            var castResult = (KeyValuePair<int, string>)result;
            Assert.That(castResult.Key, Is.EqualTo(expectedKey));
            Assert.That(castResult.Value, Is.EqualTo(expectedValue));
        }
    }
}
