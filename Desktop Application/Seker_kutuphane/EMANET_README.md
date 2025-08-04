# Emanet İşlemleri Sistemi

Bu dokümantasyon, kütüphane yönetim sisteminin emanet işlemleri modülünü açıklamaktadır.

## Özellikler

### 1. Emanet Alma İşlemi
- **Süre**: 30 gün
- **Stok Yönetimi**: Kitap emanet alındığında stok 1 azalır
- **Durum**: "AKTİF" olarak işaretlenir
- **Beklenen Teslim Tarihi**: Emanet tarihinden 30 gün sonra

### 2. Emanet İade İşlemi
- **Stok Yönetimi**: Kitap iade edildiğinde stok 1 artar
- **Durum**: "İADE EDİLDİ" olarak güncellenir
- **Teslim Tarihi**: İade tarihi kaydedilir

### 3. Gecikme Kontrolü
- **Ceza Sistemi**: 30 gün içinde iade edilmeyen kitaplar için kullanıcı cezalı duruma düşer
- **Sistem Durumu**: Gecikmiş emanetler için kullanıcı inaktif edilir

## API Endpoint'leri

### Emanet İşlemleri
- `GET /emanetler` - Tüm emanetleri listele
- `POST /emanet-ekle` - Yeni emanet oluştur
- `PUT /emanet-iade/{emanetId}` - Emanet iade et
- `GET /emanet-ara?q={searchTerm}` - Emanet ara
- `GET /gecikmis-emanetler` - Gecikmiş emanetleri listele
- `GET /kullanici-emanetler/{kullaniciId}` - Kullanıcının emanetlerini listele

### API Request Örnekleri

#### Yeni Emanet Oluşturma
```json
{
  "kullanici_id": 1,
  "kitap_id": 1,
  "odunc_tarihi": "2024-01-01",
  "beklenen_teslim": "2024-02-01",
  "durum": "AKTİF"
}
```

#### Emanet İade Etme
```
PUT /emanet-iade/1
```

## Form Yapısı

### EmanetIslemleriForm
- **Ana Form**: Tüm emanet işlemlerini yönetir
- **İstatistikler**: Toplam, aktif ve gecikmiş emanet sayıları
- **Arama**: Emanet arama özelliği
- **İade İşlemi**: Seçili emaneti iade etme

### YeniEmanetForm
- **Kullanıcı Seçimi**: Dropdown ile kullanıcı seçimi
- **Kitap Seçimi**: Dropdown ile kitap seçimi
- **Stok Kontrolü**: Seçilen kitabın stok durumu
- **Otomatik Tarih**: Emanet ve teslim tarihleri otomatik hesaplanır

## Veri Yapısı

### Emanet Tablosu
```sql
CREATE TABLE emanetler (
    emanet_id INT PRIMARY KEY AUTO_INCREMENT,
    kullanici_id INT,
    kitap_id INT,
    odunc_tarihi DATE,
    beklenen_teslim DATE,
    teslim_tarihi DATE NULL,
    durum VARCHAR(20),
    FOREIGN KEY (kullanici_id) REFERENCES kullanicilar(id),
    FOREIGN KEY (kitap_id) REFERENCES kitaplar(id)
);
```

## Güvenlik ve Doğrulama

### Stok Kontrolü
- Emanet alınmadan önce kitap stok kontrolü yapılır
- Stok 0 ise emanet alınamaz

### Kullanıcı Durumu
- Aktif kullanıcılar emanet alabilir
- Cezalı kullanıcılar emanet alamaz

### Tarih Kontrolü
- Beklenen teslim tarihi geçmiş emanetler gecikmiş olarak işaretlenir
- Gecikmiş emanetler için kullanıcı cezalı duruma düşer

## Test

### EmanetTestForm
API endpoint'lerini test etmek için kullanılır:
- Tüm endpoint'leri test et
- Emanet ekleme testi
- Emanet iade testi
- Arama testi
- Gecikmiş emanetler testi

## Kullanım

1. **Emanet Alma**:
   - Dashboard'dan "Emanet İşlemleri" butonuna tıkla
   - "Yeni Emanet" butonuna tıkla
   - Kullanıcı ve kitap seç
   - "Emanet Ver" butonuna tıkla

2. **Emanet İade**:
   - Emanet listesinden iade edilecek emaneti seç
   - "İade Et" butonuna tıkla
   - Onay ver

3. **Arama**:
   - Arama kutusuna terim gir
   - Otomatik arama sonuçları görüntülenir

## Hata Yönetimi

- API bağlantı hataları için try-catch blokları
- Kullanıcı dostu hata mesajları
- Stok yetersizliği uyarıları
- Gecikmiş emanet uyarıları

## Gelecek Geliştirmeler

- Emanet geçmişi raporu
- Toplu emanet işlemleri
- E-posta bildirimleri
- SMS bildirimleri
- Ceza hesaplama sistemi 