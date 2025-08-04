import React, { useState } from 'react';
import { Typography, TextField, Button, CircularProgress, Alert, InputAdornment } from '@mui/material';
import { useNavigate, Link } from 'react-router-dom';
import { useLoginWithTCMutation } from '../../store/api/authApi';
import sekerLogo from '../../assets/sekerlogo.png';
import BadgeIcon from '@mui/icons-material/Badge';
import LockIcon from '@mui/icons-material/Lock';
import './Auth.css';
import { selectIsAdmin } from '../../store/slices/authSlice'; // selectIsAdmin import edildi

export default function Login() {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    tc: '',
    password: ''
  });
  
  // RTK Query mutation hook
  const [loginWithTC, { isLoading, error: loginError }] = useLoginWithTCMutation();
  const [error, setError] = useState('');

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');

    // TC kimlik numarası kontrolü
    if (formData.tc.length !== 11 || !/^\d+$/.test(formData.tc)) {
      setError('TC kimlik numarası 11 haneli rakamlardan oluşmalıdır');
      return;
    }

    try {
      // RTK Query mutation'ı çağır
      const response = await loginWithTC({
        tc: formData.tc,
        password: formData.password
      }).unwrap();
      
      // Başarılı giriş - admin kontrolü yap
      // useSelector ile güncel isAdmin değerini al
      const isAdminLoggedIn = selectIsAdmin({ auth: { user: response.user } }); // Geçici olarak state simüle edildi

      if (isAdminLoggedIn) {
        // Admin ise admin paneline yönlendir
        navigate('/admin');
      } else {
        // Normal kullanıcı ise ana sayfaya yönlendir
        navigate('/');
      }
    } catch (err) {
      setError(err?.data?.message || err?.message || 'Giriş yapılırken bir hata oluştu');
    }
  };

  return (
    <div className="auth-container">
      <div className="auth-card">
        <div className="auth-logo">
          <img src={sekerLogo} alt="Logo" />
        </div>
        
        <Typography variant="h4" className="auth-title">
          Giriş Yap
        </Typography>
        <Typography variant="body1" className="auth-subtitle">
          Kütüphane sistemine hoş geldiniz
        </Typography>

        {(error || loginError) && (
          <Alert severity="error" className="auth-alert">
            {error || loginError?.data?.message || 'Giriş yapılırken bir hata oluştu'}
          </Alert>
        )}

        <form onSubmit={handleSubmit} className="auth-form">
          <TextField
            name="tc"
            label="TC Kimlik No"
            type="text"
            value={formData.tc}
            onChange={handleChange}
            fullWidth
            required
            variant="outlined"
            className="auth-input"
            inputProps={{ maxLength: 11 }}
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <BadgeIcon color="action" />
                </InputAdornment>
              ),
            }}
          />

          <TextField
            name="password"
            label="Şifre"
            type="password"
            value={formData.password}
            onChange={handleChange}
            fullWidth
            required
            variant="outlined"
            className="auth-input"
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <LockIcon color="action" />
                </InputAdornment>
              ),
            }}
          />

          <Button
            type="submit"
            variant="contained"
            fullWidth
            disabled={isLoading}
            className="auth-button"
          >
            {isLoading ? <CircularProgress size={24} color="inherit" /> : 'Giriş Yap'}
          </Button>

          <div className="auth-links">
            <Link to="/register" className="auth-link">
              Hesabınız yok mu? Kayıt olun
            </Link>
            <Link to="/forgot-password" className="auth-link">
              Şifrenizi mi unuttunuz?
            </Link>
          </div>
        </form>
      </div>
    </div>
  );
} 