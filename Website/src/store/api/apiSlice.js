import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { hashPassword } from '../../utils/hash';

const baseQuery = fetchBaseQuery({

  baseUrl: 'YOU_HERE_APİ_URL',
  
  prepareHeaders: (headers) => {
    headers.set('Content-Type', 'application/json');
    headers.set('Authorization', 'Basic ' + btoa('sbuhs:sekerstajekip'));

    const sessionId = localStorage.getItem('sessionId');
    if (sessionId) {
      headers.set('Session-ID', sessionId);
    }
    
    return headers;
  },
});

const baseQueryWithReauth = async (args, api, extraOptions) => {
  let result = await baseQuery(args, api, extraOptions);
  
  if (result.error && result.error.status === 401) {
    localStorage.removeItem('sessionId');
    localStorage.removeItem('user');
    api.dispatch(apiSlice.util.resetApiState());
  }
  
  return result;
};

export const apiSlice = createApi({
  reducerPath: 'api',
  baseQuery: baseQueryWithReauth,
  tagTypes: [
    'User', 
    'Book', 
    'Loan', 
    'Auth', 
    'Stats',
    'Role',
    'Ceza'
  ],
  endpoints: (builder) => ({
  }),
});

export { hashPassword }; 