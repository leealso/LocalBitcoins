import PropTypes from 'prop-types'
import TradesGrid from './TradesGrid'
import DatePickerButton from './DatePickerButton'
import Container from 'react-bootstrap/Container'
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import { connect } from 'react-redux'
import { setSelectedDate } from '../store/reducers/tradeSlice.ts'
import React from 'react'
import { useDispatch } from 'react-redux'
import { useGetTradesQuery } from '../services/localBitcoinsService'
import LoadingSpinner from './LoadingSpinner'
import LoadingButton from './LoadingButton'

const TodayTrades = ({ date, pageSize }) => {
    const dispatch = useDispatch()
    let startDate = new Date(date)
    startDate.setHours(0, 0, 0, 0)
    let isToday = startDate.toDateString() === new Date().toDateString()
    let endDate = new Date(startDate)
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
    const { data: trades, isLoading, isFetching, refetch } = useGetTradesQuery({ take: pageSize, skip: where: where })
    return (
        <Container className='pt-2'>
            <Row>
                <Col xs={6} sm={9}>
                    <h1 className="text-light">Daily Trades</h1>
                </Col>
                <Col xs={6} sm={3}>
                    <div className="d-flex h-100 align-items-center">
                        <DatePickerButton date={date} onDateChange={(date) => dispatch(setSelectedDate(date.getTime()))} />
                        <LoadingButton isLoading={isLoading || isFetching} handleClick={() => isToday ? refetch() : null} />
                    </div>
                </Col>
            </Row>
            <Row>
                <Col>
                    { 
                        isLoading 
                            ? <LoadingSpinner isLoading={isLoading || isFetching} /> 
                            : <TradesGrid trades={trades?.trades?.nodes ?? []} totalCount={trades?.trades?.totalCount} pageSize={pageSize} /> 
                    }
                </Col>
            </Row>
        </Container>
    )
}

TodayTrades.defaultProps = {
    date: new Date().getTime(),
    pageSize: 0
}

TodayTrades.propTypes = {
    date: PropTypes.any.isRequired,
    pageSize: PropTypes.number.isRequired
}

const mapStateToProps = state => ({
    date: state.trades.selectedDate,
    pageSize: state.trades.pageSize
});

export default connect(mapStateToProps, { setSelectedDate })(TodayTrades);