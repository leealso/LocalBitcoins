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
            { where?: any }
        >({
            query: ({ where }) => ({
                document: gql`
                query trades($where: TradeFilterInput) {
                    trades(first: 25 order: {
                        date: DESC
                    } where: $where) {
                        nodes {
                            transactionId
                            amountBtc
                            amountFiat
                            price
                            date
                            currencyCode
                        }
                        pageInfo {
                            hasNextPage
                            hasPreviousPage
                        }
                        totalCount
                    }
                }`,
                variables: {
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
