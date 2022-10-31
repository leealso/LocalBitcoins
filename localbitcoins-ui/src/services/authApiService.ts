import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { AuthState } from '../store/reducers/authSlice'

export const authApi = createApi({
    baseQuery: fetchBaseQuery({
        baseUrl: '/'
    }),
    reducerPath: 'authApi',
    endpoints: (builder) => ({
        getToken: builder.query<AuthState[], void>({
            query: () => `.auth/me`
        }),
    }),
})

export const { useGetTokenQuery } = authApi
