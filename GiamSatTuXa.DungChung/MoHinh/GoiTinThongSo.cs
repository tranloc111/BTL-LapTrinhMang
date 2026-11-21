using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace GiamSatTuXa.DungChung.MoHinh
//{
//    internal class GoiTinThongSo
//    {
//    }
//}
namespace GiamSatTuXa.DungChung.MoHinh
{
    public class GoiTinThongSo
    {
        public string MaThietBi { get; set; } = string.Empty;
        public string TenThietBi { get; set; } = string.Empty;
        public string HeDieuHanh { get; set; } = string.Empty;
        public DateTime ThoiGianGui { get; set; } = DateTime.Now;
        public double PhanTramCpu { get; set; }
        public double PhanTramRam { get; set; }
        public double PhanTramODia { get; set; }
        public long ThoiGianHoatDongGiay { get; set; }
    }
}