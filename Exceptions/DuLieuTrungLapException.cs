using System;

namespace HeThongQuanLyDuAn.Exceptions
{
   
    public class DuLieuTrungLapException : DomainValidationException
    {
        public string TenTruong { get; }
        public string GiaTri { get; }

        public DuLieuTrungLapException(string tenTruong, string giaTri)
            : base($"Dữ liệu trùng lặp: {tenTruong} '{giaTri}' đã tồn tại trong hệ thống.")
        {
            TenTruong = tenTruong;
            GiaTri = giaTri;
        }
    }
}