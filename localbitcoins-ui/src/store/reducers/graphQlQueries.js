export const getTradesQuery = `
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
    }`