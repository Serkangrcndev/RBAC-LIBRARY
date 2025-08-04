import React, { useState, useEffect } from 'react';
import { Container, Typography, TextField, Button, Tabs, Tab, Box, IconButton, Dialog, DialogTitle, DialogContent, DialogActions, DialogContentText, FormControl, InputLabel, Select, MenuItem, Checkbox, Chip } from '@mui/material';
import { Search, LibraryBooks, People, MenuBook, History, Edit, Delete, Visibility, Add, Dashboard, Security } from '@mui/icons-material';
import { useGetAllUsersQuery, useCreateUserMutation, useUpdateUserMutation, useDeleteUserMutation } from '../../store/api/usersApi';
import { useGetAllBooksQuery, useAddBookMutation, useUpdateBookMutation, useDeleteBookMutation } from '../../store/api/booksApi'; // Yeni eklenen mutasyonlar
import { useGetAllLoansQuery, useCreateLoanMutation, useUpdateLoanMutation, useGetUserLoanStatusQuery } from '../../store/api/loansApi'; // Yeni eklenen mutasyonlar
import { useGetDashboardStatsQuery } from '../../store/api/statsApi'; // useGetGeneralStatsQuery yerine useGetDashboardStatsQuery eklendi
import { useGetAllRolesQuery, useCreateRoleMutation, useUpdateRoleMutation, useDeleteRoleMutation, useGetAllPermissionsQuery } from '../../store/api/rolesApi'; // Rol yönetimi için yeni API
import { useSelector } from 'react-redux';
import { selectCurrentUser, selectIsAdmin, selectIsAuthLoaded } from '../../store/slices/authSlice'; // selectIsAuthLoaded eklendi
import { useNavigate } from 'react-router-dom';
import defaultBookImage from '../../assets/sekerlogo.png'; // Varsayılan kitap görseli
import './Admin.css';

function TabPanel(props) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`admin-tabpanel-${index}`}
      aria-labelledby={`admin-tab-${index}`}
      {...other}
    >
      {value === index && <Box sx={{ pt: 3 }}>{children}</Box>}
    </div>
  );
}

