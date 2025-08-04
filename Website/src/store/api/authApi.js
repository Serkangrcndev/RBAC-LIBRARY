import { apiSlice, hashPassword } from './apiSlice';
import { setCredentials } from '../slices/authSlice';

export const authApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({

    login: builder.mutation({
      query: ({ email, password }) => ({
        url: '/login',
        method: 'POST',
        body: { 
          email, 
          sifre: hashPassword(password) 
        },
      }),
      async onQueryStarted(arg, { dispatch, queryFulfilled }) {
        try {
          const { data } = await queryFulfilled;
          const { sessionId, user } = data;
          dispatch(setCredentials({ user, sessionId }));
        } catch (error) {
        }
      },
      invalidatesTags: ['Auth'],
    }),


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
        }
      },
      invalidatesTags: ['Auth'],
    }),

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
    verifyTC: builder.mutation({
      query: ({ tc }) => ({
        url: '/verify-tc',
        method: 'POST',
        body: { tc },
      }),
    }),

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
    changePassword: builder.mutation({
      query: ({ kullanici_id, mevcut_sifre, yeni_sifre }) => {
        const hashedMevcutSifre = hashPassword(mevcut_sifre);
        const hashedYeniSifre = hashPassword(yeni_sifre);
        
        return {
          url: '/change-password',
          method: 'POST',
          body: { 
            kullanici_id,
            mevcut_sifre: hashedMevcutSifre,
            yeni_sifre: hashedYeniSifre
          },
        };
      },
    }),

    logout: builder.mutation({
      queryFn: () => ({ data: {} }),
      async onQueryStarted(arg, { dispatch }) {
        dispatch(apiSlice.util.resetApiState());
      },
      invalidatesTags: ['Auth'],
    }),

  }),
});

export const {
  useLoginMutation,
  useLoginWithTCMutation,
  useRegisterMutation,
  useVerifyTCMutation,
  useResetPasswordMutation,
  useChangePasswordMutation,
  useLogoutMutation,
} = authApiSlice; 