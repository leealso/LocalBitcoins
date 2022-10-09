import PropTypes from 'prop-types'
import dayjs from 'dayjs'
import { formatNumber, getTradeTypeClass } from '../stringUtility'

const TradeRow = ({ trade }) => {
    const className = getTradeTypeClass(trade.closedTrade?.tradeType)
    return (
        <tr className={className}>
            <td className='d-none d-md-table-cell'>{ trade.transactionId }</td>
            <td className='d-md-none'>{ dayjs(trade.date).format('HH:mm') }</td>
            <td className='d-none d-md-table-cell'>{ dayjs(trade.date).format('MMM DD, YYYY HH:mm') }</td>
            <td className='d-md-none text-end'>
                { formatNumber(trade.amountFiat, '₡', 0) }
            </td>
            <td className='d-none d-md-table-cell text-end'>
                { formatNumber(trade.amountFiat, '₡') }
            </td>
            <td className='text-end'>
                { formatNumber(trade.amountBtc, '', 8, false) }
            </td>
            <td className='d-md-none text-end'>
                { formatNumber(trade.price, '₡', 0) }
            </td>
            <td className='d-none d-md-table-cell text-end'>
                { formatNumber(trade.price, '₡') }
            </td>
        </tr>
    )
}

TradeRow.defaultProps = {
    trade: {}
}

TradeRow.propTypes = {
    trade: PropTypes.any.isRequired
}

export default TradeRow;