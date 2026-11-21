//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GiamSatTuXa.DungChung.GiaoThuc
//{
//    internal class XuLyGoiTinTcp
//    {
//    }
//}
using System.Buffers.Binary;
using System.Text;
using System.Text.Json;

namespace GiamSatTuXa.DungChung.GiaoThuc
{
    public static class XuLyGoiTinTcp
    {
        // Gửi đối tượng bất kỳ qua luồng mạng (dùng được cho cả NetworkStream và SslStream)
        public static async Task GuiJsonAsync<T>(Stream luongDuLieu, T duLieu, CancellationToken huyBo = default)
        {
            string chuoiJson = JsonSerializer.Serialize(duLieu);
            byte[] mangDuLieu = Encoding.UTF8.GetBytes(chuoiJson);

            byte[] doDaiDauGoi = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(doDaiDauGoi, mangDuLieu.Length);

            await luongDuLieu.WriteAsync(doDaiDauGoi, huyBo);
            await luongDuLieu.WriteAsync(mangDuLieu, huyBo);
            await luongDuLieu.FlushAsync(huyBo);
        }

        // Nhận đối tượng từ luồng mạng, đảm bảo đọc đủ số byte của gói tin
        public static async Task<T?> NhanJsonAsync<T>(Stream luongDuLieu, CancellationToken huyBo = default)
        {
            byte[] doDaiDauGoi = new byte[4];
            if (!await DocDuSoByteAsync(luongDuLieu, doDaiDauGoi, huyBo))
                return default; // Đầu kết nối bên kia đã đóng

            int kichThuocGoi = BinaryPrimitives.ReadInt32BigEndian(doDaiDauGoi);
            if (kichThuocGoi <= 0 || kichThuocGoi > 10 * 1024 * 1024) // Giới hạn tối đa 10MB
                throw new InvalidDataException($"Kích thước gói tin không hợp lệ: {kichThuocGoi}");

            byte[] mangDuLieu = new byte[kichThuocGoi];
            if (!await DocDuSoByteAsync(luongDuLieu, mangDuLieu, huyBo))
                return default;

            string chuoiJson = Encoding.UTF8.GetString(mangDuLieu);
            return JsonSerializer.Deserialize<T>(chuoiJson);
        }

        private static async Task<bool> DocDuSoByteAsync(Stream luongDuLieu, byte[] boDem, CancellationToken huyBo)
        {
            int tongDaDoc = 0;
            while (tongDaDoc < boDem.Length)
            {
                int soByteDocDuoc = await luongDuLieu.ReadAsync(boDem.AsMemory(tongDaDoc, boDem.Length - tongDaDoc), huyBo);
                if (soByteDocDuoc == 0) return false; // Kết nối đã đóng
                tongDaDoc += soByteDocDuoc;
            }
            return true;
        }
    }
}