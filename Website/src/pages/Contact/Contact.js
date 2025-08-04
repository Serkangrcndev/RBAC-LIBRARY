import React from 'react';
import { 
  Container, 
  Typography, 
  Box,
  Paper,
  Divider
} from '@mui/material';
import LocationOnIcon from '@mui/icons-material/LocationOn';
import PhoneIcon from '@mui/icons-material/Phone';
import EmailIcon from '@mui/icons-material/Email';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import MapIcon from '@mui/icons-material/Map';
import './Contact.css';

export default function Contact() {

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
    </div>
  );
} 