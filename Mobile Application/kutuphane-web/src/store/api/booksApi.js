import { apiSlice } from './apiSlice';

// Books API endpoints
export const booksApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    
    // Tüm kitapları getir - eski bookService.getAllBooks ile aynı
    getAllBooks: builder.query({
      query: () => '/kitaplar',
      // Cache süresini artır (5 dakika)
      keepUnusedDataFor: 300,
      // API'den gelen veriyi transform et - yeni API formatı
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

    // Öne çıkan kitapları getir - eski bookService.getFeaturedBooks ile aynı
    getFeaturedBooks: builder.query({
      query: () => '/kitaplar',
      // Cache süresini artır (10 dakika)
      keepUnusedDataFor: 600,
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
          category: book.kategori || 'Genel'
        }));
      },
      providesTags: [{ type: 'Book', id: 'FEATURED' }],
    }),

    // Kitap arama - eski bookService.searchBooks ile aynı
    searchBooks: builder.query({
      query: (searchQuery) => `/kitap-bul?kitap_adi=${encodeURIComponent(searchQuery)}`,
      // Arama sonuçları için kısa cache (2 dakika)
      keepUnusedDataFor: 120,
      providesTags: (result, error, arg) => [
        { type: 'Book', id: `SEARCH-${arg}` }
      ],
    }),

    // Kategorileri getir - YENİ: Home sayfası için
    getCategories: builder.query({
      query: () => '/kitaplar',
      // Kategoriler için uzun cache (30 dakika)
      keepUnusedDataFor: 1800,
      transformResponse: (response) => {
        // Tüm kitaplardan benzersiz kategorileri çıkar
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

    // Kategoriye göre kitapları getir - YENİ
    getBooksByCategory: builder.query({
      query: (category) => '/kitaplar',
      // Kategori sonuçları için orta cache (5 dakika)
      keepUnusedDataFor: 300,
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
            durum: typeof book.isActive === 'boolean' ? (book.isActive ? 1 : 0) : Number(book.isActive || 1),
            status: typeof book.isActive === 'boolean' ? (book.isActive ? 'Aktif' : 'Pasif') : (Number(book.isActive || 1) === 1 ? 'Aktif' : 'Pasif'),
            isActive: typeof book.isActive === 'boolean' ? book.isActive : Number(book.isActive || 1) === 1
          }));
      },
      providesTags: (result, error, arg) => [
        { type: 'Book', id: `CATEGORY-${arg}` }
      ],
    }),

    // Tek kitap detayı getir
    getBookById: builder.query({
      query: (id) => `/kitap/${id}`,
      // Tek kitap için uzun cache (15 dakika)
      keepUnusedDataFor: 900,
      providesTags: (result, error, arg) => [{ type: 'Book', id: arg }],
    }),

    // Yeni kitap ekle
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

    // Kitap güncelle
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

    // Kitap sil
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

    // Kitap görseli yükle
    uploadBookImage: builder.mutation({
      query: (imageData) => ({
        url: '/kitap-gorsel-yukle',
        method: 'POST',
        body: imageData,
      }),
      invalidatesTags: [
        { type: 'Book', id: 'LIST' },
        { type: 'Book', id: 'FEATURED' }
      ],
    }),

    // Kitap istatistikleri
    getBookStats: builder.query({
      query: (bookId) => `/kitap-istatistikler/${bookId}`,
      providesTags: (result, error, arg) => [
        { type: 'Book', id: arg },
        { type: 'Stats', id: `BOOK-${arg}` }
      ],
    }),

    // Popüler kitaplar
    getPopularBooks: builder.query({
      query: () => '/populer-kitaplar',
      providesTags: [{ type: 'Book', id: 'POPULAR' }],
    }),

    // Yeni eklenen kitaplar
    getNewBooks: builder.query({
      query: () => '/yeni-kitaplar',
      providesTags: [{ type: 'Book', id: 'NEW' }],
    }),

  }),
});

// Hooks export et
export const {
  useGetAllBooksQuery,
  useGetFeaturedBooksQuery,
  useSearchBooksQuery,
  useGetCategoriesQuery,
  useGetBooksByCategoryQuery,
  useGetBookByIdQuery,
  useAddBookMutation,
  useUpdateBookMutation,
  useDeleteBookMutation,
  useUploadBookImageMutation,
  useGetBookStatsQuery,
  useGetPopularBooksQuery,
  useGetNewBooksQuery,
} = booksApiSlice; 