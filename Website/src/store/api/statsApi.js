import { apiSlice } from './apiSlice';

export const statsApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({

    getDashboardStats: builder.query({
      query: () => 'dashboard-stats', 
      providesTags: [{ type: 'Stats', id: 'DASHBOARD' }],
    }),

    getGeneralStats: builder.query({
      queryFn: async (arg, api, extraOptions, baseQuery) => {
        try {
          const [booksResult, usersResult, loansResult] = await Promise.all([
            baseQuery('/kitaplar'),
            baseQuery('/kullanicilar'),
            baseQuery('/odunc-islemleri')
          ]);

          if (booksResult.error) return { error: booksResult.error };
          if (usersResult.error) return { error: usersResult.error };
          if (loansResult.error) return { error: loansResult.error };

          const books = booksResult.data;
          const users = usersResult.data;
          const loans = loansResult.data;

          const today = new Date();
          const dailyLoans = loans.filter(loan => {
            const loanDate = new Date(loan.verilis_tarihi);
            return loanDate.toDateString() === today.toDateString();
          });

          return {
            data: {
              totalBooks: books.length,
              activeUsers: users.length,
              dailyLoans: dailyLoans.length
            }
          };
        } catch (error) {
          return { error: { status: 500, data: { message: 'İstatistikler alınırken hata oluştu' } } };
        }
      },
      providesTags: [{ type: 'Stats', id: 'GENERAL' }],
    }),

    getReadingStats: builder.query({
      queryFn: async (arg, api, extraOptions, baseQuery) => {
        try {
          const [booksResult, usersResult, loansResult] = await Promise.all([
            baseQuery('/kitaplar'),
            baseQuery('/kullanicilar'),
            baseQuery('/odunc-islemleri')
          ]);

          if (booksResult.error) return { error: booksResult.error };
          if (usersResult.error) return { error: usersResult.error };
          if (loansResult.error) return { error: loansResult.error };

          const books = booksResult.data;
          const users = usersResult.data;
          const loans = loansResult.data;

          const bookStats = {};
          loans.forEach(loan => {
            bookStats[loan.id] = (bookStats[loan.id] || 0) + 1;
          });

          const popularBooks = Object.entries(bookStats)
            .map(([bookId, count]) => {
              const book = books.find(b => b.id === parseInt(bookId));
              return {
                title: book ? book.title : 'Bilinmeyen Kitap',
                readCount: count
              };
            })
            .sort((a, b) => b.readCount - a.readCount)
            .slice(0, 3);

          const userStats = {};
          loans.forEach(loan => {
            userStats[loan.kullanici_id] = (userStats[loan.kullanici_id] || 0) + 1;
          });

          const activeReaders = Object.entries(userStats)
            .map(([userId, count]) => {
              const user = users.find(u => u.kullanici_id === parseInt(userId));
              return {
                name: user ? `${user.ad} ${user.soyad || ''}` : 'Bilinmeyen Kullanıcı',
                bookCount: count
              };
            })
            .sort((a, b) => b.bookCount - a.bookCount)
            .slice(0, 3);

          const authorStats = {};
          books.forEach(book => {
            if (book.author) {
              authorStats[book.author] = (authorStats[book.author] || 0) + 
                (bookStats[book.id] || 0);
            }
          });

          const popularAuthors = Object.entries(authorStats)
            .map(([author, count]) => ({
              name: author || 'Bilinmeyen Yazar',
              readCount: count
            }))
            .sort((a, b) => b.readCount - a.readCount)
            .slice(0, 3);

          return {
            data: {
              popularBooks,
              activeReaders,
              popularAuthors
            }
          };
        } catch (error) {
          return { error: { status: 500, data: { message: 'Okuma istatistikleri alınırken hata oluştu' } } };
        }
      },
      providesTags: [{ type: 'Stats', id: 'READING' }],
    }),

    getCategoryStats: builder.query({
      queryFn: async (arg, api, extraOptions, baseQuery) => {
        try {
          const [booksResult, loansResult] = await Promise.all([
            baseQuery('/kitaplar'),
            baseQuery('/odunc-islemleri')
          ]);

          if (booksResult.error) return { error: booksResult.error };
          if (loansResult.error) return { error: loansResult.error };

          const books = booksResult.data;
          const loans = loansResult.data;

          const bookStats = {};
          loans.forEach(loan => {
            bookStats[loan.id] = (bookStats[loan.id] || 0) + 1;
          });

          const categoryStats = {};
          books.forEach(book => {
            const category = book.kategori || 'Genel';
            categoryStats[category] = (categoryStats[category] || 0) + 
              (bookStats[book.id] || 0);
          });

          const topCategories = Object.entries(categoryStats)
            .map(([name, count]) => ({ name, count: `${count} ödünç` }))
            .sort((a, b) => parseInt(b.count) - parseInt(a.count))
            .slice(0, 5);

          return { data: topCategories };
        } catch (error) {
          return { error: { status: 500, data: { message: 'Kategori istatistikleri alınırken hata oluştu' } } };
        }
      },
      providesTags: [{ type: 'Stats', id: 'CATEGORIES' }],
    }),

    getTopReaders: builder.query({
      queryFn: async (arg, api, extraOptions, baseQuery) => {
        try {
          const [usersResult, loansResult] = await Promise.all([
            baseQuery('/kullanicilar'),
            baseQuery('/odunc-islemleri')
          ]);

          if (usersResult.error) return { error: usersResult.error };
          if (loansResult.error) return { error: loansResult.error };

          const users = usersResult.data;
          const loans = loansResult.data;

          const userStats = {};
          loans.forEach(loan => {
            userStats[loan.kullanici_id] = (userStats[loan.kullanici_id] || 0) + 1;
          });
          const topReaders = Object.entries(userStats)
            .map(([userId, count]) => {
              const user = users.find(u => u.kullanici_id === parseInt(userId));
              return {
                name: user ? `${user.ad} ${user.soyad || ''}`.trim() : 'Bilinmeyen Kullanıcı',
                count: `${count} kitap`
              };
            })
            .sort((a, b) => parseInt(b.count) - parseInt(a.count))
            .slice(0, 5);

          return { data: topReaders };
        } catch (error) {
          return { error: { status: 500, data: { message: 'Okuyucu istatistikleri alınırken hata oluştu' } } };
        }
      },
      providesTags: [{ type: 'Stats', id: 'TOP_READERS' }],
    }),

    getSystemLogs: builder.query({
      query: () => '/log-kayitlari',
      providesTags: ['SystemLogs'],
    }),

  }),
});
export const {
  useGetGeneralStatsQuery,
  useGetReadingStatsQuery,
  useGetCategoryStatsQuery,
  useGetTopReadersQuery,
  useGetDashboardStatsQuery,
  useGetSystemLogsQuery,
} = statsApiSlice; 