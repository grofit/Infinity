using System.Windows.Forms;
using Infinity.Injection;
using Infinity.Plugins.BIF;
using Infinity.Plugins.TwoDA;
using Infinity.Tools.Forms;
using Ninject;

namespace Infinity.Tools
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            SetupEvents();
        }

        private void SetupEvents()
        {
            bIFFViewerToolStripMenuItem.Click += delegate
            {
                var biffForm = new BiffViewerForm(InjectionKernel.Kernel.Get<BIFPlugin>());
                biffForm.MdiParent = this;
                biffForm.Show();
            };

            twoDAViewerToolStripMenuItem.Click += delegate
            {
                var twoDAForm = new TwoDAViewerForm(InjectionKernel.Kernel.Get<TwoDAPlugin>());
                twoDAForm.MdiParent = this;
                twoDAForm.Show();
            };
        }

    }
}
