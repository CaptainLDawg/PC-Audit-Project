using System;
using SQLite;

namespace PCAuditReader
{
    public class ListOSoftware
    {
        [PrimaryKey, AutoIncrement]
        public string SoftName { get; set; }
        public string SoftVendor { get; set; }
        public string SoftVersion { get; set; }

        public ListOSoftware()
        {
        }
    }
}