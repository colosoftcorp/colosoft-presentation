using System;
using System.Collections.Generic;
using System.Linq;

namespace Colosoft.Presentation
{
    public static class ResourcesManager
    {
        private static readonly object ObjLock = new object();

        private static readonly Dictionary<string, IResourcesRepository> Repositories
            = new Dictionary<string, IResourcesRepository>();

        private static readonly Dictionary<string, List<IResourcesRepository>> SchemeRepositories
            = new Dictionary<string, List<IResourcesRepository>>();

        public static void AddRepository(IResourcesRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            lock (ObjLock)
            {
                if (Repositories.ContainsKey(repository.Name))
                {
                    var repos = Repositories[repository.Name];

                    Repositories.Remove(repository.Name);

                    foreach (var i in SchemeRepositories.Values)
                    {
                        i.Remove(repos);
                    }
                }

                Repositories.Add(repository.Name, repository);

                foreach (var i in repository.Schemes)
                {
                    List<IResourcesRepository> schemeRepository = null;

                    if (!SchemeRepositories.TryGetValue(i, out schemeRepository))
                    {
                        schemeRepository = new List<IResourcesRepository>();
                        SchemeRepositories.Add(i, schemeRepository);
                    }

                    schemeRepository.Add(repository);
                }
            }
        }

        public static IResourcesRepository GetRepository(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            IResourcesRepository repository = null;

            lock (ObjLock)
            {
                if (Repositories.TryGetValue(name, out repository))
                {
                    return repository;
                }
            }

            return null;
        }

        public static IEnumerable<IResourcesRepository> GetRepositories(string scheme)
        {
            if (scheme == null)
            {
                throw new ArgumentNullException(nameof(scheme));
            }

            List<IResourcesRepository> repositories = null;

            lock (ObjLock)
            {
                if (SchemeRepositories.TryGetValue(scheme, out repositories))
                {
                    return repositories.ToArray();
                }
            }

            return Array.Empty<IResourcesRepository>();
        }

        public static object GetResource(string themeName, Uri location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            var repositories = GetRepositories(location.Scheme);

            foreach (var repository in repositories.Where(f => f.ThemeName == null || f.ThemeName == themeName))
            {
                var value = repository.Get(location);

                if (value != null)
                {
                    return value;
                }
            }

            return null;
        }

        public static System.Collections.Specialized.NameValueCollection ParseQueryString(this string queryString)
        {
            var nvc = new System.Collections.Specialized.NameValueCollection();

            if (string.IsNullOrEmpty(queryString))
            {
                return nvc;
            }

            foreach (string vp in System.Text.RegularExpressions.Regex.Split(queryString, "&"))
            {
                string[] singlePair = System.Text.RegularExpressions.Regex.Split(vp, "=");
                if (singlePair.Length == 2)
                {
                    nvc.Add(singlePair[0], singlePair[1]);
                }
            }

            return nvc;
        }
    }
}
