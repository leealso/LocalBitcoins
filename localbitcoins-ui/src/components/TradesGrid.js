import PropTypes from 'prop-types';
import { Table } from 'react-bootstrap';
import TradeRow from './TradeRow';

const TradesGrid = ({ trades }) => {
    return (
        <Table striped bordered hover variant="dark">
            <thead>
                <tr>
                    <th>Transaction ID</th>
                    <th>Date</th>
                    <th>Fiat</th>
                    <th>BTC</th>
                    <th>Price</th>
                </tr>
            </thead>
            <tbody>
                {
                    trades.map((trade, i) => (
                        <TradeRow key={i} trade={trade} />
                    ))
                }
            </tbody>
        </Table>
    )
}

TradesGrid.defaultProps = {
    trades: []
}

TradesGrid.propTypes = {
    trades: PropTypes.any.isRequired
}

export default TradesGrid;