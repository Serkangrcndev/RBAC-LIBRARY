# Library Management System (Kütüphane Yönetim Sistemi)

[![React](https://img.shields.io/badge/React-19.1.0-blue.svg)](https://reactjs.org/)
[![Redux Toolkit](https://img.shields.io/badge/Redux%20Toolkit-2.8.2-purple.svg)](https://redux-toolkit.js.org/)
[![RTK Query](https://img.shields.io/badge/RTK%20Query-2.8.2-green.svg)](https://redux-toolkit.js.org/rtk-query/overview)
[![Material-UI](https://img.shields.io/badge/Material--UI-7.2.0-blue.svg)](https://mui.com/)
[![.NET 8](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/)
[![React Native](https://img.shields.io/badge/React%20Native-0.79.5-blue.svg)](https://reactnative.dev/)

## 📚 Project Overview (Proje Genel Bakış)

This is a comprehensive **Library Management System** built with modern technologies across three platforms:

Bu, üç farklı platformda modern teknolojilerle geliştirilmiş kapsamlı bir **Kütüphane Yönetim Sistemi**dir:

### 🖥️ Desktop Application (Masaüstü Uygulaması)
- **Technology**: .NET 8 Windows Forms
- **Database**: SQL Server with Entity Framework
- **Features**: Complete library management with RBAC system

### 🌐 Web Application (Web Uygulaması)
- **Technology**: React 19 + Redux Toolkit + RTK Query
- **UI Framework**: Material-UI (MUI)
- **State Management**: Advanced Redux setup with RTK Query for API calls
- **Features**: Modern web interface with role-based access control

### 📱 Mobile Application (Mobil Uygulama)
- **Technology**: React Native + Expo
- **Navigation**: React Navigation with bottom tabs
- **Features**: Cross-platform mobile experience

---

## 🚀 Key Features (Temel Özellikler)

### 🔐 RBAC (Role-Based Access Control) System
- **Admin**: Full system access and user management
- **Librarian/Staff**: Book management and loan operations
- **Member**: Basic book browsing and personal loan history

### 📖 Book Management
- Add, edit, delete books
- Stock management
- Book search and filtering
- Category management

### 🔄 Loan System
- 30-day loan period
- Automatic overdue detection
- Penalty system for late returns
- Stock tracking

### 👥 User Management
- User registration and authentication
- Role assignment
- Profile management
- Session management

### 📊 Statistics & Reports
- Loan statistics
- User activity reports
- Book popularity metrics
- Overdue reports

---

## 🛠️ Technology Stack (Teknoloji Yığını)

### Frontend Technologies
- **React 19**: Latest React with concurrent features
- **Redux Toolkit**: Modern Redux with simplified boilerplate
- **RTK Query**: Powerful data fetching and caching
- **Material-UI**: Professional UI components
- **React Router**: Client-side routing

### Backend Technologies
- **.NET 8**: Latest .NET framework
- **Windows Forms**: Desktop application framework
- **SQL Server**: Relational database
- **Entity Framework**: ORM for database operations

### Mobile Technologies
- **React Native**: Cross-platform mobile development
- **Expo**: Development platform and tools
- **React Navigation**: Mobile navigation solution

### Security & Authentication
- **JWT Tokens**: Secure authentication
- **Session Management**: Persistent user sessions
- **Password Hashing**: BCrypt encryption
- **Role-based Authorization**: Granular access control

---

## 📁 Project Structure (Proje Yapısı)

```
📦 Library Management System
├── 🖥️ Desktop Application/
│   └── Seker_kutuphane/
│       ├── Forms/           # Windows Forms
│       ├── Models/          # Data models
│       ├── Services/        # Business logic
│       └── DatabaseHelper.cs
├── 🌐 Website/
│   ├── src/
│   │   ├── components/      # Reusable components
│   │   ├── pages/          # Page components
│   │   ├── store/          # Redux store
│   │   │   ├── api/        # RTK Query APIs
│   │   │   └── slices/     # Redux slices
│   │   ├── utils/          # Utility functions
│   │   └── styles/         # CSS styles
│   └── public/             # Static assets
└── 📱 Mobile Application/
    └── kutuphane-mobile/
        ├── app/            # Screen components
        ├── components/     # Reusable components
        ├── store/          # Redux store
        ├── services/       # API services
        └── constants/      # App constants
```

---
### Prerequisites
- Node.js 18+ 
- .NET 8 SDK
- SQL Server
- Expo CLI (for mobile development)

### Installation (Kurulum)

#### 1. Web Application
```bash
cd Website
npm install
npm start
```

#### 2. Desktop Application
```bash
cd "Desktop Application/Seker_kutuphane"
dotnet restore
dotnet run
```

#### 3. Mobile Application
```bash
cd "Mobile Application/kutuphane-mobile"
npm install
npx expo start
```

---

## 🔧 Configuration

### Database Connection
Update the connection string in `DatabaseHelper.cs` for desktop application.

### API Configuration
Set your API URL in the web application's `apiSlice.js`.

### Environment Variables
Create `.env` files for sensitive configuration data.

---

## 📱 Features by Platform

### Desktop Application
- ✅ Complete CRUD operations
- ✅ Advanced search and filtering
- ✅ Report generation
- ✅ Bulk operations
- ✅ Offline capability

### Web Application
- ✅ Responsive design (Responsive)
- ✅ Real-time updates
- ✅ Advanced state management
- ✅ Progressive Web App (PWA)
- ✅ Modern UI/UX

### Mobile Application
- ✅ Cross-platform compatibility
- ✅ Offline-first approach
- ✅ Push notifications
- ✅ Touch-optimized interface
- ✅ Native performance

---

## 🔐 Security Features

- **Authentication**: JWT-based authentication
- **Authorization**: Role-based access control (RBAC)
- **Data Encryption**: Password hashing with SHA-256
- **Session Management**: Secure session handling
- **Input Validation**: Comprehensive input sanitization
- **SQL Injection Protection**: Parameterized queries

---

## 📊 Performance Optimizations

- **RTK Query Caching**: Intelligent data caching
- **Lazy Loading**: Component and route lazy loading
- **Code Splitting**: Bundle optimization
- **Image Optimization**: Compressed assets
- **Database Indexing**: Optimized queries

---

## 🧪 Testing

```bash
# Web Application Tests
cd Website
npm test

# Desktop Application Tests
cd "Desktop Application/Seker_kutuphane"
dotnet test
```

---

## 📈 Monitoring & Analytics

- **Error Tracking**: Comprehensive error logging
- **Performance Monitoring**: Application performance metrics
- **User Analytics**: Usage statistics and patterns
- **System Health**: Database and API health checks

---

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

---

## 📄 License (Lisans)

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 🔄 Version History (Sürüm Geçmişi)

### v1.0.0 (Current)
- Initial release with all three platforms
- Complete RBAC implementation
- Advanced loan management system
- Modern UI/UX across all platforms

Developed By Serkan Gürcan , Burak Saka , Halil Malatyalı , Semiha Bahçebaşı , Umut Aydın.

## IMAGE

<details>
  <summary>Desktop Application Image</summary>
<img width="802" height="756" alt="image" src="https://github.com/user-attachments/assets/5fe75a2a-c2bb-4828-aaef-098242e51416" />
<img width="853" height="503" alt="image" src="https://github.com/user-attachments/assets/68810444-a025-41eb-a8e4-e63f06837466" />
<img width="1001" height="700" alt="image" src="https://github.com/user-attachments/assets/89e1bd59-e0b9-45d4-9fe5-9d2b1baafea6" />
<img width="899" height="651" alt="image" src="https://github.com/user-attachments/assets/76ba4cd1-3149-4630-b44d-067bc288257a" />
<img width="598" height="520" alt="image" src="https://github.com/user-attachments/assets/a17597a8-8635-4c8c-89db-a42dcc477412" />
<img width="1197" height="705" alt="image" src="https://github.com/user-attachments/assets/5f367241-e8c5-42f1-81b9-739044848c83" />
<img width="1065" height="630" alt="image" src="https://github.com/user-attachments/assets/6046175a-c02f-40ca-be14-f66d52127f21" />
<img width="1068" height="634" alt="image" src="https://github.com/user-attachments/assets/b8427383-e528-4637-87f1-9d3a169536d7" />
<img width="1069" height="633" alt="image" src="https://github.com/user-attachments/assets/77b44d80-9966-4a32-88f3-25a6ee83775b" />
<img width="716" height="523" alt="image" src="https://github.com/user-attachments/assets/3f7eb362-9584-428a-981b-44d46a4d451c" />
</details>

<details>
  <summary>Website & Mobile Image</summary>
    <img width="1914" height="936" alt="image" src="https://github.com/user-attachments/assets/639e4728-da0b-46e6-8873-9326feed8744" />
    <img width="1915" height="940" alt="image" src="https://github.com/user-attachments/assets/7d59c19a-7dd2-4d23-84f5-0164782146db" />
    <img width="1901" height="940" alt="image" src="https://github.com/user-attachments/assets/e58c9179-c18d-42a7-bb8f-4ac107dc1778" />
    <img width="1915" height="939" alt="image" src="https://github.com/user-attachments/assets/ac4bd9f9-0c07-4d4f-9366-a40cf87ad828" />
    <img width="1911" height="936" alt="image" src="https://github.com/user-attachments/assets/76816a79-ef7d-42a2-a955-305bfa35f8e1" />
    <img width="1913" height="945" alt="image" src="https://github.com/user-attachments/assets/9a592d86-d2a8-4afd-876b-db6717c015d1" />
    <img width="1907" height="931" alt="image" src="https://github.com/user-attachments/assets/7a044b98-bd6c-4916-95d9-6e4015997d08" />
    <img width="1896" height="931" alt="image" src="https://github.com/user-attachments/assets/336746ae-e4d7-439d-85ca-954fafdefde8" />
    <img width="1914" height="942" alt="image" src="https://github.com/user-attachments/assets/ff8f6e09-ed82-4ac0-93cc-63b63846c939" />
    <img width="1917" height="943" alt="image" src="https://github.com/user-attachments/assets/f83a59f8-7285-4a32-89c6-d391722858b7" />
    <img width="1080" height="2400" alt="image" src="https://github.com/user-attachments/assets/b7689bd3-65d3-45ed-a1fc-1e031484897f" />
    <img width="1080" height="2400" alt="image" src="https://github.com/user-attachments/assets/831ed56a-29d4-479c-a818-0469bed4260f" />
    <img width="1080" height="2400" alt="image" src="https://github.com/user-attachments/assets/8a7a1ccd-7c80-49f2-961a-fcca99a401fb" />
    <img width="1080" height="2400" alt="image" src="https://github.com/user-attachments/assets/5fef35b8-1155-4f90-8c8c-13957082f937" />
    <img width="1080" height="2400" alt="image" src="https://github.com/user-attachments/assets/308f946d-708f-49b3-8389-5fbec2604cb9" />
    <img width="1080" height="2400" alt="image" src="https://github.com/user-attachments/assets/ae03aa61-de19-46ef-87fe-315b5f97ad2b" />
    <img width="1080" height="2400" alt="image" src="https://github.com/user-attachments/assets/0f378a3b-26d3-47dc-afba-8f237a34d302" />
    <img width="1080" height="2400" alt="image" src="https://github.com/user-attachments/assets/481a63a6-cbc0-4703-a76c-36517f858429" />
    <img width="1080" height="2400" alt="image" src="https://github.com/user-attachments/assets/ccb71d8b-8fe9-49b6-a734-c68668a9f600" />
