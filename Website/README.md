# Library Management System - Web Application
# Kütüphane Yönetim Sistemi - Web Uygulaması

[![React](https://img.shields.io/badge/React-19.1.0-blue.svg)](https://reactjs.org/)
[![Redux Toolkit](https://img.shields.io/badge/Redux%20Toolkit-2.8.2-purple.svg)](https://redux-toolkit.js.org/)
[![RTK Query](https://img.shields.io/badge/RTK%20Query-2.8.2-green.svg)](https://redux-toolkit.js.org/rtk-query/overview)
[![Material-UI](https://img.shields.io/badge/Material--UI-7.2.0-blue.svg)](https://mui.com/)

## 📚 Overview (Genel Bakış)

Modern web teknolojileri ile geliştirilmiş kapsamlı kütüphane yönetim sistemi web uygulaması. React 19, Redux Toolkit, RTK Query ve Material-UI kullanılarak oluşturulmuştur.

## 🚀 Key Features (Temel Özellikler)

### 🔐 Advanced RBAC System (Gelişmiş RBAC Sistemi)
- **Role-based Access Control**: Granular permission system
- **Admin Panel**: Complete system management
- **Librarian Interface**: Book and loan management
- **Member Portal**: Personal book browsing and history

### 📖 Book Management (Kitap Yönetimi)
- Add, edit, delete books with rich metadata
- Advanced search and filtering capabilities
- Stock management with real-time updates
- Category and author management
- Book cover image handling

### 🔄 Loan System (Emanet Sistemi)
- 30-day loan period with automatic tracking
- Overdue detection and penalty system
- Real-time stock updates
- Loan history and statistics
- Bulk loan operations

### 👥 User Management (Kullanıcı Yönetimi)
- Secure authentication with JWT tokens
- User registration and profile management
- Role assignment and permission management
- Session management with persistence
- Password reset functionality

### 📊 Analytics & Reports (Analitik ve Raporlar)
- Real-time dashboard with key metrics
- Loan statistics and trends
- User activity reports
- Book popularity analytics
- Overdue and penalty reports

## 🛠️ Technology Stack (Teknoloji Yığını)

### Frontend Framework
- **React 19**: Latest React with concurrent features
- **React Router 7**: Client-side routing
- **Material-UI 7**: Professional UI components
- **Emotion**: CSS-in-JS styling

### State Management
- **Redux Toolkit 2.8.2**: Modern Redux with simplified boilerplate
- **RTK Query**: Powerful data fetching and caching
- **React Redux 9.2**: React bindings for Redux

### Development Tools
- **Create React App**: Development environment
- **ESLint**: Code linting
- **Web Vitals**: Performance monitoring

### Security & Authentication
- **JWT Tokens**: Secure authentication
- **Session Management**: Persistent user sessions
- **Password Hashing**: Crypto-js encryption
- **Role-based Authorization**: Granular access control

## 📁 Project Structure (Proje Yapısı)

```
src/
├── components/          # Reusable UI components
│   ├── common/         # Common components (Header, Footer, etc.)
│   ├── forms/          # Form components
│   └── tables/         # Table components
├── pages/              # Page components
│   ├── Auth/           # Authentication pages
│   ├── Dashboard/      # Admin dashboard
│   ├── Books/          # Book management
│   ├── Users/          # User management
│   ├── Loans/          # Loan management
│   └── Profile/        # User profile
├── store/              # Redux store configuration
│   ├── api/            # RTK Query API slices
│   │   ├── apiSlice.js # Base API configuration
│   │   ├── authApi.js  # Authentication API
│   │   ├── booksApi.js # Books API
│   │   ├── usersApi.js # Users API
│   │   ├── loansApi.js # Loans API
│   │   ├── rolesApi.js # Roles API
│   │   ├── statsApi.js # Statistics API
│   │   └── cezasApi.js # Penalties API
│   ├── slices/         # Redux slices
│   │   └── authSlice.js # Authentication state
│   └── store.js        # Store configuration
├── utils/              # Utility functions
│   ├── hash.js         # Password hashing
│   └── helpers.js      # Helper functions
├── styles/             # Global styles
└── assets/             # Static assets
```

## 🚀 Getting Started (Başlangıç)

### Prerequisites (Ön Gereksinimler)
- Node.js 18+ 
- npm or yarn package manager

### Installation (Kurulum)

1. **Clone the repository**
```bash
git clone <repository-url>
cd Website
```

2. **Install dependencies**
```bash
npm install
```

3. **Configure environment variables**
Create a `.env` file in the root directory:
```env
REACT_APP_API_URL=your_api_url_here
REACT_APP_ENV=development
```

4. **Start development server**
```bash
npm start
```

The application will be available at `http://localhost:3000`

## 🔧 Configuration (Yapılandırma)

### API Configuration
Update the API URL in `src/store/api/apiSlice.js`:
```javascript
baseUrl: 'YOUR_API_URL_HERE',
```

### Authentication
The application uses Basic Authentication with session management:
- Username: `sbuhs`
- Password: `sekerstajekip`
- Session ID is stored in localStorage

### Environment Variables
- `REACT_APP_API_URL`: Backend API URL
- `REACT_APP_ENV`: Environment (development/production)

## 📱 Features by User Role (Kullanıcı Rolüne Göre Özellikler)

### 👑 Admin
- Complete system access
- User management and role assignment
- System statistics and reports
- Book and category management
- Loan oversight and management

### 📚 Librarian/Staff
- Book management (add, edit, delete)
- Loan operations (create, return, extend)
- User registration and management
- Basic reporting and statistics
- Stock management

### 👤 Member
- Browse and search books
- View personal loan history
- Update profile information
- View loan status and due dates
- Request book availability

## 🔐 Security Features (Güvenlik Özellikleri)

### Authentication & Authorization
- JWT-based authentication
- Role-based access control (RBAC)
- Session management with persistence
- Secure password handling with crypto-js

### Data Protection
- Input validation and sanitization
- XSS protection
- CSRF protection
- Secure API communication

### Session Management
- Automatic session refresh
- Secure logout functionality
- Session timeout handling
- Persistent login state

## 📊 Performance Optimizations (Performans Optimizasyonları)

### RTK Query Benefits
- **Automatic Caching**: Intelligent data caching
- **Background Updates**: Real-time data synchronization
- **Optimistic Updates**: Immediate UI feedback
- **Request Deduplication**: Efficient API calls

### React Optimizations
- **Code Splitting**: Lazy loading of components
- **Memoization**: React.memo and useMemo usage
- **Bundle Optimization**: Tree shaking and minification
- **Image Optimization**: Compressed assets

### State Management
- **Normalized State**: Efficient data structure
- **Selective Re-renders**: Optimized component updates
- **Memory Management**: Proper cleanup and garbage collection

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

### Test Structure
- Unit tests for components
- Integration tests for API calls
- Redux store tests
- Utility function tests

## 📈 Monitoring & Analytics (İzleme ve Analitik)

### Web Vitals
- Core Web Vitals monitoring
- Performance metrics tracking
- User experience analytics

### Error Tracking
- Comprehensive error logging
- User feedback collection
- Performance monitoring

## 🚀 Deployment (Dağıtım)

### Production Build
```bash
npm run build
```

### Build Output
The build process creates optimized files in the `build/` directory:
- Minified JavaScript bundles
- Optimized CSS
- Compressed assets
- Service worker for PWA

### Deployment Options
- **Netlify**: Drag and drop deployment
- **Vercel**: Git-based deployment
- **AWS S3**: Static hosting
- **Azure Static Web Apps**: Microsoft cloud hosting

## 🔄 API Integration (API Entegrasyonu)

### Available Endpoints
- **Authentication**: `/auth/login`, `/auth/logout`
- **Users**: `/users`, `/users/{id}`
- **Books**: `/books`, `/books/{id}`
- **Loans**: `/loans`, `/loans/{id}`
- **Roles**: `/roles`, `/roles/{id}`
- **Statistics**: `/stats/dashboard`, `/stats/loans`

### API Features
- **Real-time Updates**: WebSocket-like behavior with RTK Query
- **Error Handling**: Comprehensive error management
- **Retry Logic**: Automatic retry on network failures
- **Cache Management**: Intelligent data caching

## 🤝 Contributing (Katkıda Bulunma)

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Development Guidelines
- Follow React best practices
- Use TypeScript for new components
- Write unit tests for new features
- Follow the existing code style
- Update documentation as needed

## 📄 License (Lisans)

This project is licensed under the MIT License - see the [LICENSE](../LICENSE) file for details.

## 📞 Support (Destek)

For support and questions:
- Create an issue in the repository
- Contact the development team
- Check the documentation

## 🔄 Version History (Sürüm Geçmişi)

### v1.0.0 (Current)
- Initial release with full RBAC implementation
- Complete book and loan management
- Advanced user management system
- Real-time dashboard and analytics
- Modern Material-UI interface