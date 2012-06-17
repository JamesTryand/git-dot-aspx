namespace GitAspx.Controllers {
	using System;
	using System.IO;
	using System.Web.Mvc;
	using System.Web.SessionState;
	using GitAspx.Lib;

	[SessionState(SessionStateBehavior.Disabled)]
	public class DumbController : BaseController {
		readonly RepositoryService repositories;

		public DumbController(RepositoryService repositories) {
			this.repositories = repositories;
		}

		public ActionResult GetTextFile(string store, string project) {
            return WriteFile(store, project, "text/plain");
		}

        public ActionResult GetInfoPacks(string store, string project)
        {
            return WriteFile(store, project, "text/plain; charset=utf-8");
		}

        public ActionResult GetLooseObject(string store, string project)
        {
            return WriteFile(store, project, "application/x-git-loose-object");
		}

        public ActionResult GetPackFile(string store, string project)
        {
            return WriteFile(store, project, "application/x-git-packed-objects");
		}

        public ActionResult GetIdxFile(string store, string project)
        {
            return WriteFile(store, project, "application/x-git-packed-objects-toc");
		}

        private ActionResult WriteFile(string store, string project, string contentType)
        {
			Response.WriteNoCache();
			Response.ContentType = contentType;
			var repo = repositories.GetRepository(store, project);

			string path = Path.Combine(repo.GitDirectory(), GetPathToRead(store, project));

			if(! System.IO.File.Exists(path)) {
				return new NotFoundResult();
			}

			Response.WriteFile(path);

			return new EmptyResult();
		}

        //TODO:Fix GetPathToRead with store value
        private string GetPathToRead(string store, string project)
        {
			int index = Request.Url.PathAndQuery.IndexOf(project) + project.Length + 1;
			return Request.Url.PathAndQuery.Substring(index);
		}
	}
}