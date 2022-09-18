import PropTypes from 'prop-types'
import { Table } from 'react-bootstrap'

const Trades = ({ trades }) => {
    return (
        <Table striped bordered hover variant="dark">
            <thead>
                <tr>
                    <th>TID</th>
                    <th>Date</th>
                    <th>Price</th>
                    <th>Amount BTC</th>
                    <th>Amount Fiat</th>
                    <th>Currency</th>
                </tr>
            </thead>
            <tbody>
                {
                    trades.map((trade) => (
                        <tr>
                            <td>{ trade.tid }</td>
                            <td>{ trade.date }</td>
                            <td>{ trade.price }</td>
                            <td>{ trade.amount_btc }</td>
                            <td>{ trade.amount_fiat }</td>
                            <td>{ trade.currency_code }</td>
                        </tr>
                    ))
                }
            </tbody>
        </Table>
    )
}

Trades.defaultProps = {
    trades: [{
        tid: "123",
        date: "2022-09-09",
        price: 123.45,
        amount_btc: 0.005,
        amount_fiat: 20000,
        currency_code: "CRC"
    },
    {
        tid: "345",
        date: "2022-09-11",
        price: 145.95,
        amount_btc: 0.0048,
        amount_fiat: 23000,
        currency_code: "CRC"
    }]
}

Trades.propTypes = {
    trades: PropTypes.any.isRequired
}

export default Trades