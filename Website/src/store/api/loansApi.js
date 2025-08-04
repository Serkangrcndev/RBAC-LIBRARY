import { apiSlice } from './apiSlice';

export const loansApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({

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

    getUserActiveLoans: builder.query({
      query: (userId) => `/kullanici-odunc/${userId}`,
      providesTags: (result, error, arg) => [
        { type: 'Loan', id: `USER-${arg}` }
      ],
    }),

    getBookLoanHistory: builder.query({
      query: (bookId) => `/kitap-odunc-gecmisi/${bookId}`,
      providesTags: (result, error, arg) => [
        { type: 'Loan', id: `BOOK-${arg}` }
      ],
    }),

    getOverdueLoans: builder.query({
      query: () => '/geciken-oduncler',
      providesTags: [{ type: 'Loan', id: 'OVERDUE' }],
    }),

    getUserLoanStatus: builder.query({
      query: (kullanici_id) => `/kullanici-odunc-durumu/${kullanici_id}`,
      providesTags: (result, error, arg) => [
        { type: 'Loan', id: `STATUS-${arg}` }
      ],
    }),

  }),
});

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
} = loansApiSlice; 