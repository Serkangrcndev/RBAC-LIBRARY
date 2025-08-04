// Basit hash fonksiyonu - production'da daha güvenli bir hash kullanılmalı
export function hashPassword(password) {
  if (!password) return '';
  
  // Basit hash algoritması (production'da bcrypt veya crypto-js kullanılmalı)
  let hash = 0;
  for (let i = 0; i < password.length; i++) {
    const char = password.charCodeAt(i);
    hash = ((hash << 5) - hash) + char;
    hash = hash & hash; // 32-bit integer'a çevir
  }
  
  // Pozitif sayıya çevir ve hex'e dönüştür
  const positiveHash = Math.abs(hash);
  return positiveHash.toString(16);
}

// TC kimlik numarası hash'i
export function hashTC(tc) {
  if (!tc || tc.length !== 11) return '';
  return hashPassword(tc);
}

// Email hash'i
export function hashEmail(email) {
  if (!email) return '';
  return hashPassword(email.toLowerCase());
}