using System;
using SQLite;

namespace PCAuditReader
{
    public class ListOSingle
    {
        [PrimaryKey, AutoIncrement]
        public string SingleItem { get; set; }

        public ListOSingle()
        {
        }
    }
}