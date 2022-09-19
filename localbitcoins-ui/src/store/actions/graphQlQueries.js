export const getTradesQuery = `
    query trades {
        trades(first: 25 order: {
            date: DESC
        }) {
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