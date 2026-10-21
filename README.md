# TinyOcean – Game phiêu lưu VR dưới biển

Game VR phiêu lưu dưới biển, làm bằng Unity 6. Người chơi thu thập sao biển ở hai màn chơi, dùng chìa khóa mở rương và tìm hiểu các loài cá.

- **Repo GitHub:** https://github.com/dxoy3n/VRprj-2026-10-04_21-04-30
- **Bản web demo:** https://tinyocean.netlify.app/
- **Nhóm thực hiện:** Nhóm 7 – Lớp 23CDP1 – Trường Đại học Kiến trúc Hà Nội

---

## 1. Yêu cầu hệ thống

| Thành phần | Phiên bản |
|---|---|
| Unity Editor | **6000.0.81f1** (Unity 6) |
| Universal Render Pipeline (URP) | 17.0.4 |
| Hệ điều hành | Windows 10/11 (khuyến nghị) |
| Git | Bản mới nhất |
| IDE (tùy chọn) | Visual Studio 2022 hoặc JetBrains Rider |

**Phần cứng:**
- Không cần kính VR để chạy thử. Dự án được kiểm tra bằng **XR Device Simulator**.
- Để chạy trên kính thật: Meta Quest (hỗ trợ OpenXR).

**Module cần cài trong Unity Hub** (chỉ khi build cho Android/Quest):
- Android Build Support (kèm OpenJDK, Android SDK & NDK Tools)

---

## 2. Các package chính

Các package được khai báo trong `Packages/manifest.json`. Unity sẽ tự tải khi mở project:

- XR Interaction Toolkit 3.0.11
- Unity OpenXR 1.16.1 và Meta OpenXR 2.6.1
- XR Hands 1.7.3
- Input System 1.19.0
- TextMeshPro (có sẵn trong Unity)
- Polybrush 1.2.1
- Timeline 1.8.12
- AI Navigation 2.0.13
- glTFast 6.20.0

---

## 3. Cài đặt

1. **Clone project:**
   ```bash
   git clone https://github.com/dxoy3n/VRprj-2026-10-04_21-04-30.git
   ```
2. Mở **Unity Hub**, vào **Installs**, và cài **Unity 6000.0.81f1** nếu chưa có.
3. Trong Unity Hub, chọn **Projects → Add → Add project from disk**, rồi trỏ tới thư mục vừa clone.
4. Mở project bằng Unity **6000.0.81f1**. Lần mở đầu tiên sẽ mất vài phút để import và tải package.
5. Vào **Window → Package Manager** và kiểm tra không còn lỗi đỏ. Nếu có, nhấn **Resolve** hoặc **Install missing packages**.
6. Vào **Edit → Project Settings → XR Plug-in Management** và đảm bảo **OpenXR** đã bật cho Standalone (PC).

> Thư mục `Library/` không được đưa lên GitHub nên lần mở đầu sẽ lâu hơn bình thường. Đây là hiện tượng bình thường.

---

## 4. Chạy thử bằng XR Device Simulator

1. Trong Project, mở scene **`Assets/Scenes/1 Start Scene.unity`**.
2. Nhấn **Play** trên Unity Editor.
3. Điều khiển XR Device Simulator (mặc định): **WASD** để di chuyển, **chuột** để xoay góc nhìn.
4. Trên PC, dùng **chuột trái** để tương tác với sao biển và con cá.

**Điều khiển trên kính VR (theo code):**

| Nút | Chức năng |
|---|---|
| Nút **B** (tay phải) | Tắt bảng hướng dẫn |
| Nút **X** (tay trái) | Thoát game |

---

## 5. Cấu trúc scene và luồng chơi

Thứ tự scene trong Build Settings:

| # | Scene | Mục tiêu |
|---|---|---|
| 1 | `1 Start Scene` (StartMenu) | Menu chính. Nút **Start** xóa tiến trình cũ và vào Level 1 |
| 2 | `Level1_TinyOcean` | Thu thập **3 sao biển** để mở đường sang Level 2 |
| 3 | `Level2_TinyOcean` | Thu thập thêm **3 sao biển** (tổng **6**) để nhận chìa khóa và mở rương |

Tiến trình được lưu bằng `PlayerPrefs`.

---

## 6. Cấu trúc thư mục chính

```
Assets/
├── Scenes/          # Scene menu, Level 1, Level 2
├── Scripts/         # Toàn bộ script gameplay
├── Settings/        # URP asset, Volume profile
├── XR/              # Cấu hình XR, XR Simulation
└── XRI/             # Cấu hình XR Interaction Toolkit
ProjectSettings/     # Cấu hình Unity
Packages/            # manifest.json và packages-lock.json
```

---

## 7. Build

**PC (Windows):**
1. Vào **File → Build Profiles**.
2. Chọn **Windows** rồi nhấn **Build**.

**Android (Meta Quest):**
1. Vào **File → Build Profiles**, chọn **Android**, rồi nhấn **Switch Platform**.
2. Bật Meta Quest trong **XR Plug-in Management → Android**.
3. Kết nối kính qua USB, bật Developer Mode, rồi nhấn **Build and Run**.

---

## 8. Lỗi thường gặp

| Lỗi | Cách xử lý |
|---|---|
| Package bị lỗi đỏ khi mở project | Window → Package Manager → Resolve, hoặc kiểm tra đúng phiên bản Unity |
| Không thấy XR Device Simulator khi Play | Kiểm tra XR Plug-in Management đã bật OpenXR và XR Interaction Toolkit đã cài |
| Bấm Play nhưng không vào được Level 1 | Kiểm tra cả ba scene đã có trong Build Settings |
| Không nhặt được sao biển khi test trên PC | Dùng chuột trái và đảm bảo camera đang nhìn trúng vật thể |
| Tiến trình cũ vẫn còn | Vào lại menu và nhấn **Start** để xóa `PlayerPrefs` |

