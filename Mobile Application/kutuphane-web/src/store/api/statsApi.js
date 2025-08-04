import { apiSlice } from './apiSlice';

// Stats API endpoints - eski statsService ile aynı yapı
export const statsApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    
    // Yeni dashboard istatistikleri endpointi
    getDashboardStats: builder.query({
      query: () => 'dashboard-stats', // Backend'deki yeni endpoint'i çağır
      providesTags: [{ type: 'Stats', id: 'DASHBOARD' }],
    }),

    // Genel istatistikler - eski statsService.getGeneralStats ile aynı
    getGeneralStats: builder.query({
      queryFn: async (arg, api, extraOptions, baseQuery) => {
        try {
          // Eski API'deki Promise.all yapısı
          const [booksResult, usersResult, loansResult] = await Promise.all([
            baseQuery('/kitaplar'),
            baseQuery('/kullanicilar'),
            baseQuery('/odunc-islemleri')
          ]);

          // Hata kontrolü
          if (booksResult.error) return { error: booksResult.error };
          if (usersResult.error) return { error: usersResult.error };
          if (loansResult.error) return { error: loansResult.error };

          const books = booksResult.data;
          const users = usersResult.data;
          const loans = loansResult.data;

          // Eski API'deki aynı hesaplama
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

    // Okuma istatistikleri - eski statsService.getReadingStats ile aynı
    getReadingStats: builder.query({
      queryFn: async (arg, api, extraOptions, baseQuery) => {
        try {
          // Eski API'deki Promise.all yapısı
          const [booksResult, usersResult, loansResult] = await Promise.all([
            baseQuery('/kitaplar'),
            baseQuery('/kullanicilar'),
            baseQuery('/odunc-islemleri')
          ]);

          // Hata kontrolü
          if (booksResult.error) return { error: booksResult.error };
          if (usersResult.error) return { error: usersResult.error };
          if (loansResult.error) return { error: loansResult.error };

          const books = booksResult.data;
          const users = usersResult.data;
          const loans = loansResult.data;

          // Eski API'deki aynı hesaplamalar
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

          // En aktif okuyucular
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

          // Popüler yazarlar
          const authorStats = {};
          books.forEach(book => {
            if (book.yazar) {
              authorStats[book.yazar] = (authorStats[book.yazar] || 0) + (bookStats[book.id] || 0);
            }
          });

          const popularAuthors = Object.entries(authorStats)
            .map(([author, count]) => ({
              name: author,
              bookCount: count
            }))
            .sort((a, b) => b.bookCount - a.bookCount)
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

    // Aylık istatistikler
    getMonthlyStats: builder.query({
      queryFn: async (arg, api, extraOptions, baseQuery) => {
        try {
          const [loansResult, booksResult] = await Promise.all([
            baseQuery('/odunc-islemleri'),
            baseQuery('/kitaplar')
          ]);

          if (loansResult.error) return { error: loansResult.error };
          if (booksResult.error) return { error: booksResult.error };

          const loans = loansResult.data;
          const books = booksResult.data;

          // Son 6 ayın verilerini hesapla
          const months = [];
          for (let i = 5; i >= 0; i--) {
            const date = new Date();
            date.setMonth(date.getMonth() - i);
            const monthName = date.toLocaleDateString('tr-TR', { month: 'long' });
            const monthYear = date.getFullYear();
            
            const monthLoans = loans.filter(loan => {
              const loanDate = new Date(loan.verilis_tarihi);
              return loanDate.getMonth() === date.getMonth() && 
                     loanDate.getFullYear() === date.getFullYear();
            });

            months.push({
              month: monthName,
              year: monthYear,
              loanCount: monthLoans.length,
              returnCount: monthLoans.filter(loan => loan.iade_tarihi).length
            });
          }

          return { data: months };
        } catch (error) {
          return { error: { status: 500, data: { message: 'Aylık istatistikler alınırken hata oluştu' } } };
        }
      },
      providesTags: [{ type: 'Stats', id: 'MONTHLY' }],
    }),

    // Yıllık istatistikler
    getYearlyStats: builder.query({
      queryFn: async (arg, api, extraOptions, baseQuery) => {
        try {
          const [loansResult, booksResult, usersResult] = await Promise.all([
            baseQuery('/odunc-islemleri'),
            baseQuery('/kitaplar'),
            baseQuery('/kullanicilar')
          ]);

          if (loansResult.error) return { error: loansResult.error };
          if (booksResult.error) return { error: booksResult.error };
          if (usersResult.error) return { error: usersResult.error };

          const loans = loansResult.data;
          const books = booksResult.data;
          const users = usersResult.data;

          const currentYear = new Date().getFullYear();
          const yearLoans = loans.filter(loan => {
            const loanDate = new Date(loan.verilis_tarihi);
            return loanDate.getFullYear() === currentYear;
          });

          return {
            data: {
              totalLoans: yearLoans.length,
              totalBooks: books.length,
              totalUsers: users.length,
              averageLoansPerMonth: Math.round(yearLoans.length / 12),
              mostActiveMonth: getMostActiveMonth(yearLoans),
              year: currentYear
            }
          };
        } catch (error) {
          return { error: { status: 500, data: { message: 'Yıllık istatistikler alınırken hata oluştu' } } };
        }
      },
      providesTags: [{ type: 'Stats', id: 'YEARLY' }],
    }),

    // En çok okuyan üyeler - YENİ: Home sayfası için
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

          // Kullanıcı bazında kitap sayısı
          const userStats = {};
          loans.forEach(loan => {
            userStats[loan.kullanici_id] = (userStats[loan.kullanici_id] || 0) + 1;
          });

          // En çok okuyan 5 üyeyi sırala
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

  }),
});

// Yardımcı fonksiyonlar
function getMostActiveMonth(loans) {
  const monthStats = {};
  loans.forEach(loan => {
    const month = new Date(loan.verilis_tarihi).getMonth();
    monthStats[month] = (monthStats[month] || 0) + 1;
  });

  const mostActiveMonth = Object.entries(monthStats)
    .sort((a, b) => b[1] - a[1])[0];

  if (mostActiveMonth) {
    const monthNames = [
      'Ocak', 'Şubat', 'Mart', 'Nisan', 'Mayıs', 'Haziran',
      'Temmuz', 'Ağustos', 'Eylül', 'Ekim', 'Kasım', 'Aralık'
    ];
    return monthNames[parseInt(mostActiveMonth[0])];
  }

  return 'Veri yok';
}

// Hooks export et
export const {
  useGetDashboardStatsQuery,
  useGetGeneralStatsQuery,
  useGetReadingStatsQuery,
  useGetMonthlyStatsQuery,
  useGetYearlyStatsQuery,
  useGetTopReadersQuery,
} = statsApiSlice; 