using SQLite.Net;

namespace MyRecordVault.Services
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
