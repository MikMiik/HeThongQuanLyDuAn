namespace HeThongQuanLyDuAn.Exceptions;

public class DuAnKhongHopLeException : DomainValidationException
{
    public DuAnKhongHopLeException(string message) : base(message) { }

    public DuAnKhongHopLeException(string message, Exception innerException)
        : base(message, innerException) { }
}