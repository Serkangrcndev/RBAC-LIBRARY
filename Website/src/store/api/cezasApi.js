import { apiSlice } from './apiSlice';

export const cezasApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    
    getCezalar: builder.query({
      query: () => '/cezalar',
      transformResponse: (response) => {
        console.log('[CezasAPI] getCezalar - Raw response:', response);
        return response.map(ceza => ({
          ceza_id: ceza.ceza_id,
          kullanici_id: ceza.kullanici_id,
          odunc_id: ceza.odunc_id,
          kullanici_adi: ceza.kullanici_adi || '',
          kullanici_soyadi: ceza.kullanici_soyadi || '',
          kullanici_email: ceza.kullanici_email || '',
          ceza_tarihi: ceza.ceza_tarihi,
          insert_date: ceza.insert_date,
          update_date: ceza.update_date,
          status: ceza.status || 1,
          aciklama: ceza.aciklama || '',
          kitap_adi: ceza.kitap_adi || '',
          odunc_tarihi: ceza.odunc_tarihi,
          iade_tarihi: ceza.iade_tarihi
        }));
      },
      providesTags: (result) =>
        result
          ? [
              ...result.map(({ ceza_id }) => ({ type: 'Ceza', id: ceza_id })),
              { type: 'Ceza', id: 'LIST' },
            ]
          : [{ type: 'Ceza', id: 'LIST' }],
    }),

    addCeza: builder.mutation({
      query: (data) => ({
        url: '/ceza-ekle',
        method: 'POST',
        body: data,
      }),
      invalidatesTags: ['Ceza', 'User'],
    }),

    updateCeza: builder.mutation({
      query: (data) => ({
        url: `/ceza-guncelle/${data.ceza_id}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: ['Ceza', 'User'],
    }),

    deleteCeza: builder.mutation({
      query: (ceza_id) => ({
        url: `/ceza-sil/${ceza_id}`,
        method: 'DELETE',
      }),
      invalidatesTags: ['Ceza', 'User'],
    }),

    gecikmisIadeKontrol: builder.mutation({
      query: () => ({
        url: '/gecikmis-iade-kontrol',
        method: 'POST',
      }),
      invalidatesTags: ['Ceza', 'User', 'Loan'],
    }),

    kullaniciHesapPasiflestir: builder.mutation({
      query: (data) => ({
        url: '/kullanici-hesap-pasiflestir',
        method: 'POST',
        body: data,
      }),
      invalidatesTags: ['Ceza', 'User', 'Loan'],
    }),

    kullaniciHesapAktiflestir: builder.mutation({
      query: (data) => ({
        url: '/kullanici-hesap-aktiflestir',
        method: 'POST',
        body: data,
      }),
      invalidatesTags: ['Ceza', 'User', 'Loan'],
    }),

    getGecikmisOduncKontrol: builder.query({
      query: () => '/gecikmis-odunc-kontrol',
      transformResponse: (response) => {
        console.log('[CezasAPI] getGecikmisOduncKontrol - Raw response:', response);
        return response;
      },
      providesTags: ['Ceza', 'User', 'Loan'],
    }),

    getKullaniciCezaDurumu: builder.query({
      query: (kullanici_id) => `/ceza-bul?kullanici_id=${kullanici_id}`,
      transformResponse: (response) => {
        console.log('[CezasAPI] getKullaniciCezaDurumu - Raw response:', response);
        return response;
      },
      providesTags: (result, error, arg) => [
        { type: 'Ceza', id: `USER-${arg}` }
      ],
    }),
  }),
});

export const {
  useGetCezalarQuery,
  useAddCezaMutation,
  useUpdateCezaMutation,
  useDeleteCezaMutation,
  useGecikmisIadeKontrolMutation,
  useKullaniciHesapPasiflestirMutation,
  useKullaniciHesapAktiflestirMutation,
  useGetGecikmisOduncKontrolQuery,
  useGetKullaniciCezaDurumuQuery,
} = cezasApiSlice;