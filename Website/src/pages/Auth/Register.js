import React, { useState } from 'react';
import { Typography, TextField, Button, CircularProgress, Alert, InputAdornment } from '@mui/material';
import { useNavigate, Link } from 'react-router-dom';
import { useRegisterMutation } from '../../store/api/authApi';
import sekerLogo from '../../assets/sekerlogo.png';
import PersonIcon from '@mui/icons-material/Person';
import EmailIcon from '@mui/icons-material/Email';
import PhoneIcon from '@mui/icons-material/Phone';
import LockIcon from '@mui/icons-material/Lock';
import BadgeIcon from '@mui/icons-material/Badge';
import './Auth.css';

export default function Register() {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    tc: '',
    email: '',
    password: '',
    confirmPassword: '',
    phone: ''
  });
  

  const [registerUser, { isLoading, error: registerError }] = useRegisterMutation();
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

  
    if (!formData.firstName || !formData.lastName || !formData.tc || !formData.email || !formData.password || !formData.confirmPassword) {
      setError('Lütfen tüm alanları doldurun');
      return;
    }

  
    if (formData.password !== formData.confirmPassword) {
      setError('Şifreler eşleşmiyor');
      return;
    }

  
    if (formData.tc.length !== 11 || !/^\d+$/.test(formData.tc)) {
      setError('TC kimlik numarası 11 haneli rakamlardan oluşmalıdır');
      return;
    }

    if (formData.password.length < 6) {
      setError('Şifre en az 6 karakter olmalıdır');
      return;
    }

    try {
    
      await registerUser({
        ad: formData.firstName,
        soyad: formData.lastName,
        tc: formData.tc,
        email: formData.email,
        sifre: formData.password,
        telefon: formData.phone,
        rol_id: 1
      }).unwrap();
      
      navigate('/login', { 
        state: { 
          message: 'Kayıt başarılı! Şimdi giriş yapabilirsiniz.' 
        }
      });
    } catch (err) {
      setError(err?.data?.message || err?.message || 'Kayıt olurken bir hata oluştu');
    }
  };

  return (
    <div className="auth-container">
      <div className="auth-card">
        <div className="auth-logo">
          <img src={sekerLogo} alt="Logo" />
        </div>
        
        <Typography variant="h4" className="auth-title">
          Kayıt Ol
        </Typography>
        <Typography variant="body1" className="auth-subtitle">
          Kütüphane sistemine üye olun
        </Typography>

        {(error || registerError) && (
          <Alert severity="error" className="auth-alert">
            {error || registerError?.data?.message || 'Kayıt olurken bir hata oluştu'}
          </Alert>
        )}

        <form onSubmit={handleSubmit} className="auth-form">
          <div className="form-row">
            <TextField
              name="firstName"
              label="Ad"
              value={formData.firstName}
              onChange={handleChange}
              required
              variant="outlined"
              className="auth-input"
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">
                    <PersonIcon color="action" />
                  </InputAdornment>
                ),
              }}
            />
            <TextField
              name="lastName"
              label="Soyad"
              value={formData.lastName}
              onChange={handleChange}
              required
              variant="outlined"
              className="auth-input"
            />
          </div>

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
            name="email"
            label="E-posta"
            type="email"
            value={formData.email}
            onChange={handleChange}
            fullWidth
            required
            variant="outlined"
            className="auth-input"
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <EmailIcon color="action" />
                </InputAdornment>
              ),
            }}
          />

          <TextField
            name="phone"
            label="Telefon"
            type="tel"
            value={formData.phone}
            onChange={handleChange}
            fullWidth
            required
            variant="outlined"
            className="auth-input"
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <PhoneIcon color="action" />
                </InputAdornment>
              ),
            }}
          />

          <div className="form-row">
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
            <TextField
              name="confirmPassword"
              label="Şifre Tekrar"
              type="password"
              value={formData.confirmPassword}
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
          </div>

          <Button
            type="submit"
            variant="contained"
            fullWidth
            disabled={isLoading}
            className="auth-button"
          >
            {isLoading ? <CircularProgress size={24} color="inherit" /> : 'Kayıt Ol'}
          </Button>

          <div className="auth-links">
            <Link to="/login" className="auth-link">
              Zaten hesabınız var mı? Giriş yapın
            </Link>
          </div>
        </form>
      </div>
    </div>
  );
} 