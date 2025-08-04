import React, { useState, useEffect, useMemo } from 'react';
import { Skeleton, Box } from '@mui/material';
import defaultBookImage from '../assets/sekerlogo.png';

const ImageOptimizer = ({ 
  src, 
  alt, 
  className, 
  style, 
  fallbackSrc = defaultBookImage,
  width = 200,
  height = 300,
  ...props 
}) => {
  const [imageSrc, setImageSrc] = useState(null);
  const [isLoading, setIsLoading] = useState(true);
  const [hasError, setHasError] = useState(false);

  // Base64 URL'lerini optimize et
  const optimizedSrc = useMemo(() => {
    if (!src) return fallbackSrc;
    
    // Base64 formatındaysa ve çok büyükse optimize et
    if (src.startsWith('data:image/')) {
      // Base64'ü canvas'a çiz ve boyutunu küçült
      return new Promise((resolve) => {
        const img = new Image();
        img.onload = () => {
          const canvas = document.createElement('canvas');
          const ctx = canvas.getContext('2d');
          
          // Maksimum boyut
          const maxWidth = 300;
          const maxHeight = 400;
          
          let { width: imgWidth, height: imgHeight } = img;
          
          // Boyut oranını koru
          if (imgWidth > maxWidth) {
            imgHeight = (imgHeight * maxWidth) / imgWidth;
            imgWidth = maxWidth;
          }
          
          if (imgHeight > maxHeight) {
            imgWidth = (imgWidth * maxHeight) / imgHeight;
            imgHeight = maxHeight;
          }
          
          canvas.width = imgWidth;
          canvas.height = imgHeight;
          
          // Kaliteyi düşür (0.8 = %80 kalite)
          ctx.drawImage(img, 0, 0, imgWidth, imgHeight);
          
          const optimizedDataUrl = canvas.toDataURL('image/jpeg', 0.8);
          resolve(optimizedDataUrl);
        };
        img.onerror = () => resolve(fallbackSrc);
        img.src = src;
      });
    }
    
    return src;
  }, [src, fallbackSrc]);

  useEffect(() => {
    if (typeof optimizedSrc === 'string') {
      setImageSrc(optimizedSrc);
      setIsLoading(false);
    } else if (optimizedSrc instanceof Promise) {
      optimizedSrc.then(result => {
        setImageSrc(result);
        setIsLoading(false);
      });
    }
  }, [optimizedSrc]);

  const handleError = () => {
    setHasError(true);
    setIsLoading(false);
    setImageSrc(fallbackSrc);
  };

  if (isLoading) {
    return (
      <Skeleton 
        variant="rectangular" 
        width={width} 
        height={height}
        sx={{ borderRadius: 1 }}
      />
    );
  }

  return (
    <img
      src={hasError ? fallbackSrc : imageSrc}
      alt={alt}
      className={className}
      style={style}
      onError={handleError}
      loading="lazy" // Lazy loading ekle
      {...props}
    />
  );
};

export default ImageOptimizer;