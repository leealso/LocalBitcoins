import { ClosedTrade } from "./closedTrade"

export class Trade {
    transactionId: number
    amountBtc: number
    amountFiat: number
    price: number
    date: Date
    currencyCode: string
    contactId: number | null
    closedTrade: ClosedTrade
}