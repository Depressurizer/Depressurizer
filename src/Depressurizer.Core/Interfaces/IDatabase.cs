using Depressurizer.Core.Models;

namespace Depressurizer.Core.Interfaces
{
    public interface IDatabase
    {
        #region Public Methods and Operators

        bool Contains(int appId);

        bool Contains(int appId, out DatabaseEntry entry);

        string GetName(int appId);

        bool IncludeItemInGameList(int appId);

        bool SupportsVR(int appId);

        #endregion
    }
}
