using System;
using System.Collections.Generic;
using System.Text;

namespace B2C4_inleveropdracht_1
{
    public class Tip
    {
        public string title { get; set; }
        public string description { get; set; }
        public string level { get; set; }

        public override string ToString()
        {
            return title.ToString();
        }

        public Tip( string _title, string _description, string _level)
        {
            this.title = _title;
            this.description = _description;
            this.level = _level;
        }
    }

}
