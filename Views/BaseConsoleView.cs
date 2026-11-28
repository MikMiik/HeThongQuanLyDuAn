using System.Globalization;
using HeThongQuanLyDuAn.Interfaces;

namespace HeThongQuanLyDuAn.Views;

public abstract class BaseConsoleView : IView
{
    protected const string DateFormat = "dd/MM/yyyy";

    public abstract void ShowMenu();


    public virtual void ShowMessage(string msg)
    {
        WriteColor(msg, ConsoleColor.Green);
    }

    public virtual void ShowError(string err)
    {
        WriteColor("[LỖI] " + err, ConsoleColor.Red);
    }

    protected void ShowWarning(string msg)
    {
        WriteColor("[CẢNH BÁO] " + msg, ConsoleColor.Yellow);
    }

    protected void ShowTitle(string title)
    {
        Console.WriteLine();
        string line = new string('=', Math.Max(title.Length + 4, 40));
        WriteColor(line, ConsoleColor.Cyan);
        WriteColor("  " + title.ToUpper(), ConsoleColor.Cyan);
        WriteColor(line, ConsoleColor.Cyan);
    }


    protected string ReadString(string prompt, bool allowEmpty = false)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            string value = (Console.ReadLine() ?? string.Empty).Trim();

            if (value.Length > 0 || allowEmpty) return value;
            ShowError("Không được để trống.");
        }
    }

    protected int ReadInt(string prompt)
    {
        while (true)
        {
            string s = ReadString(prompt);
            if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out int v))
                return v;
            ShowError("Vui lòng nhập một số nguyên hợp lệ.");
        }
    }

    protected int ReadMenuChoice(int min, int max, string prompt = "Chọn chức năng")
    {
        while (true)
        {
            int choice = ReadInt(prompt);
            if (choice >= min && choice <= max) return choice;
            ShowError($"Lựa chọn phải nằm trong khoảng {min} - {max}.");
        }
    }

    protected decimal ReadDecimal(string prompt)
    {
        while (true)
        {
            string s = ReadString(prompt).Replace(",", "").Replace(" ", "");
            if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal v))
                return v;
            ShowError("Vui lòng nhập một số hợp lệ (ví dụ: 1500000 hoặc 1500000.5).");
        }
    }

    protected DateTime ReadDate(string prompt)
    {
        while (true)
        {
            string s = ReadString($"{prompt} ({DateFormat})");
            if (DateTime.TryParseExact(s, DateFormat, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime d))
                return d;
            ShowError($"Ngày không hợp lệ. Định dạng đúng: {DateFormat}.");
        }
    }

    protected bool Confirm(string prompt)
    {
        while (true)
        {
            string s = ReadString($"{prompt} (y/n)").ToLowerInvariant();
            if (s is "y" or "yes" or "c" or "co" or "có") return true;
            if (s is "n" or "no" or "k" or "khong" or "không") return false;
            ShowError("Vui lòng nhập y hoặc n.");
        }
    }

    protected void Pause()
    {
        Console.WriteLine();
        Console.Write("Nhấn Enter để tiếp tục...");
        Console.ReadLine();
    }

    protected void ShowTable(string[] headers, List<string[]> rows)
    {
        if (rows.Count == 0)
        {
            ShowWarning("Chưa có dữ liệu.");
            return;
        }

        int[] widths = headers.Select(h => h.Length).ToArray();
        foreach (var row in rows)
            for (int i = 0; i < headers.Length && i < row.Length; i++)
                widths[i] = Math.Max(widths[i], (row[i] ?? string.Empty).Length);

        string separator = "+" + string.Join("+", widths.Select(w => new string('-', w + 2))) + "+";

        Console.WriteLine(separator);
        Console.WriteLine(FormatRow(headers, widths));
        Console.WriteLine(separator);
        foreach (var row in rows)
            Console.WriteLine(FormatRow(row, widths));
        Console.WriteLine(separator);
    }

    private static string FormatRow(string[] cells, int[] widths)
    {
        var parts = new string[widths.Length];
        for (int i = 0; i < widths.Length; i++)
        {
            string cell = i < cells.Length ? (cells[i] ?? string.Empty) : string.Empty;
            parts[i] = " " + cell.PadRight(widths[i]) + " ";
        }
        return "|" + string.Join("|", parts) + "|";
    }

    private static void WriteColor(string text, ConsoleColor color)
    {
        var old = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = old;
    }
}