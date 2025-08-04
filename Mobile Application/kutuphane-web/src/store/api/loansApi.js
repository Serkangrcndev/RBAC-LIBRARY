import { apiSlice } from './apiSlice';

// Loans API endpoints
export const loansApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    
    // Tüm ödünç işlemlerini getir
    getAllLoans: builder.query({
      query: () => '/odunc-islemleri',
      providesTags: (result) =>
        result
          ? [
              ...result.map(({ odunc_id }) => ({ type: 'Loan', id: odunc_id })),
              { type: 'Loan', id: 'LIST' },
            ]
          : [{ type: 'Loan', id: 'LIST' }],
    }),

    // Ödünç işlemleri arama
    searchLoans: builder.query({
      query: ({ userId, bookId }) => {
        const params = new URLSearchParams();
        if (userId) params.append('kullanici_id', userId);
        if (bookId) params.append('kitap_id', bookId);
        return `/odunc-bul?${params.toString()}`;
      },
      providesTags: (result, error, arg) => [
        { type: 'Loan', id: `SEARCH-${JSON.stringify(arg)}` }
      ],
    }),

    // Yeni ödünç işlemi oluştur
    createLoan: builder.mutation({
      query: (loanData) => ({
        url: '/odunc-ekle',
        method: 'POST',
        body: loanData,
      }),
      invalidatesTags: [
        { type: 'Loan', id: 'LIST' },
        { type: 'Book', id: 'LIST' },
        { type: 'Stats', id: 'GENERAL' },
      ],
    }),

    // Ödünç iade et
    returnLoan: builder.mutation({
      query: (loanId) => ({
        url: `/odunc-iade/${loanId}`,
        method: 'PUT',
      }),
      invalidatesTags: [
        { type: 'Loan', id: 'LIST' },
        { type: 'Book', id: 'LIST' },
        { type: 'Stats', id: 'GENERAL' },
      ],
    }),

    // Ödünç işlemini güncelle
    updateLoan: builder.mutation({
      query: ({ id, ...patch }) => ({
        url: `/odunc-guncelle/${id}`,
        method: 'PUT',
        body: patch,
      }),
      invalidatesTags: (result, error, arg) => [
        { type: 'Loan', id: arg.id },
        { type: 'Loan', id: 'LIST' },
      ],
    }),

    // Kullanıcının aktif ödünç kitapları
    getUserActiveLoans: builder.query({
      query: (userId) => `/kullanici-odunc/${userId}`,
      providesTags: (result, error, arg) => [
        { type: 'Loan', id: `USER-${arg}` }
      ],
    }),

    // Kitabın ödünç geçmişi
    getBookLoanHistory: builder.query({
      query: (bookId) => `/kitap-odunc-gecmisi/${bookId}`,
      providesTags: (result, error, arg) => [
        { type: 'Loan', id: `BOOK-${arg}` }
      ],
    }),

    // Geciken ödünçler
    getOverdueLoans: builder.query({
      query: () => '/geciken-oduncler',
      providesTags: [{ type: 'Loan', id: 'OVERDUE' }],
    }),

    // Kullanıcının ödünç durumu (aktif kitap sayısı, kalan hak vb.)
    getUserLoanStatus: builder.query({
      query: (userId) => `/kullanici-odunc-durum/${userId}`,
      providesTags: (result, error, arg) => [
        { type: 'Loan', id: `STATUS-${arg}` }
      ],
    }),

    // Ödünç işlemini sil
    deleteLoan: builder.mutation({
      query: (loanId) => ({
        url: `/odunc-sil/${loanId}`,
        method: 'DELETE',
      }),
      invalidatesTags: (result, error, arg) => [
        { type: 'Loan', id: arg },
        { type: 'Loan', id: 'LIST' },
      ],
    }),

    // Kullanıcının ödünç limitini kontrol et
    checkUserLoanLimit: builder.query({
      query: (userId) => `/kullanici-odunc-limit/${userId}`,
      providesTags: (result, error, arg) => [
        { type: 'Loan', id: `LIMIT-${arg}` }
      ],
    }),

    // Kitabın ödünç alınabilir olup olmadığını kontrol et
    checkBookAvailability: builder.query({
      query: (bookId) => `/kitap-musaitlik/${bookId}`,
      providesTags: (result, error, arg) => [
        { type: 'Book', id: arg },
        { type: 'Loan', id: `AVAILABILITY-${arg}` }
      ],
    }),

  }),
});

// Hooks export et
export const {
  useGetAllLoansQuery,
  useSearchLoansQuery,
  useCreateLoanMutation,
  useReturnLoanMutation,
  useUpdateLoanMutation,
  useGetUserActiveLoansQuery,
  useGetBookLoanHistoryQuery,
  useGetOverdueLoansQuery,
  useGetUserLoanStatusQuery,
  useDeleteLoanMutation,
  useCheckUserLoanLimitQuery,
  useCheckBookAvailabilityQuery,
} = loansApiSlice; 