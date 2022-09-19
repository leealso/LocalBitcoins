import PropTypes from 'prop-types';
import { Table } from 'react-bootstrap';
import TradeRow from './TradeRow';
import { connect } from 'react-redux';
import { fetchTrades } from '../store/actions/tradeActions';
import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';

const TradesGrid = ({ trades }) => {
    const dispatch = useDispatch()
    useEffect(() => {
        dispatch(fetchTrades())
    }, [dispatch])

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

const mapStateToProps = state => ({
    trades: state.trades.trades
});

export default connect(mapStateToProps, { fetchTrades })(TradesGrid);