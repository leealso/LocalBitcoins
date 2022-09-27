import { createApi } from '@reduxjs/toolkit/query/react'
import { gql } from 'graphql-request'
import { graphqlRequestBaseQuery } from '@rtk-query/graphql-request-base-query'
import { Trade } from '../types/trade'
import { Advertisement } from '../types/advertisement'

export interface BaseResponse<TResponse> {
    data: TResponse
}

export interface PageInfo {
    hasNextPage: boolean
    hasPreviousPage: boolean
}

export interface GetTradesResponse {
    trades: {
        nodes: Trade[]
        pageInfo: PageInfo
        totalCount: number
    }
}

export interface GetAdvertisementsResponse {
    ads: {
        items: Advertisement[]
    }
}

export interface GetDailySummaryResponse {
    dailySummary: {
        date: Date
        transactionCount: number
        btcVolume: number
        fiatVolume: number
        transactionCountPercentage: number
        btcVolumePercentage: number
        fiatVolumePercentage: number
    }
}

export const localBitcoinsApi = createApi({
    baseQuery: graphqlRequestBaseQuery({
        url: 'https://localbitcoinsapi.azurewebsites.net/graphql',
    }),
    endpoints: (builder) => ({
        getTrades: builder.query<
            BaseResponse<GetTradesResponse>,
            { take: number, skip?: number, where?: any }
        >({
            query: ({ take, skip, where }) => ({
                document: gql`
                query trades($take: Int $skip: Int $where: TradeFilterInput) {
                    trades(take: $take skip: $skip order: {
                        date: DESC
                    } where: $where) {
                        items {
                            transactionId
                            amountBtc
                            amountFiat
                            price
                            date
                            currencyCode
                        }
                        totalCount
                    }
                }`,
                variables: {
                    take,
                    skip,
                    where
                },
            }),
        }),
        getDailySummary: builder.query<
            BaseResponse<GetDailySummaryResponse>,
            { date: string }
        >({
            query: ({ date }) => ({
                document: gql`
                query dailySummary($date: DateTime!) {
                    dailySummary(date: $date) {
                        date
                        transactionCount
                        btcVolume
                        fiatVolume
                        transactionCountPercentage
                        btcVolumePercentage
                        fiatVolumePercentage
                    }
                }`,
                variables: {
                    date
                },
            }),
        }),
        getBuyAds: builder.query<
            BaseResponse<GetAdvertisementsResponse>,
            { countryCode: string }
        >({
            query: ({ countryCode }) => ({
                document: gql`
                query buyAdvertisements($countryCode: String!) {
                    ads: buyAdvertisements(countryCode: $countryCode) {
                        items {
                            username
                            currency
                            tempPrice
                            tempPriceUsd
                            publicViewUrl
                        }
                    }
                }`,
                variables: {
                    countryCode
                },
            }),
        }),
        getSellAds: builder.query<
            BaseResponse<GetAdvertisementsResponse>,
            { countryCode: string }
        >({
            query: ({ countryCode }) => ({
                document: gql`
                query sellAdvertisements($countryCode: String!) {
                    ads: sellAdvertisements(countryCode: $countryCode) {
                        items {
                            username
                            currency
                            tempPrice
                            tempPriceUsd
                            publicViewUrl
                        }
                    }
                }`,
                variables: {
                    countryCode
                },
            }),
        })
        /*getPost: builder.query<Post, string>({
            query: (id) => ({
                document: gql`
        query GetPost($id: ID!) {
          post(id: ${id}) {
            id
            title
            body
          }
        }
        `,
            }),
            transformResponse: (response: PostResponse) => response.data.post,
        }),*/
    }),
})

export const { useGetTradesQuery, useGetDailySummaryQuery, useGetBuyAdsQuery, useGetSellAdsQuery } = localBitcoinsApi
