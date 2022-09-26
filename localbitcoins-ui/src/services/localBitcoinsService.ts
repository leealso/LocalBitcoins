import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { Advertisement } from '../types/advertisement'

export interface BaseResponse<TResponse> {
    data: TResponse
}

export interface GetAdvertisementsResponse {
    ad_list: Advertisement[]
    ad_count: number
}

export const localBitcoins = createApi({
    reducerPath: 'localBitcoins',
    baseQuery: fetchBaseQuery({ baseUrl: 'https://localbitcoins.com/' }),
    endpoints: (builder) => ({
        getBuyAds: builder.query<BaseResponse<GetAdvertisementsResponse>, { countryCode: string, countryName: string }>({
            query: ({ countryCode, countryName }) => `buy-bitcoins-online/${countryCode}/${countryName}/.json`,
        }),
        getSellAds: builder.query<BaseResponse<GetAdvertisementsResponse>, { countryCode: string, countryName: string }>({
            query: ({ countryCode, countryName }) => `sell-bitcoins-online/${countryCode}/${countryName}/.json`,
        }),
    }),
})

export const { useGetBuyAdsQuery, useGetSellAdsQuery } = localBitcoins
