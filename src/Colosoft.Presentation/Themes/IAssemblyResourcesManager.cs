using System;

namespace Colosoft.Presentation.Themes
{
    public interface IAssemblyResourcesManager : IDisposable
    {
        IResourcesRepository AssemblyResourcesRepository { get; }

        void AddDirectory(string directoryName);

        void AddFile(string assemblyFileName);

        System.Reflection.Assembly Get(string assemblyName);
    }
}
