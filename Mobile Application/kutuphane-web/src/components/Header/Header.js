import React, { useState, useEffect } from 'react';
import { Link, NavLink, useNavigate } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import { selectCurrentUser, logout, loadUserFromStorage, selectIsAdmin } from '../../store/slices/authSlice';
import { useLogoutMutation } from '../../store/api/authApi';
import sekerLogo from '../../assets/sekerlogo.png';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import PersonIcon from '@mui/icons-material/Person';
import BookIcon from '@mui/icons-material/Book';
import ExitToAppIcon from '@mui/icons-material/ExitToApp';
import AdminPanelSettingsIcon from '@mui/icons-material/AdminPanelSettings';
import './Header.css';

function Header() {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const [menuOpen, setMenuOpen] = useState(false);
  const [profileMenuOpen, setProfileMenuOpen] = useState(false);
  
  // Redux state'inden user bilgilerini al
  const user = useSelector(selectCurrentUser);
  const isAdmin = useSelector(selectIsAdmin);
  
  // Yetkili kontrolü (rol_ids içinde 2 varsa yetkili)
  const isYetkili = user && user.rol_ids && user.rol_ids.includes(2);
  const [logoutMutation] = useLogoutMutation();

  // Sayfa yüklendiğinde localStorage'dan user bilgilerini yükle
  useEffect(() => {
    dispatch(loadUserFromStorage());
  }, [dispatch]);

  const navLinks = [
    { to: '/', label: 'Ana Sayfa' },
    { to: '/books', label: 'Kitaplar' },
    { to: '/about', label: 'Hakkında' },
    { to: '/contact', label: 'İletişim' },
  ];

  const handleLogout = async () => {
    try {
      // Redux logout action'ını dispatch et
      dispatch(logout());
      // RTK Query logout mutation'ını çağır
      await logoutMutation().unwrap();
      setProfileMenuOpen(false);
      navigate('/');
    } catch (error) {
      console.error('Çıkış yapılırken hata:', error);
    }
  };

  const toggleProfileMenu = () => {
    setProfileMenuOpen(!profileMenuOpen);
  };

  const getUserRoleText = (user) => {
    if (!user || !user.rol_ids || user.rol_ids.length === 0) return 'Üye';
    
    // Rol ID'lerine göre en yüksek yetkiyi göster
    // 1: Üye, 2: Kütüphane Yetkilisi, 3: Admin
    if (user.rol_ids.includes(3)) {
      return 'Admin';
    } else if (user.rol_ids.includes(2)) {
      return 'Kütüphane Yetkilisi';
    } else if (user.rol_ids.includes(1)) {
      return 'Üye';
    }
    
    return 'Üye';
  };

  // Kullanıcının admin olup olmadığını kontrol et
  // const isAdmin = user && user.rol_adi && user.rol_adi.toLowerCase() === 'admin'; // Bu satır artık yukarıda selector ile kontrol ediliyor

  return (
    <header className="App-header corporate-header">
      <div className="container header-flex">
        <Link to="/" className="App-logo header-logo">
          <img src={sekerLogo} alt="Logo" style={{ height: '32px', marginRight: '0.4em' }} />
          <span className="header-logo-text">Kütüphane Sistemi</span>
        </Link>
        <nav className="desktop-nav header-desktop-nav">
          {navLinks.map(link => (
            <NavLink
              key={link.to}
              to={link.to}
              className={({ isActive }) =>
                'corporate-link' + (isActive ? ' active' : '')
              }
              end={link.to === '/'}
            >
              {link.label}
            </NavLink>
          ))}
          {isYetkili && (
            <NavLink
              to="/yetkili"
              className={({ isActive }) =>
                'corporate-link yetkili-link' + (isActive ? ' active' : '')
              }
            >
              Yetkili Paneli
            </NavLink>
          )}
          {isAdmin && (
            <NavLink
              to="/admin"
              className={({ isActive }) =>
                'corporate-link admin-link' + (isActive ? ' active' : '')
              }
            >
              Yönetim
            </NavLink>
          )}
        </nav>
        <div className="header-auth-buttons">
          {user ? (
            <div className="header-user-profile">
              <div className="header-user-info" onClick={toggleProfileMenu}>
                <div className="header-avatar">
                  <AccountCircleIcon />
                </div>
                <div className="header-user-details">
                  <span className="header-username">
                    {user.ad} {user.soyad}
                  </span>
                  <span className="header-user-role">
                    {getUserRoleText(user)}
                  </span>
                </div>
                <KeyboardArrowDownIcon className={`header-dropdown-icon ${profileMenuOpen ? 'open' : ''}`} />
              </div>
              
              {profileMenuOpen && (
                <div className="header-profile-dropdown">
                  <Link to="/profile" className="header-dropdown-item" onClick={() => setProfileMenuOpen(false)}>
                    <PersonIcon className="header-dropdown-icon-item" /> Profilim
                  </Link>
                  <Link to="/my-books" className="header-dropdown-item" onClick={() => setProfileMenuOpen(false)}>
                    <BookIcon className="header-dropdown-icon-item" /> Kitaplarım
                  </Link>
                  {isYetkili && (
                    <Link to="/yetkili" className="header-dropdown-item" onClick={() => setProfileMenuOpen(false)}>
                      <AdminPanelSettingsIcon className="header-dropdown-icon-item" /> Yetkili Paneli
                    </Link>
                  )}
                  {isAdmin && (
                    <Link to="/admin" className="header-dropdown-item" onClick={() => setProfileMenuOpen(false)}>
                      <AdminPanelSettingsIcon className="header-dropdown-icon-item" /> Yönetim Paneli
                    </Link>
                  )}
                  <button 
                    className="header-dropdown-item header-dropdown-button"
                    onClick={handleLogout}
                  >
                    <ExitToAppIcon className="header-dropdown-icon-item" /> Çıkış Yap
                  </button>
                </div>
              )}
            </div>
          ) : (
            <>
              <button 
                className="header-btn header-btn--outline"
                onClick={() => navigate('/login')}
              >
                Giriş Yap
              </button>
              <button 
                className="header-btn header-btn--filled"
                onClick={() => navigate('/register')}
              >
                Kayıt Ol
              </button>
            </>
          )}
        </div>
        <button
          className="hamburger-btn header-hamburger-btn"
          aria-label="Menüyü Aç/Kapat"
          onClick={() => setMenuOpen(!menuOpen)}
        >
          <span className={menuOpen ? 'hamburger-bar open' : 'hamburger-bar'}></span>
          <span className={menuOpen ? 'hamburger-bar hide' : 'hamburger-bar'}></span>
          <span className={menuOpen ? 'hamburger-bar open2' : 'hamburger-bar'}></span>
        </button>
        {menuOpen && (
          <nav className="mobile-nav header-mobile-nav">
            {navLinks.map(link => (
              <NavLink
                key={link.to}
                to={link.to}
                className={({ isActive }) =>
                  'corporate-link' + (isActive ? ' active' : '')
                }
                end={link.to === '/'}
                onClick={() => setMenuOpen(false)}
              >
                {link.label}
              </NavLink>
            ))}
            {isYetkili && (
              <NavLink
                to="/yetkili"
                className={({ isActive }) =>
                  'corporate-link yetkili-link' + (isActive ? ' active' : '')
                }
                onClick={() => setMenuOpen(false)}
              >
                <AdminPanelSettingsIcon className="mobile-btn-icon" /> Yetkili Paneli
              </NavLink>
            )}
            {isAdmin && (
              <NavLink
                to="/admin"
                className={({ isActive }) =>
                  'corporate-link admin-link' + (isActive ? ' active' : '')
                }
                onClick={() => setMenuOpen(false)}
              >
                <AdminPanelSettingsIcon className="mobile-btn-icon" /> Yönetim
              </NavLink>
            )}
            {user ? (
              <>
                <div className="mobile-user-info">
                  <div className="mobile-avatar">
                    <AccountCircleIcon />
                  </div>
                  <div className="mobile-user-details">
                    <span className="mobile-username">
                      {user.ad} {user.soyad}
                    </span>
                    <span className="mobile-user-role">
                      {getUserRoleText(user)}
                    </span>
                  </div>
                </div>
                <Link 
                  to="/profile" 
                  className="mobile-auth-btn mobile-auth-btn--outline"
                  onClick={() => setMenuOpen(false)}
                >
                  <PersonIcon className="mobile-btn-icon" /> Profilim
                </Link>
                <Link 
                  to="/my-books" 
                  className="mobile-auth-btn mobile-auth-btn--outline"
                  onClick={() => setMenuOpen(false)}
                >
                  <BookIcon className="mobile-btn-icon" /> Kitaplarım
                </Link>
                <button 
                  className="mobile-auth-btn mobile-auth-btn--outline"
                  onClick={() => {
                    handleLogout();
                    setMenuOpen(false);
                  }}
                >
                  <ExitToAppIcon className="mobile-btn-icon" /> Çıkış Yap
                </button>
              </>
            ) : (
              <>
                <button 
                  className="mobile-auth-btn mobile-auth-btn--outline"
                  onClick={() => {
                    navigate('/login');
                    setMenuOpen(false);
                  }}
                >
                  Giriş Yap
                </button>
                <button 
                  className="mobile-auth-btn mobile-auth-btn--filled"
                  onClick={() => {
                    navigate('/register');
                    setMenuOpen(false);
                  }}
                >
                  Kayıt Ol
                </button>
              </>
            )}
          </nav>
        )}
      </div>
    </header>
  );
}

export default Header; 