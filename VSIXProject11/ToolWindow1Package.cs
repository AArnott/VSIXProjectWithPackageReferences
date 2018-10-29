﻿using System;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace VSIXProject11
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(ToolWindow1))]
    [Guid(ToolWindow1Package.PackageGuidString)]
    [ProvideService(typeof(MyService))]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class ToolWindow1Package : Package
    {
        public const string PackageGuidString = "2897fc32-a5e5-4206-8d60-7a53709eea1f";
        protected override void Initialize()
        {
            base.Initialize();
            ToolWindow1Command.Initialize(this);

            IServiceContainer serviceContainer = this;
            serviceContainer.AddService(typeof(MyService), new MyService());
        }
    }
}
