import { createApi } from '@reduxjs/toolkit/query/react'
import { gql } from 'graphql-request'
import { graphqlRequestBaseQuery } from '@rtk-query/graphql-request-base-query'
import { Trade } from '../types/trade'
import { Advertisement } from '../types/advertisement'
import { RootState } from '../store'

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

export interface GetSummaryResponse {
    response: Summary
}

export interface GetSummariesResponse {
    response: Summary[]
}

export interface Summary {
    startDate: Date
    endDate: Date
    transactionCount: number
    btcVolume: number
    fiatVolume: number
    price: number
    closedTransactionCount: number
    closedBtcVolume: number
    closedFiatVolume: number
    closedPrice: number
    closedTransactionCountPercentage: number
    closedBtcVolumePercentage: number
    closedFiatVolumePercentage: number
}

export interface GetQuoteResponse {
    quote: Quote
}

export interface Quote {
    symbol: string
    price: number
    percentChange24h: number
    lastUpdated: Date
}

export interface GetExchangeRateResponse {
    exchangeRate: ExchangeRate
}

export interface ExchangeRate {
    date: Date
    value: number
    percentChange24h: number
}

export const localBitcoinsApi = createApi({
    baseQuery: graphqlRequestBaseQuery({
        url: 'https://localbitcoinsapi.azurewebsites.net/graphql',
        prepareHeaders: (headers, { getState }) => {
            const token = (getState() as RootState).auth.access_token
            if (token) {
                headers.set('authentication', `Bearer ${token}`)
            }
            return headers
        }
    }),
    reducerPath: 'localBitcoinsApi',
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
                            contactId
                            closedTrade {
                                tradeType
                            }
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
            BaseResponse<GetSummaryResponse>,
            { date: string }
        >({
            query: ({ date }) => ({
                document: gql`
                query daySummary($date: DateTime!) {
                    response: daySummary(date: $date) {
                        startDate
                        endDate
                        transactionCount
                        btcVolume
                        fiatVolume
                        price
                        closedTransactionCount
                        closedBtcVolume
                        closedFiatVolume
                        closedPrice
                        closedTransactionCountPercentage
                        closedBtcVolumePercentage
                        closedFiatVolumePercentage
                    }
                }`,
                variables: {
                    date
                },
            }),
        }),
        getSummary: builder.query<
            BaseResponse<GetSummaryResponse>,
            { startDate: string, endDate: string }
        >({
            query: ({ startDate, endDate }) => ({
                document: gql`
                query summary($startDate: DateTime! $endDate: DateTime!) {
                    response: summary(startDate: $startDate endDate: $endDate) {
                        startDate
                        endDate
                        transactionCount
                        btcVolume
                        fiatVolume
                        price
                        closedTransactionCount
                        closedBtcVolume
                        closedFiatVolume
                        closedPrice
                        closedTransactionCountPercentage
                        closedBtcVolumePercentage
                        closedFiatVolumePercentage
                    }
                }`,
                variables: {
                    startDate,
                    endDate
                },
            }),
        }),
        getDailySummaries: builder.query<
            BaseResponse<GetSummariesResponse>,
            { startDate: string, endDate: string }
        >({
            query: ({ startDate, endDate }) => ({
                document: gql`
                query query($startDate: DateTime! $endDate: DateTime!) {
                    response: dailySummaries(startDate: $startDate endDate: $endDate order: { startDate: ASC }) {
                        startDate
                        endDate
                        transactionCount
                        btcVolume
                        fiatVolume
                        price
                        closedTransactionCount
                        closedBtcVolume
                        closedFiatVolume
                        closedPrice
                        closedTransactionCountPercentage
                        closedBtcVolumePercentage
                        closedFiatVolumePercentage
                    }
                }`,
                variables: {
                    startDate,
                    endDate
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
                    ads: buyAdvertisements(take: 25 countryCode: $countryCode order: {
                        tempPriceUsd: ASC
                    }) {
                        items {
                            username
                            currency
                            tempPrice
                            tempPriceUsd
                            minAmountAvailable
                            maxAmountAvailable
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
                    ads: sellAdvertisements(take: 25 countryCode: $countryCode order: {
                        tempPriceUsd: DESC
                    }) {
                        items {
                            username
                            currency
                            tempPrice
                            tempPriceUsd
                            minAmountAvailable
                            maxAmountAvailable
                            publicViewUrl
                        }
                    }
                }`,
                variables: {
                    countryCode
                },
            }),
        }),
        getQuote: builder.query<
            BaseResponse<GetQuoteResponse>,
            { symbol: string }
        >({
            query: ({ symbol }) => ({
                document: gql`
                query quote($symbol: String!) {
                    quote(symbol: $symbol) {
                        symbol
                        price
                        percentChange24h
                        lastUpdated
                    }
                }`,
                variables: {
                    symbol
                },
            }),
        }),
        getExchangeRate: builder.query<
            BaseResponse<GetExchangeRateResponse>,
            { date: string }
        >({
            query: ({ date }) => ({
                document: gql`
                query exchangeRate($date: DateTime!) {
                    exchangeRate(date: $date) {
                        value
                        percentChange24h
                    }
                }`,
                variables: {
                    date
                },
            }),
        })
    }),
})

export const { useGetTradesQuery, useGetDailySummaryQuery, useGetSummaryQuery, useGetBuyAdsQuery, useGetSellAdsQuery, useGetQuoteQuery, useGetExchangeRateQuery, useGetDailySummariesQuery } = localBitcoinsApi
