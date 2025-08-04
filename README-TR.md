# Kütüphane Yönetim Sistemi

[![React](https://img.shields.io/badge/React-19.1.0-blue.svg)](https://reactjs.org/)
[![Redux Toolkit](https://img.shields.io/badge/Redux%20Toolkit-2.8.2-purple.svg)](https://redux-toolkit.js.org/)
[![RTK Query](https://img.shields.io/badge/RTK%20Query-2.8.2-green.svg)](https://redux-toolkit.js.org/rtk-query/overview)
[![Material-UI](https://img.shields.io/badge/Material--UI-7.2.0-blue.svg)](https://mui.com/)
[![.NET 8](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/)
[![React Native](https://img.shields.io/badge/React%20Native-0.79.5-blue.svg)](https://reactnative.dev/)

## 📚 Proje Genel Bakış

Bu, üç farklı platformda modern teknolojilerle geliştirilmiş kapsamlı bir **Kütüphane Yönetim Sistemi**dir:

### 🖥️ Masaüstü Uygulaması
- **Teknoloji**: .NET 8 Windows Forms
- **Veritabanı**: SQL Server ve Entity Framework
- **Özellikler**: RBAC sistemi ile tam kütüphane yönetimi

### 🌐 Web Uygulaması
- **Teknoloji**: React 19 + Redux Toolkit + RTK Query
- **UI Framework**: Material-UI (MUI)
- **State Management**: RTK Query ile gelişmiş Redux kurulumu
- **Özellikler**: Rol tabanlı erişim kontrolü ile modern web arayüzü

### 📱 Mobil Uygulama
- **Teknoloji**: React Native + Expo
- **Navigasyon**: Alt sekmeli React Navigation
- **Özellikler**: Platformlar arası mobil deneyim

---

## 🚀 Temel Özellikler

### 🔐 RBAC (Rol Tabanlı Erişim Kontrolü) Sistemi
- **Admin**: Tam sistem erişimi ve kullanıcı yönetimi
- **Kütüphane Görevlisi**: Kitap yönetimi ve emanet işlemleri
- **Üye**: Temel kitap tarama ve kişisel emanet geçmişi

### 📖 Kitap Yönetimi
- Kitap ekleme, düzenleme, silme
- Stok yönetimi
- Kitap arama ve filtreleme
- Kategori yönetimi

### 🔄 Emanet Sistemi
- 30 günlük emanet süresi
- Otomatik gecikme tespiti
- Gecikme ceza sistemi
- Stok takibi

### 👥 Kullanıcı Yönetimi
- Kullanıcı kaydı ve kimlik doğrulama
- Rol atama
- Profil yönetimi
- Oturum yönetimi

### 📊 İstatistikler ve Raporlar
- Emanet istatistikleri
- Kullanıcı aktivite raporları
- Kitap popülerlik metrikleri
- Gecikme raporları

---

## 🛠️ Teknoloji Yığını

### Frontend Teknolojileri
- **React 19**: Concurrent özelliklerle en son React
- **Redux Toolkit**: Basitleştirilmiş boilerplate ile modern Redux
- **RTK Query**: Güçlü veri çekme ve önbellekleme
- **Material-UI**: Profesyonel UI bileşenleri
- **React Router**: İstemci tarafı yönlendirme

### Backend Teknolojileri
- **.NET 8**: En son .NET framework
- **Windows Forms**: Masaüstü uygulama framework'ü
- **SQL Server**: İlişkisel veritabanı
- **Entity Framework**: Veritabanı işlemleri için ORM

### Mobil Teknolojileri
- **React Native**: Platformlar arası mobil geliştirme
- **Expo**: Geliştirme platformu ve araçları
- **React Navigation**: Mobil navigasyon çözümü

### Güvenlik ve Kimlik Doğrulama
- **JWT Tokenları**: Güvenli kimlik doğrulama
- **Oturum Yönetimi**: Kalıcı kullanıcı oturumları
- **Şifre Hashleme**: SHA-256 şifreleme
- **Rol Tabanlı Yetkilendirme**: Granüler erişim kontrolü

---

## 📁 Proje Yapısı

```
📦 Kütüphane Yönetim Sistemi
├── 🖥️ Desktop Application/
│   └── Seker_kutuphane/
│       ├── Forms/           # Windows Forms
│       ├── Models/          # Veri modelleri
│       ├── Services/        # İş mantığı
│       └── DatabaseHelper.cs
├── 🌐 Website/
│   ├── src/
│   │   ├── components/      # Yeniden kullanılabilir bileşenler
│   │   ├── pages/          # Sayfa bileşenleri
│   │   ├── store/          # Redux store
│   │   │   ├── api/        # RTK Query API'leri
│   │   │   └── slices/     # Redux slice'ları
│   │   ├── utils/          # Yardımcı fonksiyonlar
│   │   └── styles/         # CSS stilleri
│   └── public/             # Statik dosyalar
└── 📱 Mobile Application/
    └── kutuphane-mobile/
        ├── app/            # Ekran bileşenleri
        ├── components/     # Yeniden kullanılabilir bileşenler
        ├── store/          # Redux store
        ├── services/       # API servisleri
        └── constants/      # Uygulama sabitleri
```

---

## 🚀 Başlangıç

### Ön Gereksinimler
- Node.js 18+ 
- .NET 8 SDK
- SQL Server
- Expo CLI (mobil geliştirme için)

### Kurulum

#### 1. Web Uygulaması
```bash
cd Website
npm install
npm start
```

#### 2. Masaüstü Uygulaması
```bash
cd "Desktop Application/Seker_kutuphane"
dotnet restore
dotnet run
```

#### 3. Mobil Uygulama
```bash
cd "Mobile Application/kutuphane-mobile"
npm install
npx expo start
```

---

## 🔧 Yapılandırma

### Veritabanı Bağlantısı
Masaüstü uygulaması için `DatabaseHelper.cs` dosyasındaki bağlantı dizesini güncelleyin.

### API Yapılandırması
Web uygulamasının `apiSlice.js` dosyasında API URL'nizi ayarlayın.

### Ortam Değişkenleri
Hassas yapılandırma verileri için `.env` dosyaları oluşturun.

---

## 📱 Platform Bazında Özellikler

### Masaüstü Uygulaması
- ✅ Tam CRUD işlemleri
- ✅ Gelişmiş arama ve filtreleme
- ✅ Rapor oluşturma
- ✅ Toplu işlemler
- ✅ Çevrimdışı yetenek

### Web Uygulaması
- ✅ Duyarlı tasarım (Responsive)
- ✅ Gerçek zamanlı güncellemeler
- ✅ Gelişmiş state yönetimi
- ✅ Progressive Web App (PWA)
- ✅ Modern UI/UX

### Mobil Uygulama
- ✅ Platformlar arası uyumluluk
- ✅ Çevrimdışı öncelikli yaklaşım
- ✅ Push bildirimleri
- ✅ Dokunmatik optimize edilmiş arayüz
- ✅ Native performans

---

## 🔐 Güvenlik Özellikleri

- **Kimlik Doğrulama**: JWT tabanlı kimlik doğrulama
- **Yetkilendirme**: Rol tabanlı erişim kontrolü (RBAC)
- **Veri Şifreleme**: BCrypt ile şifre hashleme
- **Oturum Yönetimi**: Güvenli oturum işleme
- **Girdi Doğrulama**: Kapsamlı girdi sanitizasyonu
- **SQL Injection Koruması**: Parametreli sorgular

---

## 📊 Performans Optimizasyonları

- **RTK Query Önbellekleme**: Akıllı veri önbellekleme
- **Lazy Loading**: Bileşen ve rota lazy loading
- **Code Splitting**: Bundle optimizasyonu
- **Görsel Optimizasyonu**: Sıkıştırılmış varlıklar
- **Veritabanı İndeksleme**: Optimize edilmiş sorgular

---

## 🧪 Test

```bash
# Web Uygulaması Testleri
cd Website
npm test

# Masaüstü Uygulaması Testleri
cd "Desktop Application/Seker_kutuphane"
dotnet test
```

---

## 📈 İzleme ve Analitik

- **Hata Takibi**: Kapsamlı hata günlükleme
- **Performans İzleme**: Uygulama performans metrikleri
- **Kullanıcı Analitiği**: Kullanım istatistikleri ve desenleri
- **Sistem Sağlığı**: Veritabanı ve API sağlık kontrolleri

---

## 🤝 Katkıda Bulunma

1. Repository'yi fork edin
2. Özellik dalı oluşturun
3. Değişikliklerinizi yapın
4. Uygunsa test ekleyin
5. Pull request gönderin

---

## 📄 Lisans

Bu proje MIT Lisansı altında lisanslanmıştır - detaylar için [LICENSE](LICENSE) dosyasına bakın.

---

## 🔄 Sürüm Geçmişi

### v1.0.0 (Mevcut)
- Tüm üç platformla ilk sürüm
- Tam RBAC uygulaması
- Gelişmiş emanet yönetim sistemi
- Tüm platformlarda modern UI/UX

Geliştiriciler: Serkan Gürcan , Burak Saka , Halil Malatyalı , Semiha Bahçebaşı , Umut Aydın.