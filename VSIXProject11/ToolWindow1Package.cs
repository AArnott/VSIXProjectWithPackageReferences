using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace VSIXProject11
{
	[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
	[InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
	[ProvideMenuResource("Menus.ctmenu", 1)]
	[Guid(ToolWindow1Package.PackageGuidString)]
	[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
	public sealed class ToolWindow1Package : AsyncPackage
	{
		public const string PackageGuidString = "2897fc32-a5e5-4206-8d60-7a53709eea1f";
		protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
		{
			await base.InitializeAsync(cancellationToken, progress);

			// When initialized asynchronously, we *may* be on a background thread at this point.
			// Do any initialization that requires the UI thread after switching to the UI thread.
			// Otherwise, remove the switch to the UI thread if you don't need it.
			await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
		}
	}
}
