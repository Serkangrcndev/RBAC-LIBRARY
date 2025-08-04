import CryptoJS from 'crypto-js';

/**
 * Şifreyi SHA-256 ile hash'ler
 * @param {string} password - Hash'lenecek şifre
 * @returns {string} - SHA-256 hash değeri
 */
export const hashPassword = (password) => {
  if (!password) {
    throw new Error('Şifre boş olamaz');
  }
  const hashedPassword = CryptoJS.SHA256(password).toString(CryptoJS.enc.Hex);
  
  return hashedPassword;
};