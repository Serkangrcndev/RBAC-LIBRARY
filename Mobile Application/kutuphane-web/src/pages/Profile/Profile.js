import React, { useState, useEffect } from "react";
import { useSelector, useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import "./Profile.css";
import { useUpdateUserMutation } from '../../store/api/usersApi';
import { selectCurrentUser, selectIsAuthenticated, updateUser, selectIsAuthLoaded } from '../../store/slices/authSlice';
import { useGetRolesQuery } from '../../store/api/usersApi';

const Profile = () => {
  const [editMode, setEditMode] = useState(false);
  const [form, setForm] = useState({});
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  // Redux hooks
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const currentUser = useSelector(selectCurrentUser);
  const isAuthenticated = useSelector(selectIsAuthenticated);
  const isAuthLoaded = useSelector(selectIsAuthLoaded); // Yeni eklendi
  
  // Debug: Current user bilgilerini console'a yazdır
  console.log('Profile Component - Current User:', currentUser);
  console.log('Profile Component - Is Authenticated:', isAuthenticated);

  const [updateUserMutation, { isLoading: isUpdating }] = useUpdateUserMutation();
  const { data: roles = [], isLoading: rolesLoading } = useGetRolesQuery();

  // Current User verisi ile form'u initialize et
  useEffect(() => {
    if (currentUser) {
      console.log('Profile Component - Initializing form with currentUser data:', currentUser);
      setForm({
        ad: currentUser.ad || '',
        soyad: currentUser.soyad || '',
        email: currentUser.email || '',
        telefon: currentUser.telefon || '',
        tc: currentUser.tc || '',
        rol_adi: currentUser.ana_rol_adi || '',
      });
    }
  }, [currentUser]);

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
    // Error state'i temizle kullanıcı yazmaya başladığında
    if (error) {
      setError("");
    }
  };

  const handleSave = async (e) => {
    e.preventDefault();
    setError("");
    setSuccess("");
    
    // Form validasyonu
    if (!form.ad.trim() || !form.soyad.trim() || !form.email.trim()) {
      setError("Ad, soyad ve e-posta alanları zorunludur.");
      return;
    }

    // Email validasyonu
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(form.email)) {
      setError("Geçerli bir e-posta adresi giriniz.");
      return;
    }

    // Telefon validasyonu (opsiyonel ama geçerli olmalı)
    if (form.telefon && form.telefon.trim()) {
      const phoneRegex = /^[\d\s\-+()]+$/;
      if (!phoneRegex.test(form.telefon) || form.telefon.length < 10) {
        setError("Geçerli bir telefon numarası giriniz.");
        return;
      }
    }
    
    try {
      console.log('Profile Component - Updating user with data:', {
        id: currentUser.kullanici_id,
        ad: form.ad.trim(),
        soyad: form.soyad.trim(),
        email: form.email.trim(),
        telefon: form.telefon.trim() || null,
        tc: form.tc,
        durum: currentUser.status ? 1 : 0,
        rol_ids: [currentUser.rol_id]
      });

      // RTK Query mutation ile kullanıcı güncelleme
      const updatedUser = await updateUserMutation({
        id: currentUser.kullanici_id,
        ad: form.ad.trim(),
        soyad: form.soyad.trim(),
        email: form.email.trim(),
        telefon: form.telefon.trim() || null,
        tc: form.tc,
        durum: currentUser.status ? 1 : 0,
        rol_ids: [currentUser.rol_id]
      }).unwrap();
      
      console.log('Profile Component - Update successful:', updatedUser);

      // Redux store'daki user bilgilerini güncelle
      dispatch(updateUser({
        ad: form.ad.trim(),
        soyad: form.soyad.trim(),
        email: form.email.trim(),
        telefon: form.telefon.trim() || null
      }));

      setEditMode(false);
      setSuccess("Profil başarıyla güncellendi!");
      
      // Success mesajını 3 saniye sonra temizle
      setTimeout(() => {
        setSuccess("");
      }, 3000);
    } catch (err) {
      console.error('Profil güncelleme hatası:', err);
      setError(err?.data?.message || err?.message || 'Profil güncellenirken bir hata oluştu');
    }
  };

  const handleCancel = () => {
    // Form'u orijinal verilerle resetle
    if (currentUser) {
      setForm({
        ad: currentUser.ad || '',
        soyad: currentUser.soyad || '',
        email: currentUser.email || '',
        telefon: currentUser.telefon || '',
        tc: currentUser.tc || '',
        rol_adi: currentUser.ana_rol_adi || '',
      });
    }
    setEditMode(false);
    setError("");
    setSuccess("");
  };

  // Auth ve veri yükleme kontrolü
  useEffect(() => {
    // Debug logları
    console.log('Profile useEffect - State:', {
      isAuthenticated,
      currentUser,
      isUpdating,
      rolesLoading,
      isAuthLoaded
    });

    // ProtectedRoute zaten auth kontrolü yapıyor, burada gerek yok

  }, [isAuthenticated, currentUser, navigate, isUpdating, rolesLoading, isAuthLoaded]); // isAuthLoaded bağımlılıklara eklendi

  // Yükleme durumu için render
  if (!isAuthLoaded || isUpdating || rolesLoading || (isAuthenticated && !currentUser)) {
    return (
      <div className="profile-page">
        <div className="profile-error-card">
          <div className="error-icon">⏳</div>
          <h3>Profil Yükleniyor...</h3>
          <p>Lütfen bekleyiniz.</p>
        </div>
      </div>
    );
  }

  // ProtectedRoute zaten auth kontrolü yapıyor

  return (
    <div className="profile-page">
      <div className="profile-container">
        {/* Header Section */}
        <div className="profile-header">
          <div className="profile-avatar-section">
            <div className="avatar-wrapper">
              <img 
                className="profile-avatar" 
                src={`https://ui-avatars.com/api/?name=${encodeURIComponent(currentUser.ad || '')}+${encodeURIComponent(currentUser.soyad || '')}&background=388d34&color=fff&size=150&bold=true`} 
                alt="Profil Fotoğrafı" 
              />
              <div className="avatar-overlay">
                <span className="camera-icon">📷</span>
              </div>
            </div>
          </div>
          <div className="profile-title">
            <h1>{currentUser.ad || 'Ad Yok'} {currentUser.soyad || 'Soyad Yok'}</h1>
            <p className="profile-subtitle">{currentUser.ana_rol_adi || 'Rol Belirtilmemiş'}</p>
            <p className="profile-id">Kullanıcı ID: {currentUser.kullanici_id}</p>
          </div>
        </div>

        {/* Alert Messages */}
        {success && (
          <div className="alert alert-success">
            <span className="alert-icon">✅</span>
            {success}
          </div>
        )}
        {error && (
          <div className="alert alert-error">
            <span className="alert-icon">❌</span>
            {error}
          </div>
        )}

        {/* Profile Content */}
        <div className="profile-content">
          {editMode ? (
            <div className="profile-edit-section">
              <div className="section-header">
                <h2>
                  <span className="section-icon">✏️</span>
                  Profili Düzenle
                </h2>
              </div>
              
              <form className="profile-form" onSubmit={handleSave}>
                <div className="form-grid">
                  <div className="form-group">
                    <label>
                      <span className="label-icon">👤</span>
                      Ad *
                    </label>
                    <input
                      type="text"
                      name="ad"
                      value={form.ad}
                      onChange={handleChange}
                      required
                      disabled={isUpdating}
                      placeholder="Adınızı giriniz"
                      maxLength="50"
                    />
                  </div>

                  <div className="form-group">
                    <label>
                      <span className="label-icon">👤</span>
                      Soyad *
                    </label>
                    <input
                      type="text"
                      name="soyad"
                      value={form.soyad}
                      onChange={handleChange}
                      required
                      disabled={isUpdating}
                      placeholder="Soyadınızı giriniz"
                      maxLength="50"
                    />
                  </div>

                  <div className="form-group">
                    <label>
                      <span className="label-icon">📧</span>
                      E-posta *
                    </label>
                    <input
                      type="email"
                      name="email"
                      value={form.email}
                      onChange={handleChange}
                      required
                      disabled={isUpdating}
                      placeholder="E-posta adresinizi giriniz"
                      maxLength="100"
                    />
                  </div>

                  <div className="form-group">
                    <label>
                      <span className="label-icon">📱</span>
                      Telefon
                    </label>
                    <input
                      type="tel"
                      name="telefon"
                      value={form.telefon}
                      onChange={handleChange}
                      disabled={isUpdating}
                      placeholder="Telefon numaranızı giriniz (opsiyonel)"
                      maxLength="20"
                    />
                  </div>

                  <div className="form-group">
                    <label>
                      <span className="label-icon">🆔</span>
                      TC Kimlik No
                    </label>
                    <input
                      type="text"
                      name="tc"
                      value={form.tc}
                      disabled
                      className="disabled-field"
                      title="TC Kimlik numarası değiştirilemez"
                    />
                  </div>

                  <div className="form-group">
                    <label>
                      <span className="label-icon">🎭</span>
                      Rol
                    </label>
                    <input
                      type="text"
                      name="rol_adi"
                      value={form.rol_adi}
                      disabled
                      className="disabled-field"
                      title="Rol yalnızca sistem yöneticisi tarafından değiştirilebilir"
                    />
                  </div>
                </div>
                
                <div className="form-actions">
                  <button type="submit" className="btn btn-primary" disabled={isUpdating}>
                    {isUpdating ? (
                      <>
                        <span className="spinner"></span>
                        Kaydediliyor...
                      </>
                    ) : (
                      <>
                        <span className="btn-icon">💾</span>
                        Kaydet
                      </>
                    )}
                  </button>
                  <button 
                    type="button" 
                    className="btn btn-secondary"
                    onClick={handleCancel}
                    disabled={isUpdating}
                  >
                    <span className="btn-icon">❌</span>
                    İptal
                  </button>
                </div>
              </form>
            </div>
          ) : (
            <div className="profile-view-section">
              <div className="section-header">
                <h2>
                  <span className="section-icon">📋</span>
                  Profil Bilgileri
                </h2>
              </div>
              
              <div className="profile-info-grid">
                <div className="info-card">
                  <div className="info-icon">👤</div>
                  <div className="info-content">
                    <label>Ad</label>
                    <p>{currentUser.ad || 'Belirtilmemiş'}</p>
                  </div>
                </div>

                <div className="info-card">
                  <div className="info-icon">👤</div>
                  <div className="info-content">
                    <label>Soyad</label>
                    <p>{currentUser.soyad || 'Belirtilmemiş'}</p>
                  </div>
                </div>

                <div className="info-card">
                  <div className="info-icon">📧</div>
                  <div className="info-content">
                    <label>E-posta</label>
                    <p>{currentUser.email || 'Belirtilmemiş'}</p>
                  </div>
                </div>

                <div className="info-card">
                  <div className="info-icon">📱</div>
                  <div className="info-content">
                    <label>Telefon</label>
                    <p>{currentUser.telefon || 'Belirtilmemiş'}</p>
                  </div>
                </div>

                <div className="info-card">
                  <div className="info-icon">🆔</div>
                  <div className="info-content">
                    <label>TC Kimlik No</label>
                    <p>{currentUser.tc || 'Belirtilmemiş'}</p>
                  </div>
                </div>

                <div className="info-card">
                  <div className="info-icon">🎭</div>
                  <div className="info-content">
                    <label>Rol</label>
                    <p>
                      {
                        currentUser?.rol_ids && currentUser.rol_ids.length > 0
                          ? currentUser.rol_ids.map(id => {
                              const role = roles.find(r => r.rol_id === id);
                              return role ? role.rol_adi : `ID: ${id}`;
                            }).join(', ')
                          : 'Belirtilmemiş'
                      }
                    </p>
                  </div>
                </div>

                <div className="info-card">
                  <div className="info-icon">📅</div>
                  <div className="info-content">
                    <label>Kayıt Tarihi</label>
                    <p>{currentUser.insert_date ? new Date(currentUser.insert_date).toLocaleDateString('tr-TR') : 'Belirtilmemiş'}</p>
                  </div>
                </div>

                <div className="info-card">
                  <div className="info-icon">🔄</div>
                  <div className="info-content">
                    <label>Son Güncelleme</label>
                    <p>{currentUser.update_date ? new Date(currentUser.update_date).toLocaleDateString('tr-TR') : 'Belirtilmemiş'}</p>
                  </div>
                </div>

                <div className="info-card">
                  <div className="info-icon">✅</div>
                  <div className="info-content">
                    <label>Hesap Durumu</label>
                    <p style={{ color: currentUser.status ? '#388d34' : '#dc3545' }}>
                      {currentUser.status ? 'Aktif' : 'Pasif'}
                    </p>
                  </div>
                </div>
              </div>

              <div className="profile-actions">
                <button 
                  className="btn btn-primary btn-large" 
                  onClick={() => setEditMode(true)}
                >
                  <span className="btn-icon">✏️</span>
                  Profili Düzenle
                </button>
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default Profile; 