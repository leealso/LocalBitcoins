export const getTradesQuery = `
    query trades {
        trades {
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
        }
    }`