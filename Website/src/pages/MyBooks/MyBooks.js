import React from 'react';
import { Container, Typography, CircularProgress, Button, Box, Card, CardContent, Chip } from '@mui/material';
import { CalendarToday, CheckCircle, Book, History } from '@mui/icons-material';
import { useGetUserActiveLoansQuery } from '../../store/api/loansApi';
import { useSelector } from 'react-redux';
import { selectCurrentUser, selectIsAuthenticated } from '../../store/slices/authSlice';
import { useNavigate } from 'react-router-dom';
import defaultBookImage from '../../assets/sekerlogo.png';
import './MyBooks.css';

export default function MyBooks() {
  const navigate = useNavigate();

  const user = useSelector(selectCurrentUser);
  const isAuthenticated = useSelector(selectIsAuthenticated);
  

  const { data: myBooks = [], isLoading, error, refetch } = useGetUserActiveLoansQuery(user?.kullanici_id, {
    skip: !isAuthenticated || !user?.kullanici_id
  });

  const isOverdue = (iadeTarihi) => {
    const today = new Date();
    const iadeDate = new Date(iadeTarihi);
    return today > iadeDate;
  };

  const getTimeRemaining = (iadeTarihi) => {
    const today = new Date();
    const iadeDate = new Date(iadeTarihi);
    const diffTime = iadeDate - today;
    
    if (diffTime <= 0) {
      return { days: 0, hours: 0, minutes: 0, isOverdue: true };
    }
    
    const days = Math.floor(diffTime / (1000 * 60 * 60 * 24));
    const hours = Math.floor((diffTime % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    const minutes = Math.floor((diffTime % (1000 * 60 * 60)) / (1000 * 60));
    
    return { days, hours, minutes, isOverdue: false };
  };

  if (isLoading) {
    return (
      <div className="loading-container">
        <CircularProgress size={60} thickness={4} sx={{ color: '#51a646' }} />
        <Typography variant="h6" sx={{ mt: 2 }}>Kitaplarım Yükleniyor...</Typography>
      </div>
    );
  }
  if (error) {
    return (
      <div className="error-container">
        <Typography variant="h6" color="error">
          {error?.data?.message || 'Kitaplarınız yüklenirken bir hata oluştu'}
        </Typography>
        <Button onClick={() => refetch()} variant="contained" sx={{ mt: 2 }}>
          Tekrar Dene
        </Button>
      </div>
    );
  }

  return (
    <Container maxWidth="lg" className="my-books-container">
      <Typography variant="h3" className="my-books-title">
        Kitaplarım
      </Typography>
      <Typography variant="subtitle1" className="my-books-subtitle">
        Şu anda ödünç aldığınız kitaplar
      </Typography>

      <Box sx={{ mt: 4 }}>
        {myBooks.length === 0 ? (
          <Card sx={{ p: 4, textAlign: 'center', backgroundColor: '#f8f9fa' }}>
            <Book sx={{ fontSize: 60, color: '#51A646', mb: 2 }} />
            <Typography variant="h6" sx={{ mb: 2 }}>
              Henüz ödünç aldığınız kitap bulunmuyor
            </Typography>
            <Typography variant="body1" color="text.secondary" sx={{ mb: 3 }}>
              Kitap ödünç almak için kütüphane yetkilisi veya admin ile iletişime geçiniz.
            </Typography>
            <Button 
              variant="contained" 
              onClick={() => navigate('/books')}
              sx={{ 
                backgroundColor: '#51A646',
                '&:hover': { backgroundColor: '#51a646' }
              }}
            >
              Kitaplara Git
            </Button>
          </Card>
        ) : (
          <Box sx={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(350px, 1fr))', gap: 3 }}>
                         {myBooks.map((book) => {
               const overdue = isOverdue(book.iade_tarihi);
               const timeRemaining = getTimeRemaining(book.iade_tarihi);
              
              return (
                <Card key={book.odunc_id} sx={{ p: 3, height: 'fit-content' }}>
                  <CardContent sx={{ p: 0 }}>
                    <Box sx={{ display: 'flex', gap: 2, mb: 2 }}>
                      {/* Kitap Görseli */}
                      <Box sx={{ flex: '0 0 80px' }}>
                        <img 
                          src={book.kitap_gorsel && book.kitap_gorsel.startsWith('data:image/') ? book.kitap_gorsel : defaultBookImage} 
                          alt={book.kitap_adi} 
                          style={{ 
                            width: '100%', 
                            height: '120px', 
                            objectFit: 'cover',
                            borderRadius: '8px'
                          }}
                          onError={(e) => {
                            e.target.src = defaultBookImage;
                          }}
                        />
                      </Box>
                      
                      {/* Kitap Bilgileri */}
                      <Box sx={{ flex: 1 }}>
                        <Typography variant="h6" sx={{ fontWeight: 600, color: '#232946', mb: 1 }}>
                          {book.kitap_adi}
                        </Typography>
                        <Typography variant="body2" color="text.secondary" sx={{ mb: 1 }}>
                          {book.yazar}
                        </Typography>
                        {book.yayinevi && (
                          <Typography variant="body2" color="text.secondary" sx={{ mb: 1 }}>
                            {book.yayinevi}
                          </Typography>
                        )}
                        
                        {/* Durum Etiketi */}
                        <Box sx={{ mb: 2 }}>
                          {overdue ? (
                            <Chip 
                              icon={<History />} 
                              label="Gecikmiş" 
                              color="error" 
                              size="small" 
                            />
                          ) : (
                            <Chip 
                              icon={<CheckCircle />} 
                              label="Aktif" 
                              color="success" 
                              size="small" 
                            />
                          )}
                        </Box>
                      </Box>
                    </Box>
                    
                    {/* Tarih Bilgileri */}
                    <Box sx={{ display: 'flex', flexDirection: 'column', gap: 1, mb: 3 }}>
                      <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                        <CalendarToday sx={{ fontSize: 16, color: '#51A646' }} />
                        <Typography variant="body2">
                          <strong>Ödünç Tarihi:</strong> {new Date(book.odunc_tarihi).toLocaleDateString('tr-TR')}
                        </Typography>
                      </Box>
                      <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                        <CalendarToday sx={{ fontSize: 16, color: overdue ? '#f44336' : '#51A646' }} />
                        <Typography variant="body2" color={overdue ? 'error' : 'text.primary'}>
                          <strong>İade Tarihi:</strong> {new Date(book.iade_tarihi).toLocaleDateString('tr-TR')}
                        </Typography>
                      </Box>
                      
                      
                    </Box>
                    
                    {/* Kalan Süre Sayacı */}
                     <Box sx={{ 
                       p: 2, 
                       backgroundColor: overdue ? '#ffebee' : '#f1f8e9', 
                       borderRadius: '8px',
                       border: `2px solid ${overdue ? '#f44336' : '#51A646'}`,
                       textAlign: 'center'
                     }}>
                       {overdue ? (
                         <Typography variant="h6" color="error" sx={{ fontWeight: 600 }}>
                           ⚠️ Gecikmiş
                         </Typography>
                       ) : (
                         <>
                           <Typography variant="h6" color="success.main" sx={{ fontWeight: 600, mb: 1 }}>
                             ⏰ Kalan Süre
                           </Typography>
                           <Box sx={{ display: 'flex', justifyContent: 'center', gap: 1, mb: 1 }}>
                             <Typography variant="body1" color="success.main" sx={{ fontWeight: 600 }}>
                               {timeRemaining.days} gün
                             </Typography>
                             <Typography variant="body1" color="success.main" sx={{ fontWeight: 600 }}>
                               {timeRemaining.hours} saat
                             </Typography>
                             <Typography variant="body1" color="success.main" sx={{ fontWeight: 600 }}>
                               {timeRemaining.minutes} dakika
                             </Typography>
                           </Box>
                         </>
                       )}
                       <Typography variant="body2" color="text.secondary" sx={{ mt: 1 }}>
                         Kitap iadesi için kütüphane yetkilisi ile iletişime geçiniz
                       </Typography>
                     </Box>
                  </CardContent>
                </Card>
              );
            })}
          </Box>
        )}
      </Box>

      
    </Container>
  );
} 