#region License

// Copyright 2010 Jeremy Skinner (http://www.jeremyskinner.co.uk)
//  
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
// 
// The latest version of this file can be found at http://github.com/JeremySkinner/git-dot-aspx

#endregion

namespace GitAspx.Lib {
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
    using System;

	public class RepositoryService {
		readonly AppSettings appSettings;

		public RepositoryService(AppSettings appSettings) {
			this.appSettings = appSettings;
		}

        public RepositoryService(string id)
        {
            this.appSettings = new AppSettings() { RepositoriesDirectory = new DirectoryInfo(id), };
        }

		public IEnumerable<Repository> GetAllRepositories() {
			return appSettings.RepositoriesDirectory
				.GetDirectories()
				.Select(Repository.Open)
				.Where(x => x!=null)
				.ToList();
		}

		public Repository GetRepository(string store, string project) {
			var directory = Path.Combine(appSettings.RepositoriesDirectory.FullName, store, project);

			if (!Directory.Exists(directory)) {
				return null;
			}

			//return Repository.Open(directory);
			return new Repository(new DirectoryInfo(directory));
		}

        public DirectoryInfo GetRepositoriesDirectory()
        {
            return appSettings.RepositoriesDirectory;
        }

        public string GetRepositoriesDirectoryPath()
        {
            return appSettings.RepositoriesDirectory.FullName;
        }

        //TODO:Fix GetRepositoriesStoreDirectory with store value
        public DirectoryInfo GetRepositoriesStoreDirectory(string store)
        {
            return new DirectoryInfo(Path.Combine(GetRepositoriesDirectoryPath(), store));
        }

		public void CreateRepository(string project) {

            CreateRepository(appSettings.RepositoriesDirectory.FullName, project);

		}

        public void CreateRepository(string rootrepository, string project)
        {
            var directory = Path.Combine(rootrepository, project + ".git");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);

                using (var repository = new GitSharp.Core.Repository(new DirectoryInfo(directory)))
                {
                    repository.Create(true);
                }
            }
            
        }
    }
}