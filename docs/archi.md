# Kiến Trúc & Cấu Trúc Dự Án (`HeThongQuanLyDuan`)

## 1. Cấu trúc thư mục mã nguồn

- **`Views/` (Giao diện):** hiển thị menu Console, nhận dữ liệu nhập từ người dùng và in kết quả. Chỉ gọi xuống `Services/`, không chứa logic nghiệp vụ hay truy xuất dữ liệu.
- **`Services/` (Logic nghiệp vụ):** code logic chính. Giao tiếp với tầng dữ liệu thông qua `Interfaces/`.
- **`Repositories/` (Truy xuất dữ liệu trong Data):**
- **`Enums/`**: chứa enums
- **`Models/` (Tầng Entities):** Định nghĩa các class
- **`Interfaces/`:** chứa interface
- **`Helpers/`:** chứa các tiện ích nếu cần trong quá trình làm
- **`Data/`:** dữ liệu lưu dạng csv/json

Luồng

```text
Program.cs
    │
    ▼
Views
    │
    ▼
Services
    │
    ▼
Repositories
```
