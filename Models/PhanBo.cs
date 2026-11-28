using HeThongQuanLyDuAn.Enums;
using HeThongQuanLyDuAn.Exceptions;
using HeThongQuanLyDuAn.Interfaces;

namespace HeThongQuanLyDuAn.Models;

public class PhanBo : IEntity, IKiemTraHopLe
{
    private string _id = string.Empty;
    private string _maDuAn = string.Empty;
    private string _maThanhVien = string.Empty;
    private VaiTroPhanBo _vaiTro;
    private DateTime _ngayBatDau;

    public string Id
    {
        get => _id;
        set => _id = value?.Trim() ?? string.Empty;
    }

    public string MaDuAn
    {
        get => _maDuAn;
        set => _maDuAn = value?.Trim() ?? string.Empty;
    }

    public string MaThanhVien
    {
        get => _maThanhVien;
        set => _maThanhVien = value?.Trim() ?? string.Empty;
    }

    public VaiTroPhanBo VaiTro
    {
        get => _vaiTro;
        set => _vaiTro = value;
    }

    public DateTime NgayBatDau
    {
        get => _ngayBatDau;
        set => _ngayBatDau = value;
    }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(MaThanhVien) || string.IsNullOrWhiteSpace(MaDuAn))
            throw new DomainValidationException("Phải chọn Thành viên và Dự án để phân bổ.");

        if (!Enum.IsDefined(VaiTro))
            throw new DomainValidationException("Vai trò phân bổ không hợp lệ.");

        if (NgayBatDau == default)
            throw new DomainValidationException("Ngày bắt đầu tham gia không hợp lệ.");
    }
}