---

## 9. Tác giả & giấy phép

- **Tác giả:** Nhóm 7 – Lớp 23CDP1 – Trường Đại học Kiến trúc Hà Nội
- **Giấy phép:** Chưa có. Mọi quyền thuộc về nhóm tác giả. Người khác chưa được phép sao chép hay sử dụng lại mã và tài nguyên nếu chưa có sự đồng ý.

---

# TinyOcean – VR Underwater Adventure (English)

A VR underwater adventure game built with Unity 6. Players collect starfish across two levels, use a key to open a chest, and learn about different fish species.

- **GitHub repository:** https://github.com/dxoy3n/VRprj-2026-10-04_21-04-30
- **Web demo:** https://tinyocean.netlify.app/
- **Team:** Group 7 – Class 23CDP1 – Hanoi University of Architecture

---

## 1. Requirements

| Component | Version |
|---|---|
| Unity Editor | **6000.0.81f1** (Unity 6) |
| Universal Render Pipeline (URP) | 17.0.4 |
| Operating system | Windows 10/11 (recommended) |
| Git | Latest version |
| IDE (optional) | Visual Studio 2022 or JetBrains Rider |

**Hardware:**
- A VR headset is not required to run the project. It is tested with the **XR Device Simulator**.
- To run on a real headset: Meta Quest (OpenXR supported).

**Unity Hub module** (only needed for Android/Quest builds):
- Android Build Support (includes OpenJDK, Android SDK & NDK Tools)

---

## 2. Main packages

Declared in `Packages/manifest.json`. Unity downloads them automatically when the project opens:

- XR Interaction Toolkit 3.0.11
- Unity OpenXR 1.16.1 and Meta OpenXR 2.6.1
- XR Hands 1.7.3
- Input System 1.19.0
- TextMeshPro (built into Unity)
- Polybrush 1.2.1
- Timeline 1.8.12
- AI Navigation 2.0.13
- glTFast 6.20.0

---

## 3. Installation

1. **Clone the project:**
   ```bash
   git clone https://github.com/dxoy3n/VRprj-2026-10-04_21-04-30.git
   ```
2. Open **Unity Hub**, go to **Installs**, and install **Unity 6000.0.81f1** if you don't have it.
3. In Unity Hub, choose **Projects → Add → Add project from disk**, then select the cloned folder.
4. Open the project with Unity **6000.0.81f1**. The first import takes a few minutes.
5. Go to **Window → Package Manager** and check that there are no red errors. If there are, click **Resolve** or **Install missing packages**.
6. Go to **Edit → Project Settings → XR Plug-in Management** and make sure **OpenXR** is enabled for Standalone (PC).

> The `Library/` folder is not stored on GitHub, so the first open will be slower than usual. This is normal.

---

## 4. Testing with the XR Device Simulator

1. In the Project window, open **`Assets/Scenes/1 Start Scene.unity`**.
2. Press **Play** in the Unity Editor.
3. Control the XR Device Simulator (default): **WASD** to move, **mouse** to look around.
4. On PC, use the **left mouse button** to interact with starfish and fish.

**VR controls (from the code):**

| Button | Function |
|---|---|
| **B** button (right hand) | Hide the instructions panel |
| **X** button (left hand) | Quit the game |

---

## 5. Scenes and game flow

Scene order in Build Settings:

| # | Scene | Goal |
|---|---|---|
| 1 | `1 Start Scene` (StartMenu) | Main menu. The **Start** button clears previous progress and loads Level 1 |
| 2 | `Level1_TinyOcean` | Collect **3 starfish** to unlock the way to Level 2 |
| 3 | `Level2_TinyOcean` | Collect **3 more starfish** (6 in total) to get the key and open the chest |

Progress is saved with `PlayerPrefs`.

---

## 6. Main folder structure

```
Assets/
├── Scenes/          # Menu, Level 1, and Level 2 scenes
├── Scripts/         # All gameplay scripts
├── Settings/        # URP asset, Volume profiles
├── XR/              # XR configuration, XR Simulation
└── XRI/             # XR Interaction Toolkit configuration
ProjectSettings/     # Unity project settings
Packages/            # manifest.json and packages-lock.json
```

---

## 7. Building

**PC (Windows):**
1. Go to **File → Build Profiles**.
2. Select **Windows** and click **Build**.

**Android (Meta Quest):**
1. Go to **File → Build Profiles**, select **Android**, and click **Switch Platform**.
2. Enable Meta Quest in **XR Plug-in Management → Android**.
3. Connect the headset via USB, enable Developer Mode, and click **Build and Run**.

---

## 8. Common issues

| Problem | Solution |
|---|---|
| Red package errors when opening the project | Window → Package Manager → Resolve, or check that you are using the correct Unity version |
| XR Device Simulator does not appear on Play | Check that OpenXR is enabled in XR Plug-in Management and XR Interaction Toolkit is installed |
| Pressing Play does not load Level 1 | Check that all three scenes are in Build Settings |
| Cannot collect starfish on PC | Use the left mouse button and make sure the camera is aimed at the object |
| Old progress still appears | Return to the menu and press **Start** to clear `PlayerPrefs` |

---

## 9. Authors & license

- **Authors:** Group 7 – Class 23CDP1 – Hanoi University of Architecture
- **License:** None yet. All rights belong to the authors. Others may not copy or reuse the code or assets without permission.
