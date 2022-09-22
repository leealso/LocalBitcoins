import PropTypes from 'prop-types'
import TradesGrid from './TradesGrid'
import MobileTradesGrid from './MobileTradesGrid'
import DatePickerButton from './DatePickerButton'
import Container from 'react-bootstrap/Container'
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import { connect } from 'react-redux'
import { setSelectedDate } from '../store/reducers/tradeSlice.ts'
import React from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { useGetTradesQuery } from '../services/localBitcoinsService'
import LoadingSpinner from './LoadingSpinner'
import LoadingButton from './LoadingButton'

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
    const { data: trades, isLoading } = useGetTradesQuery({ where: where })
    // 
    return (
        <Container>
            <Row>
                <Col xs="8" sm="10">
                    <h1 className="text-light">Daily Trades</h1>
                </Col>
                <Col xs="4" sm="2">
                    <div className="d-flex h-100 align-items-center">
                        <DatePickerButton date={date} onDateChange={(date) => dispatch(setSelectedDate(date.getTime()))} />
                        <div className="ml-2"><LoadingButton isLoading={isLoading} handleClick={() => {}} /></div>
                    </div>
                </Col>
            </Row>
            <Row>
                <Col>
                    { 
                        isLoading 
                            ? <LoadingSpinner isLoading={isLoading} /> 
                            : <TradesGrid trades={trades?.trades?.nodes ?? []} /> 
                    }
                </Col>
            </Row>
        </Container>
    )
}

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