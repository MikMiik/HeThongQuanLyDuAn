namespace HeThongQuanLyDuAn.Models;

public class NhaThau : DoiTac
{
    private string _linhVuc = string.Empty;

    public string LinhVuc
    {
        get => _linhVuc;
        set => _linhVuc = value?.Trim() ?? string.Empty;
    }

    public override void HienThiThongTin()
    {
        Console.WriteLine("--- NHÀ THẦU ---");
        HienThiThongTinChung();
        Console.WriteLine($"Lĩnh vực: {LinhVuc}");
    }
}