using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace GiamSatTuXa.DungChung.BaoMat
{
    public static class BaoMatTls
    {
        public static async Task<SslStream> TaoLuongMayChuAsync(
            TcpClient client,
            X509Certificate2 chungChiMayChu,
            CancellationToken huyBo = default)
        {
            var sslStream = new SslStream(client.GetStream(), leaveInnerStreamOpen: false);
            await sslStream.AuthenticateAsServerAsync(
                chungChiMayChu,
                clientCertificateRequired: false,
                checkCertificateRevocation: false);
            return sslStream;
        }

        public static async Task<SslStream> TaoLuongMayKhachAsync(
            TcpClient client,
            string tenMayChu,
            bool chapNhanChungChiTuKy = true,
            CancellationToken huyBo = default)
        {
            var sslStream = new SslStream(
                client.GetStream(),
                leaveInnerStreamOpen: false,
                userCertificateValidationCallback: (_, _, _, errors) =>
                {
                    if (errors == SslPolicyErrors.None) return true;
                    return chapNhanChungChiTuKy;
                });

            await sslStream.AuthenticateAsClientAsync(tenMayChu);
            return sslStream;
        }
    }
}