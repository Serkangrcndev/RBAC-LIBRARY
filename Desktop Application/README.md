# Library Management System - Desktop Application
# Kütüphane Yönetim Sistemi - Masaüstü Uygulaması

[![.NET 8](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/)
[![Windows Forms](https://img.shields.io/badge/Windows%20Forms-8.0-green.svg)](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2019-orange.svg)](https://www.microsoft.com/en-us/sql-server/)

## 📚 Overview (Genel Bakış)

.NET 8 Windows Forms ile geliştirilmiş kapsamlı kütüphane yönetim sistemi masaüstü uygulaması. SQL Server veritabanı ile entegre çalışan, RBAC (Rol Tabanlı Erişim Kontrolü) sistemi içeren profesyonel bir uygulama.

## 🚀 Key Features (Temel Özellikler)

### 🔐 Advanced RBAC System (Gelişmiş RBAC Sistemi)
- **Role-based Access Control**: Üç farklı kullanıcı rolü
  - **Admin**: Tam sistem erişimi ve yönetimi
  - **Kütüphane Görevlisi**: Kitap ve emanet işlemleri
  - **Üye**: Temel kitap tarama ve kişisel işlemler
- **Permission Management**: Granüler yetki sistemi
- **Session Management**: Güvenli oturum yönetimi

### 📖 Comprehensive Book Management (Kapsamlı Kitap Yönetimi)
- **Book CRUD Operations**: Kitap ekleme, düzenleme, silme
- **Advanced Search**: Gelişmiş arama ve filtreleme
- **Stock Management**: Gerçek zamanlı stok takibi
- **Category Management**: Kategori yönetimi
- **Author Management**: Yazar bilgileri yönetimi
- **ISBN Validation**: ISBN doğrulama sistemi

### 🔄 Advanced Loan System (Gelişmiş Emanet Sistemi)
- **30-Day Loan Period**: 30 günlük emanet süresi
- **Automatic Overdue Detection**: Otomatik gecikme tespiti
- **Penalty System**: Gecikme ceza sistemi
- **Stock Tracking**: Gerçek zamanlı stok güncellemesi
- **Bulk Operations**: Toplu emanet işlemleri
- **Loan History**: Detaylı emanet geçmişi

### 👥 User Management (Kullanıcı Yönetimi)
- **User Registration**: Kullanıcı kayıt sistemi
- **Profile Management**: Profil yönetimi
- **Role Assignment**: Rol atama sistemi
- **Password Management**: Şifre yönetimi
- **User Search**: Kullanıcı arama ve filtreleme

### 📊 Reporting & Analytics (Raporlama ve Analitik)
- **Dashboard Statistics**: Ana sayfa istatistikleri
- **Loan Reports**: Emanet raporları
- **User Activity Reports**: Kullanıcı aktivite raporları
- **Book Popularity**: Kitap popülerlik analizi
- **Overdue Reports**: Gecikme raporları
- **Export Functionality**: Rapor dışa aktarma

## 🛠️ Technology Stack (Teknoloji Yığını)

### Framework & Runtime
- **.NET 8**: En son .NET framework
- **Windows Forms**: Masaüstü uygulama framework'ü
- **C#**: Modern C# programlama dili

### Database & Data Access
- **SQL Server**: İlişkisel veritabanı
- **Entity Framework**: ORM framework
- **Microsoft.Data.SqlClient**: SQL Server bağlantısı
- **System.Data.SqlClient**: Legacy SQL bağlantısı

### Security & Authentication
- **BCrypt.Net-Next**: Güvenli şifre hashleme
- **Session Management**: Oturum yönetimi
- **Role-based Authorization**: Rol tabanlı yetkilendirme

### Utilities & Libraries
- **Newtonsoft.Json**: JSON işlemleri
- **Custom Database Helper**: Özel veritabanı yardımcı sınıfı

## 📁 Project Structure (Proje Yapısı)

```
Seker_kutuphane/
├── Forms/                    # Windows Forms
│   ├── Dashboard.cs         # Ana dashboard
│   ├── login.cs             # Giriş ekranı
│   ├── kayit.cs             # Kayıt ekranı
│   ├── KitapIslemleriForm.cs # Kitap işlemleri
│   ├── KitapAramaForm.cs    # Kitap arama
│   ├── KitapEkleForm.cs     # Kitap ekleme
│   ├── KitapGuncelleForm.cs # Kitap güncelleme
│   ├── EmanetIslemleriForm.cs # Emanet işlemleri
│   ├── YeniEmanetForm.cs    # Yeni emanet
│   ├── UyelikIslemleriForm.cs # Üyelik işlemleri
│   ├── KullaniciEkleForm.cs # Kullanıcı ekleme
│   ├── KullaniciGuncelleForm.cs # Kullanıcı güncelleme
│   ├── ProfilForm.cs        # Profil yönetimi
│   ├── SifreDegistirForm.cs # Şifre değiştirme
│   └── sifre_yenileme.cs    # Şifre yenileme
├── Models/                   # Veri modelleri
├── Services/                 # İş mantığı servisleri
├── DatabaseHelper.cs         # Veritabanı yardımcı sınıfı
├── Program.cs               # Uygulama giriş noktası
└── Seker_kutuphane.csproj   # Proje dosyası
```

## 🚀 Getting Started (Başlangıç)

### Prerequisites (Ön Gereksinimler)
- **Visual Studio 2022** veya **Visual Studio Code**
- **.NET 8 SDK**
- **SQL Server 2019** veya üzeri
- **Windows 10/11** işletim sistemi

### Installation (Kurulum)

1. **Clone the repository**
```bash
git clone <repository-url>
cd "Desktop Application/Seker_kutuphane"
```

2. **Restore dependencies**
```bash
dotnet restore
```

3. **Configure database connection**
Update the connection string in `DatabaseHelper.cs`:
```csharp
private static string connectionString = "Server=your_server;Database=your_database;Trusted_Connection=true;";
```

4. **Build and run**
```bash
dotnet build
dotnet run
```

## 🔧 Configuration (Yapılandırma)

### Database Configuration
Update the connection string in `DatabaseHelper.cs`:
```csharp
private static string connectionString = "Server=localhost;Database=KutuphaneDB;Trusted_Connection=true;";
```

### Application Settings
- **Loan Period**: 30 days (configurable)
- **Overdue Penalty**: Automatic user deactivation
- **Session Timeout**: Configurable session duration
- **Password Policy**: Minimum requirements

### Security Settings
- **Password Hashing**: BCrypt with salt
- **Session Management**: Secure session handling
- **Input Validation**: Comprehensive input sanitization
- **SQL Injection Protection**: Parameterized queries

## 📱 Features by User Role (Kullanıcı Rolüne Göre Özellikler)

### 👑 Admin (Yönetici)
- **Complete System Access**: Tam sistem erişimi
- **User Management**: Kullanıcı yönetimi
- **Role Assignment**: Rol atama
- **System Statistics**: Sistem istatistikleri
- **Database Management**: Veritabanı yönetimi
- **Report Generation**: Rapor oluşturma

### 📚 Librarian/Staff (Kütüphane Görevlisi)
- **Book Management**: Kitap yönetimi
- **Loan Operations**: Emanet işlemleri
- **User Registration**: Kullanıcı kaydı
- **Stock Management**: Stok yönetimi
- **Basic Reporting**: Temel raporlama

### 👤 Member (Üye)
- **Book Browsing**: Kitap tarama
- **Personal Loan History**: Kişisel emanet geçmişi
- **Profile Management**: Profil yönetimi
- **Password Management**: Şifre yönetimi

## 🔐 Security Features (Güvenlik Özellikleri)

### Authentication & Authorization
- **Secure Login**: Güvenli giriş sistemi
- **Password Hashing**: BCrypt ile şifre hashleme
- **Session Management**: Oturum yönetimi
- **Role-based Access**: Rol tabanlı erişim

### Data Protection
- **Input Validation**: Girdi doğrulama
- **SQL Injection Protection**: SQL enjeksiyon koruması
- **Data Encryption**: Veri şifreleme
- **Secure Communication**: Güvenli iletişim

### User Management
- **Password Policies**: Şifre politikaları
- **Account Lockout**: Hesap kilitleme
- **Session Timeout**: Oturum zaman aşımı
- **Audit Logging**: Denetim günlüğü

## 📊 Database Schema (Veritabanı Şeması)

### Core Tables
- **kullanicilar**: User information
- **kitaplar**: Book information
- **emanetler**: Loan records
- **roller**: User roles
- **yetkiler**: Permissions
- **kategoriler**: Book categories
- **yazarlar**: Authors

### Relationships
- Users can have multiple roles
- Books belong to categories
- Books have authors
- Loans connect users and books

## 🧪 Testing (Test)

### Unit Testing
```bash
dotnet test
```

### Manual Testing
- **Login Testing**: Giriş testleri
- **CRUD Operations**: Temel işlem testleri
- **Role-based Access**: Rol tabanlı erişim testleri
- **Loan System**: Emanet sistemi testleri

### Test Forms
- **EmanetTestForm**: Emanet işlemleri test formu
- **API Testing**: API endpoint testleri

## 📈 Performance Optimizations (Performans Optimizasyonları)

### Database Optimization
- **Indexed Queries**: İndeksli sorgular
- **Stored Procedures**: Saklı yordamlar
- **Connection Pooling**: Bağlantı havuzu
- **Query Optimization**: Sorgu optimizasyonu

### Application Performance
- **Lazy Loading**: Tembel yükleme
- **Caching**: Önbellekleme
- **Memory Management**: Bellek yönetimi
- **UI Responsiveness**: UI yanıt verebilirliği

## 🚀 Deployment (Dağıtım)

### Build for Production
```bash
dotnet publish -c Release -r win-x64 --self-contained
```

### Installation Package
- **Setup Wizard**: Kurulum sihirbazı
- **Database Setup**: Veritabanı kurulumu
- **Configuration**: Yapılandırma
- **User Guide**: Kullanıcı kılavuzu

### Distribution
- **MSI Package**: Windows Installer paketi
- **Portable Version**: Taşınabilir sürüm
- **Network Installation**: Ağ kurulumu

## 🔄 API Integration (API Entegrasyonu)

### RESTful API Support
- **HTTP Client**: HTTP istemci desteği
- **JSON Serialization**: JSON serileştirme
- **Error Handling**: Hata yönetimi
- **Authentication**: Kimlik doğrulama

### External Services
- **Email Service**: E-posta servisi
- **SMS Service**: SMS servisi
- **File Storage**: Dosya depolama

## 🤝 Contributing (Katkıda Bulunma)

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

### Development Guidelines
- Follow C# coding conventions
- Use meaningful variable names
- Add comments for complex logic
- Write unit tests for new features
- Update documentation

## 📄 License (Lisans)

This project is licensed under the MIT License - see the [LICENSE](../LICENSE) file for details.

## 🔄 Version History (Sürüm Geçmişi)

### v1.0.0 (Current)
- Initial release with full RBAC implementation
- Complete book and loan management
- Advanced user management system
- Comprehensive reporting system
- Modern Windows Forms interface

