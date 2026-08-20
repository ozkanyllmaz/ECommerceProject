# E-Ticaret Platformu - Backend API 🚀

Açık kaynaklı, yüksek ölçeklenebilir, sağlam ve sürdürülebilir bir e-ticaret platformu backend servisi. Gerçek anlamda sorumlulukların ayrılığı (separation of concerns) ve test edilebilirlik sağlamak amacıyla **ASP.NET Core** kullanılarak **Clean Architecture** (Temiz Mimari) prensipleriyle inşa edilmiştir.

> **Not:** Bu depo sadece Backend API uygulamasını içermektedir. Frontend (kullanıcı arayüzü) uygulaması bağımsızdır ve tamamen ayrı bir repoda tutulmaktadır. Buradan ulaşabilirsiniz -> [ECommerceProject-Client](https://github.com/ozkanyllmaz/ECommerceProject-Client.git)

## 🏗️ Mimari ve Tasarım Desenleri

* **Clean Architecture:** Çekirdek iş mantığının dış framework'lerden, UI katmanından ve veritabanı detaylarından tamamen bağımsız olmasını sağlar.
* **CQRS (Command Query Responsibility Segregation):** Okuma ve yazma işlemlerini **MediatR** kütüphanesi kullanarak birbirinden ayırır; sistem performansını, ölçeklenebilirliğini ve kodun okunabilirliğini artırır.
* **Unit of Work & Dependency Injection:** Veritabanı transaction (işlem) kapsamlarını tek bir merkezden verimli bir şekilde yönetir ve katmanlar arasındaki bağımlılıkları en aza indirir (loose coupling).
* **Result Pattern (Response Wrapper):** Tüm API yanıtlarını (başarılı sonuçlar veya hatalar) standart bir formata sokarak endpoint'ler arasında yapısal bütünlük ve tutarlılık sağlar.

## ⚙️ Temel Teknolojiler

* **Framework:** .NET / ASP.NET Core
* **Dil:** C# (Asenkron Programlama - Asynchronous Programming)
* **Veritabanı:** Microsoft SQL Server (MSSQL)
* **ORM:** Entity Framework Core (Code First Yaklaşımı)

## 🌟 Öne Çıkan Teknik Özellikler

* **Güvenlik, Kriptografi ve Erişim Kontrolü:**
  * **Veri Şifreleme (Encryption):** Veritabanı bağlantı dizesi (Connection String) ve JWT Güvenlik Anahtarı gibi hassas veriler `appsettings.json` içerisinde **şifrelenmiş (encrypted)** olarak tutulmaktadır. Uygulama çalışma zamanında bu verileri çözmek için dışarıdan (User Secrets üzerinden) enjekte edilen bir "Master Key" kullanır. Kaynak kod sızsa bile konfigürasyon verileri güvendedir.
  * **Parola Hashing:** Kullanıcı parolaları veritabanında asla düz metin (plain-text) olarak saklanmaz; modern hashing algoritmaları ile geri döndürülemez şekilde şifrelenerek tutulur.
  * **JWT (JSON Web Token):** Güvenli, hızlı ve durumsuz (stateless) kimlik doğrulama altyapısı.
  * **RBAC (Role-Based Access Control):** Belirli kullanıcı rollerine ve yetkilerine göre API endpoint'lerini koruyan detaylı yetkilendirme mekanizması.
  * **CORS (Cross-Origin Resource Sharing):** Bağımsız frontend istemcisinden (örn. React) gelen istekleri güvenli bir şekilde kabul edecek yapılandırma.

* **Veri Bütünlüğü ve Doğrulama:**
  * **FluentValidation & PipelineBehavior:** İş mantığını temiz tutmak adına, gelen MediatR isteklerini (DTO'ları) işleyiciye (handler) ulaşmadan araya girerek (intercept) otomatik olarak doğrulayan (validate eden) mekanizma.
  * **Soft Delete (Esnek Silme):** Verilerin veritabanından fiziksel olarak silinmesini önleyen (`IsDeleted` bayrağı ile) ve EF Core Global Query Filters kullanarak bu verilerin sorgularda otomatik olarak gizlenmesini sağlayan yapı.

* **Performans ve Veri Şekillendirme:**
  * **Asenkron Programlama:** Bloklamayan (non-blocking) I/O işlemleri ve yüksek eşzamanlılık (concurrency) için uçtan uca `async/await` implementasyonu.
  * **Gelişmiş Veri Listeleme:** Frontend tarafına en uygun veriyi sunmak için dahili **Sayfalama (Pagination), Filtreleme (Filtering) ve Sıralama (Sorting)** mimarisi.

* **İzleme ve Hata Yönetimi (Log & Exception Management):**
  * **Serilog ile Yapısal Loglama (Structured Logging):** Sistemdeki tüm hataların, API isteklerinin ve kullanıcı hareketlerinin merkezi bir ortama yapısal olarak yazılması (LogManagement sayfası için kritik altyapı).
  * **Global Exception Handling (Global Hata Yönetimi):** Sistem genelinde yakalanmayan (unhandled) hataları merkezi olarak yakalayıp istemciye standart, güvenli ve kullanıcı dostu JSON hata yanıtları döndüren özel Middleware tasarımı.

## 🚀 Başlarken

### Ön Koşullar
* .NET SDK (Güncel sürüm)
* Microsoft SQL Server (MSSQL)


### Kurulum ve Ayarlar

1. **Repoyu klonlayın:**
   ```bash
   git clone https://github.com/ozkanyllmaz/ECommerceProject.git
   cd ECommerceProject
   ```

2. **Güvenlik ve User Secrets Ayarları (Kritik Adım):**
   Bu projede konfigürasyon güvenliği için `appsettings.json` içindeki `ConnectionString` ve `Jwt` anahtarları şifrelenmiştir. Projeyi lokalinizde çalıştırabilmek için öncelikle kendi şifreleme anahtarınızı (Master Key) `User Secrets` aracılığıyla sisteme tanıtmalısınız.
   
   API proje dizininde (örneğin API veya Presentation klasöründe) terminali açıp şu komutları sırasıyla çalıştırın:
   ```bash
   dotnet user-secrets init
   dotnet user-secrets set "Jwt:EncryptionMasterKey" "kendi_gizli_anahtarinizi_buraya_yazin"
   ```

3. **Veritabanı ve Konfigürasyon Dosyası:**
   `appsettings.json` dosyasındaki şifreli değerleri kendi sisteminize göre güncellemeniz gerekmektedir:
   * Kendi yerel MSSQL bağlantı dizenizi (Connection String) oluşturun.
   * Projede bulunan şifreleme mekanizmasını (Encryption Service) kullanarak bu bağlantı dizenizi şifreleyin. (Alternatif olarak geliştirme ortamı için kod içerisindeki decrypt işlemini geçici olarak devre dışı bırakıp düz metin kullanabilirsiniz).
   * Şifrelenmiş metni `appsettings.json` içindeki ilgili alanlara yapıştırın.

4. **EF Core Migration'larını Uygulayın:**
   Terminal üzerinden Infrastructure veya API (migration'ların bulunduğu) proje dizinine giderek veritabanını oluşturmak için şu komutu çalıştırın:
   ```bash
   dotnet ef database update
   ```

5. **Projeyi Çalıştırın:**
   ```bash
   dotnet run
   ```
   API başarıyla çalıştıktan sonra endpoint'leri Swagger UI üzerinden inceleyebilir ve test edebilirsiniz. (Genellikle `https://localhost:<port>/swagger` adresinde ayağa kalkar).
