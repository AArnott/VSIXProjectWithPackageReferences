namespace VSIXProject11
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;
    using Microsoft.VisualStudio.Threading;
    using Task = System.Threading.Tasks.Task;

    /// <summary>
    /// Interaction logic for ToolWindow1Control.
    /// </summary>
    public partial class ToolWindow1Control : UserControl, IDisposable
    {
        private readonly ToolWindow1Package package;
        private bool running;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolWindow1Control"/> class.
        /// </summary>
        public ToolWindow1Control(ToolWindow1Package package)
        {
            this.package = package ?? throw new System.ArgumentNullException(nameof(package));
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            running = !running;

            ////this.package.JoinableTaskFactory.RunAsync(async delegate
            ////{
            ////    await DoLongWorkAsync();
            ////});
            DoLongWorkAsync().Forget();
        }

        public void Dispose()
        {
            this.running = false;
        }

        private async Task DoLongWorkAsync()
        {
            bool toggle = false;
            while (running)
            {
                // Do some work, and yield periodically
                await DoSomethingAsync();
                button1.Background = toggle ? Brushes.Blue : Brushes.Yellow;
                toggle = !toggle;
                await Task.Yield();
            }
        }

        private async Task DoSomethingAsync()
        {
            Thread.Sleep(100); // Simulate UI thread bound work
            await Task.Delay(10); // Simulate async yielding (e.g. I/O, or threadpool work)
        }

        private async Task MakeProgressAsync(IProgress<ThreadedWaitDialogProgressData> progress, CancellationToken cancellationToken)
        {
            for (int i = 1; i <= 100; i++)
            {
                progress.Report(new ThreadedWaitDialogProgressData(
                    waitMessage: "my wait message",
                    progressText: i < 70 ? "my progress text" : "wrapping up",
                    statusBarText: null,
                    isCancelable: i < 70,
                    currentStep: i,
                    totalSteps: 100));
                await Task.Delay(30, cancellationToken);
            }
        }
    }
}