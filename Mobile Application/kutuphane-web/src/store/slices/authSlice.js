import { createSlice } from '@reduxjs/toolkit';

// İlk auth state
const initialState = {
  user: null,
  isAuthenticated: false,
  sessionId: null,
  isAuthLoaded: false, // Yeni eklendi
};

// Auth slice
const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    setAuthLoaded: (state) => {
      state.isAuthLoaded = true;
    },
    // Kullanıcı giriş yaptığında
    setCredentials: (state, action) => {
      const { user, sessionId } = action.payload;
      state.user = user;
      state.sessionId = sessionId;
      state.isAuthenticated = true;
      
      // localStorage'a da kaydet
      localStorage.setItem('sessionId', sessionId);
      localStorage.setItem('user', JSON.stringify(user));
    },
    
    // Kullanıcı çıkış yaptığında
    logout: (state) => {
      state.user = null;
      state.sessionId = null;
      state.isAuthenticated = false;
      
      // localStorage'dan temizle
      localStorage.removeItem('sessionId');
      localStorage.removeItem('user');
    },
    
    // Sayfa yenilendiğinde localStorage'dan user bilgilerini yükle
    loadUserFromStorage: (state) => {
      const sessionId = localStorage.getItem('sessionId');
      const userStr = localStorage.getItem('user');
      
      if (sessionId && userStr) {
        try {
          const user = JSON.parse(userStr);
          state.user = user;
          state.sessionId = sessionId;
          state.isAuthenticated = true;
        } catch (error) {
          // JSON parse hatası olursa temizle
          state.user = null;
          state.sessionId = null;
          state.isAuthenticated = false;
          localStorage.removeItem('sessionId');
          localStorage.removeItem('user');
        }
      }
    },
    
    // Kullanıcı bilgilerini güncelle
    updateUser: (state, action) => {
      if (state.user) {
        state.user = { ...state.user, ...action.payload };
        localStorage.setItem('user', JSON.stringify(state.user));
      }
    },
  },
});

// Actions export et
export const { 
  setCredentials, 
  logout, 
  loadUserFromStorage, 
  updateUser,
  setAuthLoaded 
} = authSlice.actions;

// Selectors
export const selectCurrentUser = (state) => state.auth.user;
export const selectIsAuthenticated = (state) => state.auth.isAuthenticated;
export const selectSessionId = (state) => state.auth.sessionId;
export const selectIsAuthLoaded = (state) => state.auth.isAuthLoaded; // Yeni eklendi
export const selectIsAdmin = (state) => {
  const user = state.auth.user;
  // Eğer rol_adlari array'i varsa, admin var mı kontrol et
  if (user?.rol_adlari && Array.isArray(user.rol_adlari)) {
    return user.rol_adlari.map(r => r.toLowerCase()).includes('admin');
  }
  // rol_ids içinde admin rolü (3) var mı kontrol et
  if (user?.rol_ids && Array.isArray(user.rol_ids)) {
    return user.rol_ids.includes(3);
  }
  // Eski tekli rol_adi desteği (eğer hala kullanılıyorsa)
  return user?.rol_adi?.toLowerCase() === 'admin';
};

// Reducer export et
export default authSlice.reducer;