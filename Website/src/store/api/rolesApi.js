import { apiSlice } from './apiSlice';

export const rolesApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getAllRoles: builder.query({
      query: () => '/roller',
      providesTags: (result) =>
        result
          ? [
              ...result.map(({ rol_id }) => ({ type: 'Role', id: rol_id })),
              { type: 'Role', id: 'LIST' },
            ]
          : [{ type: 'Role', id: 'LIST' }],
    }),

    getRoleById: builder.query({
      query: (id) => `/rol/${id}`,
      providesTags: (result, error, arg) => [{ type: 'Role', id: arg }],
    }),

    createRole: builder.mutation({
      query: (roleData) => ({
        url: '/rol-ekle',
        method: 'POST',
        body: {
          rol_adi: roleData.rol_adi,
          aciklama: roleData.aciklama,
          yetkiler: roleData.yetkiler || []
        },
      }),
      invalidatesTags: [{ type: 'Role', id: 'LIST' }],
    }),

    updateRole: builder.mutation({
      query: ({ id, ...roleData }) => ({
        url: `/rol-guncelle/${id}`,
        method: 'PUT',
        body: {
          rol_id: parseInt(id, 10),
          rol_adi: roleData.rol_adi,
          aciklama: roleData.aciklama,
          yetkiler: roleData.yetkiler || []
        },
      }),
      invalidatesTags: (result, error, arg) => [
        { type: 'Role', id: arg.id },
        { type: 'Role', id: 'LIST' },
      ],
    }),
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

    getAllPermissions: builder.query({
      query: () => '/yetkiler',
      providesTags: [{ type: 'Permission', id: 'LIST' }],
    }),

  }),
});

export const {
  useGetAllRolesQuery,
  useGetRoleByIdQuery,
  useCreateRoleMutation,
  useUpdateRoleMutation,
  useDeleteRoleMutation,
  useGetAllPermissionsQuery,
} = rolesApiSlice; 