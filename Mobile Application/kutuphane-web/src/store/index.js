// Store exports
export { store } from './store';

// Auth slice
export { 
  authReducer, 
  setCredentials, 
  logout, 
  selectCurrentUser, 
  selectIsAuthenticated 
} from './slices/authSlice';

// API slices
export { apiSlice } from './api/apiSlice';

// Auth API
export {
  useLoginMutation,
  useLoginWithTCMutation,
  useRegisterMutation,
  useVerifyTCMutation,
  useResetPasswordMutation,
  useLogoutMutation,
  useGetCurrentUserQuery,
} from './api/authApi';

// Books API
export {
  useGetAllBooksQuery,
  useGetFeaturedBooksQuery,
  useSearchBooksQuery,
  useGetCategoriesQuery,
  useGetBooksByCategoryQuery,
  useGetBookByIdQuery,
  useAddBookMutation,
  useUpdateBookMutation,
  useDeleteBookMutation,
  useUploadBookImageMutation,
  useGetBookStatsQuery,
  useGetPopularBooksQuery,
  useGetNewBooksQuery,
} from './api/booksApi';

// Users API
export {
  useGetAllUsersQuery,
  useSearchUsersQuery,
  useGetUserByIdQuery,
  useGetUserByTCQuery,
  useGetRolesQuery,
  useCreateUserMutation,
  useUpdateUserMutation,
  useDeleteUserMutation,
} from './api/usersApi';

// Loans API
export {
  useGetAllLoansQuery,
  useSearchLoansQuery,
  useCreateLoanMutation,
  useReturnLoanMutation,
  useUpdateLoanMutation,
  useGetUserActiveLoansQuery,
  useGetBookLoanHistoryQuery,
  useGetOverdueLoansQuery,
  useGetUserLoanStatusQuery,
  useDeleteLoanMutation,
  useCheckUserLoanLimitQuery,
  useCheckBookAvailabilityQuery,
} from './api/loansApi';

// Stats API
export {
  useGetDashboardStatsQuery,
  useGetGeneralStatsQuery,
  useGetReadingStatsQuery,
  useGetMonthlyStatsQuery,
  useGetYearlyStatsQuery,
  useGetTopReadersQuery,
} from './api/statsApi';

// Roles API
export {
  useGetAllRolesQuery,
  useGetRoleByIdQuery,
  useCreateRoleMutation,
  useUpdateRoleMutation,
  useDeleteRoleMutation,
} from './api/rolesApi'; 