import { apiSlice, hashPassword } from './apiSlice';
import { setCredentials } from '../slices/authSlice';

// Auth API endpoints
export const authApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    
    // Email ile giriş
    login: builder.mutation({
      query: ({ email, password }) => ({
        url: '/login',
        method: 'POST',
        body: { 
          email, 
          sifre: hashPassword(password) 
        },
      }),
      // Başarılı giriş sonrası user bilgilerini store'a kaydet
      async onQueryStarted(arg, { dispatch, queryFulfilled }) {
        try {
          const { data } = await queryFulfilled;
          const { sessionId, user } = data;
          dispatch(setCredentials({ user, sessionId }));
        } catch (error) {
          // Hata durumunda işlem yapma
        }
      },
      invalidatesTags: ['Auth'],
    }),

    // TC ile giriş
    loginWithTC: builder.mutation({
      query: ({ tc, password }) => ({
        url: '/login-tc',
        method: 'POST',
        body: { 
          tc, 
          sifre: hashPassword(password) 
        },
      }),
      async onQueryStarted(arg, { dispatch, queryFulfilled }) {
        try {
          const { data } = await queryFulfilled;
          const { sessionId, user } = data;
          dispatch(setCredentials({ user, sessionId }));
        } catch (error) {
          // Hata durumunda işlem yapma
        }
      },
      invalidatesTags: ['Auth'],
    }),

    // Kullanıcı kaydı
    register: builder.mutation({
      query: (userData) => ({
        url: '/register',
        method: 'POST',
        body: {
          ...userData,
          sifre: hashPassword(userData.sifre)
        },
      }),
      invalidatesTags: ['User'],
    }),

    // TC doğrulama (şifre sıfırlama için)
    verifyTC: builder.mutation({
      query: ({ tc }) => ({
        url: '/verify-tc',
        method: 'POST',
        body: { tc },
      }),
    }),

    // Şifre sıfırlama
    resetPassword: builder.mutation({
      query: ({ tc, newPassword }) => ({
        url: '/reset-password',
        method: 'POST',
        body: { 
          tc, 
          sifre: hashPassword(newPassword) 
        },
      }),
    }),

    // Çıkış (client-side işlem)
    logout: builder.mutation({
      queryFn: () => ({ data: {} }),
      async onQueryStarted(arg, { dispatch }) {
        // Auth state'i temizle
        dispatch(apiSlice.util.resetApiState());
      },
      invalidatesTags: ['Auth'],
    }),

    // Mevcut kullanıcıyı getir
    getCurrentUser: builder.query({
      query: () => '/me',
      providesTags: ['Auth'],
    }),

  }),
});

// Hooks export et
export const {
  useLoginMutation,
  useLoginWithTCMutation,
  useRegisterMutation,
  useVerifyTCMutation,
  useResetPasswordMutation,
  useLogoutMutation,
  useGetCurrentUserQuery,
} = authApiSlice; 