import PropTypes from 'prop-types'
import { AreaChart, Area, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer } from 'recharts';
import DatePickerButton from './DatePickerButton'
import Container from 'react-bootstrap/Container'
import { connect } from 'react-redux'
import { setSelectedDate } from '../store/reducers/tradeSlice.ts'
import React from 'react'
import { useDispatch } from 'react-redux'
import { useGetDailySummariesQuery, useGetSummaryQuery } from '../services/localBitcoinsApiService'
import ContentHeader from './ContentHeader'
import ContentBody from './ContentBody'
import SummaryContainer from './SummaryContainer'
import SummaryRow from './SummaryRow'
import { formatNumber } from '../stringUtility'

const MonthlyTrades = ({ date }) => {
    const selectedDate = new Date(date)
    const startDate = new Date(selectedDate.getFullYear(), selectedDate.getMonth(), 1, 0, 0, 0);
    const endDate = new Date(selectedDate.getFullYear(), selectedDate.getMonth() + 1, 0, 0, 0, 0);

    const dispatch = useDispatch()

    const { data: summary, isLoading: isLoadingSummary, isFetching: isFetchingSummary, refetch: refetchSummary } = useGetSummaryQuery({ startDate: startDate.toISOString(), endDate: endDate.toISOString() })
    const { data: dailySummary, isLoading: isLoadingDailySummary, isFetching: isFetchingDailySummary, refetch: refetchDailySummary } = useGetDailySummariesQuery({ startDate: startDate.toISOString(), endDate: endDate.toISOString() })

    const fiatVolumeFormatted = formatNumber(summary?.response?.fiatVolume, '₡', 2)
    const priceFormatted = formatNumber(summary?.response?.price, '₡', 2)
    const priceReference = 1 - summary?.response?.closedPrice / summary?.response?.price
    const isCurrentMonth = selectedDate.getMonth() === new Date().getMonth()

    let btcVolumeData = []
    let transactionCountData = []
    dailySummary?.response.forEach(x => {
        const date = new Date(x.startDate).getDate()
        btcVolumeData.push({
            day: date,
            btcVolume: x.btcVolume - x.closedBtcVolume,
            closedBtcVolume: x.closedBtcVolume
        })
        transactionCountData.push({
            day: date,
            transactionCount: x.transactionCount - x.closedTransactionCount,
            closedTransactionCount: x.closedTransactionCount
        })
    })

    const toPercent = (decimal) => decimal ? `${(decimal * 100)}%` : '';

    const refresh = () => {
        if (isCurrentMonth) {
            refetchSummary()
            refetchDailySummary()
        }
    }
    const isLoading = isLoadingSummary || isFetchingSummary || isLoadingDailySummary || isFetchingDailySummary
    return (
        <Container className='pt-2'>
            <ContentHeader title={'Monthly Trades'} isLoading={isLoading} onRefreshClick={refresh}>
                <DatePickerButton date={date} dateFormat='MM/yyyy' showFullMonthYearPicker={true} showMonthYearPicker={true} onDateChange={(date) => dispatch(setSelectedDate(date.getTime()))} />
            </ContentHeader>
            <ContentBody isLoading={isLoading}>
                <SummaryContainer>
                    <SummaryRow label={'Transactions'} value={`${summary?.response?.transactionCount}`} reference={summary?.response?.closedTransactionCountPercentage} />
                    <SummaryRow label={'BTC Volume'} value={`${summary?.response?.btcVolume}`} reference={summary?.response?.closedBtcVolumePercentage} />
                    <SummaryRow label={'Fiat Volume'} value={`${fiatVolumeFormatted}`} reference={summary?.response?.closedFiatVolumePercentage} />
                    <SummaryRow label={'Average Price'} value={`${priceFormatted}`} reference={priceReference} />
                </SummaryContainer>
                <h5 className="text-light text-center">BTC Volume</h5>
                <ResponsiveContainer width="100%" height={400}>
                    <AreaChart
                        data={btcVolumeData}
                        stackOffset="expand"
                    >
                        <CartesianGrid strokeDasharray="3 3" />
                        <XAxis dataKey="day" stroke="#fff" />
                        <YAxis tickFormatter={toPercent} stroke="#fff" />
                        <Area type="monotone" dataKey="closedBtcVolume" stackId="1" stroke="#62c462" fill="#62c462" />
                        <Area type="monotone" dataKey="btcVolume" stackId="1" stroke="#ee5f5b" fill="#ee5f5b" />
                    </AreaChart>
                </ResponsiveContainer>
                <h5 className="text-light text-center">Transactions</h5>
                <ResponsiveContainer width="100%" height={400}>
                    <AreaChart
                        data={transactionCountData}
                        stackOffset="expand"
                    >
                        <CartesianGrid strokeDasharray="3 3" />
                        <XAxis dataKey="day" stroke="#fff" />
                        <YAxis tickFormatter={toPercent} stroke="#fff" />
                        <Area type="monotone" dataKey="closedTransactionCount" stackId="1" stroke="#62c462" fill="#62c462" />
                        <Area type="monotone" dataKey="transactionCount" stackId="1" stroke="#ee5f5b" fill="#ee5f5b" />
                    </AreaChart>
                </ResponsiveContainer>
            </ContentBody>
        </Container>
    )
}

MonthlyTrades.defaultProps = {
    date: new Date().getTime()
}

MonthlyTrades.propTypes = {
    date: PropTypes.any.isRequired
}

const mapStateToProps = state => ({
    date: state.trades.selectedDate
});

export default connect(mapStateToProps, { setSelectedDate })(MonthlyTrades);