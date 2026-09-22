# Omni Dragon Online

Chào mừng đến với dự án **Omni Dragon Online**! Cấu trúc dự án đã được tối ưu hóa, chuẩn hóa tên gọi và quét sạch toàn bộ các backdoor, cheat code ẩn và bot tracking độc hại.

---

## 📁 Cấu trúc Thư mục

```
G:\New folder (2)\
├── Client\                # Source Unity Game Client (Project Mod & Build)
├── Server\                # Mã nguồn & Runtime Game Server (Java / Spring Boot)
├── Database\              # Cơ sở dữ liệu chuẩn hóa (PostgreSQL 18) và công cụ sao lưu
├── Portal\                # Cổng Game & Trang Chủ & Nạp Thẻ cho Game Thủ (Port 3000)
└── GMTool\                # Bảng Điều Khiển Quản Trị GM (Add Item, Cải Trang, Giftcode) (Port 3001)
```

---

## 🛠️ Hướng dẫn Khởi chạy & Vận hành

### 1. Database (`Database`)
- Đọc tài liệu chi tiết: `HD_CAI_DAT_DATABASE.txt`.
- Hệ quản trị: **PostgreSQL 18** (Port mặc định: `5432`, User: `postgres`, Password: `postgres`).
- Tạo cơ sở dữ liệu: `omni_dragon`.
- Import file dữ liệu PostgreSQL chuẩn: `omni_dragon_postgres.sql`.
- Sao lưu tự động bất kỳ lúc nào: Chạy file `backup_postgres.bat`.

### 2. Server (`Server`)
- **Tài nguyên Game**: Toàn bộ ảnh quái, map, icon, hiệu ứng nằm tập trung tại thư mục chuẩn `assets/`.
- **Cấu hình DB & Máy chủ**: Chỉnh sửa file `src/main/resources/application-dev.yml` (hoặc `application.yml`).
- **Chạy Server**: Chạy trực tiếp `run.bat` (chạy jar `target/omnidragon.jar`).
- **Build lại Server**: Chạy file `build.bat` (tự động biên dịch ra jar mới với driver PostgreSQL).
- **Tự động giám sát & phục hồi**: Chạy file `autoRun.bat`.
- Thư mục log chuẩn hóa: `logs/`.

### 3. Cổng Game Người Chơi (`Portal` - Port 3000)
- Chạy trực tiếp file `run_portal.bat` để khởi động cổng game (mở tại `http://localhost:3000`).
- Giao diện Gaming Dark Mode đỉnh cao: Tải game APK/PC, nạp thẻ cào / quét QR MoMo tự động, bảng xếp hạng Top sức mạnh & Top nạp, tin tức sự kiện.

### 4. Công Cụ Quản Trị GM (`GMTool` - Port 3001)
- Chạy trực tiếp file `run_gmtool.bat` để khởi động công cụ quản trị (mở tại `http://localhost:3001`).
- Công cụ nội bộ cho Admin: Upload ảnh sprite, thêm cải trang mới, cấu hình item trong shop, quản lý thú cưng, tạo giftcode phát quà.

### 5. Client (`Client`)
- Mở thư mục `Client` bằng **Unity Editor** (khuyên dùng Unity 2022.3 LTS).
- Mở Scene chính tại `Assets/Scenes/`.
- File cấu hình IP Server: [ServerListScreen.cs](file:///G:/New%20folder%20(2)/Client/Assets/Scripts/ServerListScreen.cs) (mặc định đã cấu hình `Omni Dragon:127.0.0.1:14445:0,0,0`).
- Build ra bản cài đặt Android (.apk) hoặc Windows (.exe).

---

## 🛡️ Nhật ký Bảo mật & Tối ưu hóa (Antigravity Audit)
1. **Loại bỏ Malware / Trojan**:
   - Xóa bỏ bộ cài giả mạo `BlueStacks10Installer` bị cài cắm trong Resources của Client.
   - Xóa sạch backdoor script `check_gold.bat` & `autoGoldbar.bat` âm thầm gửi số liệu vàng qua webhook Make.com.
2. **Loại bỏ Backdoor Accounts & Cheats**:
   - Xóa tài khoản ngầm `23688` (user `1`, pass `1`, 2 tỷ thỏi vàng) và `23699` trong database.
   - Gỡ bỏ tập lệnh sát thương ảo bí mật (`_dc`, `_dl`, `_dct`, `_dlt`, `_dcx`, `_dlx`) trong `Player.java`.
   - Vô hiệu hóa endpoint `/get-item` không xác thực trong `ServerController.java` và bật xác thực `SecurityConfig.java`.
   - Vô hiệu hóa tính năng gửi log CCU ngầm tới Telegram Bot hacker và Proxy SOCKS ngoại quốc.
3. **Chuẩn hóa Thương hiệu**:
   - Rebrand đồng bộ toàn diện thành **Omni Dragon Online**.
