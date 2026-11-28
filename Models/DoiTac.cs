using HeThongQuanLyDuAn.Exceptions;
using HeThongQuanLyDuAn.Interfaces;

namespace HeThongQuanLyDuAn.Models;

public abstract class DoiTac : IEntity, IKiemTraHopLe
{

    private string _id = string.Empty;
    private string _maSoThue = string.Empty;
    private string _tenDoiTac = string.Empty;
    private string _diaChi = string.Empty;
    private string _soDienThoai = string.Empty;
    private string _email = string.Empty;
    private string _nguoiDaiDien = string.Empty;

    public string Id
    {
        get => _id;
        set => _id = value?.Trim() ?? string.Empty;
    }

    public string MaSoThue
    {
        get => _maSoThue;
        set => _maSoThue = value?.Trim() ?? string.Empty;
    }

    public string TenDoiTac
    {
        get => _tenDoiTac;
        set => _tenDoiTac = value?.Trim() ?? string.Empty;
    }

    public string DiaChi
    {
        get => _diaChi;
        set => _diaChi = value?.Trim() ?? string.Empty;
    }

    public string SoDienThoai
    {
        get => _soDienThoai;
        set => _soDienThoai = value?.Trim() ?? string.Empty;
    }

    public string Email
    {
        get => _email;
        set => _email = value?.Trim() ?? string.Empty;
    }

    public string NguoiDaiDien
    {
        get => _nguoiDaiDien;
        set => _nguoiDaiDien = value?.Trim() ?? string.Empty;
    }

    public virtual void Validate()
    {
        if (string.IsNullOrWhiteSpace(TenDoiTac))
            throw new DomainValidationException("Tên đối tác không được để trống.");

        if (string.IsNullOrWhiteSpace(MaSoThue))
            throw new DomainValidationException("Mã số thuế không được để trống.");

        if (!string.IsNullOrWhiteSpace(Email) && !Email.Contains('@'))
            throw new DomainValidationException("Định dạng email không hợp lệ.");
    }

    public abstract void HienThiThongTin();

    protected void HienThiThongTinChung()
    {
        Console.WriteLine($"Mã: {Id}");
        Console.WriteLine($"Mã số thuế: {MaSoThue}");
        Console.WriteLine($"Tên: {TenDoiTac}");
        Console.WriteLine($"Địa chỉ: {DiaChi}");
        Console.WriteLine($"Điện thoại: {SoDienThoai}");
        Console.WriteLine($"Email: {Email}");
        Console.WriteLine($"Người đại diện: {NguoiDaiDien}");
    }
}