using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace B2C4_inleveropdracht_1
{
    public class FullTip
    {
        [PrimaryKey, AutoIncrement]
        public int tipId { get; set; }

        public string hobbyName { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string level { get; set; }
        public string link { get; set; }
        public byte[] imageLink { get; set; }
        public string tipInfo { get; set; }
    }
}
