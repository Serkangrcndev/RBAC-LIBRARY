# Library Management System (Kütüphane Yönetim Sistemi)

[![React](https://img.shields.io/badge/React-19.1.0-blue.svg)](https://reactjs.org/)
[![Redux Toolkit](https://img.shields.io/badge/Redux%20Toolkit-2.8.2-purple.svg)](https://redux-toolkit.js.org/)
[![RTK Query](https://img.shields.io/badge/RTK%20Query-2.8.2-green.svg)](https://redux-toolkit.js.org/rtk-query/overview)
[![Material-UI](https://img.shields.io/badge/Material--UI-7.2.0-blue.svg)](https://mui.com/)
[![.NET 8](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/)
[![React Native](https://img.shields.io/badge/React%20Native-0.79.5-blue.svg)](https://reactnative.dev/)

# 📚 Kütüphane Yönetim Sistemi (Library Management System)

Bu proje, üç farklı platformda modern teknolojiler kullanılarak geliştirilmiş kapsamlı bir **Kütüphane Yönetim Sistemi**dir.

---

## 🖥️ Platformlar

### 🖥️ Masaüstü Uygulaması
- **Teknoloji**: .NET 8 Windows Forms  
- **Veritabanı**: SQL Server + Entity Framework  
- **Özellikler**: Rol tabanlı yetkilendirme (RBAC) ile tam kapsamlı yönetim  

---

### 🌐 Web Uygulaması
- **Teknoloji**: React 19 + Redux Toolkit + RTK Query  
- **UI Framework**: Material-UI (MUI)  
- **Durum Yönetimi**: Redux + RTK Query  
- **Özellikler**: Modern arayüz, gerçek zamanlı veri yönetimi, RBAC sistemi  

---

### 📱 Mobil Uygulama
- **Teknoloji**: React Native + Expo  
- **Navigasyon**: React Navigation (Bottom Tabs)  
- **Özellikler**: Cross-platform mobil deneyim, performans odaklı yapı  

---

## 🚀 Temel Özellikler

### 🔐 Rol Tabanlı Yetkilendirme (RBAC)
- **Admin**: Tüm sistem erişimi ve kullanıcı yönetimi  
- **Kütüphaneci (Staff)**: Kitap ve ödünç işlemleri  
- **Üye (Member)**: Kitap görüntüleme ve ödünç geçmişi  

---

### 📖 Kitap Yönetimi
- Kitap ekleme, düzenleme, silme  
- Stok takibi  
- Arama ve filtreleme  
- Kategori yönetimi  

---

### 🔄 Ödünç Alma Sistemi
- 30 gün ödünç süresi  
- Gecikme otomatik tespiti  
- Ceza sistemi  
- Stok kontrolü  

---

### 👥 Kullanıcı Yönetimi
- Kayıt ve giriş işlemleri  
- Rol atama  
- Profil yönetimi  
- Oturum yönetimi  

---

### 📊 İstatistikler ve Raporlar
- Ödünç alma istatistikleri  
- Kullanıcı aktiviteleri  
- Popüler kitaplar  
- Gecikme raporları  

---

## 🛠️ Teknoloji Yığını

### Frontend
- React 19  
- Redux Toolkit  
- RTK Query  
- Material-UI  
- React Router  

---

### Backend
- .NET 8  
- Windows Forms  
- SQL Server  
- Entity Framework  

---

### Mobil
- React Native  
- Expo  
- React Navigation  

---

### Güvenlik
- JWT Authentication  
- BCrypt ile şifreleme  
- Rol bazlı yetkilendirme  
- Session yönetimi  
- SQL Injection koruması  

---

## 📁 Proje Yapısı

```
📦 Library Management System
├── 🖥️ Desktop Application/
│   └── Seker_kutuphane/
│       ├── Forms/
│       ├── Models/
│       ├── Services/
│       └── DatabaseHelper.cs
├── 🌐 Website/
│   ├── src/
│   │   ├── components/
│   │   ├── pages/
│   │   ├── store/
│   │   │   ├── api/
│   │   │   └── slices/
│   │   ├── utils/
│   │   └── styles/
│   └── public/
└── 📱 Mobile Application/
    └── kutuphane-mobile/
        ├── app/
        ├── components/
        ├── store/
        ├── services/
        └── constants/
```

---

## ⚙️ Kurulum

### Gereksinimler
- Node.js 18+  
- .NET 8 SDK  
- SQL Server  
- Expo CLI  

---

### 1. Web Uygulaması
```bash
cd Website
npm install
npm start
```

---

### 2. Masaüstü Uygulaması
```bash
cd "Desktop Application/Seker_kutuphane"
dotnet restore
dotnet run
```

---

### 3. Mobil Uygulama
```bash
cd "Mobile Application/kutuphane-mobile"
npm install
npx expo start
```

---

## 🔧 Yapılandırma

### Veritabanı
`DatabaseHelper.cs` dosyasındaki connection string’i güncelleyin.

### API Ayarları
Web projesinde `apiSlice.js` dosyasına API URL ekleyin.

### Ortam Değişkenleri
Hassas bilgiler için `.env` dosyaları oluşturun.

---

## 📱 Platform Bazlı Özellikler

### 🖥️ Masaüstü
- CRUD işlemleri  
- Gelişmiş arama  
- Raporlama  
- Toplu işlemler  
- Offline çalışma  

---

### 🌐 Web
- Responsive tasarım  
- Gerçek zamanlı güncelleme  
- PWA desteği  
- Modern UI/UX  

---

### 📱 Mobil
- Cross-platform  
- Offline-first yaklaşım  
- Push bildirimler  
- Dokunmatik optimizasyon  

---

## 🔐 RBAC Nedir?

RBAC (Role-Based Access Control), kullanıcıların sistemdeki yetkilerini rollere göre belirleyen bir yetkilendirme modelidir.

### Yapı:
```
Kullanıcı → Rol → Yetkiler → İşlemler
```

### Avantajları:
- Merkezi yetki yönetimi  
- Güvenli erişim kontrolü  
- Ölçeklenebilir mimari  

---

## 📊 Performans Optimizasyonları
- RTK Query cache sistemi  
- Lazy loading  
- Code splitting  
- Veritabanı indeksleme  
- Görsel optimizasyonu  

---

## 🧪 Test

### Web
```bash
cd Website
npm test
```

### Masaüstü
```bash
cd "Desktop Application/Seker_kutuphane"
dotnet test
```

---

## 📈 İzleme ve Analiz
- Hata loglama  
- Performans takibi  
- Kullanıcı analizi  
- Sistem sağlık kontrolleri  

---

## 🤝 Katkıda Bulunma

1. Repo’yu forkla  
2. Yeni branch oluştur  
3. Değişiklikleri yap  
4. Test ekle (varsa)  
5. Pull request gönder  

---

## 📄 Lisans

Bu proje **MIT Lisansı** ile lisanslanmıştır.

---

## 🔄 Sürüm Geçmişi

### v1.0.0
- İlk sürüm  
- 3 platform desteği  
- RBAC sistemi  
- Gelişmiş ödünç yönetimi  
- Modern arayüz  

---

## 👨‍💻 Geliştiriciler

- Serkan Gürcan  
- Burak Saka  
- Halil Malatyalı  
- Semiha Bahçebaşı  
- Umut Aydın  

## IMAGE

<details>
  <summary>Desktop Application Image</summary>
<img width="802" height="756" alt="image" src="https://github.com/user-attachments/assets/5fe75a2a-c2bb-4828-aaef-098242e51416" />
<img width="853" height="503" alt="image" src="https://github.com/user-attachments/assets/68810444-a025-41eb-a8e4-e63f06837466" />
<img width="1001" height="700" alt="image" src="https://github.com/user-attachments/assets/89e1bd59-e0b9-45d4-9fe5-9d2b1baafea6" />
<img width="899" height="651" alt="image" src="https://github.com/user-attachments/assets/76ba4cd1-3149-4630-b44d-067bc288257a" />
<img width="598" height="520" alt="image" src="https://github.com/user-attachments/assets/a17597a8-8635-4c8c-89db-a42dcc477412" />
<img width="1197" height="705" alt="image" src="https://github.com/user-attachments/assets/5f367241-e8c5-42f1-81b9-739044848c83" />
<img width="1065" height="630" alt="image" src="https://github.com/user-attachments/assets/6046175a-c02f-40ca-be14-f66d52127f21" />
<img width="1068" height="634" alt="image" src="https://github.com/user-attachments/assets/b8427383-e528-4637-87f1-9d3a169536d7" />
<img width="1069" height="633" alt="image" src="https://github.com/user-attachments/assets/77b44d80-9966-4a32-88f3-25a6ee83775b" />
<img width="716" height="523" alt="image" src="https://github.com/user-attachments/assets/3f7eb362-9584-428a-981b-44d46a4d451c" />
</details>

<details>
  <summary>Website & Mobile Image</summary>
    <img width="1914" height="936" alt="image" src="https://github.com/user-attachments/assets/639e4728-da0b-46e6-8873-9326feed8744" />
    <img width="1915" height="940" alt="image" src="https://github.com/user-attachments/assets/7d59c19a-7dd2-4d23-84f5-0164782146db" />
    <img width="1901" height="940" alt="image" src="https://github.com/user-attachments/assets/e58c9179-c18d-42a7-bb8f-4ac107dc1778" />
    <img width="1915" height="939" alt="image" src="https://github.com/user-attachments/assets/ac4bd9f9-0c07-4d4f-9366-a40cf87ad828" />
    <img width="1911" height="936" alt="image" src="https://github.com/user-attachments/assets/76816a79-ef7d-42a2-a955-305bfa35f8e1" />
    <img width="1913" height="945" alt="image" src="https://github.com/user-attachments/assets/9a592d86-d2a8-4afd-876b-db6717c015d1" />
    <img width="1907" height="931" alt="image" src="https://github.com/user-attachments/assets/7a044b98-bd6c-4916-95d9-6e4015997d08" />
    <img width="1896" height="931" alt="image" src="https://github.com/user-attachments/assets/336746ae-e4d7-439d-85ca-954fafdefde8" />
    <img width="1914" height="942" alt="image" src="https://github.com/user-attachments/assets/ff8f6e09-ed82-4ac0-93cc-63b63846c939" />
    <img width="1917" height="943" alt="image" src="https://github.com/user-attachments/assets/f83a59f8-7285-4a32-89c6-d391722858b7" />
    <img width="1080" height="2400" alt="image" src="https://github.com/user-attachments/assets/b7689bd3-65d3-45ed-a1fc-1e031484897f" />
    <img width="1080" height="2400" alt="image" src="https://github.com/user-attachments/assets/831ed56a-29d4-479c-a818-0469bed4260f" />
    <img width="1080" height="2400" alt="image" src="https://github.com/user-attachments/assets/8a7a1ccd-7c80-49f2-961a-fcca99a401fb" />
    <img width="1080" height="2400" alt="image" src="https://github.com/user-attachments/assets/5fef35b8-1155-4f90-8c8c-13957082f937" />
    <img width="1080" height="2400" alt="image" src="https://github.com/user-attachments/assets/308f946d-708f-49b3-8389-5fbec2604cb9" />
    <img width="1080" height="2400" alt="image" src="https://github.com/user-attachments/assets/ae03aa61-de19-46ef-87fe-315b5f97ad2b" />
    <img width="1080" height="2400" alt="image" src="https://github.com/user-attachments/assets/0f378a3b-26d3-47dc-afba-8f237a34d302" />
    <img width="1080" height="2400" alt="image" src="https://github.com/user-attachments/assets/481a63a6-cbc0-4703-a76c-36517f858429" />
    <img width="1080" height="2400" alt="image" src="https://github.com/user-attachments/assets/ccb71d8b-8fe9-49b6-a734-c68668a9f600" />

