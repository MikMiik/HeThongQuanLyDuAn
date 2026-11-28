using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using HeThongQuanLyDuAn.Exceptions;
using HeThongQuanLyDuAn.Interfaces;

namespace HeThongQuanLyDuAn.Repositories;

public class JsonRepository<T> : IRepository<T> where T : class, IEntity
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, 
        Converters = { new JsonStringEnumConverter() }         
    };

    private readonly string _filePath;
    private readonly object _lock = new();

    public JsonRepository(string folderPath)
    {
        Directory.CreateDirectory(folderPath);
        _filePath = Path.Combine(folderPath, typeof(T).Name + ".json");
    }

    public List<T> GetAll()
    {
        lock (_lock)
        {
            return Load();
        }
    }

    public T? GetById(string id)
    {
        lock (_lock)
        {
            return Load().FirstOrDefault(e => SameId(e.Id, id));
        }
    }

    public void Add(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        lock (_lock)
        {
            entity.Id = (entity.Id ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(entity.Id))
                throw new DomainValidationException("Mã định danh không được để trống.");

            if (entity is IKiemTraHopLe kiemTra)
                kiemTra.Validate();

            var list = Load();
            if (list.Any(e => SameId(e.Id, entity.Id)))
                throw new DuLieuTrungLapException("Mã", entity.Id);

            list.Add(entity);
            Save(list);
        }
    }

    public void Update(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        lock (_lock)
        {
            entity.Id = (entity.Id ?? string.Empty).Trim();

            if (entity is IKiemTraHopLe kiemTra)
                kiemTra.Validate();

            var list = Load();
            int index = list.FindIndex(e => SameId(e.Id, entity.Id));
            if (index < 0)
                throw new DomainValidationException(
                    $"Không tìm thấy {typeof(T).Name} có mã '{entity.Id}' để cập nhật.");

            list[index] = entity;
            Save(list);
        }
    }

    public bool Delete(string id)
    {
        lock (_lock)
        {
            var list = Load();
            int removed = list.RemoveAll(e => SameId(e.Id, id));
            if (removed == 0) return false;

            Save(list);
            return true;
        }
    }

    private static bool SameId(string? a, string? b) =>
        string.Equals(a?.Trim(), b?.Trim(), StringComparison.OrdinalIgnoreCase);

    private List<T> Load()
    {
        if (!File.Exists(_filePath)) return new List<T>();

        string json = File.ReadAllText(_filePath);
        if (string.IsNullOrWhiteSpace(json)) return new List<T>();

        try
        {
            return JsonSerializer.Deserialize<List<T>>(json, Options) ?? new List<T>();
        }
        catch (JsonException ex)
        {
            throw new DomainValidationException(
                $"Tệp dữ liệu '{Path.GetFileName(_filePath)}' bị lỗi định dạng JSON.", ex);
        }
    }

    private void Save(List<T> list)
    {
        string json = JsonSerializer.Serialize(list, Options);
        string tempPath = _filePath + ".tmp";
        File.WriteAllText(tempPath, json);
        File.Move(tempPath, _filePath, overwrite: true);
    }
}