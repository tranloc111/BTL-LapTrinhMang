using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiamSatTuXa.DungChung.MoHinh
{
    public enum HanhDongDieuKhien
    {
        TamDung = 1,
        TiepTuc = 2,
        ThuHoiQuyen = 3
    }

    public class LenhDieuKhienTin
    {
        public string MaThietBi { get; set; } = string.Empty;
        public HanhDongDieuKhien HanhDong { get; set; }
        public string LyDo { get; set; } = string.Empty;
        public DateTime ThoiGianGui { get; set; } = DateTime.Now;
    }
}
