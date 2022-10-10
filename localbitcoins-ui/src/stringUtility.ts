import { Trade } from "./types/trade"

export function truncate(text: string, maxLength: number = 10) {
    return text.substring(0, maxLength)
}

export function formatNumber(amount: number, symbol: string = '', decimals: number = 2, thousandSeparator: boolean = true) {
    if (amount === undefined || amount == null)
        return
    let formattedAmount = amount.toFixed(decimals) 
    if (thousandSeparator)
        formattedAmount = formattedAmount.replace(/(\d)(?=(\d{3})+(?!\d))/g, `$1,`)
    return `${symbol}${formattedAmount}`
}

export function formatDate(date: Date, locale: string = 'en-US') {
    return date.toLocaleDateString(locale, {year: 'numeric', month: '2-digit', day: '2-digit'})
}

export function getTradeTypeClass(tradeType: string) {
    if (tradeType === 'BUY')
        return 'text-success'
    if (tradeType === 'SELL')
        return 'text-danger'
    return ''
}