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

        [MaxLength(250)]
        public string hobbyName { get; set; }

    }
}
