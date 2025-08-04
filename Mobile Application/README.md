# Library Management System - Mobile Application
# Kütüphane Yönetim Sistemi - Mobil Uygulama

[![React Native](https://img.shields.io/badge/React%20Native-0.79.5-blue.svg)](https://reactnative.dev/)
[![Expo](https://img.shields.io/badge/Expo-53.0.20-purple.svg)](https://expo.dev/)
[![Redux Toolkit](https://img.shields.io/badge/Redux%20Toolkit-2.8.2-purple.svg)](https://redux-toolkit.js.org/)

## 📚 Overview (Genel Bakış)

React Native ve Expo ile geliştirilmiş kapsamlı kütüphane yönetim sistemi mobil uygulaması. Cross-platform mobil deneyim sunan, RBAC (Rol Tabanlı Erişim Kontrolü) sistemi içeren modern bir uygulama.

## 🚀 Key Features (Temel Özellikler)

### 🔐 Advanced RBAC System (Gelişmiş RBAC Sistemi)
- **Role-based Access Control**: Üç farklı kullanıcı rolü
  - **Admin**: Tam sistem erişimi ve yönetimi
  - **Kütüphane Görevlisi**: Kitap ve emanet işlemleri
  - **Üye**: Temel kitap tarama ve kişisel işlemler
- **Dynamic Navigation**: Rol bazlı dinamik navigasyon
- **Permission-based UI**: Yetki tabanlı kullanıcı arayüzü

### 📖 Mobile Book Management (Mobil Kitap Yönetimi)
- **Book Browsing**: Kitap tarama ve arama
- **Advanced Search**: Gelişmiş arama ve filtreleme
- **Book Details**: Detaylı kitap bilgileri
- **Stock Information**: Gerçek zamanlı stok bilgisi
- **Category Filtering**: Kategori bazlı filtreleme
- **Author Information**: Yazar bilgileri

### 🔄 Mobile Loan System (Mobil Emanet Sistemi)
- **Loan Management**: Emanet yönetimi
- **Loan History**: Emanet geçmişi
- **Due Date Tracking**: Son teslim tarihi takibi
- **Overdue Alerts**: Gecikme uyarıları
- **Quick Actions**: Hızlı işlemler
- **Status Updates**: Durum güncellemeleri

### 👥 Mobile User Management (Mobil Kullanıcı Yönetimi)
- **User Authentication**: Kullanıcı kimlik doğrulama
- **Profile Management**: Profil yönetimi
- **Role-based Features**: Rol tabanlı özellikler
- **Session Management**: Oturum yönetimi
- **Offline Support**: Çevrimdışı destek

### 📊 Mobile Analytics (Mobil Analitik)
- **Dashboard**: Mobil dashboard
- **Statistics**: İstatistikler
- **Real-time Updates**: Gerçek zamanlı güncellemeler
- **Performance Metrics**: Performans metrikleri

## 🛠️ Technology Stack (Teknoloji Yığını)

### Mobile Framework
- **React Native 0.79.5**: Cross-platform mobile development
- **Expo 53.0.20**: Development platform and tools
- **TypeScript**: Type-safe development

### Navigation & UI
- **React Navigation 7**: Mobile navigation solution
- **Expo Router 5.1.4**: File-based routing
- **React Native Paper 5.14.5**: Material Design components
- **Expo Vector Icons 14.1.0**: Icon library

### State Management
- **Redux Toolkit 2.8.2**: Modern Redux with simplified boilerplate
- **React Redux 9.2**: React bindings for Redux
- **Async Storage**: Local data persistence

### Development Tools
- **Expo CLI**: Development and build tools
- **ESLint**: Code linting
- **TypeScript**: Type checking

### Security & Authentication
- **JWT Tokens**: Secure authentication
- **Session Management**: Persistent user sessions
- **Password Hashing**: Crypto-js encryption
- **Role-based Authorization**: Granular access control

## 📁 Project Structure (Proje Yapısı)

```
kutuphane-mobile/
├── app/                     # Screen components (Expo Router)
│   ├── (tabs)/             # Tab navigation
│   │   ├── index.tsx       # Home tab
│   │   ├── Kitaplar.tsx    # Books tab
│   │   ├── Emanetler.tsx   # Loans tab
│   │   ├── Profil.tsx      # Profile tab
│   │   └── Yonetim.tsx     # Management tab
│   ├── _layout.tsx         # Root layout
│   └── auth/               # Authentication screens
├── components/             # Reusable components
│   ├── common/            # Common UI components
│   ├── forms/             # Form components
│   └── cards/             # Card components
├── store/                 # Redux store
│   ├── slices/            # Redux slices
│   └── store.ts           # Store configuration
├── services/              # API services
│   ├── api.ts             # API configuration
│   └── auth.ts            # Authentication service
├── hooks/                 # Custom React hooks
├── constants/             # App constants
├── assets/                # Static assets
└── types/                 # TypeScript type definitions
```

## 🚀 Getting Started (Başlangıç)

### Prerequisites (Ön Gereksinimler)
- **Node.js 18+**
- **Expo CLI**
- **Android Studio** (Android development)
- **Xcode** (iOS development, macOS only)
- **Git**

### Installation (Kurulum)

1. **Clone the repository**
```bash
git clone <repository-url>
cd "Mobile Application/kutuphane-mobile"
```

2. **Install dependencies**
```bash
npm install
```

3. **Configure environment variables**
Create a `.env` file in the root directory:
```env
EXPO_PUBLIC_API_URL=your_api_url_here
EXPO_PUBLIC_ENV=development
```

4. **Start development server**
```bash
npx expo start
```

5. **Run on device/simulator**
- **iOS**: Press `i` in terminal or scan QR code with Camera app
- **Android**: Press `a` in terminal or scan QR code with Expo Go app
- **Web**: Press `w` in terminal

## 🔧 Configuration (Yapılandırma)

### API Configuration
Update the API URL in `services/api.ts`:
```typescript
const API_BASE_URL = 'YOUR_API_URL_HERE';
```

### Authentication
The application uses Basic Authentication with session management:
- Username: `sbuhs`
- Password: `sekerstajekip`
- Session ID is stored in AsyncStorage

### Environment Variables
- `EXPO_PUBLIC_API_URL`: Backend API URL
- `EXPO_PUBLIC_ENV`: Environment (development/production)

## 📱 Features by User Role (Kullanıcı Rolüne Göre Özellikler)

### 👑 Admin
- **Complete System Access**: Tam sistem erişimi
- **User Management**: Kullanıcı yönetimi
- **System Statistics**: Sistem istatistikleri
- **Role Management**: Rol yönetimi
- **Advanced Analytics**: Gelişmiş analitik

### 📚 Librarian/Staff
- **Book Management**: Kitap yönetimi
- **Loan Operations**: Emanet işlemleri
- **User Management**: Kullanıcı yönetimi
- **Basic Reporting**: Temel raporlama
- **Stock Management**: Stok yönetimi

### 👤 Member
- **Book Browsing**: Kitap tarama
- **Personal Loan History**: Kişisel emanet geçmişi
- **Profile Management**: Profil yönetimi
- **Search & Filter**: Arama ve filtreleme
- **Notifications**: Bildirimler

## 🔐 Security Features (Güvenlik Özellikleri)

### Authentication & Authorization
- **JWT-based Authentication**: JWT tabanlı kimlik doğrulama
- **Role-based Access Control**: Rol tabanlı erişim kontrolü
- **Session Management**: Oturum yönetimi
- **Secure Storage**: Güvenli veri depolama

### Data Protection
- **Input Validation**: Girdi doğrulama
- **Secure Communication**: Güvenli iletişim
- **Data Encryption**: Veri şifreleme
- **Offline Security**: Çevrimdışı güvenlik

### Mobile Security
- **Biometric Authentication**: Biyometrik kimlik doğrulama
- **Secure Storage**: Güvenli depolama
- **Network Security**: Ağ güvenliği
- **App Security**: Uygulama güvenliği

## 📊 Performance Optimizations (Performans Optimizasyonları)

### React Native Optimizations
- **FlatList**: Optimized list rendering
- **Memoization**: React.memo and useMemo usage
- **Lazy Loading**: Component lazy loading
- **Image Optimization**: Optimized image loading

### State Management
- **Redux Toolkit**: Efficient state management
- **Selective Re-renders**: Optimized component updates
- **Memory Management**: Proper cleanup
- **Async Operations**: Efficient async handling

### Mobile Performance
- **Bundle Optimization**: Optimized JavaScript bundle
- **Native Performance**: Native module usage
- **Memory Efficiency**: Efficient memory usage
- **Battery Optimization**: Battery-friendly operations

## 🧪 Testing (Test)

### Running Tests
```bash
# Run all tests
npm test

# Run tests in watch mode
npm test -- --watch

# Run tests with coverage
npm test -- --coverage
```

### Testing Tools
- **Jest**: Testing framework
- **React Native Testing Library**: Component testing
- **Detox**: E2E testing (optional)

### Test Structure
- Unit tests for components
- Integration tests for API calls
- Redux store tests
- Navigation tests

## 📈 Monitoring & Analytics (İzleme ve Analitik)

### Performance Monitoring
- **Expo Analytics**: Built-in analytics
- **Performance Metrics**: App performance tracking
- **Error Tracking**: Error monitoring
- **User Analytics**: User behavior tracking

### Debugging Tools
- **Expo DevTools**: Development tools
- **React Native Debugger**: Debugging interface
- **Flipper**: Mobile app debugging
- **Logs**: Comprehensive logging

## 🚀 Deployment (Dağıtım)

### Build for Production

#### Android
```bash
# Build Android APK
eas build --platform android

# Build Android App Bundle
eas build --platform android --profile production
```

#### iOS
```bash
# Build iOS app
eas build --platform ios

# Build for App Store
eas build --platform ios --profile production
```

### App Store Deployment
- **Google Play Store**: Android app distribution
- **Apple App Store**: iOS app distribution
- **Expo Application Services**: EAS build and submit

### Configuration
- **app.json**: App configuration
- **eas.json**: Build configuration
- **app.config.js**: Dynamic configuration

## 🔄 API Integration (API Entegrasyonu)

### Available Endpoints
- **Authentication**: `/auth/login`, `/auth/logout`
- **Users**: `/users`, `/users/{id}`
- **Books**: `/books`, `/books/{id}`
- **Loans**: `/loans`, `/loans/{id}`
- **Roles**: `/roles`, `/roles/{id}`
- **Statistics**: `/stats/dashboard`, `/stats/loans`

### API Features
- **Real-time Updates**: WebSocket-like behavior
- **Error Handling**: Comprehensive error management
- **Retry Logic**: Automatic retry on network failures
- **Cache Management**: Intelligent data caching
- **Offline Support**: Offline data handling

## 📱 Platform Support (Platform Desteği)

### iOS
- **iOS 13+**: Minimum iOS version
- **iPhone & iPad**: Full device support
- **App Store**: Official distribution
- **TestFlight**: Beta testing

### Android
- **Android 6+**: Minimum Android version
- **Phone & Tablet**: Full device support
- **Google Play**: Official distribution
- **Internal Testing**: Beta testing

### Web (Optional)
- **Progressive Web App**: PWA support
- **Cross-browser**: Multiple browser support
- **Responsive Design**: Adaptive layout

## 🤝 Contributing (Katkıda Bulunma)

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Development Guidelines
- Follow React Native best practices
- Use TypeScript for type safety
- Write unit tests for new features
- Follow the existing code style
- Update documentation as needed

## 📄 License (Lisans)

This project is licensed under the MIT License - see the [LICENSE](../LICENSE) file for details.

## 🔄 Version History (Sürüm Geçmişi)

### v1.0.0 (Current)
- Initial release with full RBAC implementation
- Complete book and loan management
- Advanced user management system
- Cross-platform mobile experience
- Modern React Native interface