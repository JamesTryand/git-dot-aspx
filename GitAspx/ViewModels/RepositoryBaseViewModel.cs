using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace GitAspx.ViewModels
{
    public class RepositoryBaseViewModel
    {
        private DirectoryInfo directory;
        public string Name { get { return directory.Name; } }
        public string FullName { get { return directory.FullName; } }

        public RepositoryBaseViewModel() { }

        public RepositoryBaseViewModel(string fullPath) : this(new DirectoryInfo(fullPath)) { }

        public RepositoryBaseViewModel(DirectoryInfo directory)
        {
            this.directory = directory;
        }

        public static explicit operator RepositoryBaseViewModel(DirectoryInfo directory)
        {
            return new RepositoryBaseViewModel(directory);
        }
    }

    public class RepositoryBaseProvider
    {
        public static IEnumerable<RepositoryBaseViewModel> RepositoryBases(string directorySource)
        {
            return RepositoryBases(new DirectoryInfo(directorySource));
        }
        public static IEnumerable<RepositoryBaseViewModel> RepositoryBases(DirectoryInfo directorySource)
        {
            return directorySource.GetDirectories().Select(e => new RepositoryBaseViewModel(e));
        }

    }
}