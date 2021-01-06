using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace B2C4_inleveropdracht_1
{
    public class Hobby
    {
        [PrimaryKey, AutoIncrement]
        public int hobbyId { get; set; }
        public string hobbyName { get; set; }

        public Hobby(string _hobbyName)
        {
            this.hobbyName = _hobbyName;
        }
    }
}
