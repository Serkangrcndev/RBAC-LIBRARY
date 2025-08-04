import React from 'react';
import { useSelector } from 'react-redux';
import { Navigate } from 'react-router-dom';
import { CircularProgress, Box, Typography } from '@mui/material';
import { selectIsAuthenticated, selectIsAuthLoaded } from '../../store/slices/authSlice';

const ProtectedRoute = ({ children }) => {
  const isAuthenticated = useSelector(selectIsAuthenticated);
  const isAuthLoaded = useSelector(selectIsAuthLoaded);


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
        <CircularProgress size={60} thickness={4} sx={{ color: '#51a646' }} />
        <Typography variant="h6" sx={{ color: '#666' }}>
          Yükleniyor...
        </Typography>
      </Box>
    );
  }


  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }


  return children;
};

export default ProtectedRoute; 