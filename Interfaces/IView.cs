namespace HeThongQuanLyDuAn.Interfaces
{
    public interface IView
    {
        void ShowMenu();
        void ShowMessage(string msg);
        void ShowError(string err);
    }
}