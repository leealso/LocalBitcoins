export function truncate(text: string, maxLength: number = 10) {
    return text.substring(0, maxLength)
}

export function toCurrency(amount: number, symbol: string = '', decimals: number = 2) {
    return `${symbol}${amount.toFixed(decimals).replace(/(\d)(?=(\d{3})+(?!\d))/g, `$1,`)}`
}