# OmniDragon - Architecture & Codebase Map

Tài liệu này là **Bản đồ kiến trúc tổng thể (Codebase Map)** của toàn bộ hệ sinh thái dự án OmniDragon. Bất kỳ AI Agent hoặc lập trình viên nào khi bắt đầu làm việc cần tham khảo file này để định vị nhanh chóng các thành phần, luồng nghiệp vụ và quy chuẩn kỹ thuật.

---

## 1. Bản Đồ Tổng Thể Hệ Sinh Thái (Ecosystem Map)

```text
G:/New folder (2)/
├── Client/            # Unity C# Client (Game Client 2D)
├── Server/            # Java Netty Game Server (Backend)
├── Database/          # CSDL SQL, Scripts Migration (PostgreSQL / MySQL)
├── GMTool/            # Web quản trị Admin / Game Master (Next.js / TypeScript)
├── Portal/            # Web Portal / Landing Page người chơi (Next.js / React)
├── ARCHITECTURE.md    # Tài liệu bản đồ kiến trúc (File này)
└── .gitignore         # Cấu hình bỏ qua file rác IDE, cache build, Unity Library
```

---

## 2. Chi Tiết Kiến Trúc Client (`Client/Assets/Scripts`)

Toàn bộ mã nguồn Client được tổ chức theo kiến trúc phân tầng (Layered Architecture). **100% các file đều tuân thủ giới hạn < 1.000 dòng** (sử dụng C# `partial class` để module hóa).

### A. Tầng Giao Diện (`UI/`)
* **`UI/Panels/`** - Giao diện bảng chức năng (`public partial class Panel`):
  * `Panel.Inventory.cs`: Túi đồ, trang bị nhân vật, so sánh chỉ số trang bị.
  * `Panel.Shop.cs`: Cửa hàng, ký gửi vật phẩm, mua bán bằng vàng/ngọc.
  * `Panel.Pet.cs`: Quản lý đệ tử, nâng cấp, chỉ số, skill đệ tử.
  * `Panel.Clan.cs`: Bang hội, danh sách thành viên, cống hiến, bang chiến.
  * `Panel.Upgrade.cs`: Nâng cấp, đập đồ, kết hợp trang bị (combine) tại NPC.
  * `Panel.Detail.cs`: Hiển thị bảng tooltip thông tin chi tiết item & skill.
  * `Panel.Skill.cs`: Danh sách chiêu thức nhân vật, cộng điểm tiềm năng.
  * `Panel.Zone.cs` & `Panel.MapTrans.cs`: Chọn khu vực (zone), đổi map tàu bay.
  * `Panel.Mail.cs`, `Panel.Box.cs`, `Panel.KeyTouch.cs`, `Panel.Actions.cs`...
* **`UI/Screens/`** - Các màn hình chính:
  * `GameScr.cs` (12 modules): Màn hình chơi game chính (`GameScr.Skills.cs`, `GameScr.Fields.cs`, `GameScr.Input.cs`, `GameScr.Paint.cs`, `GameScr.Update.cs`...).
  * `LoginScr.cs` (2 modules): Màn hình đăng nhập tài khoản.
  * `ServerListScreen.cs` (2 modules): Màn hình chọn máy chủ.
  * `SplashScr.cs`, `TransportScr.cs`, `mScreen.cs`.
* **`UI/Dialogs/` & `UI/Chat/`**:
  * `Menu.cs`, `ChatPopup.cs`, `InfoDlg.cs`: Các popup thông báo, menu ngữ cảnh.
  * `ChatTextField.cs`: Khung nhập liệu chat thế giới, chat riêng.

### B. Tầng Thực Thể Nghiệp Vụ (`Domain/`)
* **`Domain/Entities/`** - Nhân vật & Thực thể (`public partial class Char`):
  * `Char.Combat.cs`: Xử lý sát thương, tấn công, đòn đánh, hồi sinh, tử vong.
  * `Char.Skills.cs`: Danh sách chiêu thức, gồng chiêu (charge), template skill.
  * `Char.Movement.cs`: Vật lý di chuyển, trọng lực, tốc độ, nhảy, bay, rơi.
  * `Char.Navigation.cs`: Tọa độ waypoint, tìm đường, di chuyển theo điểm đích.
  * `Char.Update.cs` & `Char.UpdateStatus.cs`: Vòng lặp cập nhật trạng thái nhân vật.
  * `Char.Paint.cs`: Render animation, trang bị trên người, body parts.
  * `Char.Effects.cs` & `Char.Buffs.cs`: Hiệu ứng bùa chú, biến hình, hóa đá, trói.
  * `Char.Mount.cs`, `Char.Overhead.cs`, `Char.Fields.cs`...
* **`Domain/Monsters/`** - Quái vật & Boss:
  * `Mob.cs` (3 modules: `Mob.cs`, `Mob.Fields.cs`, `Mob.Update.cs`).
  * `BachTuoc.cs`, `BigBoss2.cs`, `NewBoss.cs`.
* **`Domain/Items/`**: Quản lý vật phẩm (`Item.cs`, `ItemOption.cs`, `ItemTemplate.cs`).
* **`Domain/Skills/`**: Định nghĩa kỹ năng (`Skill.cs`, `SkillPaint.cs`, `SkillOption.cs`).
* **`Domain/Effects/`**: Hiệu ứng đồ họa (`Effect_End.cs` [4 modules], `ServerEffect.cs`).
* **`Domain/Map/`**: Bản đồ ô gạch, vật cản (`TileMap.cs`, `Waypoint.cs`).
* **`Domain/Clan/`** & **`Domain/Tasks/`**: Dữ liệu bang hội và nhiệm vụ.

### C. Tầng Mạng & Kết Nối (`Network/`)
* **`Network/Controller.cs`** (13 modules): Bộ nhận và giải mã gói tin từ Server:
  * `Controller.Msg1.cs` đến `Controller.Msg6.cs`: Xử lý message nhận theo dải opcode.
  * `Controller.SubCommand.cs`: Xử lý các lệnh phụ sub-command.
  * `Controller.Player.cs`, `Controller.Items.cs`, `Controller.Map.cs`, `Controller.Resources.cs`: Helper nghiệp vụ.
* **`Network/Controller2.cs`** (4 modules): Bộ xử lý các gói tin mở rộng phiên bản mới.
* **`Network/Service.cs`** (5 modules): Bộ gửi yêu cầu từ Client lên Server:
  * `Service.Combat.cs`: Gửi lệnh tấn công quái, pk người chơi, dùng chiêu.
  * `Service.Items.cs`: Gửi lệnh nhặt đồ, vứt đồ, mua bán, nâng cấp.
  * `Service.Map.cs`: Gửi lệnh đổi khu, dịch chuyển map, tương tác NPC.
  * `Service.Social.cs`: Gửi tin nhắn chat, kết bạn, xin vào bang.
* **`Network/Transport/`**: Tầng truyền tải socket TCP (`Session_ME.cs`, `Message.cs`, `ISession.cs`).

### D. Tầng Cốt Lõi Hệ Thống (`Core/`)
* **`Core/Input/`**: `GameCanvas.cs` (6 modules) quản lý vòng lặp vẽ, touch, bàn phím, điều hướng dialog.
* **`Core/Graphics/`**: `mGraphics.cs` (2 modules) wrapper đồ họa Unity, vẽ texture, draw region, blend shader.
* **`Core/Audio/`**: `SoundMn.cs` quản lý âm thanh, nhạc nền, hiệu ứng chiêu.
* **`Core/Utils/`**: `mFont.cs`, `Res.cs`, `mVector.cs`, `ScaleGUI.cs`.

### E. Tính Năng Mod Hỗ Trợ (`Features/`)
* **`Features/ModFunc.cs`** (6 modules: `ModFunc.Auto.cs`, `ModFunc.Chat.cs`, `ModFunc.UI.cs`, `ModFunc.Utils.cs`, `ModFunc.Fields.cs`):
  * Tự động luyện tập, auto nhặt đồ, auto hồi sinh, phím tắt mod, chuyển tài khoản nhanh.

---

## 3. Kiến Trúc Backend Server (`Server/`)

* **Nền tảng**: Java 17 + Netty Framework + Maven (`Server/pom.xml`).
* **Cấu trúc chính**:
  * `src/main/java/com/omnidragon/game/`: Logic nhân vật (`User`, `PlayerMovement`, `Info`), chiến đấu (`Mob`, `Skill`), kinh tế (`Shop`).
  * `src/main/java/com/omnidragon/infrastructure/network/`: `MessageHandler`, `Session`, packet services (`PlayerPacketService`, `ItemPacketService`, `ClanPacketService`).
  * `src/main/java/com/omnidragon/infrastructure/database/`: Kết nối và truy vấn CSDL (`SQLStatement`, repositories).
  * `Config/` & `application-dev.yml`: Thiết lập thông số máy chủ, cổng mạng (port), kết nối DB.

---

## 4. Quy Chuẩn Phát Triển & Làm Việc (Development Rules)

Khi bất kỳ AI hoặc Lập trình viên nào phát triển mã nguồn trong dự án này, **BẮT BUỘC** tuân thủ các quy tắc sau:

1. **Giới hạn số dòng file (File Size Limit)**:
   * **Mọi file C# phải luôn dưới 1.000 dòng mã** (ngưỡng lý tưởng: 300 - 800 dòng).
   * Khi thêm logic mới vào một lớp đã lớn, hãy tạo một module partial class mới (ví dụ: `Panel.<Feature>.cs` hoặc `Char.<Feature>.cs`).
2. **Nguyên vẹn file `.meta` của Unity**:
   * Khi tạo file `.cs` hoặc thư mục mới trong `Client/Assets/`, luôn đảm bảo có file `.meta` tương ứng với GUID độc nhất.
3. **Kiểm tra biên dịch trước khi commit**:
   * Chạy lệnh sau để đảm bảo 0 lỗi biên dịch:
     ```powershell
     dotnet build Client\OmniDragon.sln -v q -nologo
     ```
4. **Quy chuẩn Git Commit**:
   * Sử dụng định dạng **Conventional Commits**:
     * `refactor(<scope>): <mô tả ngắn bằng chữ thường>`
     * `feat(<scope>): <mô tả tính năng mới>`
     * `fix(<scope>): <mô tả lỗi đã sửa>`
     * `chore(<scope>): <cấu hình, dọn dẹp file>`
