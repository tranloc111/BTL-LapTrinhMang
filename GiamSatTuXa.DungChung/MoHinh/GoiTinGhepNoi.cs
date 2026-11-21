using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiamSatTuXa.DungChung.MoHinh
{
    public class YeuCauGhepNoi
    {
        public string MaGhepNoi { get; set; } = string.Empty;
        public string MaThietBi { get; set; } = string.Empty;
        public string TenThietBi { get; set; } = string.Empty;
        public string HeDieuHanh { get; set; } = string.Empty;
        public bool NguoiDungDaDongY { get; set; }
    }

    public class PhanHoiGhepNoi
    {
        public bool ThanhCong { get; set; }
        public string ThongBao { get; set; } = string.Empty;
        public string TokenXacThuc { get; set; } = string.Empty;
        public int ChuKyGuiGiay { get; set; } = 5;
    }
}
