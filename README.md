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
- Dự án được kiểm tra bằng **XR Device Simulator**.
- Để chạy trên kính thật: Meta Quest 2 (hoặc các model Meta Quest hỗ trợ OpenXR).

**Module cần cài trong Unity Hub** (chỉ cần khi build tương ứng):
- Windows Build Support (để build bản PC)
- WebGL Build Support (để build bản web)
- Android Build Support (kèm OpenJDK, Android SDK & NDK Tools, chỉ khi build cho Android/Quest)

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
2. Bật OpenXR và Meta Quest feature group trong **XR Plug-in Management → Android**.
3. Đặt Minimum API Level = 32 và kiểm tra Application Identifier trong **Player Settings**.
4. Chọn một trong hai cách:
   - **Build**: chỉ xuất file `.apk`, dùng để cài thủ công (xem mục 8).
   - **Build and Run**: build và cài trực tiếp khi kính đã kết nối USB và đã bật Developer Mode.

---

## 8. Cài đặt và chạy APK trên Meta Quest 2

### 8.1. Chuẩn bị

- Kính Meta Quest 2 đã cập nhật lên phiên bản Horizon OS mới nhất.
- Điện thoại đã cài app **Meta Horizon**, đăng nhập cùng tài khoản Meta với kính.
- Cáp USB-C **có truyền dữ liệu** (không phải cáp chỉ dùng để sạc).
- Máy tính Windows đã cài một trong các công cụ sau:
  - **ADB** (Android platform-tools), dùng dòng lệnh. Có sẵn trong `...\Unity\Hub\Editor\6000.0.81f1\Editor\Data\PlaybackEngines\AndroidPlayer\SDK\platform-tools\`.
  - Hoặc **Meta Quest Developer Hub**.
  - Hoặc **SideQuest**.
- File `TinyOcean_Quest2.apk` đã build theo mục 7.

### 8.2. Bật Developer Mode

Developer Mode cho phép cài ứng dụng từ bên ngoài Meta Store.

1. Trên điện thoại, mở app **Meta Horizon**.
2. Chọn biểu tượng kính (hoặc menu thiết bị), chọn **Quest 2** của bạn.
3. Vào **Cài đặt thiết bị** (Device settings), tìm mục **Chế độ nhà phát triển** (Developer Mode) và bật.
4. Nếu app yêu cầu tạo tổ chức developer, làm theo hướng dẫn trên màn hình. Việc này chỉ cần làm một lần.

> Tên menu có thể khác đôi chút tùy phiên bản app. Nếu không thấy, tìm kiếm từ khóa "Developer Mode" trong phần cài đặt của app.

### 8.3. Kết nối kính với máy tính

1. Cắm Quest 2 vào máy tính bằng cáp USB.
2. Trong kính, khi có hộp thoại yêu cầu USB debugging, chọn **Allow** (Cho phép). Nên tích **Always allow from this computer** để lần sau không phải xác nhận lại.
3. Mở Command Prompt hoặc PowerShell, chạy:
   ```
   adb devices
   ```
4. Kết quả đúng là một dòng có serial của kính và chữ `device`:
   ```
   1WMHH000XXXXXX    device
   ```
   - Nếu hiện `unauthorized`: tháo cáp, cắm lại, rồi xác nhận trong kính.
   - Nếu không thấy thiết bị nào: đổi cáp hoặc cổng USB, đảm bảo kính không ở chế độ ngủ.

### 8.4. Cài APK

**Cách 1: dùng adb (khuyến nghị)**
```
adb install -r "D:\Builds\TinyOcean\TinyOcean_Quest2.apk"
```
- `-r` ghi đè bản cũ nếu đã cài cùng package name.
- Kết quả thành công hiện chữ `Success`.

**Cách 2: dùng Unity**
Trong **Build Profiles → Android**, chọn **Build and Run** khi kính đã kết nối.

**Cách 3: dùng Meta Quest Developer Hub hoặc SideQuest**
Kéo thả file `.apk` vào cửa sổ của công cụ và chọn cài đặt.

**Nếu bị lỗi khi cài:**
- `INSTALL_FAILED_UPDATE_INCOMPATIBLE`: bản đã cài có chữ ký khác. Gỡ bản cũ trước:
  ```
  adb uninstall com.tenban.tinyocean
  ```
  (Thay tên package bằng Application Identifier bạn đã đặt trong Unity.)
- `INSTALL_FAILED_INSUFFICIENT_STORAGE`: kính hết bộ nhớ. Xóa bớt ứng dụng không dùng trong **Settings → Storage**.

### 8.5. Mở game

1. Đeo kính, vào **Thư viện** (Library).
2. Ở phần bộ lọc (Filter) phía trên, chọn **Unknown Sources** (Nguồn không xác định).
3. Chọn **TinyOcean** và mở.

> Lần đầu vào, Quest có thể yêu cầu thiết lập **Guardian** (ranh giới an toàn). Chọn không gian đủ rộng, hoặc chọn chế độ ngồi nếu không có nhiều chỗ.

### 8.6. Điều khiển trên kính

| Thao tác | Chức năng |
|---|---|
| Tia điều khiển từ tay cầm + nút **Trigger** | Chọn sao biển, con cá, nút trên menu |
| Nút **B** (tay phải) | Tắt bảng hướng dẫn |
| Nút **X** (tay trái) | Thoát game |
| Cần analog (joystick) | Di chuyển và xoay góc nhìn, tùy cấu hình trong game |

### 8.7. Gỡ cài đặt

- **Trong kính:** vào **Settings → Apps** (hoặc Library), chọn **TinyOcean**, bấm **Uninstall**.
- **Bằng adb:**
  ```
  adb uninstall com.tenban.tinyocean
  ```

Để xóa tiến trình chơi (sao biển, rương) mà không gỡ app, vào menu chính và nhấn **Start**.

### 8.8. Lưu ý khi test trên kính

- **Bản Development Build** có log chi tiết và cho phép kết nối debug. Bản phát hành nên build ở chế độ Release.
- **Hiệu năng, tay cầm, độ rõ hình ảnh và hiệu ứng** chỉ đánh giá chính xác khi chạy trên kính thật, không thể thay thế bằng XR Device Simulator.
- Khi sửa code và muốn test lại, chỉ cần build APK mới rồi cài lại bằng lệnh `adb install -r` ở mục 8.4.
- Không chia sẻ APK ra ngoài nhóm khi chưa có sự đồng ý của nhóm tác giả.

---

## 9. Lỗi thường gặp

| Lỗi | Cách xử lý |
|---|---|
| Package bị lỗi đỏ khi mở project | Window → Package Manager → Resolve, hoặc kiểm tra đúng phiên bản Unity |
| Không thấy XR Device Simulator khi Play | Kiểm tra XR Plug-in Management đã bật OpenXR và XR Interaction Toolkit đã cài |
| Bấm Play nhưng không vào được Level 1 | Kiểm tra cả ba scene đã có trong Build Settings |
| Không nhặt được sao biển khi test trên PC | Dùng chuột trái và đảm bảo camera đang nhìn trúng vật thể |
| Tiến trình cũ vẫn còn | Vào lại menu và nhấn **Start** để xóa `PlayerPrefs` |
| `adb devices` không thấy kính | Đổi cáp USB, kiểm tra Developer Mode đã bật, kiểm tra kính đã chọn Allow |
| `adb devices` hiện `unauthorized` | Tháo cáp, cắm lại, và xác nhận hộp thoại trong kính |
| Cài APK xong không thấy game trong Library | Chọn bộ lọc **Unknown Sources** trong Thư viện |
| Game đen màn hình hoặc không vào VR | Kiểm tra lại OpenXR Loader và Meta Quest feature group ở mục 7 |
| Game crash ngay khi mở | Chạy `adb logcat -s Unity` khi mở game và gửi log để kiểm tra |

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
- To run on a real headset: Meta Quest 2 (or other Meta Quest models supporting OpenXR).

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
2. Enable OpenXR and the Meta Quest feature group in **XR Plug-in Management → Android**.
3. Set Minimum API Level = 32 and check the Application Identifier in **Player Settings**.
4. Choose one of the following:
   - **Build**: only exports an `.apk` file, which you install manually (see section 8).
   - **Build and Run**: builds and installs directly when the headset is connected via USB and Developer Mode is enabled.

---

## 8. Installing and running the APK on Meta Quest 2

### 8.1. Preparation

- Meta Quest 2 updated to the latest Horizon OS version.
- A phone with the **Meta Horizon** app, signed in with the same Meta account as the headset.
- A USB-C cable **that transfers data** (not a charge-only cable).
- A Windows computer with one of the following tools:
  - **ADB** (Android platform-tools), used from the command line. It is included in `...\Unity\Hub\Editor\6000.0.81f1\Editor\Data\PlaybackEngines\AndroidPlayer\SDK\platform-tools\`.
  - Or **Meta Quest Developer Hub**.
  - Or **SideQuest**.
- The `TinyOcean_Quest2.apk` file built following section 7.

> Do not upload the `.apk` file to GitHub. It is large and is not source code. Share it directly through Google Drive or a USB drive.

### 8.2. Enable Developer Mode

Developer Mode allows installing apps from outside the Meta Store.

1. On your phone, open the **Meta Horizon** app.
2. Select the headset icon (or the device menu), then select your **Quest 2**.
3. Go to **Device settings**, find **Developer Mode**, and turn it on.
4. If the app asks you to create a developer organization, follow the on-screen steps. This is only needed once.

> Menu names may vary slightly between app versions. If you cannot find the option, search for "Developer Mode" in the device settings.

### 8.3. Connect the headset to the computer

1. Connect the Quest 2 to the computer with a USB cable.
2. In the headset, when the USB debugging prompt appears, select **Allow**. It is recommended to tick **Always allow from this computer** so you are not asked again.
3. Open Command Prompt or PowerShell and run:
   ```
   adb devices
   ```
4. The correct result is one line with the headset's serial number and the word `device`:
   ```
   1WMHH000XXXXXX    device
   ```
   - If it shows `unauthorized`: unplug the cable, plug it back in, and confirm in the headset.
   - If no device appears: try a different cable or USB port, and make sure the headset is not asleep.

### 8.4. Install the APK

**Option 1: use adb (recommended)**
```
adb install -r "D:\Builds\TinyOcean\TinyOcean_Quest2.apk"
```
- `-r` overwrites the old version if the same package is already installed.
- A successful install shows `Success`.

**Option 2: use Unity**
In **Build Profiles → Android**, select **Build and Run** when the headset is connected.

**Option 3: use Meta Quest Developer Hub or SideQuest**
Drag and drop the `.apk` file into the tool and follow the install prompts.

**If the install fails:**
- `INSTALL_FAILED_UPDATE_INCOMPATIBLE`: the installed version was signed differently. Uninstall the old version first:
  ```
  adb uninstall com.tenban.tinyocean
  ```
  (Replace the package name with the Application Identifier you set in Unity.)
- `INSTALL_FAILED_INSUFFICIENT_STORAGE`: the headset is out of storage. Remove unused apps in **Settings → Storage**.

### 8.5. Open the game

1. Put on the headset and go to **Library**.
2. In the filter options at the top, select **Unknown Sources**.
3. Select **TinyOcean** and open it.

> On first launch, Quest may ask you to set up **Guardian** (the safety boundary). Choose a space large enough, or choose seated mode if you have limited room.

### 8.6. Controls in the headset

| Action | Function |
|---|---|
| Ray from the controller + **Trigger** button | Select starfish, fish, and menu buttons |
| **B** button (right hand) | Hide the instructions panel |
| **X** button (left hand) | Quit the game |
| Thumbstick | Move and turn, depending on the in-game settings |

### 8.7. Uninstall

- **In the headset:** go to **Settings → Apps** (or Library), select **TinyOcean**, and press **Uninstall**.
- **With adb:**
  ```
  adb uninstall com.tenban.tinyocean
  ```

To clear saved game progress (starfish, chest) without uninstalling the app, go to the main menu and press **Start**.

### 8.8. Notes for testing on the headset

- **Development Builds** include detailed logs and allow debug connections. Release builds should use Release mode.
- **Performance, controllers, visual quality, and effects** can only be judged accurately on a real headset. The XR Device Simulator cannot replace this.
- When you change the code and want to test again, build a new APK and reinstall it with `adb install -r` as described in section 8.4.
- Do not share the APK outside the group without the authors' permission.

---

## 9. Common issues

| Problem | Solution |
|---|---|
| Red package errors when opening the project | Window → Package Manager → Resolve, or check that you are using the correct Unity version |
| XR Device Simulator does not appear on Play | Check that OpenXR is enabled in XR Plug-in Management and XR Interaction Toolkit is installed |
| Pressing Play does not load Level 1 | Check that all three scenes are in Build Settings |
| Cannot collect starfish on PC | Use the left mouse button and make sure the camera is aimed at the object |
| Old progress still appears | Return to the menu and press **Start** to clear `PlayerPrefs` |
| `adb devices` does not show the headset | Change the USB cable, check that Developer Mode is on, and confirm Allow in the headset |
| `adb devices` shows `unauthorized` | Unplug, plug back in, and confirm the prompt in the headset |
| Installed APK but cannot find the game in Library | Select the **Unknown Sources** filter in Library |
| Black screen or does not enter VR | Check the OpenXR Loader and Meta Quest feature group in section 7 |
| Game crashes on launch | Run `adb logcat -s Unity` while opening the game and send the log for review |
