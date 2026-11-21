using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace GiamSatTuXa.DungChung.MoHinh
{
    public class KhungGoiTin
    {
        public LoaiGoiTin Loai { get; set; }
        public string NoiDungJson { get; set; } = string.Empty;

        public static KhungGoiTin DongGoi<T>(LoaiGoiTin loai, T duLieu)
        {
            return new KhungGoiTin
            {
                Loai = loai,
                NoiDungJson = JsonSerializer.Serialize(duLieu)
            };
        }

        public T? GiaiMa<T>()
        {
            return JsonSerializer.Deserialize<T>(NoiDungJson);
        }
    }
}