import React, { useState, useEffect } from 'react';
import { Container, Typography, TextField, Button, Tabs, Tab, Box, IconButton, Dialog, DialogTitle, DialogContent, DialogActions, FormControl, InputLabel, Select, MenuItem } from '@mui/material';
import { Search, LibraryBooks, People, MenuBook, History, Edit, Visibility, Add, Dashboard, PersonAdd, BookmarkAdd } from '@mui/icons-material';
import { useGetAllUsersQuery, useCreateUserMutation } from '../../store/api/usersApi';
import { useGetAllBooksQuery } from '../../store/api/booksApi';
import { useGetAllLoansQuery, useCreateLoanMutation, useUpdateLoanMutation, useGetUserLoanStatusQuery } from '../../store/api/loansApi';
import { useGetDashboardStatsQuery } from '../../store/api/statsApi';
import { useSelector } from 'react-redux';
import { selectCurrentUser, selectIsAuthLoaded } from '../../store/slices/authSlice';
import { useNavigate } from 'react-router-dom';

import './Yetkili.css';

function TabPanel(props) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`yetkili-tabpanel-${index}`}
      aria-labelledby={`yetkili-tab-${index}`}
      {...other}
    >
      {value === index && <Box sx={{ pt: 3 }}>{children}</Box>}
    </div>
  );
}

