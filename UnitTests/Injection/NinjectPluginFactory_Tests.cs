using Infinity;
using NUnit.Framework;

namespace UnitTests.Injection
{
    public class TestPlugin : IPlugin
    {
        public int PluginSignature
        {
            get { return 12345; }
        }

        public override bool Equals(object obj)
        {
            if(obj is TestPlugin)
            {
                return (PluginSignature == (obj as TestPlugin).PluginSignature); 
            }
            return base.Equals(obj);
        }
    }

    [TestFixture]
    public class NinjectPluginFactory_Tests
    {
        [Test]
        public void should_return_registered_plugins()
        {
            var testPlugin = new TestPlugin();
            var ninjectPluginFactory = new NinjectPluginFactory();
            ninjectPluginFactory.RegisterPlugin(testPlugin);

            var returnedPlugin = ninjectPluginFactory.RequestPlugin(testPlugin.PluginSignature);

            Assert.That(returnedPlugin, Is.EqualTo(testPlugin));
        }
    }
}
