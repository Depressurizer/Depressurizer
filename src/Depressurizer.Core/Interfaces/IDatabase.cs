using Depressurizer.Core.Models;

namespace Depressurizer.Core.Interfaces
{
    public interface IDatabase
    {
        #region Public Methods and Operators

        bool Contains(long appId);

        bool Contains(long appId, out DatabaseEntry entry);

        string GetName(long appId);

        bool IncludeItemInGameList(long appId);

        bool SupportsVR(long appId);

        #endregion
    }
}
