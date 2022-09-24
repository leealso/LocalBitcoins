import { createApi } from '@reduxjs/toolkit/query/react'
import { gql } from 'graphql-request'
import { graphqlRequestBaseQuery } from '@rtk-query/graphql-request-base-query'
import { Trade } from '../types/trade'

export const postStatuses = ['draft', 'published', 'pending_review'] as const

export interface BaseResponse<TResponse> {
    data: TResponse
}

export interface PageInfo {
    hasNextPage: boolean
    hasPreviousPage: boolean
    startCursor: string
    endCursor: string
}

export interface GetTradesResponse {
    trades: {
        nodes: Trade[]
        pageInfo: PageInfo
        totalCount: number
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

export const { useGetTradesQuery } = localBitcoinsApi
