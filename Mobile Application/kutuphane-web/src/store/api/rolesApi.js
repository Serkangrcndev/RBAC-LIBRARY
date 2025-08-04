import { apiSlice } from './apiSlice';

// Roles API endpoints
export const rolesApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    
    // Tüm rolleri getir
    getAllRoles: builder.query({
      query: () => '/roller',
      providesTags: [{ type: 'Role', id: 'LIST' }],
    }),

    // ID ile rol getir
    getRoleById: builder.query({
      query: (id) => `/rol/${id}`,
      providesTags: (result, error, arg) => [{ type: 'Role', id: arg }],
    }),

    // Yeni rol oluştur
    createRole: builder.mutation({
      query: (roleData) => ({
        url: '/rol-ekle',
        method: 'POST',
        body: roleData,
      }),
      invalidatesTags: [{ type: 'Role', id: 'LIST' }],
    }),

    // Rol güncelle
    updateRole: builder.mutation({
      query: ({ id, ...patch }) => ({
        url: `/rol-guncelle/${id}`,
        method: 'PUT',
        body: patch,
      }),
      invalidatesTags: (result, error, arg) => [
        { type: 'Role', id: arg.id },
        { type: 'Role', id: 'LIST' },
      ],
    }),

    // Rol sil
    deleteRole: builder.mutation({
      query: (id) => ({
        url: `/rol-sil/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: (result, error, arg) => [
        { type: 'Role', id: arg },
        { type: 'Role', id: 'LIST' },
      ],
    }),

  }),
});

// Hooks export et
export const {
  useGetAllRolesQuery,
  useGetRoleByIdQuery,
  useCreateRoleMutation,
  useUpdateRoleMutation,
  useDeleteRoleMutation,
} = rolesApiSlice; 