import React, { useState } from 'react';
import { Container, Typography, TextField, CircularProgress, Chip, Dialog, DialogTitle, DialogContent, DialogActions, Button, Box, Alert } from '@mui/material';
import { Search, MenuBook, Business, CalendarToday, Inventory, CheckCircle, Cancel, Close, Language, Category } from '@mui/icons-material';
import { useGetAllBooksQuery } from '../../store/api/booksApi';

import defaultBookImage from '../../assets/sekerlogo.png'; 
import './Books.css';

export default function Books() {
  const [search, setSearch] = useState('');
  const [selectedBook, setSelectedBook] = useState(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const { data: books = [], isLoading, error, refetch } = useGetAllBooksQuery();

  const handleOpenModal = (book) => {
    setSelectedBook(book);
    setIsModalOpen(true);
  };

  const handleCloseModal = () => {
    setIsModalOpen(false);
    setSelectedBook(null);
  };
  const filtered = books.filter(book =>
    (book.title?.toLowerCase() || '').includes(search.toLowerCase()) ||
    (book.author?.toLowerCase() || '').includes(search.toLowerCase()) ||
    (book.yayinevi?.toLowerCase() || '').includes(search.toLowerCase())
  );

  if (isLoading) {
    return (
      <div className="loading-container">
        <CircularProgress size={60} thickness={4} sx={{ color: '#51a646' }} />
        <Typography variant="h6" sx={{ mt: 2 }}>Kitaplar Yükleniyor...</Typography>
      </div>
    );
  }

  if (error) {
    return (
      <div className="error-container">
        <Typography variant="h6" color="error">
          {error?.data?.message || 'Kitaplar yüklenirken bir hata oluştu'}
        </Typography>
        <button onClick={() => refetch()} className="retry-button">
          Tekrar Dene
        </button>
      </div>
    );
  }

  return (
    <Container maxWidth="lg" className="books-container">
      <Typography variant="h3" className="books-title">
        Kitaplar
      </Typography>
      <Typography variant="subtitle1" className="books-subtitle">
        Kütüphanemizdeki tüm kitapları keşfedin
      </Typography>

      <div className="search-container">
        <Search className="search-icon" />
        <TextField
          placeholder="Kitap adı, yazar ara..."
          variant="outlined"
          value={search}
          onChange={e => setSearch(e.target.value)}
          className="search-input"
          fullWidth
        />
      </div>


      <div className="home-featured-list">
        {filtered.map((book) => (
          <div className="home-featured-card" key={book.id}>
            <div className="book-image-container">
              <img 
                src={book.kitap_gorsel && book.kitap_gorsel.startsWith('data:image/') ? book.kitap_gorsel : defaultBookImage} 
                alt={book.title} 
                className="home-featured-img"
                onError={(e) => {
                  e.target.src = defaultBookImage;
                }}
              />
              <div className="book-status-tag">
                {book.stok_durumu === 'Stokta Var' ? (
                  <Chip 
                    icon={<CheckCircle />} 
                    label={`Stok: ${book.kitap_adet}`} 
                    color="success" 
                    size="small" 
                  />
                ) : (
                  <Chip 
                    icon={<Cancel />} 
                    label="Stokta Yok" 
                    color="error" 
                    size="small" 
                  />
                )}
              </div>
            </div>
            <div className="home-featured-content">
              <h3 className="home-featured-book-title">{book.title}</h3>
              <p className="home-featured-book-author">{book.author}</p>
              {book.yayinevi && (
                <p className="home-featured-book-publisher">
                  <Business sx={{ fontSize: 14, mr: 0.5 }} />
                  {book.yayinevi}
                </p>
              )}
              <div className="book-details">
                {book.pageCount && (
                  <div className="book-detail-item">
                    <MenuBook sx={{ fontSize: 14 }} />
                    {book.pageCount} sayfa
                  </div>
                )}
                {book.publishYear && (
                  <div className="book-detail-item">
                    <CalendarToday sx={{ fontSize: 14 }} />
                    {book.publishYear}
                  </div>
                )}
                <div className="book-detail-item">
                  <Inventory sx={{ fontSize: 14 }}/>
                  Stok: {book.stok_durumu === 'Stokta Var' ? book.kitap_adet : 0}
                </div>
              </div>
              <button 
                className="home-featured-btn" 
                onClick={() => handleOpenModal(book)}
              >
                Detayları Gör
              </button>
            </div>
          </div>
        ))}
      </div>

      {filtered.length === 0 && (
        <div className="no-results">
          <Typography variant="h6">
            Aradığınız kriterlere uygun kitap bulunamadı.
          </Typography>
        </div>
      )}

      {/* Kitap Detay Modal */}
      <Dialog 
        open={isModalOpen} 
        onClose={handleCloseModal}
        maxWidth="md"
        fullWidth
        PaperProps={{
          style: {
            borderRadius: '16px',
            maxHeight: '90vh'
          }
        }}
      >
        {selectedBook && (
          <>
            <DialogTitle sx={{ 
              display: 'flex', 
              justifyContent: 'space-between', 
              alignItems: 'center',
              borderBottom: '1px solid #e0e0e0',
              pb: 2
            }}>
              <Typography variant="h5" sx={{ fontWeight: 600, color: '#232946' }}>
                {selectedBook.title}
              </Typography>
              <Button
                onClick={handleCloseModal}
                sx={{ minWidth: 'auto', p: 1 }}
              >
                <Close />
              </Button>
            </DialogTitle>
            
            <DialogContent sx={{ pt: 3 }}>
              <Box sx={{ display: 'flex', gap: 3, flexWrap: 'wrap' }}>
                {/* Kitap Görseli */}
                <Box sx={{ flex: '0 0 200px' }}>
                  <img 
                    src={selectedBook.kitap_gorsel && selectedBook.kitap_gorsel.startsWith('data:image/') ? selectedBook.kitap_gorsel : defaultBookImage} 
                    alt={selectedBook.title} 
                    style={{ 
                      width: '100%', 
                      height: '280px', 
                      objectFit: 'cover',
                      borderRadius: '8px',
                      boxShadow: '0 4px 12px rgba(0,0,0,0.1)'
                    }}
                    onError={(e) => {
                      e.target.src = defaultBookImage;
                    }}
                  />
                  <Box sx={{ mt: 2, display: 'flex', justifyContent: 'center' }}>
                    {selectedBook.stok_durumu === 'Stokta Var' ? (
                      <Chip 
                        icon={<CheckCircle />} 
                        label={`Stok: ${selectedBook.kitap_adet} adet`} 
                        color="success" 
                        size="medium" 
                      />
                    ) : (
                      <Chip 
                        icon={<Cancel />} 
                        label="Stokta Yok" 
                        color="error" 
                        size="medium" 
                      />
                    )}
                  </Box>
                </Box>

                {/* Kitap Bilgileri */}
                <Box sx={{ flex: 1, minWidth: '300px' }}>
                  <Typography variant="h6" sx={{ mb: 2, color: '#232946', fontWeight: 600 }}>
                    {selectedBook.author}
                  </Typography>
                  
                  <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
                    {selectedBook.yayinevi && (
                      <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                        <Business sx={{ color: '#51A646', fontSize: 20 }} />
                        <Typography variant="body1">
                          <strong>Yayınevi:</strong> {selectedBook.yayinevi}
                        </Typography>
                      </Box>
                    )}
                    
                    {selectedBook.pageCount && (
                      <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                        <MenuBook sx={{ color: '#51A646', fontSize: 20 }} />
                        <Typography variant="body1">
                          <strong>Sayfa Sayısı:</strong> {selectedBook.pageCount}
                        </Typography>
                      </Box>
                    )}
                    
                    {selectedBook.publishYear && (
                      <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                        <CalendarToday sx={{ color: '#51A646', fontSize: 20 }} />
                        <Typography variant="body1">
                          <strong>Yayın Yılı:</strong> {selectedBook.publishYear}
                        </Typography>
                      </Box>
                    )}
                    
                    <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                      <Inventory sx={{ color: '#51A646', fontSize: 20 }} />
                      <Typography variant="body1">
                        <strong>Stok Durumu:</strong> {selectedBook.stok_durumu === 'Stokta Var' ? selectedBook.kitap_adet : 0} adet
                      </Typography>
                    </Box>

                    {selectedBook.kategori && (
                      <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                        <Category sx={{ color: '#51A646', fontSize: 20 }} />
                        <Typography variant="body1">
                          <strong>Kategori:</strong> {selectedBook.kategori}
                        </Typography>
                      </Box>
                    )}

                    {selectedBook.dil && (
                      <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                        <Language sx={{ color: '#51A646', fontSize: 20 }} />
                        <Typography variant="body1">
                          <strong>Dil:</strong> {selectedBook.dil}
                        </Typography>
                      </Box>
                    )}
                  </Box>

                  {selectedBook.aciklama && (
                    <Box sx={{ mt: 3 }}>
                      <Typography variant="h6" sx={{ mb: 1, color: '#232946' }}>
                        Kitap Açıklaması
                      </Typography>
                      <Typography variant="body2" sx={{ 
                        lineHeight: 1.6, 
                        color: '#666',
                        backgroundColor: '#f8f9fa',
                        padding: 2,
                        borderRadius: '8px',
                        border: '1px solid #e9ecef'
                      }}>
                        {selectedBook.aciklama}
                      </Typography>
                    </Box>
                  )}
                </Box>
              </Box>
            </DialogContent>
            
            <DialogActions sx={{ p: 3, borderTop: '1px solid #e0e0e0', display: 'flex', gap: 2, justifyContent: 'space-between' }}>
              <Box>
                {selectedBook.mevcut && selectedBook.kitap_adet > 0 ? (
                  <Alert severity="info" sx={{ mb: 0, maxWidth: '400px' }}>
                    <Typography variant="body2" sx={{ fontWeight: 600, mb: 1 }}>
                      📚 Kitap Ödünç Alma
                    </Typography>
                    <Typography variant="body2">
                      Bu kitabı ödünç almak için kütüphane yetkilisi veya admin ile iletişime geçiniz. 
                      Kendi başınıza ödünç alma işlemi yapamazsınız.
                    </Typography>
                  </Alert>
                ) : (
                  <Alert severity="warning" sx={{ mb: 0 }}>
                    Bu kitap şu anda mevcut değil
                  </Alert>
                )}
              </Box>
              
              <Button 
                onClick={handleCloseModal}
                variant="outlined"
                sx={{ 
                  borderColor: '#666', 
                  color: '#666',
                  '&:hover': {
                    borderColor: '#333',
                    backgroundColor: 'rgba(0, 0, 0, 0.04)'
                  }
                }}
              >
                Kapat
              </Button>
            </DialogActions>
          </>
                 )}
       </Dialog>


     </Container>
   );
 } 