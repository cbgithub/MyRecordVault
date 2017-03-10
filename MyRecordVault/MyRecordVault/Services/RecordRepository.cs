using MyRecordVault.Models;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyRecordVault.Services
{
    public class RecordRepository
    {
        static readonly object Locker = new object();
        readonly SQLiteConnection _db;

        public RecordRepository()
        {
            _db = DependencyService.Get<ISQLite>().GetConnection();
            _db.CreateTable<Record>();
        }

        public async Task<IEnumerable<Record>> GetItemsAsync(int parentID = 0)
        {
            return await Task.Run<IEnumerable<Record>>(() => {
                lock (Locker)
                {
                    return _db.Table<Record>()
                        .Where(m => m.Delete == false && m.ParentID == parentID);
                }
            });
        }

        public async Task<Password> FindFirstAsync(int id)
        {
            return await Task.Run<Record>(() => {
                lock (Locker)
                {
                    return _db.Table<Record>()
                        .First(m => m.ID == id && m.Delete == false);
                }
            });
        }

        public async Task<int> SaveItemAsync(Record item)
        {
            return await Task.Run<int>(() => {
                lock (Locker)
                {
                    if (item.ID != 0)
                    {
                        _db.Update(item);
                        return item.ID;
                    }
                    return _db.Insert(item);
                }
            });
        }

        public async Task<int> DeleteItemAsync(Record item)
        {
            return await Task.Run<int>(() => {
                lock (Locker)
                {
                    return _db.Delete(item);
                }
            });
        }
    }
}
