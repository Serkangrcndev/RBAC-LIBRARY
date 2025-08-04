import { apiSlice } from './apiSlice';

export const booksApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    
    getAllBooks: builder.query({
      query: () => '/kitaplar',
      transformResponse: (response) => {
        console.log('[BooksAPI] getAllBooks - Raw response:', response);
        return response.map(book => ({
          id: book.id,
          title: book.title || 'İsimsiz Kitap',
          author: book.author || 'Yazar Belirtilmemiş',
          kitap_gorsel: book.kitap_gorsel || '',
          description: book.aciklama || 'Kitap açıklaması bulunmamaktadır.',
          pageCount: book.pageCount || 0,
          language: 'Türkçe',
          publishYear: book.publishYear || new Date().getFullYear(),
          category: book.kategori || 'Genel',
          yayinevi: book.yayinevi || '',
          mevcut: book.mevcut || 1,
          kitap_adet: book.kitap_adet || 1,
          stok_durumu: book.stok_durumu || 'Stokta Var',
          durum: typeof book.isActive === 'boolean' ? (book.isActive ? 1 : 0) : Number(book.isActive || 1),
          status: typeof book.isActive === 'boolean' ? (book.isActive ? 'Aktif' : 'Pasif') : (Number(book.isActive || 1) === 1 ? 'Aktif' : 'Pasif'),
          isActive: typeof book.isActive === 'boolean' ? book.isActive : Number(book.isActive || 1) === 1
        }));
      },
      providesTags: (result) =>
        result
          ? [
              ...result.map(({ id }) => ({ type: 'Book', id })),
              { type: 'Book', id: 'LIST' },
            ]
          : [{ type: 'Book', id: 'LIST' }],
    }),

    getFeaturedBooks: builder.query({
      query: () => '/kitaplar',
      transformResponse: (response) => {
        console.log('[BooksAPI] getFeaturedBooks - Raw response:', response);
        return response.slice(0, 3).map(book => ({
          id: book.id,
          title: book.title || 'İsimsiz Kitap',
          author: book.author || 'Yazar Belirtilmemiş',
          kitap_gorsel: book.kitap_gorsel || '',
          description: book.aciklama || 'Kitap açıklaması bulunmamaktadır.',
          pageCount: book.pageCount || 0,
          language: 'Türkçe',
          publishYear: book.publishYear || new Date().getFullYear(),
          category: book.kategori || 'Genel',
          mevcut: book.mevcut || 1,
          kitap_adet: book.kitap_adet || 1,
          stok_durumu: book.stok_durumu || 'Stokta Var'
        }));
      },
      providesTags: [{ type: 'Book', id: 'FEATURED' }],
    }),
    searchBooks: builder.query({
      query: (searchQuery) => `/kitap-bul?kitap_adi=${encodeURIComponent(searchQuery)}`,
      providesTags: (result, error, arg) => [
        { type: 'Book', id: `SEARCH-${arg}` }
      ],
    }),

    getCategories: builder.query({
      query: () => '/kitaplar',
      transformResponse: (response) => {
        const categories = [...new Set(
          response
            .map(book => book.kategori || 'Genel')
            .filter(Boolean)
        )].sort();
        
        return categories.map(category => ({
          name: category,
          count: response.filter(book => (book.kategori || 'Genel') === category).length
        }));
      },
      providesTags: [{ type: 'Book', id: 'CATEGORIES' }],
    }),
    getBookStockStatus: builder.query({
      query: (kitap_id) => `/kitap-stok-kontrol/${kitap_id}`,
      transformResponse: (response) => {
        console.log('[BooksAPI] getBookStockStatus - Raw response:', response);
        return {
          kitap_id: response.kitap_id,
          kitap_adi: response.kitap_adi,
          kitap_adet: response.kitap_adet,
          mevcut: response.mevcut,
          stok_durumu: response.stok_durumu,
          stok_var: response.kitap_adet > 0 && response.mevcut === 1
        };
      },
      providesTags: (result, error, arg) => [
        { type: 'Book', id: `STOK-${arg}` }
      ],
    }),

    getBooksByCategory: builder.query({
      query: (category) => '/kitaplar',
      transformResponse: (response, meta, arg) => {
        return response
          .filter(book => (book.kategori || 'Genel') === arg)
          .map(book => ({
            id: book.id,
            title: book.title || 'İsimsiz Kitap',
            author: book.author || 'Yazar Belirtilmemiş',
            kitap_gorsel: book.kitap_gorsel || '',
            description: book.aciklama || 'Kitap açıklaması bulunmamaktadır.',
            pageCount: book.pageCount || 0,
            language: 'Türkçe',
            publishYear: book.publishYear || new Date().getFullYear(),
            category: book.kategori || 'Genel',
            yayinevi: book.yayinevi || '',
            mevcut: book.mevcut || 1,
            kitap_adet: book.kitap_adet || 1,
            stok_durumu: book.stok_durumu || 'Stokta Var',
            durum: typeof book.isActive === 'boolean' ? (book.isActive ? 1 : 0) : Number(book.isActive || 1),
            status: typeof book.isActive === 'boolean' ? (book.isActive ? 'Aktif' : 'Pasif') : (Number(book.isActive || 1) === 1 ? 'Aktif' : 'Pasif'),
            isActive: typeof book.isActive === 'boolean' ? book.isActive : Number(book.isActive || 1) === 1
          }));
      },
      providesTags: (result, error, arg) => [
        { type: 'Book', id: `CATEGORY-${arg}` }
      ],
    }),

    getBookById: builder.query({
      query: (id) => `/kitap/${id}`,
      providesTags: (result, error, arg) => [{ type: 'Book', id: arg }],
    }),

    addBook: builder.mutation({
      query: (newBook) => ({
        url: '/kitap-ekle',
        method: 'POST',
        body: newBook,
      }),
      invalidatesTags: [
        { type: 'Book', id: 'LIST' },
        { type: 'Book', id: 'CATEGORIES' },
        { type: 'Book', id: 'FEATURED' }
      ],
    }),

    updateBook: builder.mutation({
      query: ({ id, ...patch }) => ({
        url: `/kitap-guncelle/${id}`,
        method: 'PUT',
        body: patch,
      }),
      invalidatesTags: (result, error, arg) => [
        { type: 'Book', id: arg.id },
        { type: 'Book', id: 'LIST' },
        { type: 'Book', id: 'CATEGORIES' },
        { type: 'Book', id: 'FEATURED' }
      ],
    }),

    deleteBook: builder.mutation({
      query: (id) => ({
        url: `/kitap-sil/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: (result, error, arg) => [
        { type: 'Book', id: arg },
        { type: 'Book', id: 'LIST' },
        { type: 'Book', id: 'CATEGORIES' },
        { type: 'Book', id: 'FEATURED' }
      ],
    }),

  }),
});
export const {
  useGetAllBooksQuery,
  useGetFeaturedBooksQuery,
  useSearchBooksQuery,
  useGetCategoriesQuery,
  useGetBooksByCategoryQuery,
  useGetBookByIdQuery,
  useGetBookStockStatusQuery,
  useAddBookMutation,
  useUpdateBookMutation,
  useDeleteBookMutation,
} = booksApiSlice; 