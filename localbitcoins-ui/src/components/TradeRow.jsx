import PropTypes from 'prop-types';
import FormattedNumber from './FormattedNumber';
import dayjs from 'dayjs';

const TradeRow = ({ trade }) => {
    return (
        <tr>
            <td className='d-none d-md-table-cell'>{ trade.transactionId }</td>
            <td className='d-md-none'>{ dayjs(trade.date).format('HH:mm') }</td>
            <td className='d-none d-md-table-cell'>{ dayjs(trade.date).format('MMM DD, YYYY HH:mm') }</td>
            <td className='numberText d-md-none'>
                <FormattedNumber text={trade.amountFiat} decimals={0} prefix='₡'/>
            </td>
            <td className='numberText d-none d-md-table-cell'>
                <FormattedNumber text={trade.amountFiat} prefix='₡'/>
            </td>
            <td className='numberText'>
                <FormattedNumber text={trade.amountBtc} decimals={8} thousandSeparator={false}/>
            </td>
            <td className='numberTex d-md-none'>
                <FormattedNumber text={trade.price} decimals={0} prefix='₡'/>
            </td>
            <td className='numberText d-none d-md-table-cell'>
                <FormattedNumber text={trade.price} prefix='₡'/>
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