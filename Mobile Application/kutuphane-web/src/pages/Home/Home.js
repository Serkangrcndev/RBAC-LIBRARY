import React from 'react';
import './Home.css';
import { LibraryBooks, AutoStories, MenuBook, Language, CalendarToday } from '@mui/icons-material';
import { useGetFeaturedBooksQuery } from '../../store/api/booksApi';
import { useGetGeneralStatsQuery, useGetTopReadersQuery, useGetReadingStatsQuery } from '../../store/api/statsApi';
import ImageOptimizer from '../../components/ImageOptimizer';
import defaultBookImage from '../../assets/sekerlogo.png'; // Varsayılan kitap görseli

export default function Home() {
  // RTK Query hooks - API'den veri çekiyoruz
  const { data: featuredBooks = [], isLoading: booksLoading, error: booksError } = useGetFeaturedBooksQuery();
  const { data: statsData, isLoading: statsLoading, error: statsError } = useGetGeneralStatsQuery();
  const { data: topReaders = [], isLoading: readersLoading, error: readersError } = useGetTopReadersQuery();
  const { data: readingStats, isLoading: readingStatsLoading, error: readingStatsError } = useGetReadingStatsQuery();

  // Stats array'ini API'den gelen veriye göre dinamik oluştur
  const stats = React.useMemo(() => {
    if (!statsData) return [];
    
    return [
      {
        icon: <LibraryBooks sx={{ fontSize: 40, color: '#388d34' }} />,
        number: statsData.totalBooks ? `${statsData.totalBooks}+` : '0',
        text: 'Toplam Kitap',
        loading: statsLoading
      },
      {
        icon: <AutoStories sx={{ fontSize: 40, color: '#388d34' }} />,
        number: statsData.activeUsers ? `${statsData.activeUsers}+` : '0',
        text: 'Aktif Kullanıcı',
        loading: statsLoading
      },
      {
        icon: <MenuBook sx={{ fontSize: 40, color: '#388d34' }} />,
        number: statsData.dailyLoans ? `${statsData.dailyLoans}+` : '0',
        text: 'Günlük Ödünç',
        loading: statsLoading
      }
    ];
  }, [statsData, statsLoading]);

  // Popüler kitapları al
  const popularBooks = readingStats?.popularBooks || [];

  // Error durumları
  if (booksError || statsError || readersError || readingStatsError) {
    return (
      <div className="error-container">
        <p>Veriler yüklenirken bir hata oluştu. Lütfen sayfayı yenileyin.</p>
        <button 
          className="retry-button"
          onClick={() => window.location.reload()}
        >
          Yeniden Dene
        </button>
      </div>
    );
  }

  // Loading durumu
  if (booksLoading || statsLoading || readersLoading || readingStatsLoading) {
    return (
      <div className="loading-container">
        <div className="loading-spinner"></div>
        <p>Veriler yükleniyor...</p>
      </div>
    );
  }

  return (
    <div className="home-margin-top">
      <div className="home-container">
        {/* Ana başlık */}
        <h1 className="home-title">
          Kayseri Şeker Kütüphanesi'ne Hoş Geldiniz
        </h1>
        <p className="home-subtitle">
          Bilgi ve keşfin kapılarını açan dijital kütüphanemizde binlerce kitap sizi bekliyor.
        </p>

        {/* İstatistikler - API'den gelen veriler */}
        <div className="stats-section">
          {stats.map((stat, index) => (
            <div key={index} className="stat-card">
              {stat.loading ? (
                <div className="stat-loading">
                  <div className="loading-spinner"></div>
                </div>
              ) : (
                stat.icon
              )}
              <div className="stat-number">
                {stat.loading ? '...' : stat.number}
              </div>
              <div className="stat-text">{stat.text}</div>
            </div>
          ))}
        </div>

        {/* Öne çıkan kitaplar */}
        <div className="home-featured-section">
          <h2 className="home-featured-title">Öne Çıkan Kitaplar</h2>
          {featuredBooks.length === 0 ? (
            <div className="no-data-message">
              <p>Henüz öne çıkan kitap bulunmuyor.</p>
            </div>
          ) : (
            <div className={`home-featured-list ${featuredBooks.length === 1 ? 'flex-layout' : ''}`}>
              {featuredBooks.map((book) => (
                <div key={book.id} className="home-featured-card">
                  <div className="book-image-container">
                    <ImageOptimizer 
                      src={book.kitap_gorsel} 
                      alt={book.title} 
                      className="home-featured-img"
                      fallbackSrc={defaultBookImage}
                      width={200}
                      height={300}
                    />
                    <div className="book-category-tag">{book.category || 'Genel'}</div>
                  </div>
                  <div className="home-featured-content">
                    <h3 className="home-featured-book-title">{book.title}</h3>
                    <p className="home-featured-book-author">{book.author}</p>
                    <p className="home-featured-book-desc">{book.description}</p>
                    <div className="book-details">
                      <div className="book-detail-item">
                        <MenuBook sx={{ fontSize: 14 }} />
                        {book.pageCount} sayfa
                      </div>
                      <div className="book-detail-item">
                        <Language sx={{ fontSize: 14 }} />
                        {book.language}
                      </div>
                      <div className="book-detail-item">
                        <CalendarToday sx={{ fontSize: 14 }} />
                        {book.publishYear}
                      </div>
                    </div>
                    <button className="home-featured-btn">Detayları Gör</button>
                  </div>
                </div>
              ))}
            </div>
          )}
        </div>

        {/* Okuma İstatistikleri - En Alt Bölüm */}
        <div className="reading-stats-section">
          <h2 className="reading-stats-title">Bu Ay En Çok Okunanlar</h2>
          <p className="reading-stats-subtitle">
            Kütüphanemizin en aktif okuyucuları ve en popüler kitaplar
          </p>
          <div className="reading-stats-grid">
            <div className="reading-stats-card">
              <h3 className="reading-stats-card-title">En Çok Okuyan Üyeler</h3>
              <div className="reading-stats-list">
                {topReaders.length === 0 ? (
                  <div className="no-data-message">
                    <p>Henüz okuma verisi bulunmuyor.</p>
                  </div>
                ) : (
                  topReaders.map((reader, index) => (
                    <div key={index} className="reading-stats-item">
                      <div className="reading-stats-rank">{index + 1}</div>
                      <div className="reading-stats-name">{reader.name}</div>
                      <div className="reading-stats-count">{reader.count}</div>
                    </div>
                  ))
                )}
              </div>
            </div>
            <div className="reading-stats-card">
              <h3 className="reading-stats-card-title">En Popüler Kitaplar</h3>
              <div className="reading-stats-list">
                {popularBooks.length === 0 ? (
                  <div className="no-data-message">
                    <p>Henüz kitap okuma verisi bulunmuyor.</p>
                  </div>
                ) : (
                  popularBooks.map((book, index) => (
                    <div key={index} className="reading-stats-item">
                      <div className="reading-stats-rank">{index + 1}</div>
                      <div className="reading-stats-name">{book.title}</div>
                      <div className="reading-stats-count">{book.readCount} kez okundu</div>
                    </div>
                  ))
                )}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
} 