export default function Admin() {
  const navigate = useNavigate();
  const [activeTab, setActiveTab] = useState(0);
  
  // RTK Query hooks
  const { data: users = [], isLoading: usersLoading, refetch: refetchUsers } = useGetAllUsersQuery();
  const { data: books = [], isLoading: booksLoading, refetch: refetchBooks } = useGetAllBooksQuery(); 
  const { data: loans = [], isLoading: loansLoading, refetch: refetchLoans } = useGetAllLoansQuery();
  const { data: dashboardStats, isLoading: dashboardStatsLoading } = useGetDashboardStatsQuery();
  const { data: allRoles = [], isLoading: allRolesLoading } = useGetAllRolesQuery();
  const { data: permissions = [], isLoading: permissionsLoading } = useGetAllPermissionsQuery();
  
  // Mutations
  const [createUser] = useCreateUserMutation();
  const [updateUser] = useUpdateUserMutation();
  const [deleteUser] = useDeleteUserMutation();
  const [addBook] = useAddBookMutation(); // Yeni
  const [updateBook] = useUpdateBookMutation(); // Yeni
  const [deleteBook] = useDeleteBookMutation(); // Yeni
  const [createLoan] = useCreateLoanMutation(); // Yeni
  const [updateLoan] = useUpdateLoanMutation(); // Yeni
  const [createRole] = useCreateRoleMutation(); // Rol yönetimi
  const [updateRole] = useUpdateRoleMutation(); // Rol yönetimi
  const [deleteRole] = useDeleteRoleMutation(); // Rol yönetimi

  // Current user from Redux
  const currentUser = useSelector(selectCurrentUser);
  const isAdminUser = useSelector(selectIsAdmin); // Yeni eklenen satır
  const isAuthLoaded = useSelector(selectIsAuthLoaded); // Yeni eklendi

  // Local state
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedUser, setSelectedUser] = useState(null);
  const [userDialog, setUserDialog] = useState({ open: false, mode: 'view' });
  const [deleteDialog, setDeleteDialog] = useState({ open: false, user: null });
  const [userFormData, setUserFormData] = useState({
    ad: '',
    soyad: '',
    email: '',
    telefon: '',
    tc: '',
    rol_ids: [],
    durum: 1,
    sifre: '' // Yeni kullanıcı için şifre alanı ekle
  });

  // Kitap yönetimi state'leri
  const [bookSearchTerm, setBookSearchTerm] = useState('');
  const [selectedBook, setSelectedBook] = useState(null);
  const [bookDialog, setBookDialog] = useState({ open: false, mode: 'add' });
  const [deleteBookDialog, setDeleteBookDialog] = useState({ open: false, book: null });
  const [bookFormData, setBookFormData] = useState({
    kitap_adi: '',
    yazar: '',
    yayinevi: '',
    basim_yili: '',
    kitap_gorsel: '',
    mevcut: 1,
    kitap_adet: 1,
    sayfa_sayisi: '',
    status: 1
  });

  // Ödünç yönetimi state'leri
  const [loanSearchTerm, setLoanSearchTerm] = useState('');
  const [selectedLoan, setSelectedLoan] = useState(null);
  const [loanDialog, setLoanDialog] = useState({ open: false, mode: 'add' });
  const [deleteLoanDialog, setDeleteLoanDialog] = useState({ open: false, loan: null });
  const [loanFormData, setLoanFormData] = useState({
    kullanici_id: '',
    kitap_id: '',
    odunc_tarihi: '',
    iade_tarihi: '',
    teslim_tarihi: null, // İade edildiğinde set edilecek
    status: 1 // 1: aktif, 0: iade edildi/pasif
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

  // Rol yönetimi state'leri
  const [roleSearchTerm, setRoleSearchTerm] = useState('');
  const [selectedRole, setSelectedRole] = useState(null);
  const [roleDialog, setRoleDialog] = useState({ open: false, mode: 'add' });
  const [deleteRoleDialog, setDeleteRoleDialog] = useState({ open: false, role: null });
  const [roleFormData, setRoleFormData] = useState({
    rol_adi: '',
    aciklama: '',
    yetkiler: []
  });

  // Kullanıcı ödünç durumu değiştiğinde kontrol et
  useEffect(() => {
    if (userLoanStatus) {
      setSelectedUserLoanStatus(userLoanStatus);
      setShowUserStatusWarning(!userLoanStatus.yeni_kitap_alabilir);
    }
  }, [userLoanStatus]);

  // Admin kontrolü
  useEffect(() => {
    // Debug için kullanıcı bilgilerini konsola yazdır
    console.log('Admin useEffect - State:', {
      isAuthenticated: currentUser ? true : false, // isAuthenticated yerine currentUser kontrolü
      currentUser,
      isAdminUser,
      usersLoading,
      booksLoading,
      loansLoading,
      dashboardStatsLoading, // Yeni eklenen loading state
      allRolesLoading, // rolesLoading yerine allRolesLoading kullanılıyor
      isAuthLoaded
    });

    // ProtectedRoute zaten auth kontrolü yapıyor, burada gerek yok

  }, [currentUser, navigate, isAdminUser, usersLoading, booksLoading, loansLoading, dashboardStatsLoading, allRolesLoading, permissionsLoading, isAuthLoaded]); // dashboardStatsLoading eklendi

  // Tab değiştirme
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
        rol_ids: [],
        durum: 1,
        sifre: '' // Şifre alanı boş bırakılırsa TC otomatik olarak şifre olarak kullanılacak
      });
      setUserDialog({ open: true, mode: 'add' });
    } else if (action === 'edit' && user) {
      setSelectedUser(user);
      setUserFormData({
        ad: user.ad || '',
        soyad: user.soyad || '',
        email: user.email || '',
        telefon: user.telefon || '',
        tc: user.tc || '',
        rol_ids: user.rol_ids || [],
        durum: user.durum || 1,
        sifre: '' // Şifre düzenleme sırasında boş bırakılacak, sadece değişirse gönderilecek
      });
      setUserDialog({ open: true, mode: 'edit' });
    } else if (action === 'view' && user) {
      setSelectedUser(user);
      setUserDialog({ open: true, mode: 'view' });
    } else if (action === 'delete' && user) {
      setDeleteDialog({ open: true, user });
    }
  };

  const handleUserFormSubmit = async () => {
    try {
      const submitData = { ...userFormData };
      
      // Eğer şifre alanı boşsa ve yeni kullanıcı ekleniyorsa, TC'yi şifre olarak kullan
      if (userDialog.mode === 'add' && (!submitData.sifre || submitData.sifre.trim() === '')) {
        submitData.sifre = submitData.tc; // TC'yi şifre olarak kullan
      }
      
      if (userDialog.mode === 'edit' && !submitData.sifre) {
        delete submitData.sifre; // Şifre boşsa gönderme
      }

      if (userDialog.mode === 'add') {
        await createUser(submitData).unwrap(); // submitData kullan
        alert('Kullanıcı başarıyla eklendi!');
      } else if (userDialog.mode === 'edit') {
        await updateUser({ id: selectedUser.kullanici_id, ...submitData }).unwrap(); // submitData kullan
        alert('Kullanıcı başarıyla güncellendi!');
      }
      
      setUserDialog({ open: false, mode: 'view' });
      refetchUsers();
    } catch (error) {
      alert('İşlem başarısız: ' + (error.data?.message || error.message));
    }
  };

  const handleUserDelete = async () => {
    try {
      await deleteUser(deleteDialog.user.kullanici_id).unwrap();
      alert('Kullanıcı başarıyla devre dışı bırakıldı!');
      setDeleteDialog({ open: false, user: null });
      refetchUsers();
    } catch (error) {
      alert('Devre dışı bırakma işlemi başarısız: ' + (error.data?.message || error.message));
    }
  };

  // Kitap işlemleri
  const handleBookAction = (action, book = null) => {
    if (action === 'add') {
      setBookFormData({
        kitap_adi: '',
        yazar: '',
        yayinevi: '',
        basim_yili: '',
        kitap_gorsel: '',
        mevcut: 1,
        kitap_adet: 1,
        sayfa_sayisi: '',
        status: 1
      });
      setBookDialog({ open: true, mode: 'add' });
    } else if (action === 'edit' && book) {
      setSelectedBook(book);
      setBookFormData({
        kitap_adi: book.title || '',
        yazar: book.author || '',
        yayinevi: book.yayinevi || '',
        basim_yili: book.publishYear ? new Date(book.publishYear).toISOString().split('T')[0] : '', // YYYY-MM-DD formatı
        kitap_gorsel: book.kitap_gorsel && book.kitap_gorsel.startsWith('data:image/') ? book.kitap_gorsel : '',
        mevcut: book.mevcut !== undefined ? book.mevcut : 1,
        kitap_adet: book.kitap_adet !== undefined ? book.kitap_adet : 1,
        sayfa_sayisi: book.pageCount || '',
        status: book.isActive !== undefined ? (book.isActive ? 1 : 0) : 1
      });
      setBookDialog({ open: true, mode: 'edit' });
    } else if (action === 'delete' && book) {
      setDeleteBookDialog({ open: true, book });
    }
  };

  const handleBookFormSubmit = async () => {
    try {
      const submitData = {
        ...bookFormData,
        basim_yili: bookFormData.basim_yili ? new Date(bookFormData.basim_yili).toISOString().split('T')[0] : null, // Backend için tarih formatı
        mevcut: Number(bookFormData.mevcut),
        kitap_adet: Number(bookFormData.kitap_adet),
        sayfa_sayisi: Number(bookFormData.sayfa_sayisi),
        status: Number(bookFormData.status)
      };

      if (bookDialog.mode === 'add') {
        await addBook(submitData).unwrap();
        alert('Kitap başarıyla eklendi!');
      } else if (bookDialog.mode === 'edit') {
        await updateBook({ id: selectedBook.id, ...submitData }).unwrap();
        alert('Kitap başarıyla güncellendi!');
      }
      
      setBookDialog({ open: false, mode: 'add' });
      refetchBooks();
    } catch (error) {
      alert('İşlem başarısız: ' + (error.data?.message || error.message));
    }
  };

  const handleBookDelete = async () => {
    try {
      await deleteBook(deleteBookDialog.book.id).unwrap();
      alert('Kitap başarıyla silindi!');
      setDeleteBookDialog({ open: false, book: null });
      refetchBooks();
    } catch (error) {
      alert('Silme işlemi başarısız: ' + (error.data?.message || error.message));
    }
  };

  // Dosya yükleme - Base64 formatında
  const handleImageUpload = (event) => {
    const file = event.target.files[0];
    if (file) {
      // Dosya boyutu kontrolü (5MB limit)
      const maxSize = 5 * 1024 * 1024; // 5MB
      if (file.size > maxSize) {
        alert('Dosya boyutu 5MB\'dan büyük olamaz!');
        return;
      }

      // Dosya tipi kontrolü
      const allowedTypes = ['image/jpeg', 'image/jpg', 'image/png', 'image/gif', 'image/webp'];
      if (!allowedTypes.includes(file.type)) {
        alert('Sadece JPG, PNG, GIF ve WebP formatları desteklenir!');
        return;
      }

      const reader = new FileReader();
      reader.onloadend = () => {
        console.log('[Admin] Görsel yüklendi, boyut:', file.size, 'bytes');
        setBookFormData({ ...bookFormData, kitap_gorsel: reader.result }); // Base64 formatında kaydet
      };
      reader.onerror = () => {
        alert('Görsel yüklenirken hata oluştu!');
      };
      reader.readAsDataURL(file);
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
        teslim_tarihi: null,
        status: 1
      });
      setLoanDialog({ open: true, mode: 'add' });
    } else if (action === 'edit' && loan) {
      setSelectedLoan(loan);
      setLoanFormData({
        kullanici_id: loan.kullanici_id || '',
        kitap_id: loan.kitap_id || '',
        odunc_tarihi: loan.odunc_tarihi ? new Date(loan.odunc_tarihi).toISOString().split('T')[0] : '',
        iade_tarihi: loan.iade_tarihi ? new Date(loan.iade_tarihi).toISOString().split('T')[0] : '',
        teslim_tarihi: loan.iade_tarihi ? new Date(loan.iade_tarihi).toISOString().split('T')[0] : null,
        status: loan.iade_tarihi ? 0 : 1 // İade tarihi varsa pasif, yoksa aktif
      });
      setLoanDialog({ open: true, mode: 'edit' });
    } else if (action === 'return' && loan) {
      // Sadece teslim tarihi güncellenecek
      setSelectedLoan(loan);
      setLoanFormData({
        ...loanFormData,
        teslim_tarihi: new Date().toISOString().split('T')[0], // Bugünün tarihi
        status: 0 // Pasif yap
      });
      setLoanDialog({ open: true, mode: 'return' });
    } else if (action === 'delete' && loan) {
      setDeleteLoanDialog({ open: true, loan });
    }
  };

  const handleLoanFormSubmit = async () => {
    try {
      console.log('[Admin] handleLoanFormSubmit - loanFormData:', loanFormData);
      
      // Form validasyonu
      if (!loanFormData.kullanici_id) {
        alert('Lütfen kullanıcı seçiniz.');
        return;
      }
      
      if (!loanFormData.kitap_id) {
        alert('Lütfen kitap seçiniz.');
        return;
      }
      
      if (!loanFormData.odunc_tarihi) {
        alert('Lütfen ödünç tarihini belirtiniz.');
        return;
      }
      
      if (!loanFormData.iade_tarihi) {
        alert('Lütfen iade tarihini belirtiniz.');
        return;
      }

      // Kullanıcı kitap limiti kontrolü
      if (selectedUserLoanStatus && !selectedUserLoanStatus.yeni_kitap_alabilir) {
        alert('Bu kullanıcı maksimum 3 kitap almış durumda. Yeni kitap verilemez.');
        return;
      }
      
      const submitData = {
        ...loanFormData,
        odunc_tarihi: loanFormData.odunc_tarihi ? new Date(loanFormData.odunc_tarihi).toISOString().split('T')[0] : null,
        iade_tarihi: loanFormData.iade_tarihi ? new Date(loanFormData.iade_tarihi).toISOString().split('T')[0] : null,
        teslim_tarihi: loanFormData.teslim_tarihi ? new Date(loanFormData.teslim_tarihi).toISOString().split('T')[0] : null,
        status: Number(loanFormData.status)
      };

      console.log('[Admin] handleLoanFormSubmit - submitData:', submitData);
      console.log('[Admin] handleLoanFormSubmit - mode:', loanDialog.mode);

      if (loanDialog.mode === 'add') {
        console.log('[Admin] handleLoanFormSubmit - createLoan çağrılıyor...');
        await createLoan(submitData).unwrap();
        alert('Ödünç işlemi başarıyla eklendi!');
      } else if (loanDialog.mode === 'edit') {
        console.log('[Admin] handleLoanFormSubmit - updateLoan çağrılıyor...');
        await updateLoan({ id: selectedLoan.odunc_id, ...submitData }).unwrap();
        alert('Ödünç işlemi başarıyla güncellendi!');
      } else if (loanDialog.mode === 'return') {
        // Sadece iade işlemi için: status ve teslim_tarihi güncellenecek
        console.log('[Admin] handleLoanFormSubmit - return işlemi çağrılıyor...');
        await updateLoan({ 
          id: selectedLoan.odunc_id, 
          teslim_tarihi: submitData.teslim_tarihi, 
          status: 0 
        }).unwrap();
        alert('Kitap başarıyla iade edildi!');
      }
      
      setLoanDialog({ open: false, mode: 'add' });
      refetchLoans(); // Ödünçleri yeniden çek
    } catch (error) {
      console.error('[Admin] handleLoanFormSubmit - Hata:', error);
      console.error('[Admin] handleLoanFormSubmit - Error data:', error.data);
      console.error('[Admin] handleLoanFormSubmit - Error message:', error.message);
      
      let errorMessage = 'Bilinmeyen hata';
      if (error.data?.message) {
        errorMessage = error.data.message;
      } else if (error.message) {
        errorMessage = error.message;
      } else if (error.status) {
        errorMessage = `HTTP ${error.status}: ${error.statusText || 'Sunucu hatası'}`;
      }
      
      alert('İşlem başarısız: ' + errorMessage);
    }
  };

  const handleLoanDelete = async () => {
    try {
      // Soft delete: updateLoan ile status'ü 0 yap
      await updateLoan({ id: deleteLoanDialog.loan.odunc_id, status: 0 }).unwrap();
      alert('Ödünç işlemi başarıyla silindi (pasifize edildi)!');
      setDeleteLoanDialog({ open: false, loan: null });
      refetchLoans();
    } catch (error) {
      alert('Silme işlemi başarısız: ' + (error.data?.message || error.message));
    }
  };

  // Rol işlemleri
  const handleRoleAction = (action, role = null) => {
    if (action === 'add') {
      setRoleFormData({
        rol_adi: '',
        aciklama: '',
        yetkiler: []
      });
      setRoleDialog({ open: true, mode: 'add' });
    } else if (action === 'edit' && role) {
      setSelectedRole(role);
      setRoleFormData({
        rol_adi: role.rol_adi || '',
        aciklama: role.aciklama || '',
        yetkiler: role.yetkiler || []
      });
      setRoleDialog({ open: true, mode: 'edit' });
    } else if (action === 'delete' && role) {
      setDeleteRoleDialog({ open: true, role });
    }
  };

  const handleRoleFormSubmit = async () => {
    try {
      const submitData = { ...roleFormData };

      if (roleDialog.mode === 'add') {
        await createRole(submitData).unwrap();
        alert('Rol başarıyla eklendi!');
      } else if (roleDialog.mode === 'edit') {
        await updateRole({ id: selectedRole.rol_id, ...submitData }).unwrap();
        alert('Rol başarıyla güncellendi!');
      }
      
      setRoleDialog({ open: false, mode: 'add' });
    } catch (error) {
      alert('İşlem başarısız: ' + (error.data?.message || error.message));
    }
  };

  const handleRoleDelete = async () => {
    try {
      await deleteRole(deleteRoleDialog.role.rol_id).unwrap();
      alert('Rol başarıyla silindi!');
      setDeleteRoleDialog({ open: false, role: null });
    } catch (error) {
      alert('Silme işlemi başarısız: ' + (error.data?.message || error.message));
    }
  };

  // Kullanıcının tüm yetkilere sahip olup olmadığını kontrol eden fonksiyon
  const hasAllPermissions = (userRoles) => {
    if (!userRoles || !permissions || permissions.length === 0) return false;
    
    // Kullanıcının tüm rollerinin yetkilerini topla
    const userPermissions = new Set();
    userRoles.forEach(roleId => {
      const role = allRoles.find(r => r.rol_id === roleId);
      if (role && role.yetkiler) {
        role.yetkiler.forEach(yetki => {
          userPermissions.add(yetki.yetki_id || yetki.id);
        });
      }
    });
    
    // Tüm yetkilerin sayısı ile kullanıcının yetkilerinin sayısını karşılaştır
    return userPermissions.size === permissions.length;
  };

  // Rol yetkilerini gösteren fonksiyon
  const renderRolePermissions = (role) => {
    if (!role.yetkiler || role.yetkiler.length === 0) {
      return 'Yetki yok';
    }

    // Kullanıcının tüm yetkilere sahip olup olmadığını kontrol et
    if (hasAllPermissions([role.rol_id])) {
      return (
        <div className="role-permissions">
          <span className="permission-tag full-permission">
            Full Yetki
          </span>
        </div>
      );
    }

    // İlk 3 yetkiyi göster, kalanı için "+X daha" ekle
    return (
      <div className="role-permissions">
        {role.yetkiler.slice(0, 3).map((yetki, index) => (
          <span key={index} className="permission-tag">
            {yetki.yetki_adi || yetki.ad || yetki}
          </span>
        ))}
        {role.yetkiler.length > 3 && (
          <span className="permission-tag">
            +{role.yetkiler.length - 3} daha
          </span>
        )}
      </div>
    );
  };

  // Filtrelenmiş kullanıcılar
  const filteredUsers = users.filter(user =>
    user.ad?.toLowerCase().includes(searchTerm.toLowerCase()) ||
    user.soyad?.toLowerCase().includes(searchTerm.toLowerCase()) ||
    user.email?.toLowerCase().includes(searchTerm.toLowerCase()) ||
    user.tc?.includes(searchTerm)
  );

  // Filtrelenmiş kitaplar
  const filteredBooks = books.filter(book =>
    book.title?.toLowerCase().includes(bookSearchTerm.toLowerCase()) ||
    book.author?.toLowerCase().includes(bookSearchTerm.toLowerCase()) ||
    book.yayinevi?.toLowerCase().includes(bookSearchTerm.toLowerCase())
  );

  // Filtrelenmiş ödünç işlemleri
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

  // Filtrelenmiş roller
  const filteredRoles = allRoles.filter(role =>
    role.rol_adi?.toLowerCase().includes(roleSearchTerm.toLowerCase()) ||
    role.aciklama?.toLowerCase().includes(roleSearchTerm.toLowerCase())
  );

  if (!isAuthLoaded || usersLoading || booksLoading || loansLoading || dashboardStatsLoading || allRolesLoading || permissionsLoading) { // dashboardStatsLoading eklendi
    return (
      <Container className="admin-loading">
        <Typography variant="h6">Yönetim paneli yükleniyor...</Typography>
      </Container>
    );
  }

  return (
    <Container maxWidth="xl" className="admin-container">
      <Typography variant="h3" className="admin-title">
        Kütüphane Yönetim Paneli
      </Typography>
      <Typography variant="subtitle1" className="admin-subtitle">
        Hoş geldiniz, {currentUser?.ad} {currentUser?.soyad}
      </Typography>

      {/* İstatistik Kartları */}
      <div className="admin-stats">
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
            <h3>{dashboardStats?.totalUsers || 0}</h3> {/* totalUsers olarak güncellendi */}
            <p>Toplam Kullanıcı</p>
          </div>
        </div>
        <div className="stat-card">
          <div className="stat-icon">
            <MenuBook sx={{ fontSize: 40, color: '#388d34' }} />
          </div>
          <div className="stat-info">
            <h3>{dashboardStats?.loanedBooks || 0}</h3> {/* loanedBooks olarak güncellendi */}
            <p>Ödünç Verilen Kitap</p>
          </div>
        </div>
        <div className="stat-card">
          <div className="stat-icon">
            <History sx={{ fontSize: 40, color: '#d32f2f' }} /> {/* Renk kırmızıya çevrildi */}
          </div>
          <div className="stat-info">
            <h3>{dashboardStats?.overdueBooks || 0}</h3> {/* overdueBooks olarak güncellendi */}
            <p>Gecikmiş Kitap</p>
          </div>
        </div>
      </div>

      {/* Tabs */}
      <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
        <Tabs value={activeTab} onChange={handleTabChange}>
          <Tab icon={<Dashboard />} label="Dashboard" />
          <Tab icon={<People />} label="Kullanıcılar" />
          <Tab icon={<LibraryBooks />} label="Kitaplar" />
          <Tab icon={<History />} label="Ödünç İşlemleri" />
          <Tab icon={<Security />} label="Rol Yönetimi" />
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

      {/* Kullanıcılar Tab */}
      <TabPanel value={activeTab} index={1}>
        <div className="users-section">
          <div className="section-header">
            <Typography variant="h5">Kullanıcı Yönetimi</Typography>
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
              <span>Rol</span>
              <span>Durum</span>
              <span>İşlemler</span>
            </div>
            
            {filteredUsers.map((user) => (
              <div key={user.kullanici_id} className="table-row">
                <span>{user.ad} {user.soyad}</span>
                <span>{user.email}</span>
                <span>{user.tc}</span>
                <span>
                  {user.rol_ids && user.rol_ids.length > 0 ? (
                    <div className="user-roles">
                      {user.rol_ids.map(id => {
                        const role = allRoles.find(r => r.rol_id === id);
                        return role ? (
                          <span key={id} className="user-role-tag">
                            {role.rol_adi}
                          </span>
                        ) : (
                          <span key={id} className="user-role-tag">
                            ID: {id}
                          </span>
                        );
                      })}
                    </div>
                  ) : (
                    'Belirtilmemiş'
                  )}
                </span>
                <span className={`status ${user.durum === 1 ? 'active' : 'inactive'}`}>
                  {user.durum === 1 ? 'Aktif' : 'Pasif'}
                </span>
                <div className="table-actions">
                  <IconButton onClick={() => handleUserAction('view', user)}>
                    <Visibility />
                  </IconButton>
                  <IconButton onClick={() => handleUserAction('edit', user)}>
                    <Edit />
                  </IconButton>
                  <IconButton onClick={() => handleUserAction('delete', user)}>
                    <Delete />
                  </IconButton>
                </div>
              </div>
            ))}
          </div>
        </div>
      </TabPanel>

      {/* Kitaplar Tab */}
      <TabPanel value={activeTab} index={2}>
        <div className="books-section">
          <div className="section-header">
            <Typography variant="h5">Kitap Yönetimi</Typography>
            <Button
              variant="contained"
              startIcon={<Add />}
              onClick={() => handleBookAction('add')}
              className="add-button"
            >
              Yeni Kitap Ekle
            </Button>
          </div>

          <TextField
            fullWidth
            variant="outlined"
            placeholder="Kitap adı, yazar ara..."
            value={bookSearchTerm}
            onChange={(e) => setBookSearchTerm(e.target.value)}
            InputProps={{
              startAdornment: <Search />
            }}
            className="search-field"
          />

          <div className="books-table">
            <div className="table-header">
              <span>Görsel</span>
              <span>Kitap Adı</span>
              <span>Yazar</span>
              <span>Yayın Evi</span>
              <span>Basım Yılı</span>
              <span>Sayfa Sayısı</span>
              <span>Stok</span>
              <span>Durum</span>
              <span>İşlemler</span>
            </div>
            
            {filteredBooks.map((book) => (
              <div key={book.id} className="table-row">
                <img 
                  src={book.kitap_gorsel && book.kitap_gorsel.startsWith('data:image/') ? book.kitap_gorsel : defaultBookImage}
                  alt={book.title}
                  className="book-table-image"
                  onError={(e) => {
                    e.target.src = defaultBookImage;
                  }}
                />
                <span>{book.title}</span>
                <span>{book.author}</span>
                <span>{book.yayinevi || '-'}</span>
                <span>{book.publishYear || '-'}</span>
                <span>{book.pageCount || '-'}</span>
                <span>{book.kitap_adet !== undefined ? book.kitap_adet : '-'}</span>
                <span className={`status ${book.isActive ? 'active' : 'inactive'}`}>
                  {book.isActive ? 'Aktif' : 'Pasif'}
                </span>
                <div className="table-actions">
                  <IconButton onClick={() => handleBookAction('edit', book)}>
                    <Edit />
                  </IconButton>
                  <IconButton onClick={() => handleBookAction('delete', book)}>
                    <Delete />
                  </IconButton>
                </div>
              </div>
            ))}
          </div>
        </div>
      </TabPanel>

      {/* Ödünç İşlemleri Tab */}
      <TabPanel value={activeTab} index={3}>
        <div className="loans-section">
          <div className="section-header">
            <Typography variant="h5">Ödünç İşlemleri Yönetimi</Typography>
            <Button
              variant="contained"
              startIcon={<Add />}
              onClick={() => handleLoanAction('add')}
              className="add-button"
            >
              Yeni Ödünç Kaydı
            </Button>
          </div>

          <TextField
            fullWidth
            variant="outlined"
            placeholder="Kullanıcı veya kitap ara..."
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
              
              // Teslim tarihi - teslim_edildi = 1 ise bugünün tarihi göster, değilse "İade Edilmedi"
              const actualReturnDate = (loan.teslim_edildi === 1 || loan.teslim_edildi === true) ? 
                new Date().toLocaleDateString('tr-TR') : 
                'İade Edilmedi';
              
              // Durum - teslim_edildi = 1 ise "İade Edildi", değilse "Aktif" veya "Gecikti"
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
                  <span className={`loan-status ${statusClass}`}>
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
                    <IconButton onClick={() => handleLoanAction('edit', loan)} title="Düzenle">
                      <Edit />
                    </IconButton>
                    <IconButton onClick={() => handleLoanAction('delete', loan)} title="Sil (Pasifize Et)">
                      <Delete />
                    </IconButton>
                  </div>
                </div>
              );
            })}
          </div>
        </div>
      </TabPanel>

      {/* Rol Yönetimi Tab */}
      <TabPanel value={activeTab} index={4}>
        <div className="roles-section">
          <div className="section-header">
            <Typography variant="h5">Rol Yönetimi</Typography>
            <Button
              variant="contained"
              startIcon={<Add />}
              onClick={() => handleRoleAction('add')}
              className="add-button"
            >
              Yeni Rol Ekle
            </Button>
          </div>

          <TextField
            fullWidth
            variant="outlined"
            placeholder="Rol adı veya açıklama ara..."
            value={roleSearchTerm}
            onChange={(e) => setRoleSearchTerm(e.target.value)}
            InputProps={{
              startAdornment: <Search />
            }}
            className="search-field"
          />

          <div className="roles-table">
            <div className="table-header">
              <span>Rol Adı</span>
              <span>Açıklama</span>
              <span>Yetkiler</span>
              <span>Kullanıcı Sayısı</span>
              <span>İşlemler</span>
            </div>
            
            {filteredRoles.map((role) => {
              const userCount = users.filter(user => 
                user.rol_ids && user.rol_ids.includes(role.rol_id)
              ).length;

              return (
                <div key={role.rol_id} className="table-row">
                  <span>{role.rol_adi}</span>
                  <span>{role.aciklama || '-'}</span>
                  <span>
                    {renderRolePermissions(role)}
                  </span>
                  <span>{userCount} kullanıcı</span>
                  <div className="table-actions">
                    <IconButton onClick={() => handleRoleAction('edit', role)}>
                      <Edit />
                    </IconButton>
                    <IconButton 
                      onClick={() => handleRoleAction('delete', role)}
                      disabled={userCount > 0} // Kullanıcısı olan rolü silmeye izin verme
                      title={userCount > 0 ? 'Bu rolün kullanıcıları var, silinemez' : 'Rolü sil'}
                    >
                      <Delete />
                    </IconButton>
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
      >
        <DialogTitle>
          {userDialog.mode === 'add' ? 'Yeni Kullanıcı Ekle' : 
           userDialog.mode === 'edit' ? 'Kullanıcı Düzenle' : 'Kullanıcı Detayları'}
        </DialogTitle>
        <DialogContent>
          <div className="user-form">
            <TextField
              label="Ad"
              value={userFormData.ad}
              onChange={(e) => setUserFormData({...userFormData, ad: e.target.value})}
              disabled={userDialog.mode === 'view'}
              fullWidth
              margin="normal"
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
              value={userFormData.email}
              onChange={(e) => setUserFormData({...userFormData, email: e.target.value})}
              disabled={userDialog.mode === 'view'}
              fullWidth
              margin="normal"
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
              disabled={userDialog.mode === 'view' || userDialog.mode === 'edit'} // TC düzenlemede de disabled olsun
              fullWidth
              margin="normal"
              inputProps={{ maxLength: 11 }}
            />
            
            {(userDialog.mode === 'add' || userDialog.mode === 'edit') && (
              <TextField
                label={userDialog.mode === 'add' ? "Şifre" : "Yeni Şifre (Boş bırakılırsa değişmez)"}
                type="password"
                value={userFormData.sifre}
                onChange={(e) => setUserFormData({...userFormData, sifre: e.target.value})}
                fullWidth
                margin="normal"
                required={userDialog.mode === 'add'} // Sadece eklerken zorunlu
              />
            )}
            
            <FormControl fullWidth margin="normal">
              <InputLabel>Roller</InputLabel>
              <Select
                multiple
                value={userFormData.rol_ids}
                onChange={(e) => setUserFormData({...userFormData, rol_ids: e.target.value})}
                disabled={userDialog.mode === 'view'}
                renderValue={(selected) => 
                  selected.length > 0 
                    ? `${selected.length} rol seçildi`
                    : 'Rol seçiniz'
                }
              >
                {allRoles.map((role) => (
                  <MenuItem key={role.rol_id} value={role.rol_id}>
                    <Checkbox checked={userFormData.rol_ids.includes(role.rol_id)} />
                    <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-start' }}>
                      <span style={{ fontWeight: 'bold' }}>{role.rol_adi}</span>
                      {role.aciklama && (
                        <span style={{ fontSize: '0.8rem', color: '#666' }}>
                          {role.aciklama}
                        </span>
                      )}
                    </div>
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
            
            {/* Seçili rollerin yetkilerini göster */}
            {userFormData.rol_ids.length > 0 && (
              <div style={{ marginTop: '1em' }}>
                <Typography variant="subtitle2" style={{ marginBottom: '0.5em', color: '#666' }}>
                  Seçili Rollerin Yetkileri:
                </Typography>
                <div style={{ display: 'flex', flexWrap: 'wrap', gap: '0.5em' }}>
                  {userFormData.rol_ids.map(roleId => {
                    const role = allRoles.find(r => r.rol_id === roleId);
                    if (!role || !role.yetkiler) return null;
                    
                                         return role.yetkiler.map((yetki, index) => (
                       <Chip
                         key={`${roleId}-${index}`}
                         label={`${role.rol_adi}: ${yetki.yetki_adi || yetki.ad || yetki}`}
                         color="secondary"
                         variant="outlined"
                         size="small"
                       />
                     ));
                  }).flat()}
                </div>
              </div>
            )}

            <FormControl fullWidth margin="normal">
              <InputLabel>Durum</InputLabel>
              <Select
                value={userFormData.durum}
                onChange={(e) => setUserFormData({...userFormData, durum: e.target.value})}
                disabled={userDialog.mode === 'view'}
              >
                <MenuItem value={1}>Aktif</MenuItem>
                <MenuItem value={0}>Pasif</MenuItem>
              </Select>
            </FormControl>
          </div>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setUserDialog({ open: false, mode: 'view' })}>
            {userDialog.mode === 'view' ? 'Kapat' : 'İptal'}
          </Button>
          {userDialog.mode !== 'view' && (
            <Button 
              onClick={handleUserFormSubmit}
              variant="contained"
            >
              {userDialog.mode === 'add' ? 'Ekle' : 'Güncelle'}
            </Button>
          )}
        </DialogActions>
      </Dialog>

      {/* Silme Onayı Dialog */}
      <Dialog 
        open={deleteDialog.open} 
        onClose={() => setDeleteDialog({ open: false, user: null })}
      >
        <DialogTitle>Kullanıcı Hesabını Devre Dışı Bırakma</DialogTitle>
        <DialogContent>
          <DialogContentText>
            {deleteDialog.user?.ad} {deleteDialog.user?.soyad} kullanıcısının hesabını devre dışı bırakmak istediğinizden emin misiniz?
            Bu işlem geri alınamaz.
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setDeleteDialog({ open: false, user: null })}>
            İptal
          </Button>
          <Button 
            onClick={handleUserDelete}
            color="error"
            variant="contained"
          >
            Devre Dışı Bırak
          </Button>
        </DialogActions>
      </Dialog>

      {/* Kitap Dialog */}
      <Dialog 
        open={bookDialog.open} 
        onClose={() => setBookDialog({ open: false, mode: 'add' })}
        maxWidth="md"
        fullWidth
      >
        <DialogTitle>
          {bookDialog.mode === 'add' ? 'Yeni Kitap Ekle' : 'Kitap Düzenle'}
        </DialogTitle>
        <DialogContent>
          <div className="user-form"> {/* user-form sınıfını yeniden kullanıyorum */}
            <TextField
              label="Kitap Adı"
              value={bookFormData.kitap_adi}
              onChange={(e) => setBookFormData({...bookFormData, kitap_adi: e.target.value})}
              fullWidth
              margin="normal"
            />
            <TextField
              label="Yazar"
              value={bookFormData.yazar}
              onChange={(e) => setBookFormData({...bookFormData, yazar: e.target.value})}
              fullWidth
              margin="normal"
            />
            <TextField
              label="Yayınevi"
              value={bookFormData.yayinevi}
              onChange={(e) => setBookFormData({...bookFormData, yayinevi: e.target.value})}
              fullWidth
              margin="normal"
            />
            <TextField
              label="Basım Yılı"
              type="date"
              value={bookFormData.basim_yili}
              onChange={(e) => setBookFormData({...bookFormData, basim_yili: e.target.value})}
              fullWidth
              margin="normal"
              InputLabelProps={{
                shrink: true,
              }}
            />
            <TextField
              label="Sayfa Sayısı"
              type="number"
              value={bookFormData.sayfa_sayisi}
              onChange={(e) => setBookFormData({...bookFormData, sayfa_sayisi: e.target.value})}
              fullWidth
              margin="normal"
            />
            <TextField
              label="Kitap Adeti (Stok)"
              type="number"
              value={bookFormData.kitap_adet}
              onChange={(e) => setBookFormData({...bookFormData, kitap_adet: e.target.value})}
              fullWidth
              margin="normal"
            />
            <FormControl fullWidth margin="normal">
              <InputLabel>Mevcut</InputLabel>
              <Select
                value={bookFormData.mevcut}
                onChange={(e) => setBookFormData({...bookFormData, mevcut: e.target.value})}
              >
                <MenuItem value={1}>Mevcut</MenuItem>
                <MenuItem value={0}>Mevcut Değil</MenuItem>
              </Select>
            </FormControl>
            <FormControl fullWidth margin="normal">
              <InputLabel>Durum</InputLabel>
              <Select
                value={bookFormData.status}
                onChange={(e) => setBookFormData({...bookFormData, status: e.target.value})}
              >
                <MenuItem value={1}>Aktif</MenuItem>
                <MenuItem value={0}>Pasif</MenuItem>
              </Select>
            </FormControl>

            {/* Kitap Görseli Yükleme */}
            <div className="image-upload-section">
              <Button
                variant="contained"
                component="label"
              >
                Görsel Yükle
                <input
                  type="file"
                  hidden
                  accept="image/*"
                  onChange={handleImageUpload}
                />
              </Button>
              {bookFormData.kitap_gorsel && (
                <img 
                  src={bookFormData.kitap_gorsel}
                  alt="Kitap Görseli"
                  className="uploaded-book-image"
                  onError={(e) => {
                    e.target.src = defaultBookImage;
                  }}
                />
              )}
            </div>

          </div>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setBookDialog({ open: false, mode: 'add' })}>
            İptal
          </Button>
          <Button 
            onClick={handleBookFormSubmit}
            variant="contained"
          >
            {bookDialog.mode === 'add' ? 'Ekle' : 'Güncelle'}
          </Button>
        </DialogActions>
      </Dialog>

      {/* Kitap Silme Onayı Dialog */}
      <Dialog 
        open={deleteBookDialog.open} 
        onClose={() => setDeleteBookDialog({ open: false, book: null })}
      >
        <DialogTitle>Kitap Silme Onayı</DialogTitle>
        <DialogContent>
          <DialogContentText>
            "{deleteBookDialog.book?.title}" kitabını silmek istediğinizden emin misiniz?
            Bu işlem geri alınamaz.
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setDeleteBookDialog({ open: false, book: null })}>
            İptal
          </Button>
          <Button 
            onClick={handleBookDelete}
            color="error"
            variant="contained"
          >
            Sil
          </Button>
        </DialogActions>
      </Dialog>

      {/* Ödünç Dialog */}
      <Dialog 
        open={loanDialog.open} 
        onClose={() => setLoanDialog({ open: false, mode: 'add' })}
        maxWidth="md"
        fullWidth
      >
        <DialogTitle>
          {loanDialog.mode === 'add' ? 'Yeni Ödünç Kaydı Ekle' :
           loanDialog.mode === 'edit' ? 'Ödünç Kaydı Düzenle' :
           'Kitap İade Et'}
        </DialogTitle>
        <DialogContent>
          <div className="user-form"> {/* user-form sınıfını yeniden kullanıyorum */}
            <FormControl fullWidth margin="normal">
              <InputLabel>Kullanıcı</InputLabel>
              <Select
                value={loanFormData.kullanici_id}
                onChange={(e) => handleUserSelectionForLoan(e.target.value)}
                disabled={loanDialog.mode === 'return'}
              >
                {users.map((user) => (
                  <MenuItem key={user.kullanici_id} value={user.kullanici_id}>
                    {user.ad} {user.soyad} ({user.tc})
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
                disabled={loanDialog.mode === 'return'}
              >
                {books.map((book) => (
                  <MenuItem key={book.id} value={book.id}>
                    {book.title} ({book.author})
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
              fullWidth
              margin="normal"
              InputLabelProps={{
                shrink: true,
              }}
              disabled={loanDialog.mode === 'return'}
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
              fullWidth
              margin="normal"
              InputLabelProps={{
                shrink: true,
              }}
              disabled={loanDialog.mode === 'return' || loanDialog.mode === 'add'}
              helperText={loanDialog.mode === 'add' ? 'Otomatik olarak 1 ay sonrası ayarlanır' : ''}
            />

            {loanDialog.mode === 'return' && (
              <TextField
                label="Gerçek Teslim Tarihi"
                type="date"
                value={loanFormData.teslim_tarihi || new Date().toISOString().split('T')[0]}
                onChange={(e) => setLoanFormData({...loanFormData, teslim_tarihi: e.target.value})}
                fullWidth
                margin="normal"
                InputLabelProps={{
                  shrink: true,
                }}
              />
            )}
            
            {loanDialog.mode !== 'return' && (
              <FormControl fullWidth margin="normal">
                <InputLabel>Durum</InputLabel>
                <Select
                  value={loanFormData.status}
                  onChange={(e) => setLoanFormData({...loanFormData, status: e.target.value})}
                >
                  <MenuItem value={1}>Aktif</MenuItem>
                  <MenuItem value={0}>Pasif</MenuItem>
                </Select>
              </FormControl>
            )}

          </div>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setLoanDialog({ open: false, mode: 'add' })}>
            İptal
          </Button>
          <Button 
            onClick={handleLoanFormSubmit}
            variant="contained"
          >
            {loanDialog.mode === 'add' ? 'Ekle' :
             loanDialog.mode === 'edit' ? 'Güncelle' :
             'İade Et'}
          </Button>
        </DialogActions>
      </Dialog>

      {/* Ödünç Silme Onayı Dialog */}
      <Dialog 
        open={deleteLoanDialog.open} 
        onClose={() => setDeleteLoanDialog({ open: false, loan: null })}
      >
        <DialogTitle>Ödünç İşlemi Silme Onayı</DialogTitle>
        <DialogContent>
          <DialogContentText>
            "{deleteLoanDialog.loan?.odunc_id}" ID'li ödünç işlemini silmek (pasifize etmek) istediğinizden emin misiniz?
            Bu işlem geri alınamaz ve ödünç kaydını pasif hale getirecektir.
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setDeleteLoanDialog({ open: false, loan: null })}>
            İptal
          </Button>
          <Button 
            onClick={handleLoanDelete}
            color="error"
            variant="contained"
          >
            Sil (Pasifize Et)
          </Button>
        </DialogActions>
      </Dialog>

      {/* Rol Dialog */}
      <Dialog 
        open={roleDialog.open} 
        onClose={() => setRoleDialog({ open: false, mode: 'add' })}
        maxWidth="md"
        fullWidth
      >
        <DialogTitle>
          {roleDialog.mode === 'add' ? 'Yeni Rol Ekle' : 'Rol Düzenle'}
        </DialogTitle>
        <DialogContent>
          <div className="user-form">
            <TextField
              label="Rol Adı"
              value={roleFormData.rol_adi}
              onChange={(e) => setRoleFormData({...roleFormData, rol_adi: e.target.value})}
              fullWidth
              margin="normal"
              required
            />
            <TextField
              label="Açıklama"
              value={roleFormData.aciklama}
              onChange={(e) => setRoleFormData({...roleFormData, aciklama: e.target.value})}
              fullWidth
              margin="normal"
              multiline
              rows={3}
            />
            
            <FormControl fullWidth margin="normal">
              <InputLabel>Yetkiler</InputLabel>
              <Select
                multiple
                value={roleFormData.yetkiler}
                onChange={(e) => setRoleFormData({...roleFormData, yetkiler: e.target.value})}
                renderValue={(selected) => 
                  selected.length > 0 
                    ? `${selected.length} yetki seçildi`
                    : 'Yetki seçiniz'
                }
              >
                {permissions.map((permission) => (
                  <MenuItem key={permission.yetki_id || permission.id} value={permission}>
                    <Checkbox checked={roleFormData.yetkiler.some(y => 
                      (y.yetki_id || y.id) === (permission.yetki_id || permission.id)
                    )} />
                    <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-start' }}>
                      <span style={{ fontWeight: 'bold' }}>{permission.yetki_adi || permission.ad || permission}</span>
                      {permission.aciklama && (
                        <span style={{ fontSize: '0.8rem', color: '#666' }}>
                          {permission.aciklama}
                        </span>
                      )}
                    </div>
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
            
            {/* Seçili yetkileri göster */}
            {roleFormData.yetkiler.length > 0 && (
              <div style={{ marginTop: '1em' }}>
                <Typography variant="subtitle2" style={{ marginBottom: '0.5em', color: '#666' }}>
                  Seçili Yetkiler:
                </Typography>
                <div style={{ display: 'flex', flexWrap: 'wrap', gap: '0.5em' }}>
                  {roleFormData.yetkiler.map((yetki, index) => (
                    <Chip
                      key={index}
                      label={yetki.yetki_adi || yetki.ad || yetki}
                      onDelete={() => {
                        const updatedYetkiler = roleFormData.yetkiler.filter((_, i) => i !== index);
                        setRoleFormData({...roleFormData, yetkiler: updatedYetkiler});
                      }}
                      color="primary"
                      variant="outlined"
                      size="small"
                    />
                  ))}
                </div>
              </div>
            )}
          </div>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setRoleDialog({ open: false, mode: 'add' })}>
            İptal
          </Button>
          <Button 
            onClick={handleRoleFormSubmit}
            variant="contained"
          >
            {roleDialog.mode === 'add' ? 'Ekle' : 'Güncelle'}
          </Button>
        </DialogActions>
      </Dialog>

      {/* Rol Silme Onayı Dialog */}
      <Dialog 
        open={deleteRoleDialog.open} 
        onClose={() => setDeleteRoleDialog({ open: false, role: null })}
      >
        <DialogTitle>Rol Silme Onayı</DialogTitle>
        <DialogContent>
          <DialogContentText>
            "{deleteRoleDialog.role?.rol_adi}" rolünü silmek istediğinizden emin misiniz?
            Bu işlem geri alınamaz.
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setDeleteRoleDialog({ open: false, role: null })}>
            İptal
          </Button>
          <Button 
            onClick={handleRoleDelete}
            color="error"
            variant="contained"
          >
            Sil
          </Button>
        </DialogActions>
      </Dialog>


    </Container>
  );
}