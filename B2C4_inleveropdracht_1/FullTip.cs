using System;
using System.Collections.Generic;
using System.Text;

namespace B2C4_inleveropdracht_1
{
    public class FullTip
    {
        public Hobby hobby { get; set; }
        public Tip tip { get; set; }
        public string link { get; set; }
        public string imageLink { get; set; }
        public string tipInfo { get; set; }

        public FullTip(Hobby _hobby, Tip _tip, string _link, string _image, string _info)
        {
            this.hobby = _hobby;
            this.tip = _tip;
            this.link = _link;
            this.imageLink = _image;
            this.tipInfo = _info;
        }
    }
}
