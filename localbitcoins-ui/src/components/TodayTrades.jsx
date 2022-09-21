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
import { useDispatch, useSelector } from 'react-redux';
import { useGetTradesQuery } from '../services/localBitcoinsService'

const TodayTrades = ({ date }) => {
    const dispatch = useDispatch()
    const selectedDate = useSelector(state => state.trades.selectedDate)
    var startDate = new Date(selectedDate)
    startDate.setHours(0, 0, 0, 0)
    var endDate = new Date(startDate)
    endDate.setTime(endDate.getTime() + (24*60*60*1000))
    const where = {
        and: [
            {
                date: {
                    gte: startDate.toISOString()
                }
            },
            {
                date: {
                    lt: endDate.toISOString()
                }
            }
        ]
        
    }
    const { data: trades } = useGetTradesQuery({ where: where })
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
                    <TradesGrid trades={trades?.trades?.nodes ?? []} />
                </Col>
            </Row>
        </Container>
    )
}
//<DatePicker selected={date} onChange={(date) => dispatch(setSelectedDate(date))} />

TodayTrades.defaultProps = {
    date: new Date().getTime()
}

TodayTrades.propTypes = {
    date: PropTypes.any.isRequired
}

const mapStateToProps = state => ({
    date: state.trades.selectedDate
});

export default connect(mapStateToProps, { setSelectedDate })(TodayTrades);