import PropTypes from 'prop-types';
import FormattedNumber from './FormattedNumber';
import dayjs from 'dayjs';

const TradeRow = ({ trade }) => {
    return (
        <tr>
            <td>{ trade.transactionId }</td>
            <td>{ dayjs(trade.date).format('MMM DD, YYYY HH:mm') }</td>
            <td className='numberText'>
                <FormattedNumber text={ trade.amountFiat } prefix='₡'/>
            </td>
            <td className='numberText'>
                <FormattedNumber text={ trade.amountBtc } decimals={ 8 } thousandSeparator={ false }/>
            </td>
            <td className='numberText'>
                <FormattedNumber text={ trade.price } prefix='₡'/>
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