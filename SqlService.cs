using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Xamarin_Test
{
    public sealed class SqlService
    {
        private static SqlService instance = null;
        private static readonly object padlock = new object();
        private static SQLiteAsyncConnection sQLiteAsyncConnection;
        
        public static SqlService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SqlService();
                        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "packages.db");
                        sQLiteAsyncConnection = new SQLiteAsyncConnection(dbPath);
                        sQLiteAsyncConnection.CreateTableAsync<PackageTable>().Wait();
                    }
                }
                return instance;
            }
        }
        public Task<List<PackageTable>> GetPackageDetailssAsync()
        {
            return sQLiteAsyncConnection.Table<PackageTable>().ToListAsync();
        }

        public Task<PackageTable> GetPackageDetailAsync(int id)
        {
            return sQLiteAsyncConnection.Table<PackageTable>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SavePackageAsync(PackageTable package)
        {
            if (package.Id != 0)
            {
                return sQLiteAsyncConnection.UpdateAsync(package);
            }
            else
            {
                return sQLiteAsyncConnection.InsertAsync(package);
            }
        }

        public Task<int> DeletePackageAsync(PackageTable package)
        {
            return sQLiteAsyncConnection.DeleteAsync(package);
        }
    }
}