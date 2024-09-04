using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class PrintModel : IDisposable
    {
        private bool _disposed = false;

        /// <summary>
        /// prakiraan berapa baris yang dipakai
        /// </summary>
        public int UsedLine { get; set; } = 0;
        /// <summary>
        /// Deskripsi atau string yang akan diprint
        /// </summary>
        public string PrintDescription { get; set; } = string.Empty;
        /// <summary>
        /// Sisa dari print yang dicetak
        /// </summary>
        public string SisaDeskripsi { get; set; } = string.Empty;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                // kalau pakai source lain yang mau didispose. jangan instance yang singleton tapinya bahayul!
            }
            _disposed = true;
        }
    }
}
