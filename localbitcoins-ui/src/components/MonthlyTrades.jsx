import PropTypes from 'prop-types'
import TradesGrid from './TradesGrid'
import DatePickerButton from './DatePickerButton'
import Container from 'react-bootstrap/Container'
import { connect } from 'react-redux'
import { setSelectedDate } from '../store/reducers/tradeSlice.ts'
import React from 'react'
import { useDispatch } from 'react-redux'
import { useGetSummaryQuery } from '../services/localBitcoinsApiService'
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

    const fiatVolumeFormatted = formatNumber(summary?.response?.fiatVolume, '₡', 2)
    const priceFormatted = formatNumber(summary?.response?.price, '₡', 2)
    const priceReference = summary?.response?.closedPrice / summary?.response?.price
    const isCurrentMonth = selectedDate.getMonth() === new Date().getMonth()
    const refresh = () => {
        if (isCurrentMonth) {
            refetchSummary()
        }
    }
    const isLoading = isLoadingSummary || isFetchingSummary
    return (
        <Container className='pt-2'>
            <ContentHeader title={'Monthly Trades'} isLoading={isLoading} onRefreshClick={refresh}>
                <DatePickerButton date={date} onDateChange={(date) => dispatch(setSelectedDate(date.getTime()))} />
            </ContentHeader>
            <ContentBody isLoading={isLoading}>
                <SummaryContainer>
                    <SummaryRow label={'Transactions'} value={`${summary?.response?.transactionCount}`} reference={summary?.response?.closedTransactionCountPercentage} />
                    <SummaryRow label={'BTC Volume'} value={`${summary?.response?.btcVolume}`} reference={summary?.response?.closedBtcVolumePercentage} />
                    <SummaryRow label={'Fiat Volume'} value={`${fiatVolumeFormatted}`} reference={summary?.response?.closedFiatVolumePercentage} />
                    <SummaryRow label={'Average Price'} value={`${priceFormatted}`} reference={priceReference} />
                </SummaryContainer>
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