import PropTypes from 'prop-types'
import { Chart } from 'react-google-charts'
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
import { formatDate, formatNumber } from '../stringUtility'

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

    let data = [
        ['Transactions', 'Total', 'Closed']
    ]
    let dataBtcVolume = [
        ['BTC Volume', 'Total', 'Closed']
    ]
    dailySummary?.response.forEach(x => {
        const formattedDate = formatDate(new Date(x.startDate))
        data.push([formattedDate, x.transactionCount, x.closedTransactionCount])
        dataBtcVolume.push([formattedDate, x.btcVolume, x.closedBtcVolume])
    })
    const options = {
        title: 'Transactions',
        backgroundColor: 'transparent',
        colors: ['#ee5f5b','#62c462'],
        legend: 'none',
        titleTextStyle: {
            color: "#e9ecef",
            fontSize: 20
        },
        vAxsis: {
            titleTextStyle: {
                color: "#e9ecef",
            }
        },
        hAxsis: {
            titleTextStyle: {
                color: "#e9ecef",
            }
        }
    }
    const optionsBtcVolume = {
        title: 'BTC Volume',
        backgroundColor: 'transparent',
        colors: ['#ee5f5b','#62c462'],
        legend: 'none',
        titleTextStyle: {
            color: "#e9ecef",
            fontSize: 20
        },
        vAxsis: {
            titleTextStyle: {
                color: "#e9ecef",
            }
        },
        hAxsis: {
            titleTextStyle: {
                color: "#e9ecef",
            }
        }
    }
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
                <Chart
                    chartType='AreaChart'
                    data={data}
                    options={options}
                    width={'100%'}
                />
                <Chart
                    chartType='AreaChart'
                    data={dataBtcVolume}
                    options={optionsBtcVolume}
                    width={'100%'}
                    className='text-light'
                />
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