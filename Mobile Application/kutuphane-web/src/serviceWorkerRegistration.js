// Bu dosya service worker'ı yönetmek için kullanılır
// Şu anda projede PWA aktif değil, sadece unregister fonksiyonu kullanılıyor

export function unregister() {
  if ('serviceWorker' in navigator) {
    navigator.serviceWorker.ready
      .then((registration) => {
        registration.unregister();
      })
      .catch((error) => {
        console.error(error.message);
      });
  }
}
