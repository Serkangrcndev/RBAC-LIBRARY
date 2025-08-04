using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic; // Added for List

namespace Seker_kutuphane
{
    public class ApiHelper
    {
        private readonly string apiBaseUrl = "YOU_HERE_APİ_URL";
        private readonly string username = "APİ_USERNAME";
        private readonly string password = "APİ_PASSWORD";
        private readonly HttpClient client;
        private string? sessionId;

        public ApiHelper()
        {
            client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        // Login işlemi: POST /login
        public async Task<(string sessionId, dynamic user)> LoginAsync(string tc, string sifre)
        {
            var payload = new { tc, sifre };
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{apiBaseUrl}/login-tc", content); // endpoint değişti
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            
            dynamic obj = JsonConvert.DeserializeObject(json);
            sessionId = obj.sessionId;
            
            // Session-ID header'ı ekle
            client.DefaultRequestHeaders.Remove("Session-ID");
            client.DefaultRequestHeaders.Add("Session-ID", sessionId);
            
            return (sessionId, obj.user);
        }

        // Kayıt olma işlemi: POST /register
        public async Task<dynamic> RegisterAsync(object userData)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(userData);
                System.Windows.Forms.MessageBox.Show($"Gönderilen JSON: {jsonData}", "Debug Info");
                
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                
                // Sadece /register endpoint'ini kullan
                var response = await client.PostAsync($"{apiBaseUrl}/register", content);
                
                var responseContent = await response.Content.ReadAsStringAsync();
                System.Windows.Forms.MessageBox.Show($"API Response Status: {response.StatusCode}\nResponse Content: {responseContent}", "API Response");
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP {response.StatusCode}: {responseContent}");
                }
                
