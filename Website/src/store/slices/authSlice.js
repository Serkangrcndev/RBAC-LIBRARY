import { createSlice } from '@reduxjs/toolkit';

const initialState = {
  user: null,
  isAuthenticated: false,
  sessionId: null,
  isAuthLoaded: false,
};

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    setAuthLoaded: (state) => {
      state.isAuthLoaded = true;
    },
    setCredentials: (state, action) => {
      const { user, sessionId } = action.payload;
      state.user = user;
      state.sessionId = sessionId;
      state.isAuthenticated = true;
      
      localStorage.setItem('sessionId', sessionId);
      localStorage.setItem('user', JSON.stringify(user));
    },
    
    logout: (state) => {
      state.user = null;
      state.sessionId = null;
      state.isAuthenticated = false;
      
      localStorage.removeItem('sessionId');
      localStorage.removeItem('user');
    },
    loadUserFromStorage: (state) => {
      // Eğer zaten yüklendiyse tekrar yükleme
      if (state.isAuthLoaded) return;
      
      const sessionId = localStorage.getItem('sessionId');
      const userStr = localStorage.getItem('user');
      
      if (sessionId && userStr) {
        try {
          const user = JSON.parse(userStr);
          state.user = user;
          state.sessionId = sessionId;
          state.isAuthenticated = true;
        } catch (error) {
          state.user = null;
          state.sessionId = null;
          state.isAuthenticated = false;
          localStorage.removeItem('sessionId');
          localStorage.removeItem('user');
        }
      }
    },
    
    updateUser: (state, action) => {
      if (state.user) {
        const updatedUser = { ...state.user, ...action.payload };
        if (state.user.rol_ids) updatedUser.rol_ids = state.user.rol_ids;
        if (state.user.rol_adlari) updatedUser.rol_adlari = state.user.rol_adlari;
        if (state.user.ana_rol_adi) updatedUser.ana_rol_adi = state.user.ana_rol_adi;
        if (state.user.rol_id) updatedUser.rol_id = state.user.rol_id;
        
        state.user = updatedUser;
        localStorage.setItem('user', JSON.stringify(state.user));
      }
    },
  },
});

export const { 
  setCredentials, 
  logout, 
  loadUserFromStorage, 
  updateUser,
  setAuthLoaded 
} = authSlice.actions;

export const selectCurrentUser = (state) => state.auth.user;
export const selectIsAuthenticated = (state) => state.auth.isAuthenticated;
export const selectSessionId = (state) => state.auth.sessionId;
export const selectIsAuthLoaded = (state) => state.auth.isAuthLoaded; 
export const selectIsAdmin = (state) => {
  const user = state.auth.user;
  if (user?.rol_adlari && Array.isArray(user.rol_adlari)) {
    return user.rol_adlari.map(r => r.toLowerCase()).includes('admin');
  }
  if (user?.rol_ids && Array.isArray(user.rol_ids)) {
    return user.rol_ids.includes(3);
  }
  return user?.rol_adi?.toLowerCase() === 'admin';
};
export default authSlice.reducer;