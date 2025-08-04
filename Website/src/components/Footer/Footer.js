import React from 'react';
import './Footer.css';

function Footer() {
  return (
    <footer className="App-footer">
      <div className="footer-content">
        <div className="footer-legal">
          <span className="footer-copyright">
            © {new Date().getFullYear()} Kütüphane. Tüm hakları saklıdır.
          </span>
        </div>
      </div>
    </footer>
  );
}

export default Footer; 