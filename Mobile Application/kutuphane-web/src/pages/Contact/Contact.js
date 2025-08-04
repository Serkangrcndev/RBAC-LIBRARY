import React, { useState } from 'react';
import { 
  Container, 
  Typography, 
  TextField, 
  Button, 
  Box,
  Snackbar,
  Alert,
  Paper,
  Divider
} from '@mui/material';
import LocationOnIcon from '@mui/icons-material/LocationOn';
import PhoneIcon from '@mui/icons-material/Phone';
import EmailIcon from '@mui/icons-material/Email';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import SendIcon from '@mui/icons-material/Send';
import MapIcon from '@mui/icons-material/Map';
import './Contact.css';

export default function Contact() {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    phone: '',
    subject: '',
    message: ''
  });
  
  const [errors, setErrors] = useState({});
  const [snackbar, setSnackbar] = useState({
    open: false,
    message: '',
    severity: 'success'
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
    
    // Hata varsa temizle
    if (errors[name]) {
      setErrors(prev => ({
        ...prev,
        [name]: null
      }));
    }
  };

  const validate = () => {
    const newErrors = {};
    
    if (!formData.name.trim()) {
      newErrors.name = 'İsim alanı zorunludur';
    }
    
    if (!formData.email.trim()) {
      newErrors.email = 'E-posta alanı zorunludur';
    } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) {
      newErrors.email = 'Geçerli bir e-posta adresi giriniz';
    }
    
    if (!formData.message.trim()) {
      newErrors.message = 'Mesaj alanı zorunludur';
    }
    
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    
    if (validate()) {
      // Form gönderme simülasyonu
      setTimeout(() => {
        setSnackbar({
          open: true,
          message: 'Mesajınız başarıyla gönderildi. En kısa sürede size dönüş yapacağız.',
          severity: 'success'
        });
        
        // Formu temizle
        setFormData({
          name: '',
          email: '',
          phone: '',
          subject: '',
          message: ''
        });
      }, 1000);
    }
  };

  const handleCloseSnackbar = () => {
    setSnackbar(prev => ({
      ...prev,
      open: false
    }));
  };

  // İletişim bilgileri
  const contactInfo = [
    {
      icon: <LocationOnIcon />,
      title: 'Adres',
      content: 'Kayseri Şeker Fabrikası, Kocasinan, Kayseri'
    },
    {
      icon: <PhoneIcon />,
      title: 'Telefon',
      content: '+90 352 123 45 67'
    },
    {
      icon: <EmailIcon />,
      title: 'E-posta',
      content: 'kutuphane@kayseriseker.com.tr'
    },
    {
      icon: <AccessTimeIcon />,
      title: 'Çalışma Saatleri',
      content: 'Hafta içi: 08:00 - 18:00\nHafta sonu: 10:00 - 16:00'
    }
  ];

  return (
    <div className="contact-page">
      <Container maxWidth="lg" className="contact-container">
        <Box className="contact-wrapper">
          {/* İletişim Bilgileri */}
          <Paper elevation={0} className="contact-info-paper">
            <Typography variant="h5" className="section-title">
              İletişim Bilgileri
            </Typography>
            <Divider className="section-divider" />
            
            <div className="contact-info-list">
              {contactInfo.map((item, index) => (
                <div className="contact-info-item" key={index}>
                  <div className="contact-info-icon">
                    {item.icon}
                  </div>
                  <div className="contact-info-content">
                    <Typography variant="h6" className="contact-info-title">
                      {item.title}
                    </Typography>
                    <Typography variant="body1" className="contact-info-text" style={{ whiteSpace: 'pre-line' }}>
                      {item.content}
                    </Typography>
                  </div>
                </div>
              ))}
            </div>
            
            <Box mt={4}>
              <Typography variant="h6" className="social-title">
                Bizi Takip Edin
              </Typography>
              <div className="social-icons">
                <a 
                  href="https://facebook.com" 
                  className="social-icon"
                  target="_blank"
                  rel="noopener noreferrer"
                  aria-label="Facebook sayfamızı ziyaret edin"
                >
                  <i className="fab fa-facebook-f"></i>
                </a>
                <a 
                  href="https://twitter.com" 
                  className="social-icon"
                  target="_blank"
                  rel="noopener noreferrer"
                  aria-label="Twitter sayfamızı ziyaret edin"
                >
                  <i className="fab fa-twitter"></i>
                </a>
                <a 
                  href="https://instagram.com" 
                  className="social-icon"
                  target="_blank"
                  rel="noopener noreferrer"
                  aria-label="Instagram sayfamızı ziyaret edin"
                >
                  <i className="fab fa-instagram"></i>
                </a>
                <a 
                  href="https://linkedin.com" 
                  className="social-icon"
                  target="_blank"
                  rel="noopener noreferrer"
                  aria-label="LinkedIn sayfamızı ziyaret edin"
                >
                  <i className="fab fa-linkedin-in"></i>
                </a>
              </div>
            </Box>
          </Paper>

          {/* İletişim Formu */}
          <div className="contact-form-wrapper">
            <Typography variant="h5" className="section-title">
              Bize Ulaşın
            </Typography>
            <Typography variant="body1" className="form-subtitle">
              Aşağıdaki formu doldurarak bize mesaj gönderebilirsiniz. En kısa sürede size dönüş yapacağız.
            </Typography>
            
            <Box component="form" onSubmit={handleSubmit} className="contact-form">
              <div className="form-row">
                <TextField
                  fullWidth
                  label="Adınız Soyadınız"
                  name="name"
                  value={formData.name}
                  onChange={handleChange}
                  variant="outlined"
                  error={!!errors.name}
                  helperText={errors.name}
                  className="form-input"
                  placeholder="Adınız Soyadınız"
                />
                <TextField
                  fullWidth
                  label="E-posta Adresiniz"
                  name="email"
                  type="email"
                  value={formData.email}
                  onChange={handleChange}
                  variant="outlined"
                  error={!!errors.email}
                  helperText={errors.email}
                  className="form-input"
                  placeholder="E-posta Adresiniz"
                />
              </div>
              
              <div className="form-row">
                <TextField
                  fullWidth
                  label="Telefon Numaranız"
                  name="phone"
                  value={formData.phone}
                  onChange={handleChange}
                  variant="outlined"
                  className="form-input"
                  placeholder="Telefon Numaranız"
                />
                <TextField
                  fullWidth
                  label="Konu"
                  name="subject"
                  value={formData.subject}
                  onChange={handleChange}
                  variant="outlined"
                  className="form-input"
                  placeholder="Konu"
                />
              </div>
              
              <TextField
                fullWidth
                label="Mesajınız"
                name="message"
                value={formData.message}
                onChange={handleChange}
                variant="outlined"
                multiline
                rows={4}
                error={!!errors.message}
                helperText={errors.message}
                className="form-input"
                placeholder="Mesajınız"
              />
              
              <Button
                type="submit"
                variant="contained"
                size="large"
                endIcon={<SendIcon />}
                className="submit-button"
              >
                Mesajı Gönder
              </Button>
            </Box>
          </div>
        </Box>
      </Container>
      
      {/* Harita Bölümü */}
      <div className="map-section">
        <Container maxWidth="lg">
          <div className="map-header">
            <div className="map-title-container">
              <MapIcon className="map-icon" />
              <Typography variant="h5" className="map-title">
                Bizi Ziyaret Edin
              </Typography>
            </div>
            <Typography variant="body1" className="map-subtitle">
              Kayseri Şeker Fabrikası içerisinde yer alan kütüphanemize kolayca ulaşabilirsiniz.
            </Typography>
          </div>
          
          <Paper elevation={3} className="map-container">
            <iframe 
              src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3125.1948378034283!2d35.4304293!3d38.7331108!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x152a1112511ab377%3A0xebc5ca5393adf536!2sKayseri%20%C5%9Eeker%20Fabrikas%C4%B1!5e0!3m2!1str!2str!4v1654152360000!5m2!1str!2str" 
              width="100%" 
              height="450" 
              style={{ border: 0 }} 
              allowFullScreen="" 
              loading="lazy" 
              referrerPolicy="no-referrer-when-downgrade"
              title="Kayseri Şeker Fabrikası Konumu"
            ></iframe>
          </Paper>
        </Container>
      </div>
      
      <Snackbar 
        open={snackbar.open} 
        autoHideDuration={6000} 
        onClose={handleCloseSnackbar}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}
      >
        <Alert 
          onClose={handleCloseSnackbar} 
          severity={snackbar.severity} 
          sx={{ width: '100%' }}
        >
          {snackbar.message}
        </Alert>
      </Snackbar>
    </div>
  );
} 