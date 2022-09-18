import PropTypes from 'prop-types';

const TradeRow = ({ trade }) => {
    return (
        <tr>
            <td>{ trade.tid }</td>
            <td>{ trade.date }</td>
            <td>{ trade.price }</td>
            <td>{ trade.amount_btc }</td>
            <td>{ trade.amount_fiat }</td>
            <td>{ trade.currency_code }</td>
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