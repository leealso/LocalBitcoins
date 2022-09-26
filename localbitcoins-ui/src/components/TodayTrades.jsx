import PropTypes from 'prop-types'
import TradesGrid from './TradesGrid'
import DatePickerButton from './DatePickerButton'
import Container from 'react-bootstrap/Container'
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import { connect } from 'react-redux'
import { setSelectedDate, setSelectedPage } from '../store/reducers/tradeSlice.ts'
import React from 'react'
import { useDispatch } from 'react-redux'
import { useGetDailySummaryQuery, useGetTradesQuery } from '../services/localBitcoinsService'
import LoadingSpinner from './LoadingSpinner'
import LoadingButton from './LoadingButton'
import DailySummaryCard from './DailySummaryCard'

const TodayTrades = ({ date, pageSize, selectedPage }) => {
    const dispatch = useDispatch()
    let startDate = new Date(date)
    startDate.setHours(0, 0, 0, 0)
    let isToday = startDate.toDateString() === new Date().toDateString()
    let endDate = new Date(startDate)
    endDate.setTime(endDate.getTime() + (24 * 60 * 60 * 1000))
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
    const skip = pageSize * (selectedPage - 1)
    const { data: trades, isLoading: isLoadingTrades, isFetching: isFetchingTrades, refetch: refetchTrades } = useGetTradesQuery({ take: pageSize, skip: skip, where: where })
    const { data: dailySummary, isLoading: isLoadingSummary, isFetching: isFetchingSummary, refetch: refetchSummary } = useGetDailySummaryQuery({ date: startDate.toISOString() })
    const refresh = () => {
        refetchTrades()
        refetchSummary()
    }
    const isLoading = isLoadingTrades || isFetchingTrades || isLoadingSummary || isFetchingSummary
    return (
        <Container className='pt-2'>
            <Row>
                <Col xs={6} sm={9}>
                    <h1 className="text-light">Daily Trades</h1>
                </Col>
                <Col xs={6} sm={3}>
                    <div className="d-flex h-100 align-items-center">
                        <DatePickerButton date={date} onDateChange={(date) => dispatch(setSelectedDate(date.getTime()))} />
                        <LoadingButton isLoading={isLoading} handleClick={() => isToday ? refresh() : null} />
                    </div>
                </Col>
            </Row>
            <Row>
                <Col>
                    {
                        isLoading
                            ? <LoadingSpinner isLoading={isLoading} />
                            : <div>
                                <DailySummaryCard totalCount={dailySummary?.dailySummary?.transactionCount} btcVolume={dailySummary?.dailySummary?.btcVolume} fiatVolume={dailySummary?.dailySummary?.fiatVolume} 
                                     totalCountPercentage={dailySummary?.dailySummary?.transactionCountPercentage} btcVolumePercentage={dailySummary?.dailySummary?.btcVolumePercentage} fiatVolumePercentage={dailySummary?.dailySummary?.fiatVolumePercentage} 
                                />
                                <TradesGrid trades={trades?.trades?.items ?? []} totalCount={trades?.trades?.totalCount} pageSize={pageSize} selectedPage={selectedPage} onPageClick={(page) => dispatch(setSelectedPage(page))} />
                            </div> 
                            
                    }
                </Col>
            </Row>
        </Container>
    )
}

TodayTrades.defaultProps = {
    date: new Date().getTime(),
    pageSize: 0,
    selectedPage: 1
}

TodayTrades.propTypes = {
    date: PropTypes.any.isRequired,
    pageSize: PropTypes.number.isRequired,
    selectedPage: PropTypes.number.isRequired
}

const mapStateToProps = state => ({
    date: state.trades.selectedDate,
    pageSize: state.trades.pageSize,
    selectedPage: state.trades.selectedPage
});

export default connect(mapStateToProps, { setSelectedDate, setSelectedPage })(TodayTrades);