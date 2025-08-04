import { apiSlice, hashPassword } from './apiSlice';

export const usersApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    
    getAllUsers: builder.query({
      query: () => '/kullanicilar',
      transformResponse: (response) => {
        return response.map(user => ({
          ...user,
          durum: user.status === 1 || user.status === true ? 1 : 0,
          status: user.status === 1 || user.status === true ? 1 : 0,
          rol_ids: Array.isArray(user.rol_ids)
            ? user.rol_ids
            : (typeof user.rol_ids === 'string' && user.rol_ids.length > 0
                ? user.rol_ids.split(',').map(id => parseInt(id, 10))
                : []),
          rol_adlari: Array.isArray(user.rol_adlari)
            ? user.rol_adlari
            : (typeof user.rol_adlari === 'string' && user.rol_adlari.length > 0
                ? user.rol_adlari.split(',')
                : [])
        }));
      },
      providesTags: (result) =>
        result
          ? [
              ...result.map(({ kullanici_id }) => ({ type: 'User', id: kullanici_id })),
              { type: 'User', id: 'LIST' },
            ]
          : [{ type: 'User', id: 'LIST' }],
    }),
    searchUsers: builder.query({
      query: (searchQuery) => `/kullanici-bul?ad=${encodeURIComponent(searchQuery)}`,
      providesTags: (result, error, arg) => [
        { type: 'User', id: `SEARCH-${arg}` }
      ],
    }),

    getUserById: builder.query({
      query: (id) => `/kullanici-bul?kullanici_id=${id}`,
      transformResponse: (response) => {
        if (Array.isArray(response) && response.length > 0) {
          return response[0];
        }
        throw new Error('Kullanıcı bulunamadı');
      },
      providesTags: (result, error, arg) => [{ type: 'User', id: arg }],
    }),
    getRoles: builder.query({
      query: () => '/roller',
      providesTags: [{ type: 'Role', id: 'LIST' }],
    }),

    createUser: builder.mutation({
      query: (userData) => {
        if (!userData.tc || userData.tc.length !== 11 || !/^\d+$/.test(userData.tc)) {
          throw new Error('Geçersiz TC Kimlik numarası');
        }
        const cleanRolIds = (userData.rol_ids || [])
          .filter(id => id !== null && id !== undefined)
          .map(id => parseInt(id, 10));
        if (cleanRolIds.length === 0) {
          cleanRolIds.push(1);
        }
        const passwordToSend = userData.sifre && userData.sifre.trim() !== '' ? userData.sifre : userData.tc;
        
        return {
          url: '/register',
          method: 'POST',
          body: {
            ...userData,
            rol_ids: cleanRolIds,
            sifre: hashPassword(passwordToSend)
          },
        };
      },
      invalidatesTags: [{ type: 'User', id: 'LIST' }],
    }),

    updateUser: builder.mutation({
      query: ({ id, ...userData }) => {
        if (userData.tc && (userData.tc.length !== 11 || !/^\d+$/.test(userData.tc))) {
          throw new Error('Geçersiz TC Kimlik numarası');
        }
        const updateData = {
          kullanici_id: parseInt(id, 10)
        };
        if (userData.ad !== undefined) updateData.ad = userData.ad;
        if (userData.soyad !== undefined) updateData.soyad = userData.soyad;
        if (userData.email !== undefined) updateData.email = userData.email;
        if (userData.telefon !== undefined) updateData.telefon = userData.telefon;
        if (userData.tc !== undefined) updateData.tc = userData.tc;
        if (userData.durum !== undefined) updateData.durum = parseInt(userData.durum, 10);

        if (userData.rol_ids && Array.isArray(userData.rol_ids)) {
          const cleanRolIds = userData.rol_ids
            .filter(id => id !== null && id !== undefined)
            .map(id => parseInt(id, 10))
            .filter(id => !isNaN(id));
          
          if (cleanRolIds.length > 0) {
            updateData.rol_ids = cleanRolIds;
          }
        }

        console.log('API updateUser - Sending data:', updateData);

        return {
          url: '/kullanici-guncelle',
          method: 'PUT',
          body: updateData,
        };
      },
      async onQueryStarted({ id }, { dispatch, queryFulfilled, getState }) {
        try {
          const { data } = await queryFulfilled;
          if (data.user) {
            const currentUser = JSON.parse(localStorage.getItem('user') || '{}');
            if (currentUser.kullanici_id === data.user.kullanici_id) {
              const updatedUser = { ...data.user };
              
              if (currentUser.rol_ids) updatedUser.rol_ids = currentUser.rol_ids;
              if (currentUser.rol_adlari) updatedUser.rol_adlari = currentUser.rol_adlari;
              if (currentUser.ana_rol_adi) updatedUser.ana_rol_adi = currentUser.ana_rol_adi;
              if (currentUser.rol_id) updatedUser.rol_id = currentUser.rol_id;
              
              localStorage.setItem('user', JSON.stringify(updatedUser));
              dispatch(apiSlice.util.updateQueryData('getCurrentUser', undefined, () => updatedUser));
            }
          }
        } catch (error) {
        }
      },
      invalidatesTags: (result, error, arg) => [
        { type: 'User', id: arg.id },
        { type: 'User', id: 'LIST' },
      ],
    }),

    deleteUser: builder.mutation({
      query: (id) => ({
        url: `/kullanici-sil/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: (result, error, arg) => [
        { type: 'User', id: arg },
        { type: 'User', id: 'LIST' },
      ],
    }),

  }),
});

export const {
  useGetAllUsersQuery,
  useSearchUsersQuery,
  useGetUserByIdQuery,
  useGetRolesQuery,
  useCreateUserMutation,
  useUpdateUserMutation,
  useDeleteUserMutation,
} = usersApiSlice; 