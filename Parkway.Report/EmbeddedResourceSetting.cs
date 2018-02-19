using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Parkway.Report.EmbeddedResourceSetting
{
    public class EmbeddedResourcePathConfiguration
    {
        public EmbeddedResourcePathConfiguration(string rootNameSpace, string viewFolderName, Assembly resourceAssembly)
        {
            this.RootNameSpace = rootNameSpace;
            this.ViewFolderName = viewFolderName;
            this.ResourceAssembly = resourceAssembly;
        }
        public string RootNameSpace { get; set; }
        public string ViewFolderName { get; set; }
        public Assembly ResourceAssembly { get; set; }
        public string ViewNameSpace
        {
            get
            {
                return RootNameSpace + "." + ViewFolderName;
            }
        }

    }

    public class EmbeddedResourceNameProvider
    {
        private EmbeddedResourcePathConfiguration configuration = null;
        public EmbeddedResourcePathConfiguration Configuration
        {
            get { return configuration; }
            set { configuration = value; }
        }

        public string GetResourceName(string virtualPath)
        {
            var resourcename = virtualPath
                .Substring(virtualPath.IndexOf(configuration.ViewNameSpace));

            return resourcename;
        }
    }

    public class EmbeddedVirtualFile : VirtualFile
    {
        public EmbeddedVirtualFile(string virtualPath, EmbeddedResourcePathConfiguration configuration)
            : base(virtualPath)
        {
            this.configuration = configuration;
            EmbeddedResourceNameProvider.Configuration = configuration;
        }
        private EmbeddedResourcePathConfiguration configuration = null;

        EmbeddedResourceNameProvider embeddedResourceNameProvider = new EmbeddedResourceNameProvider();
        public EmbeddedResourceNameProvider EmbeddedResourceNameProvider
        {
            get { return embeddedResourceNameProvider; }
            set { embeddedResourceNameProvider = value; }
        }

        public override Stream Open()
        {
            string resourcePath = EmbeddedResourceNameProvider.GetResourceName(VirtualPath);
            return configuration.ResourceAssembly.GetManifestResourceStream(resourcePath);
        }
    }
    public class EmbeddedViewPathProvider : VirtualPathProvider
    {
        EmbeddedResourceNameProvider embeddedResourceNameProvider = new EmbeddedResourceNameProvider();
        public EmbeddedResourceNameProvider EmbeddedResourceNameProvider
        {
            get { return embeddedResourceNameProvider; }
            set { embeddedResourceNameProvider = value; }
        }

        private EmbeddedResourcePathConfiguration configuration = null;
        public EmbeddedViewPathProvider(EmbeddedResourcePathConfiguration configuration)
        {
            this.EmbeddedResourceNameProvider.Configuration = configuration;
            this.configuration = configuration;
        }

        private bool IsVirtualFile(string virtualPath)
        {
            return virtualPath.Contains(configuration.ViewNameSpace);
        }

        private bool ResourceFileExists(string virtualPath)
        {
            var resourcename = EmbeddedResourceNameProvider.GetResourceName(virtualPath);
            var result = resourcename != null && configuration.ResourceAssembly.GetManifestResourceNames().Contains(resourcename);
            return result;
        }

        public override System.Web.Caching.CacheDependency GetCacheDependency(string virtualPath, System.Collections.IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (IsVirtualFile(virtualPath))
            {
                return new System.Web.Caching.CacheDependency(configuration.ResourceAssembly.Location);
            }
            return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        public override bool FileExists(string virtualPath)
        {
            if (IsVirtualFile(virtualPath))
            {
                bool exists = ResourceFileExists(virtualPath);
                return exists;
            }
            else
            {
                return base.FileExists(virtualPath);
            }
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (IsVirtualFile(virtualPath))
            {
                return new EmbeddedVirtualFile(virtualPath, configuration);
            }
            else
            {
                return base.GetFile(virtualPath);
            }
        }
    }
}