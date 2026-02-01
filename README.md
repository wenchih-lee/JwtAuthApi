JWT Authentication API
一、專案練習說明
使用 ASP.NET Core 8 實作 JWT 身份驗證 API，包含完整的驗證、授權、Refresh Token 機制和登入紀錄功能。

二、技術架構

框架: ASP.NET Core 8 Web API
驗證方式: JWT (JSON Web Token)
簽章演算法: HMAC-SHA256（對稱式加密）
資料儲存: In-Memory（記憶體儲存）
測試工具: Postman


三、作業 6：JWT 存放方式比較
1. Authorization Header (Bearer Token)
運作方式：
在每個 HTTP 請求的 Header 中加入 Authorization: Bearer {token}，伺服器從 Header 解析 Token 並驗證。
優點：

符合 JWT 標準
適合前後端分離
支援 Mobile App
不受 CSRF 攻擊

缺點：

若存在 localStorage 易受 XSS 攻擊
需手動管理 Token


2. Cookie (HttpOnly)
運作方式：
登入成功後，伺服器將 Token 設定在 HttpOnly Cookie 中，瀏覽器會自動在每個請求中帶上 Cookie。
優點：

HttpOnly 防止 XSS 攻擊
瀏覽器自動管理

缺點：

不符合 JWT 標準
易受 CSRF 攻擊
不支援 Mobile App
不適合前後端分離


3. 結論
現代 Web 開發應優先採用 Authorization Header 方式，因為它符合業界標準、適用性廣、安全性高，且易於維護和擴展。
Cookie 方式雖然在特定場景（如傳統網站）有其優勢，但在 API 開發和前後端分離架構中並非最佳選擇。

四、JWT 思考題
1. JWT 為什麼是無狀態？
因為伺服器不需要儲存任何 Session 資料。JWT 將使用者資訊編碼在 Token 中，伺服器只需驗證 Token 的簽章即可確認身份，不用查詢資料庫或記憶體。
優點： 易於水平擴展、不需共享 Session Store、適合微服務架構。

2. JWT 可以被竄改嗎？為什麼？
JWT 的內容可以被解碼和修改，但無法通過驗證。
原因： JWT 的簽章是由 HMACSHA256(Header + Payload, SecretKey) 產生的。攻擊者沒有 Secret Key，無法產生正確的簽章。伺服器驗證時會重新計算簽章並比對，不符合就拒絕。
結論： 內容可被看到但無法成功竄改，這就是為什麼 Secret Key 必須保密。

3. 為什麼不應該在 JWT 中存放密碼？
三個原因：

JWT 不是加密
只是 Base64 編碼，任何人都可以解碼查看內容
會在網路傳輸
即使用 HTTPS，也可能在 Proxy、日誌、監控系統中被記錄
可能被長期保存
瀏覽器 Console、Postman History、CDN 快取等地方都可能留存

原則： JWT 只放身份識別資訊（使用者 ID、名稱、角色），不放敏感資料（密碼、信用卡號、身份證號）。

4. Middleware 與 Filter 在 JWT 驗證中的角色差異？
Middleware（中介軟體）

處理所有 HTTP 請求
在 MVC 框架之前執行
負責：解析 JWT、驗證簽章、設定 HttpContext.User、全域錯誤處理

Filter（過濾器）

只在特定 Controller 或 Action 執行
在 MVC 框架內部執行
負責：檢查 [Authorize] 屬性、驗證角色權限、特定授權邏輯
