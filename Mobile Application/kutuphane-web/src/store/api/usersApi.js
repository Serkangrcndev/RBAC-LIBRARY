import { apiSlice } from './apiSlice';

// Users API endpoints
export const usersApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    
    // Tüm kullanıcıları getir
    getAllUsers: builder.query({
      query: () => '/kullanicilar',
      // API'den gelen veriyi transform et
      transformResponse: (response) => {
        return response.map(user => ({
          ...user,
          durum: user.status === true ? 1 : user.status === false ? 0 : 1,
          status: user.status,
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

    // Kullanıcı arama
    searchUsers: builder.query({
      query: (searchQuery) => `/kullanici-bul?ad=${encodeURIComponent(searchQuery)}`,
      providesTags: (result, error, arg) => [
        { type: 'User', id: `SEARCH-${arg}` }
      ],
    }),

    // ID ile kullanıcı getir
    getUserById: builder.query({
      query: (id) => `/kullanici-bul?kullanici_id=${id}`,
      transformResponse: (response) => {
        if (Array.isArray(response) && response.length > 0) {
          const user = response[0];
          return {
            ...user,
            durum: user.status === true ? 1 : user.status === false ? 0 : 1,
            status: user.status,
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
          };
        }
        throw new Error('Kullanıcı bulunamadı');
      },
      providesTags: (result, error, arg) => [{ type: 'User', id: arg }],
    }),

    // TC ile kullanıcı getir
    getUserByTC: builder.query({
      query: (tc) => `/kullanici-bul?tc=${tc}`,
      transformResponse: (response) => {
        if (Array.isArray(response) && response.length > 0) {
          const user = response[0];
          return {
            ...user,
            durum: user.status === true ? 1 : user.status === false ? 0 : 1,
            status: user.status,
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
          };
        }
        throw new Error('Kullanıcı bulunamadı');
      },
      providesTags: (result, error, arg) => [{ type: 'User', id: `TC-${arg}` }],
    }),

    // Rolleri getir
    getRoles: builder.query({
      query: () => '/roller',
      providesTags: [{ type: 'Role', id: 'LIST' }],
    }),

    // Yeni kullanıcı oluştur
    createUser: builder.mutation({
      query: (userData) => ({
        url: '/kullanici-ekle',
        method: 'POST',
        body: {
          ...userData,
          sifre: hashPassword(userData.sifre)
        },
      }),
      invalidatesTags: [{ type: 'User', id: 'LIST' }],
    }),

    // Kullanıcı güncelle
    updateUser: builder.mutation({
      query: ({ id, ...patch }) => {
        const updateData = { ...patch };
        
        // Şifre varsa hash'le
        if (updateData.sifre) {
          updateData.sifre = hashPassword(updateData.sifre);
        }
        
        // Rol ID'leri temizle
        if (updateData.rol_ids) {
          const cleanRolIds = updateData.rol_ids
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
      // Başarılı güncelleme sonrası localStorage'ı güncelle
      async onQueryStarted(arg, { dispatch, queryFulfilled }) {
        try {
          const { data } = await queryFulfilled;
          
          // Kullanıcı bilgilerini localStorage'da güncelle
          if (data.user) {
            const currentUser = JSON.parse(localStorage.getItem('user') || '{}');
            if (currentUser.kullanici_id === data.user.kullanici_id) {
              localStorage.setItem('user', JSON.stringify(data.user));
              // Auth slice'ı da güncelle
              dispatch(apiSlice.util.updateQueryData('getCurrentUser', undefined, () => data.user));
            }
          }
        } catch (error) {
          // Hata durumunda işlem yapma
        }
      },
      invalidatesTags: (result, error, arg) => [
        { type: 'User', id: arg.id },
        { type: 'User', id: 'LIST' },
        { type: 'Auth', id: 'CURRENT_USER' }
      ],
    }),

    // Kullanıcı sil (soft delete)
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

// Hooks export et
export const {
  useGetAllUsersQuery,
  useSearchUsersQuery,
  useGetUserByIdQuery,
  useGetUserByTCQuery,
  useGetRolesQuery,
  useCreateUserMutation,
  useUpdateUserMutation,
  useDeleteUserMutation,
} = usersApiSlice; 