import { configureStore } from '@reduxjs/toolkit';
import { apiSlice } from './api/apiSlice';
import authReducer from './slices/authSlice';

export const store = configureStore({
  reducer: {
    // RTK Query API slice
    [apiSlice.reducerPath]: apiSlice.reducer,
    // Auth state slice
    auth: authReducer,
  },
  // RTK Query middleware'i ekle
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: {
        // RTK Query'den gelen action'ları serializableCheck'ten hariç tut
        ignoredActions: [
          'persist/PERSIST',
          'persist/REHYDRATE',
          apiSlice.util.resetApiState.type,
        ],
        ignoredActionsPaths: ['meta.arg', 'payload.timestamp'],
        ignoredPaths: ['items.dates'],
      },
    }).concat(apiSlice.middleware),
  
  // DevTools'u aktif et
  devTools: process.env.NODE_ENV !== 'production',
}); 