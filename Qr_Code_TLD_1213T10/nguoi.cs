using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qr_Code_TLD_1213T10
{
    internal class nguoi
    {
        public string maqr { get; set; }
        public string donvi { get; set; }
        public string hoten { get; set; }
        public string gioitinh { get; set; }
        public string chucvu { get; set; }
        public string timecheck { get; set; }

        public nguoi()
        {

        }

        public nguoi(string maqr_, string donvi_, string hoten_, string gioitinh_, string chucvu_, string timecheck_)
        {
            maqr = maqr_;
            donvi = donvi_;
            hoten = hoten_;
            gioitinh = gioitinh_;
            chucvu = chucvu_;
            timecheck = timecheck_;
        }

        public string get_nguoi()
        {
            return maqr + "-" + timecheck;
        }

    }
}
