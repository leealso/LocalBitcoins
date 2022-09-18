import PropTypes from 'prop-types';
import { Table } from 'react-bootstrap';
import TradeRow from './TradeRow';
import { connect } from 'react-redux';
import { fetchTrades } from '../store/actions/tradeActions';
import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux'

const Trades = ({ trades }) => {
    const dispatch = useDispatch()
    //const usersListData = useSelector(state => state.usersList)
    //const { loading, error, users } = usersListData
    useEffect(() => {
        dispatch(fetchTrades()) 
      }, [dispatch])

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
                    trades.map((trade, i) => (
                        <TradeRow key={i} trade={trade} />
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

export default connect(null, { fetchTrades })(Trades);