export default function Yetkili() {
  const navigate = useNavigate();
  const [activeTab, setActiveTab] = useState(0);
  
  // RTK Query hooks
  const { data: users = [], isLoading: usersLoading, refetch: refetchUsers } = useGetAllUsersQuery();
  const { data: books = [], isLoading: booksLoading } = useGetAllBooksQuery();
  const { data: loans = [], isLoading: loansLoading, refetch: refetchLoans } = useGetAllLoansQuery();
  const { data: dashboardStats, isLoading: dashboardStatsLoading } = useGetDashboardStatsQuery();
  
  // Mutations
  const [createUser] = useCreateUserMutation();
  const [createLoan] = useCreateLoanMutation();
  const [updateLoan] = useUpdateLoanMutation();

  // Current user from Redux
  const currentUser = useSelector(selectCurrentUser);
  const isAuthLoaded = useSelector(selectIsAuthLoaded);

  // Local state
  const [searchTerm, setSearchTerm] = useState('');
  const [userDialog, setUserDialog] = useState({ open: false, mode: 'view' });
  const [userFormData, setUserFormData] = useState({
    ad: '',
    soyad: '',
    email: '',
    telefon: '',
    tc: '',
    rol_ids: [1], // Varsayılan olarak Üye rolü
    durum: 1,
    sifre: ''
  });

  // Ödünç yönetimi state'leri
  const [loanSearchTerm, setLoanSearchTerm] = useState('');
  const [loanDialog, setLoanDialog] = useState({ open: false, mode: 'add' });
  const [loanFormData, setLoanFormData] = useState({
    kullanici_id: '',
    kitap_id: '',
    odunc_tarihi: new Date().toISOString().split('T')[0],
    iade_tarihi: '',
    durum: 1
  });

  // Kullanıcı ödünç durumu kontrolü için state
  const [selectedUserLoanStatus, setSelectedUserLoanStatus] = useState(null);
  const [showUserStatusWarning, setShowUserStatusWarning] = useState(false);

  // Kullanıcı ödünç durumu sorgusu
  const { data: userLoanStatus } = useGetUserLoanStatusQuery(
    loanFormData.kullanici_id, 
    { skip: !loanFormData.kullanici_id }
  );

  // Kullanıcı seçildiğinde ödünç durumunu kontrol et
  const handleUserSelectionForLoan = (kullanici_id) => {
    setLoanFormData({ ...loanFormData, kullanici_id });
    
    if (kullanici_id) {
      // Kullanıcı ID'si değiştiğinde sorgu otomatik olarak yeniden çalışacak
      setSelectedUserLoanStatus(null);
      setShowUserStatusWarning(false);
    } else {
      setSelectedUserLoanStatus(null);
      setShowUserStatusWarning(false);
    }
  };

  // Kullanıcı ödünç durumu değiştiğinde kontrol et
  useEffect(() => {
    if (userLoanStatus) {
      setSelectedUserLoanStatus(userLoanStatus);
      setShowUserStatusWarning(!userLoanStatus.yeni_kitap_alabilir);
    }
  }, [userLoanStatus]);

  // Yetkili kontrolü
  useEffect(() => {
    if (isAuthLoaded && currentUser) {
      const isYetkili = currentUser.rol_ids && currentUser.rol_ids.includes(2);
      if (!isYetkili) {
        navigate('/');
      }
    }
  }, [isAuthLoaded, currentUser, navigate]);

  const handleTabChange = (event, newValue) => {
    setActiveTab(newValue);
  };

  // Kullanıcı işlemleri
  const handleUserAction = (action, user = null) => {
    if (action === 'add') {
      setUserFormData({
        ad: '',
        soyad: '',
        email: '',
        telefon: '',
        tc: '',
        rol_ids: [1], // Sadece Üye rolü
        durum: 1,
        sifre: ''
      });
      setUserDialog({ open: true, mode: 'add' });
    } else if (action === 'view' && user) {
      setUserFormData({
        ad: user.ad || '',
        soyad: user.soyad || '',
        email: user.email || '',
        telefon: user.telefon || '',
        tc: user.tc || '',
        rol_ids: user.rol_ids || [1],
        durum: user.durum || 1,
        sifre: ''
      });
      setUserDialog({ open: true, mode: 'view', user: user });
    }
  };

  const handleUserFormSubmit = async () => {
    try {
      const submitData = { ...userFormData };
      
      // Eğer şifre alanı boşsa TC'yi şifre olarak kullan
      if (!submitData.sifre || submitData.sifre.trim() === '') {
        submitData.sifre = submitData.tc;
      }

      await createUser(submitData).unwrap();
      alert('Kullanıcı başarıyla eklendi!');
      setUserDialog({ open: false, mode: 'view' });
      refetchUsers();
    } catch (error) {
      alert('İşlem başarısız: ' + (error.data?.message || error.message));
    }
  };

  // Ödünç işlemleri
  const handleLoanAction = (action, loan = null) => {
    if (action === 'add') {
      // 1 ay sonrası için iade tarihi hesapla
      const iadeTarihi = new Date();
      iadeTarihi.setMonth(iadeTarihi.getMonth() + 1);
      
      setLoanFormData({
        kullanici_id: '',
        kitap_id: '',
        odunc_tarihi: new Date().toISOString().split('T')[0], // Varsayılan bugün
        iade_tarihi: iadeTarihi.toISOString().split('T')[0], // Otomatik 1 ay sonrası
        durum: 1
      });
      setLoanDialog({ open: true, mode: 'add' });
    } else if (action === 'edit' && loan) {
      setLoanFormData({
        kullanici_id: loan.kullanici_id || '',
        kitap_id: loan.kitap_id || '',
        odunc_tarihi: loan.odunc_tarihi ? new Date(loan.odunc_tarihi).toISOString().split('T')[0] : '',
        iade_tarihi: loan.iade_tarihi ? new Date(loan.iade_tarihi).toISOString().split('T')[0] : '',
        durum: loan.durum || 1
      });
      setLoanDialog({ open: true, mode: 'edit', loan: loan });
    } else if (action === 'return' && loan) {
      // Sadece teslim tarihi güncellenecek
      setLoanFormData({
        ...loanFormData,
        teslim_tarihi: new Date().toISOString().split('T')[0], // Bugünün tarihi
        status: 0 // Pasif yap
      });
      setLoanDialog({ open: true, mode: 'return', loan: loan });
    } else if (action === 'view' && loan) {
      setLoanDialog({ open: true, mode: 'view', loan: loan });
    }
  };

  const handleLoanFormSubmit = async () => {
    try {
      const submitData = { ...loanFormData };
      
      // Beklenen teslim tarihi hesapla (1 ay sonra)
      if (!submitData.iade_tarihi) {
        const oduncDate = new Date(submitData.odunc_tarihi);
        oduncDate.setMonth(oduncDate.getMonth() + 1);
        submitData.iade_tarihi = oduncDate.toISOString().split('T')[0];
      }

      // Kullanıcı kitap limiti kontrolü
      if (selectedUserLoanStatus && !selectedUserLoanStatus.yeni_kitap_alabilir) {
        alert('Bu kullanıcı maksimum 3 kitap almış durumda. Yeni kitap verilemez.');
        return;
      }

      if (loanDialog.mode === 'add') {
        await createLoan(submitData).unwrap();
        alert('Ödünç işlemi başarıyla oluşturuldu!');
      } else if (loanDialog.mode === 'edit') {
        await updateLoan({ id: loanDialog.loan.odunc_id, ...submitData }).unwrap();
        alert('Ödünç işlemi başarıyla güncellendi!');
      } else if (loanDialog.mode === 'return') {
        // Sadece iade işlemi için: status ve teslim_tarihi güncellenecek
        await updateLoan({ 
          id: loanDialog.loan.odunc_id, 
          teslim_tarihi: submitData.teslim_tarihi, 
          status: 0 
        }).unwrap();
        alert('Kitap başarıyla iade edildi!');
      }
      
      setLoanDialog({ open: false, mode: 'view' });
      refetchLoans();
    } catch (error) {
      alert('İşlem başarısız: ' + (error.data?.message || error.message));
    }
  };

  // Filtrelenmiş veriler
  const filteredUsers = users.filter(user =>
    user.ad?.toLowerCase().includes(searchTerm.toLowerCase()) ||
    user.soyad?.toLowerCase().includes(searchTerm.toLowerCase()) ||
    user.email?.toLowerCase().includes(searchTerm.toLowerCase()) ||
    user.tc?.includes(searchTerm)
  );

  const filteredLoans = loans.filter(loan => {
    const searchTermLower = loanSearchTerm.toLowerCase();

    return (
      (loan.ad?.toLowerCase().includes(searchTermLower) ||
      loan.soyad?.toLowerCase().includes(searchTermLower) ||
      loan.email?.toLowerCase().includes(searchTermLower) ||
      loan.tc?.includes(searchTermLower)) ||
      (loan.kitap_adi?.toLowerCase().includes(searchTermLower) ||
      loan.yazar?.toLowerCase().includes(searchTermLower))
    );
  });

  if (!isAuthLoaded || usersLoading || booksLoading || loansLoading || dashboardStatsLoading) {
    return (
      <Container className="yetkili-loading">
        <Typography variant="h6">Yetkili paneli yükleniyor...</Typography>
      </Container>
    );
  }

  return (
    <Container maxWidth="xl" className="yetkili-container">
      <Typography variant="h3" className="yetkili-title">
        Kütüphane Yetkili Paneli
      </Typography>
      <Typography variant="subtitle1" className="yetkili-subtitle">
        Hoş geldiniz, {currentUser?.ad} {currentUser?.soyad}
      </Typography>

      {/* İstatistik Kartları */}
      <div className="yetkili-stats">
        <div className="stat-card">
          <div className="stat-icon">
            <LibraryBooks sx={{ fontSize: 40, color: '#388d34' }} />
          </div>
          <div className="stat-info">
            <h3>{dashboardStats?.totalBooks || 0}</h3>
            <p>Toplam Kitap</p>
          </div>
        </div>
        <div className="stat-card">
          <div className="stat-icon">
            <People sx={{ fontSize: 40, color: '#388d34' }} />
          </div>
          <div className="stat-info">
            <h3>{dashboardStats?.totalUsers || 0}</h3>
            <p>Toplam Kullanıcı</p>
          </div>
        </div>
        <div className="stat-card">
          <div className="stat-icon">
            <MenuBook sx={{ fontSize: 40, color: '#388d34' }} />
          </div>
          <div className="stat-info">
            <h3>{dashboardStats?.loanedBooks || 0}</h3>
            <p>Ödünç Verilen Kitap</p>
          </div>
        </div>
        <div className="stat-card">
          <div className="stat-icon">
            <History sx={{ fontSize: 40, color: '#d32f2f' }} />
          </div>
          <div className="stat-info">
            <h3>{dashboardStats?.overdueBooks || 0}</h3>
            <p>Gecikmiş Kitap</p>
          </div>
        </div>
      </div>

      {/* Tabs */}
      <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
        <Tabs value={activeTab} onChange={handleTabChange}>
          <Tab icon={<Dashboard />} label="Dashboard" />
          <Tab icon={<PersonAdd />} label="Kullanıcı Kaydı" />
          <Tab icon={<BookmarkAdd />} label="Ödünç İşlemleri" />
        </Tabs>
      </Box>

      {/* Dashboard Tab */}
      <TabPanel value={activeTab} index={0}>
        <div className="dashboard-content">
          <Typography variant="h5" gutterBottom>Kütüphane İstatistikleri</Typography>
          
          <div className="dashboard-cards-container">
            <div className="dashboard-card">
              <Typography variant="h6">Toplam Kullanıcılar</Typography>
              <Typography variant="h4">{dashboardStats?.totalUsers || 0}</Typography>
            </div>
            <div className="dashboard-card">
              <Typography variant="h6">Aktif Kullanıcılar</Typography>
              <Typography variant="h4">{dashboardStats?.activeUsers || 0}</Typography>
            </div>
            <div className="dashboard-card">
              <Typography variant="h6">Toplam Kitaplar</Typography>
              <Typography variant="h4">{dashboardStats?.totalBooks || 0}</Typography>
            </div>
            <div className="dashboard-card">
              <Typography variant="h6">Mevcut Kitaplar</Typography>
              <Typography variant="h4">{dashboardStats?.availableBooks || 0}</Typography>
            </div>
            <div className="dashboard-card">
              <Typography variant="h6">Ödünç Verilen Kitaplar</Typography>
              <Typography variant="h4">{dashboardStats?.loanedBooks || 0}</Typography>
            </div>
            <div className="dashboard-card">
              <Typography variant="h6">Gecikmiş Kitaplar</Typography>
              <Typography variant="h4">{dashboardStats?.overdueBooks || 0}</Typography>
            </div>
          </div>
        </div>
      </TabPanel>

      {/* Kullanıcı Kaydı Tab */}
      <TabPanel value={activeTab} index={1}>
        <div className="users-section">
          <div className="section-header">
            <Typography variant="h5">Kullanıcı Kaydı</Typography>
            <Button
              variant="contained"
              startIcon={<Add />}
              onClick={() => handleUserAction('add')}
              className="add-button"
            >
              Yeni Kullanıcı
            </Button>
          </div>

          <TextField
            fullWidth
            variant="outlined"
            placeholder="Kullanıcı ara..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            InputProps={{
              startAdornment: <Search />
            }}
            className="search-field"
          />

          <div className="users-table">
            <div className="table-header">
              <span>Ad Soyad</span>
              <span>Email</span>
              <span>TC</span>
              <span>Telefon</span>
              <span>Durum</span>
              <span>İşlemler</span>
            </div>
            
            {filteredUsers.map((user) => (
              <div key={user.kullanici_id} className="table-row">
                <span>{user.ad} {user.soyad}</span>
                <span>{user.email}</span>
                <span>{user.tc}</span>
                <span>{user.telefon || '-'}</span>
                <span className={`status ${user.durum === 1 ? 'active' : 'inactive'}`}>
                  {user.durum === 1 ? 'Aktif' : 'Pasif'}
                </span>
                <div className="table-actions">
                  <IconButton onClick={() => handleUserAction('view', user)}>
                    <Visibility />
                  </IconButton>
                </div>
              </div>
            ))}
          </div>
        </div>
      </TabPanel>

      {/* Ödünç İşlemleri Tab */}
      <TabPanel value={activeTab} index={2}>
        <div className="loans-section">
          <div className="section-header">
            <Typography variant="h5">Ödünç İşlemleri</Typography>
            <Button
              variant="contained"
              startIcon={<Add />}
              onClick={() => handleLoanAction('add')}
              className="add-button"
            >
              Yeni Ödünç
            </Button>
          </div>

          <TextField
            fullWidth
            variant="outlined"
            placeholder="Ödünç ara..."
            value={loanSearchTerm}
            onChange={(e) => setLoanSearchTerm(e.target.value)}
            InputProps={{
              startAdornment: <Search />
            }}
            className="search-field"
          />

          <div className="loans-table">
            <div className="table-header">
              <span>Ödünç ID</span>
              <span>Kullanıcı</span>
              <span>Kitap</span>
              <span>Ödünç Tarihi</span>
              <span>Beklenen Teslim</span>
              <span>Teslim Tarihi</span>
              <span>Durum</span>
              <span>İşlemler</span>
            </div>
            
            {filteredLoans.map((loan) => {
              const loanDate = loan.odunc_tarihi ? new Date(loan.odunc_tarihi).toLocaleDateString('tr-TR') : '-';
              const expectedReturnDate = loan.iade_tarihi ? new Date(loan.iade_tarihi).toLocaleDateString('tr-TR') : '-';
              const actualReturnDate = (loan.teslim_edildi === 1 || loan.teslim_edildi === true) ? new Date().toLocaleDateString('tr-TR') : 'İade Edilmedi';
              
              // Durum kontrolü
              let statusText, statusClass;
              if (loan.teslim_edildi === 1 || loan.teslim_edildi === true) {
                statusText = 'İade Edildi';
                statusClass = 'returned';
              } else {
                // Gecikti kontrolü
                const iadeTarihi = loan.iade_tarihi ? new Date(loan.iade_tarihi) : null;
                const bugun = new Date();
                if (iadeTarihi && iadeTarihi < bugun) {
                  statusText = 'Gecikti';
                  statusClass = 'overdue';
                } else {
                  statusText = 'Aktif';
                  statusClass = 'active';
                }
              }

              return (
                <div key={loan.odunc_id} className="table-row">
                  <span>{loan.odunc_id}</span>
                  <span>{loan.ad && loan.soyad ? `${loan.ad} ${loan.soyad}` : 'Bilinmeyen Kullanıcı'}</span>
                  <span>{loan.kitap_adi ? loan.kitap_adi : 'Bilinmeyen Kitap'}</span>
                  <span>{loanDate}</span>
                  <span>{expectedReturnDate}</span>
                  <span>{actualReturnDate}</span>
                  <span className={`status ${statusClass}`}>
                    {statusText}
                  </span>
                  <div className="table-actions">
                    {(!loan.teslim_edildi || loan.teslim_edildi === 0 || loan.teslim_edildi === false) && ( // Sadece teslim edilmemişse iade butonu göster
                      <Button 
                        variant="contained" 
                        size="small"
                        onClick={() => handleLoanAction('return', loan)} 
                        title="İade Et"
                        className={`return-button ${statusClass === 'overdue' ? 'overdue' : ''}`}
                      >
                        {statusClass === 'overdue' ? 'Gecikmiş İade' : 'İade Et'}
                      </Button>
                    )}
                    {loan.teslim_edildi === 0 && (
                      <IconButton onClick={() => handleLoanAction('edit', loan)}>
                        <Edit />
                      </IconButton>
                    )}
                  </div>
                </div>
              );
            })}
          </div>
        </div>
      </TabPanel>

      {/* Kullanıcı Dialog */}
      <Dialog 
        open={userDialog.open} 
        onClose={() => setUserDialog({ open: false, mode: 'view' })}
        maxWidth="md"
        fullWidth
        className="yetkili-modal"
      >
        <DialogTitle className="yetkili-dialog-title">
          {userDialog.mode === 'add' ? 'Yeni Kullanıcı Ekle' : userDialog.mode === 'view' ? 'Kullanıcı Detayları' : 'Kullanıcı Düzenle'}
        </DialogTitle>
        <DialogContent className="yetkili-dialog-content">
          <div className="yetkili-form">
            <TextField
              label="Ad"
              value={userFormData.ad}
              onChange={(e) => setUserFormData({...userFormData, ad: e.target.value})}
              disabled={userDialog.mode === 'view'}
              fullWidth
              margin="normal"
              required
            />
            
            <TextField
              label="Soyad"
              value={userFormData.soyad}
              onChange={(e) => setUserFormData({...userFormData, soyad: e.target.value})}
              disabled={userDialog.mode === 'view'}
              fullWidth
              margin="normal"
            />
            
            <TextField
              label="Email"
              type="email"
              value={userFormData.email}
              onChange={(e) => setUserFormData({...userFormData, email: e.target.value})}
              disabled={userDialog.mode === 'view'}
              fullWidth
              margin="normal"
              required
            />
            
            <TextField
              label="Telefon"
              value={userFormData.telefon}
              onChange={(e) => setUserFormData({...userFormData, telefon: e.target.value})}
              disabled={userDialog.mode === 'view'}
              fullWidth
              margin="normal"
            />
            
            <TextField
              label="TC Kimlik No"
              value={userFormData.tc}
              onChange={(e) => setUserFormData({...userFormData, tc: e.target.value})}
              disabled={userDialog.mode === 'view'}
              fullWidth
              margin="normal"
              inputProps={{ maxLength: 11 }}
              required
            />
            
            {userDialog.mode === 'add' && (
              <TextField
                label="Şifre"
                type="password"
                value={userFormData.sifre}
                onChange={(e) => setUserFormData({...userFormData, sifre: e.target.value})}
                fullWidth
                margin="normal"
                helperText="Boş bırakılırsa TC kimlik numarası şifre olarak kullanılır"
              />
            )}
          </div>
        </DialogContent>
        <DialogActions className="yetkili-dialog-actions">
          <Button 
            onClick={() => setUserDialog({ open: false, mode: 'view' })}
            className="yetkili-button-secondary"
          >
            Kapat
          </Button>
          {userDialog.mode === 'add' && (
            <Button 
              onClick={handleUserFormSubmit}
              variant="contained"
              className="yetkili-button"
            >
              Kaydet
            </Button>
          )}
        </DialogActions>
      </Dialog>

      {/* Ödünç Dialog */}
      <Dialog 
        open={loanDialog.open} 
        onClose={() => setLoanDialog({ open: false, mode: 'view' })}
        maxWidth="md"
        fullWidth
        className="yetkili-modal"
      >
        <DialogTitle className="yetkili-dialog-title">
          {loanDialog.mode === 'add' ? 'Yeni Ödünç İşlemi' : 
           loanDialog.mode === 'edit' ? 'Ödünç Düzenle' : 
           loanDialog.mode === 'return' ? 'Kitap İade Et' : 'Ödünç Detayları'}
        </DialogTitle>
        <DialogContent className="yetkili-dialog-content">
          <div className="yetkili-form">
            {loanDialog.mode !== 'return' && (
              <>
                <FormControl fullWidth margin="normal">
                  <InputLabel>Kullanıcı</InputLabel>
                  <Select
                    value={loanFormData.kullanici_id}
                    onChange={(e) => handleUserSelectionForLoan(e.target.value)}
                    disabled={loanDialog.mode === 'view'}
                    required
                  >
                    {users.map((user) => (
                      <MenuItem key={user.kullanici_id} value={user.kullanici_id}>
                        {user.ad} {user.soyad} - {user.tc}
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>

                {/* Kullanıcı ödünç durumu gösterimi */}
                {selectedUserLoanStatus && (
                  <div className="user-loan-status" style={{ 
                    margin: '1rem 0', 
                    padding: '1rem', 
                    borderRadius: '8px',
                    backgroundColor: showUserStatusWarning ? '#fff3cd' : '#d1ecf1',
                    border: `1px solid ${showUserStatusWarning ? '#ffeaa7' : '#bee5eb'}`,
                    color: showUserStatusWarning ? '#856404' : '#0c5460'
                  }}>
                    <Typography variant="subtitle2" style={{ fontWeight: 'bold', marginBottom: '0.5rem' }}>
                      📚 Kullanıcı Ödünç Durumu
                    </Typography>
                    <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                      <div>
                        <Typography variant="body2">
                          Aktif Kitap: <strong>{selectedUserLoanStatus.aktif_odunc_sayisi}</strong> / {selectedUserLoanStatus.maksimum_kitap}
                        </Typography>
                        <Typography variant="body2">
                          Kalan Hak: <strong>{selectedUserLoanStatus.kalan_hak}</strong> kitap
                        </Typography>
                      </div>
                      <div style={{ textAlign: 'right' }}>
                        {showUserStatusWarning ? (
                          <Typography variant="body2" style={{ color: '#d63384', fontWeight: 'bold' }}>
                            ⚠️ Maksimum limite ulaştı!
                          </Typography>
                        ) : (
                          <Typography variant="body2" style={{ color: '#198754', fontWeight: 'bold' }}>
                            ✅ Yeni kitap alabilir
                          </Typography>
                        )}
                      </div>
                    </div>
                    
                    {/* Aktif kitaplar listesi */}
                    {selectedUserLoanStatus.aktif_kitaplar && selectedUserLoanStatus.aktif_kitaplar.length > 0 && (
                      <div style={{ marginTop: '0.5rem' }}>
                        <Typography variant="body2" style={{ fontWeight: 'bold', marginBottom: '0.25rem' }}>
                          Aktif Kitaplar:
                        </Typography>
                        {selectedUserLoanStatus.aktif_kitaplar.map((kitap, index) => (
                          <Typography key={index} variant="caption" style={{ display: 'block', marginBottom: '0.1rem' }}>
                            • {kitap.kitap_adi} - {kitap.yazar}
                          </Typography>
                        ))}
                      </div>
                    )}
                  </div>
                )}
                
                <FormControl fullWidth margin="normal">
                  <InputLabel>Kitap</InputLabel>
                  <Select
                    value={loanFormData.kitap_id}
                    onChange={(e) => setLoanFormData({...loanFormData, kitap_id: e.target.value})}
                    disabled={loanDialog.mode === 'view'}
                    required
                  >
                    {books.filter(book => book.isActive).map((book) => (
                      <MenuItem key={book.id} value={book.id}>
                        {book.title} - {book.author}
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>
                
                <TextField
                  label="Ödünç Tarihi"
                  type="date"
                  value={loanFormData.odunc_tarihi}
                  onChange={(e) => {
                    const newOduncTarihi = e.target.value;
                    // Ödünç tarihi değiştiğinde iade tarihini otomatik güncelle
                    if (loanDialog.mode === 'add' && newOduncTarihi) {
                      const iadeTarihi = new Date(newOduncTarihi);
                      iadeTarihi.setMonth(iadeTarihi.getMonth() + 1);
                      setLoanFormData({
                        ...loanFormData, 
                        odunc_tarihi: newOduncTarihi,
                        iade_tarihi: iadeTarihi.toISOString().split('T')[0]
                      });
                    } else {
                      setLoanFormData({...loanFormData, odunc_tarihi: newOduncTarihi});
                    }
                  }}
                  disabled={loanDialog.mode === 'view'}
                  fullWidth
                  margin="normal"
                  required
                  InputLabelProps={{ shrink: true }}
                />
                
                {loanDialog.mode === 'add' && (
                  <Typography variant="body2" color="text.secondary" sx={{ mt: 1, mb: 1 }}>
                    💡 Beklenen teslim tarihi otomatik olarak 1 ay sonrası olarak ayarlanacaktır.
                  </Typography>
                )}
                
                <TextField
                  label="Beklenen Teslim Tarihi (Otomatik: 1 ay sonrası)"
                  type="date"
                  value={loanFormData.iade_tarihi}
                  onChange={(e) => setLoanFormData({...loanFormData, iade_tarihi: e.target.value})}
                  disabled={loanDialog.mode === 'view' || loanDialog.mode === 'add'}
                  fullWidth
                  margin="normal"
                  InputLabelProps={{ shrink: true }}
                  helperText={loanDialog.mode === 'add' ? 'Otomatik olarak 1 ay sonrası ayarlanır' : ''}
                />
                
                {loanDialog.mode === 'edit' && (
                  <TextField
                    label="İade Tarihi"
                    type="date"
                    value={loanFormData.iade_tarihi}
                    onChange={(e) => setLoanFormData({...loanFormData, iade_tarihi: e.target.value})}
                    fullWidth
                    margin="normal"
                    InputLabelProps={{ shrink: true }}
                  />
                )}
              </>
            )}
            
            {loanDialog.mode === 'return' && (
              <TextField
                label="Gerçek Teslim Tarihi"
                type="date"
                value={loanFormData.teslim_tarihi || new Date().toISOString().split('T')[0]}
                onChange={(e) => setLoanFormData({...loanFormData, teslim_tarihi: e.target.value})}
                fullWidth
                margin="normal"
                InputLabelProps={{ shrink: true }}
              />
            )}
          </div>
        </DialogContent>
        <DialogActions className="yetkili-dialog-actions">
          <Button 
            onClick={() => setLoanDialog({ open: false, mode: 'view' })}
            className="yetkili-button-secondary"
          >
            Kapat
          </Button>
          {(loanDialog.mode === 'add' || loanDialog.mode === 'edit' || loanDialog.mode === 'return') && (
            <Button 
              onClick={handleLoanFormSubmit}
              variant="contained"
              className="yetkili-button"
            >
              {loanDialog.mode === 'add' ? 'Kaydet' : 
               loanDialog.mode === 'edit' ? 'Güncelle' : 'İade Et'}
            </Button>
          )}
        </DialogActions>
      </Dialog>
    </Container>
  );
} 