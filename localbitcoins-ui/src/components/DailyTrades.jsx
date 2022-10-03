import PropTypes from 'prop-types'
import TradesGrid from './TradesGrid'
import DatePickerButton from './DatePickerButton'
import Container from 'react-bootstrap/Container'
import { connect } from 'react-redux'
import { setSelectedDate, setSelectedPage } from '../store/reducers/tradeSlice.ts'
import React from 'react'
import { useDispatch } from 'react-redux'
import { useGetDailySummaryQuery, useGetTradesQuery } from '../services/localBitcoinsApiService'
import ContentHeader from './ContentHeader'
import ContentBody from './ContentBody'
import SummaryContainer from './SummaryContainer'
import SummaryRow from './SummaryRow'
import { formatNumber } from '../stringUtility'

const DailyTrades = ({ date, pageSize, selectedPage }) => {
    const dispatch = useDispatch()
    let startDate = new Date(date)
    startDate.setHours(0, 0, 0, 0)
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

    const fiatVolumeFormatted = formatNumber(dailySummary?.dailySummary?.fiatVolume, '₡', 2)
    const isToday = startDate.toDateString() === new Date().toDateString()
    const refresh = () => {
        if (isToday) {
            refetchTrades()
            refetchSummary()
        }
    }
    const isLoading = isLoadingTrades || isFetchingTrades || isLoadingSummary || isFetchingSummary
    return (
        <Container className='pt-2'>
            <ContentHeader title={'Daily Trades'} isLoading={isLoading} onRefreshClick={refresh}>
                <DatePickerButton date={date} onDateChange={(date) => dispatch(setSelectedDate(date.getTime()))} />
            </ContentHeader>
            <ContentBody isLoading={isLoading}>
                <SummaryContainer>
                    <SummaryRow label={'Transactions'} value={`${dailySummary?.dailySummary?.transactionCount}`} reference={dailySummary?.dailySummary?.transactionCountPercentage} />
                    <SummaryRow label={'BTC Volume'} value={`${dailySummary?.dailySummary?.btcVolume}`} reference={dailySummary?.dailySummary?.btcVolumePercentage} />
                    <SummaryRow label={'Fiat Volume'} value={`${fiatVolumeFormatted}`} reference={dailySummary?.dailySummary?.fiatVolumePercentage} />
                </SummaryContainer>
                <TradesGrid trades={trades?.trades?.items ?? []} totalCount={trades?.trades?.totalCount} pageSize={pageSize} selectedPage={selectedPage} onPageClick={(page) => dispatch(setSelectedPage(page))} />
            </ContentBody>
        </Container>
    )
}

DailyTrades.defaultProps = {
    date: new Date().getTime(),
    pageSize: 0,
    selectedPage: 1
}

DailyTrades.propTypes = {
    date: PropTypes.any.isRequired,
    pageSize: PropTypes.number.isRequired,
    selectedPage: PropTypes.number.isRequired
}

const mapStateToProps = state => ({
    date: state.trades.selectedDate,
    pageSize: state.trades.pageSize,
    selectedPage: state.trades.selectedPage
});

export default connect(mapStateToProps, { setSelectedDate, setSelectedPage })(DailyTrades);