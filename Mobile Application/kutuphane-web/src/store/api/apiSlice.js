import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { hashPassword } from '../../utils/hash';

// Base query konfigürasyonu
const baseQuery = fetchBaseQuery({
  baseUrl: 'YOU_HERE_APİ_URL',
  // Her istekte basic auth eklenir
  prepareHeaders: (headers) => {
    headers.set('Content-Type', 'application/json');
    headers.set('Authorization', 'Basic ' + btoa('sbuhs:sekerstajekip'));
    
    // Session ID varsa ekle
    const sessionId = localStorage.getItem('sessionId');
    if (sessionId) {
      headers.set('Session-ID', sessionId);
    }
    
    return headers;
  },
});

// Base query wrapper (token refresh için)
const baseQueryWithReauth = async (args, api, extraOptions) => {
  let result = await baseQuery(args, api, extraOptions);
  
  // 401 hatası alındığında session'ı temizle
  if (result.error && result.error.status === 401) {
    localStorage.removeItem('sessionId');
    localStorage.removeItem('user');
    // Auth state'i güncelle
    api.dispatch(apiSlice.util.resetApiState());
  }
  
  return result;
};

// Ana API slice
export const apiSlice = createApi({
  reducerPath: 'api',
  baseQuery: baseQueryWithReauth,
  // Cache tag türleri
  tagTypes: [
    'User', 
    'Book', 
    'Loan', 
    'Auth', 
    'Stats',
    'Role'
  ],
  endpoints: (builder) => ({
    // Base endpoints burada tanımlanacak
  }),
});

// hashPassword fonksiyonunu export et
export { hashPassword }; 