                return JsonConvert.DeserializeObject(responseContent);
            }
            catch (HttpRequestException ex)
            {
                System.Windows.Forms.MessageBox.Show($"HTTP Error: {ex.Message}", "Error");
                throw;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"General Error: {ex.Message}", "Error");
                throw;
            }
        }

        // Kullanıcıları getir: GET /kullanicilar
        public async Task<dynamic> GetAllUsersAsync()
        {
            var response = await client.GetAsync($"{apiBaseUrl}/kullanicilar");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject(json) ?? new List<object>();
        }

        // Roller: GET /roller
        public async Task<dynamic> GetRolesAsync()
        {
            var response = await client.GetAsync($"{apiBaseUrl}/roller");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject(json) ?? new List<object>();
        }

        // TC doğrulama: POST /verify-tc
        public async Task<bool> VerifyTCAsync(string tc)
        {
            try
            {
                var payload = new { tc };
                var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{apiBaseUrl}/verify-tc", content);
                var json = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    return false;
                dynamic obj = JsonConvert.DeserializeObject(json);
                return obj.verified == true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Hata");
                return false;
            }
        }

        // API endpoint'lerini test et
        public async Task<string> TestEndpointsAsync()
        {
            var endpoints = new[] { "/register", "/kayit", "/user", "/users", "/kullanici", "/kitaplar" };
            var results = new List<string>();
            
            foreach (var endpoint in endpoints)
            {
                try
                {
                    var response = await client.GetAsync($"{apiBaseUrl}{endpoint}");
                    results.Add($"{endpoint}: {response.StatusCode}");
                    
                    // Kitaplar endpoint'i için detaylı bilgi
                    if (endpoint == "/kitaplar")
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        results.Add($"Kitaplar Response Length: {content?.Length ?? 0}");
                        results.Add($"Kitaplar Response Preview: {content?.Substring(0, Math.Min(200, content?.Length ?? 0))}");
                    }
                }
                catch (Exception ex)
                {
                    results.Add($"{endpoint}: Error - {ex.Message}");
                }
            }
            
            return string.Join("\n", results);
        }

        // Şifre sıfırlama: POST /reset-password
        public async Task<bool> ResetPasswordAsync(string tc, string newPasswordHash)
        {
            var payload = new { tc, sifre = newPasswordHash };
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{apiBaseUrl}/reset-password", content);
            if (!response.IsSuccessStatusCode)
                return false;
            var json = await response.Content.ReadAsStringAsync();
            dynamic obj = JsonConvert.DeserializeObject(json);
            return obj.success == true;
        }

        // Kullanıcının ödünç aldığı kitapları getir: GET /kullanici-odunc/{userId}
        public async Task<dynamic> GetKitaplarimAsync(int kullaniciId)
        {
            try
            {
                // Yeni API dokümantasyonuna göre endpoint
                var response = await client.GetAsync($"{apiBaseUrl}/kullanici-odunc/{kullaniciId}");
                
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // 404 hatası durumunda boş liste döndür
                    return new { kitaplar = new List<object>() };
                }
                
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject(json);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show($"Kitaplarınız yüklenirken hata oluştu: Hata oluştu.", "Hata", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                // Hata durumunda boş liste döndür
                return new { kitaplar = new List<object>() };
            }
        }

        // Tüm kitapları getir: GET /kitaplar
        public async Task<dynamic> GetAllBooksAsync()
        {
            try
            {
                var fullUrl = $"{apiBaseUrl}/kitaplar";
                var response = await client.GetAsync(fullUrl);
                
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new List<object>();
                }
                
                if (!response.IsSuccessStatusCode)
                {
                    return new List<object>();
                }
                
                var json = await response.Content.ReadAsStringAsync();
                
                if (string.IsNullOrEmpty(json))
                {
                    return new List<object>();
                }
                
                var result = JsonConvert.DeserializeObject(json) ?? new List<object>();
                return result;
            }
            catch (Exception)
            {
                return new List<object>();
            }
        }

        // Kitap arama: GET /kitap-bul
        public async Task<dynamic> SearchBooksAsync(string searchTerm = "", string filterType = "")
        {
            try
            {
                var queryParams = new List<string>();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    // Filter türüne göre parametre adını belirle
                    string paramName = filterType switch
                    {
                        "kitap_adi" => "kitap_adi",
                        "yazar" => "yazar",
                        "yil" => "yil",
                        "yayinevi" => "yayinevi",
                        _ => "q" // Varsayılan genel arama
                    };
                    queryParams.Add($"{paramName}={Uri.EscapeDataString(searchTerm)}");
                }
                if (!string.IsNullOrEmpty(filterType) && filterType != "q")
                {
                    queryParams.Add($"filter={Uri.EscapeDataString(filterType)}");
                }

                var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
                var fullUrl = $"{apiBaseUrl}/kitap-bul{queryString}";
                
                // Debug: URL'yi yazdır
                Console.WriteLine($"SearchBooksAsync - Filter Type: {filterType}");
                Console.WriteLine($"SearchBooksAsync - Search Term: {searchTerm}");
                Console.WriteLine($"API URL: {fullUrl}");
                
                var response = await client.GetAsync(fullUrl);
                
                Console.WriteLine($"Response Status: {response.StatusCode}");
                
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // 404 hatası durumunda boş liste döndür
                    Console.WriteLine("404 Not Found - Boş liste döndürülüyor");
                    return new List<object>();
                }
                
                if (!response.IsSuccessStatusCode)
                {
                    // API'den veri gelmezse boş liste döndür
                    Console.WriteLine($"API Error: {response.StatusCode} - Boş liste döndürülüyor");
                    return new List<object>();
                }
                
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Response: {json}");
                
                var result = JsonConvert.DeserializeObject(json) ?? new List<object>();
                Console.WriteLine($"Parsed Result Type: {result?.GetType()}");
                
                return result;
            }
            catch (Exception ex)
            {
                // Hata durumunda boş liste döndür
                Console.WriteLine($"SearchBooksAsync Error: {ex.Message}");
                return new List<object>();
            }
        }

        // Emanet işlemleri için metodlar (ODUNC_ISLEMLERI tablosu kullanılıyor)
        // Tüm emanet işlemlerini getir: GET /odunc-islemleri
        public async Task<dynamic> GetAllOdunclerAsync()
        {
            try
            {
                var response = await client.GetAsync($"{apiBaseUrl}/odunc-islemleri");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAllEmanetlerAsync Error: {ex.Message}");
                return new List<object>();
            }
        }

        // Yeni emanet oluştur: POST /odunc-ekle
        public async Task<dynamic> CreateOduncAsync(object oduncData)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(oduncData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                
                Console.WriteLine($"CreateOduncAsync - URL: {apiBaseUrl}/odunc-ekle");
                Console.WriteLine($"CreateOduncAsync - Data: {jsonData}");
                
                var response = await client.PostAsync($"{apiBaseUrl}/odunc-ekle", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine($"CreateOduncAsync - Status: {response.StatusCode}");
                Console.WriteLine($"CreateOduncAsync - Response: {responseContent}");
                
                // API yanıtını parse et
                var result = JsonConvert.DeserializeObject(responseContent);
                
                // Başarılı yanıt aldıktan sonra, gerçekten kayıt oluşup oluşmadığını kontrol et
                if (response.IsSuccessStatusCode)
                {
                    // 2 saniye bekle ve sonra tüm ödünçleri kontrol et
                    await Task.Delay(2000);
                    
                    try
                    {
                        var allOduncler = await GetAllOdunclerAsync();
                        Console.WriteLine($"CreateOduncAsync - Kontrol: Toplam ödünç sayısı: {allOduncler?.GetType()}");
                        
                        if (allOduncler is Newtonsoft.Json.Linq.JArray oduncArray)
                        {
                            Console.WriteLine($"CreateOduncAsync - Kontrol: JArray count: {oduncArray.Count}");
                            
                            // Son eklenen ödünçü bul
                            var lastOdunc = oduncArray.LastOrDefault();
                            if (lastOdunc != null)
                            {
                                Console.WriteLine($"CreateOduncAsync - Kontrol: Son ödünç ID: {lastOdunc["odunc_id"]}");
                                Console.WriteLine($"CreateOduncAsync - Kontrol: Son ödünç kullanıcı: {lastOdunc["kullanici_id"]}");
                                Console.WriteLine($"CreateOduncAsync - Kontrol: Son ödünç kitap: {lastOdunc["kitap_id"]}");
                            }
                        }
                    }
                    catch (Exception checkEx)
                    {
                        Console.WriteLine($"CreateOduncAsync - Kontrol hatası: {checkEx.Message}");
                    }
                }
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP {response.StatusCode}: {responseContent}");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CreateOduncAsync Error: {ex.Message}");
                throw;
            }
        }

        // Emanet iade et: PUT /odunc-iade/{emanetId}
        public async Task<dynamic> ReturnEmanetAsync(int emanetId)
        {
            try
            {
                var response = await client.PutAsync($"{apiBaseUrl}/odunc-iade/{emanetId}", null);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP {response.StatusCode}: {responseContent}");
                }
                
                return JsonConvert.DeserializeObject(responseContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ReturnEmanetAsync Error: {ex.Message}");
                throw;
            }
        }

        // Emanet arama: GET /odunc-islemleri (filtreleme client tarafında yapılacak)
        public async Task<dynamic> SearchOdunclerAsync(string searchTerm = "")
        {
            try
            {
                var response = await client.GetAsync($"{apiBaseUrl}/odunc-islemleri");
                
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new List<object>();
                }
                
                if (!response.IsSuccessStatusCode)
                {
                    return new List<object>();
                }
                
                var json = await response.Content.ReadAsStringAsync();
                var allOduncler = JsonConvert.DeserializeObject(json);
                
                // Eğer arama terimi boşsa, sadece aktif ödünçleri döndür
                if (string.IsNullOrEmpty(searchTerm))
                {
                    return allOduncler;
                }
                
                // Arama terimi varsa, hem arama hem de aktif filtreleme yap
                if (allOduncler is Newtonsoft.Json.Linq.JArray oduncArray)
                {
                    var filteredResults = new List<object>();
                    
                    foreach (var odunc in oduncArray)
                    {
                        // Önce aktif ödünç kontrolü yap
                        var teslimEdildi = odunc["teslim_edildi"]?.ToString() ?? "0";
                        if (teslimEdildi == "1" || teslimEdildi == "True" || teslimEdildi == "true")
                        {
                            continue; // İade edilmiş ödünçleri atla
                        }
                        
                        // Sonra arama kontrolü yap
                        var kullaniciAdi = $"{odunc["ad"]?.ToString() ?? ""} {odunc["soyad"]?.ToString() ?? ""}".ToLower();
                        var kitapAdi = odunc["title"]?.ToString()?.ToLower() ?? "";
                        var searchLower = searchTerm.ToLower();
                        
                        if (kullaniciAdi.Contains(searchLower) || kitapAdi.Contains(searchLower))
                        {
                            filteredResults.Add(odunc);
                        }
                    }
                    
                    return filteredResults;
                }
                
                return allOduncler;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SearchOdunclerAsync Error: {ex.Message}");
                return new List<object>();
            }
        }

        // Gecikmiş emanetleri getir: GET /odunc-islemleri (client tarafında filtreleme)
        public async Task<dynamic> GetGecikmisEmanetlerAsync()
        {
            try
            {
                var response = await client.GetAsync($"{apiBaseUrl}/odunc-islemleri");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetGecikmisEmanetlerAsync Error: {ex.Message}");
                return new List<object>();
            }
        }

        // Kullanıcı emanetlerini getir: GET /kullanici-odunc/{kullaniciId}
        public async Task<dynamic> GetKullaniciEmanetlerAsync(int kullaniciId)
        {
            try
            {
                var response = await client.GetAsync($"{apiBaseUrl}/kullanici-odunc/{kullaniciId}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetKullaniciEmanetlerAsync Error: {ex.Message}");
                return new List<object>();
            }
        }

        // Kullanıcının güncel rolünü getir
        public async Task<dynamic> GetCurrentUserRoleAsync(int kullaniciId)
        {
            try
            {
                // Kullanıcı bilgilerini getir ve rolünü çıkar
                var response = await client.GetAsync($"{apiBaseUrl}/kullanicilar");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                
                var kullanicilar = JsonConvert.DeserializeObject(json);
                
                if (kullanicilar is Newtonsoft.Json.Linq.JArray kullaniciArray)
                {
                    var kullanici = kullaniciArray.FirstOrDefault(k => 
                        k["kullanici_id"]?.ToString() == kullaniciId.ToString());
                    
                    if (kullanici != null)
                    {
                        var rol = kullanici["rol"] ?? kullanici["rol_adi"];
                        return rol;
                    }
                }
                
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetCurrentUserRoleAsync Error: {ex.Message}");
                return null;
            }
        }

        // Profil güncelleme: PUT /kullanici-guncelle
        public async Task<dynamic> UpdateUserProfileAsync(object userData)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(userData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                
                Console.WriteLine($"UpdateUserProfileAsync - URL: {apiBaseUrl}/kullanici-guncelle");
                Console.WriteLine($"UpdateUserProfileAsync - Data: {jsonData}");
                
                var response = await client.PutAsync($"{apiBaseUrl}/kullanici-guncelle", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine($"UpdateUserProfileAsync - Status: {response.StatusCode}");
                Console.WriteLine($"UpdateUserProfileAsync - Response: {responseContent}");
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP {response.StatusCode}: {responseContent}");
                }
                
                return JsonConvert.DeserializeObject(responseContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdateUserProfileAsync Error: {ex.Message}");
                throw;
            }
        }

        // Şifre güncelleme
        public async Task<dynamic> UpdatePasswordAsync(int kullaniciId, string mevcutSifre, string yeniSifre)
        {
            try
            {
                var updateData = new
                {
                    kullanici_id = kullaniciId,
                    mevcut_sifre = mevcutSifre, // Hash'lenmemiş mevcut şifre
                    yeni_sifre = yeniSifre // Hash'lenmiş yeni şifre
                };
                
                var jsonData = JsonConvert.SerializeObject(updateData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                
                Console.WriteLine($"UpdatePasswordAsync - URL: {apiBaseUrl}/change-password");
                Console.WriteLine($"UpdatePasswordAsync - Data: {jsonData}");
                
                var response = await client.PostAsync($"{apiBaseUrl}/change-password", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine($"UpdatePasswordAsync - Status: {response.StatusCode}");
                Console.WriteLine($"UpdatePasswordAsync - Response: {responseContent}");
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP {response.StatusCode}: {responseContent}");
                }
                
                return JsonConvert.DeserializeObject(responseContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdatePasswordAsync Error: {ex.Message}");
                throw;
            }
        }

        // KULLANICI_ROLLERI tablosuna rol ekleme: PUT /kullanici-guncelle
        public async Task<dynamic> AddRoleToUserAsync(int kullaniciId, int rolId)
        {
            try
            {
                // Önce mevcut kullanıcı bilgilerini al
                var allUsers = await GetAllUsersAsync();
                dynamic currentUser = null;
                
                if (allUsers is Newtonsoft.Json.Linq.JArray usersArray)
                {
                    foreach (var user in usersArray)
                    {
                        if (Convert.ToInt32(user["kullanici_id"]) == kullaniciId)
                        {
                            currentUser = user;
                            break;
                        }
                    }
                }
                
                if (currentUser == null)
                {
                    throw new Exception("Kullanıcı bulunamadı");
                }
                
                // Mevcut rol ID'lerini al
                var currentRolIds = new List<int>();
                if (currentUser["rol_ids"] != null)
                {
                    var rolIdsArray = currentUser["rol_ids"] as Newtonsoft.Json.Linq.JArray;
                    if (rolIdsArray != null)
                    {
                        foreach (var rolIdItem in rolIdsArray)
                        {
                            currentRolIds.Add(Convert.ToInt32(rolIdItem));
                        }
                    }
                }
                
                // Yeni rol ID'sini ekle (eğer zaten yoksa)
                if (!currentRolIds.Contains(rolId))
                {
                    currentRolIds.Add(rolId);
                }
                
                // Güncelleme verisi hazırla
                var updateData = new
                {
                    kullanici_id = kullaniciId,
                    ad = currentUser["ad"],
                    soyad = currentUser["soyad"],
                    tc = currentUser["tc"],
                    telefon = currentUser["telefon"],
                    email = currentUser["email"],
                    rol_ids = currentRolIds.ToArray()
                };
                
                var jsonData = JsonConvert.SerializeObject(updateData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                
                Console.WriteLine($"AddRoleToUserAsync - URL: {apiBaseUrl}/kullanici-guncelle");
                Console.WriteLine($"AddRoleToUserAsync - Data: {jsonData}");
                
                var response = await client.PutAsync($"{apiBaseUrl}/kullanici-guncelle", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine($"AddRoleToUserAsync - Status: {response.StatusCode}");
                Console.WriteLine($"AddRoleToUserAsync - Response: {responseContent}");
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP {response.StatusCode}: {responseContent}");
                }
                
                return JsonConvert.DeserializeObject(responseContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddRoleToUserAsync Error: {ex.Message}");
                throw;
            }
        }

        // Kullanıcı silme (soft delete) - status'u 0 yap
        public async Task<dynamic> DeleteUserAsync(int kullaniciId)
        {
            try
            {
                Console.WriteLine($"DeleteUserAsync - URL: {apiBaseUrl}/kullanici-sil/{kullaniciId}");
                
                var response = await client.DeleteAsync($"{apiBaseUrl}/kullanici-sil/{kullaniciId}");
                var responseContent = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine($"DeleteUserAsync - Status: {response.StatusCode}");
                Console.WriteLine($"DeleteUserAsync - Response: {responseContent}");
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP {response.StatusCode}: {responseContent}");
                }
                
                return JsonConvert.DeserializeObject(responseContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DeleteUserAsync Error: {ex.Message}");
                throw;
            }
        }

        public async Task<dynamic> ActivateUserAsync(int kullaniciId)
        {
            try
            {
                // Önce kullanıcının mevcut bilgilerini al
                var allUsers = await GetAllUsersAsync();
                dynamic currentUser = null;
                
                if (allUsers is Newtonsoft.Json.Linq.JArray usersArray)
                {
                    foreach (var user in usersArray)
                    {
                        if (Convert.ToInt32(user["kullanici_id"]) == kullaniciId)
                        {
                            currentUser = user;
                            break;
                        }
                    }
                }

                if (currentUser == null)
                {
                    throw new Exception("Kullanıcı bulunamadı");
                }

                // Kullanıcının status'unu 1 yaparak aktifleştir
                var updateData = new
                {
                    kullanici_id = kullaniciId,
                    ad = currentUser["ad"],
                    soyad = currentUser["soyad"],
                    tc = currentUser["tc"],
                    telefon = currentUser["telefon"],
                    email = currentUser["email"],
                    status = 1
                };

                var jsonData = JsonConvert.SerializeObject(updateData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                
                Console.WriteLine($"ActivateUserAsync - URL: {apiBaseUrl}/kullanici-guncelle");
                Console.WriteLine($"ActivateUserAsync - Data: {jsonData}");
                
                var response = await client.PutAsync($"{apiBaseUrl}/kullanici-guncelle", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine($"ActivateUserAsync - Status: {response.StatusCode}");
                Console.WriteLine($"ActivateUserAsync - Response: {responseContent}");
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP {response.StatusCode}: {responseContent}");
                }
                
                return JsonConvert.DeserializeObject(responseContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ActivateUserAsync Error: {ex.Message}");
                throw;
            }
        }

        public async Task<dynamic> AddBookAsync(dynamic bookData)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(bookData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                
                Console.WriteLine($"AddBookAsync - URL: {apiBaseUrl}/kitap-ekle");
                Console.WriteLine($"AddBookAsync - Data: {jsonData}");
                
                var response = await client.PostAsync($"{apiBaseUrl}/kitap-ekle", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine($"AddBookAsync - Status: {response.StatusCode}");
                Console.WriteLine($"AddBookAsync - Response: {responseContent}");
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP {response.StatusCode}: {responseContent}");
                }
                
                return JsonConvert.DeserializeObject(responseContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddBookAsync Error: {ex.Message}");
                throw;
            }
        }

        public async Task<dynamic> UpdateBookAsync(dynamic bookData)
        {
            try
            {
                // kitap_id'yi al
                int kitapId = Convert.ToInt32(bookData.kitap_id);
                
                // kitap_id'yi çıkar ve sadece güncellenecek verileri al
                var updateData = new
                {
                    kitap_adi = bookData.kitap_adi,
                    yazar = bookData.yazar,
                    yayinevi = bookData.yayinevi,
                    basim_yili = bookData.date, // API'de basim_yili olarak tanımlı
                    kitap_adet = bookData.kitap_adet,
                    sayfa_sayisi = bookData.sayfa_sayisi // Sayfa sayısı alanını ekle
                };
                
                var jsonData = JsonConvert.SerializeObject(updateData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                
                Console.WriteLine($"UpdateBookAsync - URL: {apiBaseUrl}/kitap-guncelle/{kitapId}");
                Console.WriteLine($"UpdateBookAsync - Data: {jsonData}");
                
                var response = await client.PutAsync($"{apiBaseUrl}/kitap-guncelle/{kitapId}", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine($"UpdateBookAsync - Status: {response.StatusCode}");
                Console.WriteLine($"UpdateBookAsync - Response: {responseContent}");
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP {response.StatusCode}: {responseContent}");
                }
                
                return JsonConvert.DeserializeObject(responseContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdateBookAsync Error: {ex.Message}");
                throw;
            }
        }

        public async Task<dynamic> DeleteBookAsync(int kitapId)
        {
            try
            {
                Console.WriteLine($"DeleteBookAsync - URL: {apiBaseUrl}/kitap-sil/{kitapId}");
                
                var response = await client.DeleteAsync($"{apiBaseUrl}/kitap-sil/{kitapId}");
                var responseContent = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine($"DeleteBookAsync - Status: {response.StatusCode}");
                Console.WriteLine($"DeleteBookAsync - Response: {responseContent}");
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP {response.StatusCode}: {responseContent}");
                }
                
                return JsonConvert.DeserializeObject(responseContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DeleteBookAsync Error: {ex.Message}");
                throw;
            }
        }

        // Emanet endpoint'lerini test et
        public async Task<string> TestEmanetEndpointsAsync()
        {
            var results = new List<string>();
            
            try
            {
                // Test odunc-islemleri endpoint (mevcut API'deki endpoint)
                try
                {
                    var response = await client.GetAsync($"{apiBaseUrl}/odunc-islemleri");
                    var content = await response.Content.ReadAsStringAsync();
                    results.Add($"GET /odunc-islemleri: {response.StatusCode}");
                    results.Add($"Response: {content}");
                }
                catch (Exception ex)
                {
                    results.Add($"GET /odunc-islemleri: Error - {ex.Message}");
                }

                // Test odunc-ekle endpoint (mevcut API'deki endpoint)
                try
                {
                    var testData = new { kullanici_id = 30, kitap_id = 229, odunc_tarihi = "2024-01-01", iade_tarihi = "2024-02-01", durum = "AKTİF" };
                    var content = new StringContent(JsonConvert.SerializeObject(testData), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{apiBaseUrl}/odunc-ekle", content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    results.Add($"POST /odunc-ekle: {response.StatusCode}");
                    results.Add($"Response: {responseContent}");
                }
                catch (Exception ex)
                {
                    results.Add($"POST /odunc-ekle: Error - {ex.Message}");
                }

                // Test emanet-ekle endpoint (alternatif endpoint)
                try
                {
                    var testData = new { kullanici_id = 30, kitap_id = 229, odunc_tarihi = "2024-01-01", iade_tarihi = "2024-02-01", durum = "AKTİF" };
                    var content = new StringContent(JsonConvert.SerializeObject(testData), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{apiBaseUrl}/emanet-ekle", content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    results.Add($"POST /emanet-ekle: {response.StatusCode}");
                    results.Add($"Response: {responseContent}");
                }
                catch (Exception ex)
                {
                    results.Add($"POST /emanet-ekle: Error - {ex.Message}");
                }

                // Test odunc-iade endpoint (mevcut API'deki endpoint)
                try
                {
                    var response = await client.PutAsync($"{apiBaseUrl}/odunc-iade/1", null);
                    var content = await response.Content.ReadAsStringAsync();
                    results.Add($"PUT /odunc-iade/1: {response.StatusCode}");
                    results.Add($"Response: {content}");
                }
                catch (Exception ex)
                {
                    results.Add($"PUT /odunc-iade/1: Error - {ex.Message}");
                }

                // Test kullanici-odunc endpoint (mevcut API'deki endpoint)
                try
                {
                    var response = await client.GetAsync($"{apiBaseUrl}/kullanici-odunc/1");
                    var content = await response.Content.ReadAsStringAsync();
                    results.Add($"GET /kullanici-odunc/1: {response.StatusCode}");
                    results.Add($"Response: {content}");
                }
                catch (Exception ex)
                {
                    results.Add($"GET /kullanici-odunc/1: Error - {ex.Message}");
                }

                return string.Join("\n", results);
            }
            catch (Exception ex)
            {
                return $"Test Error: {ex.Message}";
            }
        }
    }
} 