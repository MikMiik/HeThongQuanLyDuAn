using HeThongQuanLyDuAn.Enums;
using HeThongQuanLyDuAn.Exceptions;
using HeThongQuanLyDuAn.Interfaces;

namespace HeThongQuanLyDuAn.Models;

public class DuAn : IEntity, IKiemTraHopLe
{
    private string _id = string.Empty;
    private string _tenDuAn = string.Empty;
    private DateTime _ngayBatDau;
    private DateTime _ngayKetThuc;
    private string _diaDiem = string.Empty;
    private TrangThaiDuAn _trangThai;
    private decimal _tongVonDuToan;
    private string _maChuDauTu = string.Empty;
    private string _maPhong = string.Empty;

    public string Id
    {
        get => _id;
        set => _id = value?.Trim() ?? string.Empty;
    }

    public string TenDuAn
    {
        get => _tenDuAn;
        set => _tenDuAn = value?.Trim() ?? string.Empty;
    }

    public DateTime NgayBatDau
    {
        get => _ngayBatDau;
        set => _ngayBatDau = value;
    }

    public DateTime NgayKetThuc
    {
        get => _ngayKetThuc;
        set => _ngayKetThuc = value;
    }

    public string DiaDiem
    {
        get => _diaDiem;
        set => _diaDiem = value?.Trim() ?? string.Empty;
    }

    public TrangThaiDuAn TrangThai
    {
        get => _trangThai;
        set => _trangThai = value;
    }

    public decimal TongVonDuToan
    {
        get => _tongVonDuToan;
        set => _tongVonDuToan = value;
    }

    /// <summary>Mã chủ đầu tư của dự án (bắt buộc).</summary>
    public string MaChuDauTu
    {
        get => _maChuDauTu;
        set => _maChuDauTu = value?.Trim() ?? string.Empty;
    }

    public string MaPhong
    {
        get => _maPhong;
        set => _maPhong = value?.Trim() ?? string.Empty;
    }

  
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(TenDuAn))
            throw new DuAnKhongHopLeException("Tên dự án không được để trống.");

        if (NgayBatDau == default || NgayKetThuc == default)
            throw new DuAnKhongHopLeException("Ngày bắt đầu và ngày kết thúc phải được nhập.");

        if (NgayKetThuc < NgayBatDau)
            throw new DuAnKhongHopLeException("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu.");

        if (TongVonDuToan <= 0)
            throw new DuAnKhongHopLeException("Tổng vốn dự toán phải lớn hơn 0.");

        if (string.IsNullOrWhiteSpace(MaChuDauTu))
            throw new DuAnKhongHopLeException("Dự án phải có chủ đầu tư.");

        if (string.IsNullOrWhiteSpace(MaPhong))
            throw new DuAnKhongHopLeException("Dự án phải thuộc một phòng ban giám sát.");

        if (!Enum.IsDefined(TrangThai))
            throw new DuAnKhongHopLeException("Trạng thái dự án không hợp lệ.");
    }
}