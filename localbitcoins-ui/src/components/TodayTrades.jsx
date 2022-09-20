import PropTypes from 'prop-types';
import IndicatorCard from './IndicatorCard';
import TradesGrid from './TradesGrid';
import DatePickerButton from './DatePickerButton';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { connect } from 'react-redux';
import { setSelectedDate } from '../store/reducers/tradeSlice.ts';
import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { useGetTradesQuery } from '../services/localBitcoinsService'

const TodayTrades = ({ date, trades }) => {
    const dispatch = useDispatch()
    const { data: posts } = useGetTradesQuery({})

    useEffect(() => {
        //dispatch(fetchTrades())
    }, [dispatch]);

    return (
        <Container>
            <Row>
                <Col>
                    <h1 className="text-light">Daily Trades</h1>
                </Col>
                <Col lg="2">
                    <DatePickerButton date={date} onDateChange={(date) => dispatch(setSelectedDate(date.getTime()))} />
                </Col>
            </Row>
            <Row>
                <Col>
                    <TradesGrid trades={trades} />
                </Col>
            </Row>
        </Container>
    )
}
//<DatePicker selected={date} onChange={(date) => dispatch(setSelectedDate(date))} />

TodayTrades.defaultProps = {
    date: new Date().getTime(),
    trades: []
}

TodayTrades.propTypes = {
    date: PropTypes.any.isRequired,
    trades: PropTypes.array.isRequired,
}

const mapStateToProps = state => ({
    date: state.trades.selectedDate,
    trades: state.trades.trades
});

export default connect(mapStateToProps, { setSelectedDate })(TodayTrades);