import React from 'react';
import { useSelector } from 'react-redux';
import { Navigate } from 'react-router-dom';
import { CircularProgress, Box, Typography } from '@mui/material';
import { selectIsAuthenticated, selectIsAuthLoaded } from '../../store/slices/authSlice';

const ProtectedRoute = ({ children }) => {
  const isAuthenticated = useSelector(selectIsAuthenticated);
  const isAuthLoaded = useSelector(selectIsAuthLoaded);

  // Auth bilgileri henüz yüklenmediyse loading göster
  if (!isAuthLoaded) {
    return (
      <Box 
        sx={{ 
          display: 'flex', 
          flexDirection: 'column',
          alignItems: 'center', 
          justifyContent: 'center', 
          minHeight: '60vh',
          gap: 2
        }}
      >
        <CircularProgress size={60} thickness={4} sx={{ color: '#388d34' }} />
        <Typography variant="h6" sx={{ color: '#666' }}>
          Yükleniyor...
        </Typography>
      </Box>
    );
  }

  // Auth bilgileri yüklendi ama kullanıcı giriş yapmamışsa login'e yönlendir
  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }

  // Kullanıcı giriş yapmışsa sayfayı göster
  return children;
};

export default ProtectedRoute; 