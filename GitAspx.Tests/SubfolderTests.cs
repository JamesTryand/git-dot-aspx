using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GitAspx.Controllers;
using GitAspx.Lib;
using System.IO;
using System.Web.Mvc;
using GitAspx.ViewModels;

namespace GitAspx.Tests
{
    [TestFixture]
    public class SubfolderTests
    {
        [Test]
        public void Index_with_new_path_should_set_the_view_model_directory_to_that_provided()
        {
            var controller = new DirectoryListController(new RepositoryService(new AppSettings()));
            // var controller = new DirectoryListController(new RepositoryService(new AppSettings() { RepositoriesDirectory = new DirectoryInfo(@"C:\Repositories\Monkey1"), }));
            var result = controller.List(@"Monkey1") as ViewResult;
            ((DirectoryListViewModel)result.Model).RepositoriesDirectory.ShouldEqual(@"C:\Repositories\Monkey1");
        }

        [Test]
        public void Index_with_new_path_should_set_the_view_model_directory_to_that_provided_in_urlencoded_form()
        {
            var controller = new DirectoryListController(new RepositoryService(new AppSettings()));
            // var controller = new DirectoryListController(new RepositoryService(new AppSettings() { RepositoriesDirectory = new DirectoryInfo(@"C:\Repositories\Monkey1"), }));
            var result = controller.List(@"Monkey1") as ViewResult;
            ((DirectoryListViewModel)result.Model).RepositoriesDirectory.ShouldEqual(@"C:\Repositories\Monkey1");
        }
    }
}
