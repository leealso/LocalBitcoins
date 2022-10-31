import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

export interface AuthInfo {
    access_token: string
    id_token: string
    user_id: string
}

export const authApi = createApi({
    baseQuery: fetchBaseQuery({
        baseUrl: '/'
    }),
    reducerPath: 'authApi',
    endpoints: (builder) => ({
        getToken: builder.query<AuthInfo, void>({
            query: () => `.auth/me`
        }),
    }),
})

export const { useGetTokenQuery } = authApi
