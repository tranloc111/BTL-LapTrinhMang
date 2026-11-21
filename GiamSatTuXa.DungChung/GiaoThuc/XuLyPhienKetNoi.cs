using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiamSatTuXa.DungChung.MoHinh;

namespace GiamSatTuXa.DungChung.GiaoThuc
{
    public class XuLyPhienKetNoi
    {
        public PhanHoiGhepNoi XuLyYeuCauGhepNoi(
            YeuCauGhepNoi yeuCau,
            Func<string, bool> kiemTraMaGhepNoiHopLe,
            Action<string, string, string, string> luuThietBiDaGhepNoi)
        {
            if (!yeuCau.NguoiDungDaDongY)
            {
                return new PhanHoiGhepNoi
                {
                    ThanhCong = false,
                    ThongBao = "Người dùng thiết bị chưa xác nhận đồng ý giám sát."
                };
            }

            if (string.IsNullOrWhiteSpace(yeuCau.MaGhepNoi) || !kiemTraMaGhepNoiHopLe(yeuCau.MaGhepNoi))
            {
                return new PhanHoiGhepNoi
                {
                    ThanhCong = false,
                    ThongBao = "Mã ghép nối không hợp lệ hoặc đã được sử dụng."
                };
            }

            string token = Guid.NewGuid().ToString("N");
            luuThietBiDaGhepNoi(yeuCau.MaThietBi, yeuCau.TenThietBi, yeuCau.HeDieuHanh, token);

            return new PhanHoiGhepNoi
            {
                ThanhCong = true,
                ThongBao = "Ghép nối thiết bị thành công.",
                TokenXacThuc = token,
                ChuKyGuiGiay = 5
            };
        }

        public async Task GuiLenhDieuKhienAsync(
            Stream luongDuLieu,
            string maThietBi,
            HanhDongDieuKhien hanhDong,
            string lyDo,
            CancellationToken huyBo = default)
        {
            var lenh = new LenhDieuKhienTin
            {
                MaThietBi = maThietBi,
                HanhDong = hanhDong,
                LyDo = lyDo,
                ThoiGianGui = DateTime.Now
            };

            var goiTin = KhungGoiTin.DongGoi(LoaiGoiTin.LenhDieuKhien, lenh);
            await XuLyGoiTinTcp.GuiJsonAsync(luongDuLieu, goiTin, huyBo);
        }
    }
}
