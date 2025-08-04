import React, { useState } from 'react';
import { Typography, TextField, Button, CircularProgress, Alert, InputAdornment, Stepper, Step, StepLabel } from '@mui/material';
import { useNavigate, Link } from 'react-router-dom';
import { useVerifyTCMutation, useResetPasswordMutation } from '../../store/api/authApi';
import sekerLogo from '../../assets/sekerlogo.png';
import BadgeIcon from '@mui/icons-material/Badge';
import LockIcon from '@mui/icons-material/Lock';
import './Auth.css';

export default function ForgotPassword() {
  const navigate = useNavigate();
  const [activeStep, setActiveStep] = useState(0);
  const [formData, setFormData] = useState({
    tc: '',
    newPassword: '',
    confirmPassword: ''
  });
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');
  

  const [verifyTC, { isLoading: isVerifying }] = useVerifyTCMutation();
  const [resetPassword, { isLoading: isResetting }] = useResetPasswordMutation();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleVerifyTC = async (e) => {
    e.preventDefault();
    setError('');
    setSuccess('');

  
    if (formData.tc.length !== 11 || !/^\d+$/.test(formData.tc)) {
      setError('TC kimlik numarası 11 haneli rakamlardan oluşmalıdır');
      return;
    }

    try {
    
      await verifyTC({ tc: formData.tc }).unwrap();
      setActiveStep(1);
      setSuccess('TC kimlik numarası doğrulandı. Yeni şifrenizi belirleyebilirsiniz.');
    } catch (err) {
      setError(err?.data?.message || err?.message || 'TC kimlik numarası doğrulanamadı');
    }
  };

  const handleResetPassword = async (e) => {
    e.preventDefault();
    setError('');

  
    if (formData.newPassword !== formData.confirmPassword) {
      setError('Şifreler eşleşmiyor');
      return;
    }

    if (formData.newPassword.length < 6) {
      setError('Şifre en az 6 karakter olmalıdır');
      return;
    }

    try {
    
      await resetPassword({ 
        tc: formData.tc, 
        newPassword: formData.newPassword 
      }).unwrap();
      
      setSuccess('Şifreniz başarıyla güncellendi! Giriş sayfasına yönlendiriliyorsunuz...');
      
      setTimeout(() => {
        navigate('/login');
      }, 2000);
    } catch (err) {
      setError(err?.data?.message || err?.message || 'Şifre sıfırlama başarısız');
    }
  };

  const steps = ['TC Kimlik Doğrulama', 'Yeni Şifre Belirleme'];

  return (
    <div className="auth-container">
      <div className="auth-card">
        <div className="auth-logo">
          <img src={sekerLogo} alt="Kayseri Şeker" />
        </div>
        
        <Typography variant="h4" className="auth-title">
          Şifre Sıfırlama
        </Typography>
        <Typography variant="body1" className="auth-subtitle">
          TC kimlik numaranızı doğrulayarak şifrenizi sıfırlayabilirsiniz
        </Typography>

        <Stepper activeStep={activeStep} alternativeLabel className="auth-stepper">
          {steps.map((label) => (
            <Step key={label}>
              <StepLabel>{label}</StepLabel>
            </Step>
          ))}
        </Stepper>

        {error && (
          <Alert severity="error" className="auth-alert">
            {error}
          </Alert>
        )}

        {success && (
          <Alert severity="success" className="auth-alert">
            {success}
          </Alert>
        )}

        {activeStep === 0 && (
          <form onSubmit={handleVerifyTC} className="auth-form">
            <TextField
              name="tc"
              type="text"
              label="TC Kimlik Numarası"
              placeholder="11 haneli TC kimlik numaranızı girin"
              value={formData.tc}
              onChange={handleChange}
              inputProps={{ maxLength: 11 }}
              required
              fullWidth
              className="auth-input"
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">
                    <BadgeIcon />
                  </InputAdornment>
                ),
              }}
            />

            <Button
              type="submit"
              variant="contained"
              fullWidth
              disabled={isVerifying}
              className="auth-button"
            >
              {isVerifying ? <CircularProgress size={24} color="inherit" /> : 'TC Kimlik Doğrula'}
            </Button>
          </form>
        )}

        {activeStep === 1 && (
          <form onSubmit={handleResetPassword} className="auth-form">
            <TextField
              name="newPassword"
              type="password"
              label="Yeni Şifre"
              placeholder="Yeni şifrenizi girin"
              value={formData.newPassword}
              onChange={handleChange}
              required
              fullWidth
              className="auth-input"
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">
                    <LockIcon />
                  </InputAdornment>
                ),
              }}
            />

            <TextField
              name="confirmPassword"
              type="password"
              label="Şifre Tekrar"
              placeholder="Yeni şifrenizi tekrar girin"
              value={formData.confirmPassword}
              onChange={handleChange}
              required
              fullWidth
              className="auth-input"
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">
                    <LockIcon />
                  </InputAdornment>
                ),
              }}
            />

            <Button
              type="submit"
              variant="contained"
              fullWidth
              disabled={isResetting}
              className="auth-button"
            >
              {isResetting ? <CircularProgress size={24} color="inherit" /> : 'Şifreyi Sıfırla'}
            </Button>
          </form>
        )}

        <div className="auth-links">
          <Link to="/login" className="auth-link">
            Giriş yapmaya dön
          </Link>
        </div>
      </div>
    </div>
  );
} 