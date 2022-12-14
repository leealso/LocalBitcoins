import AdvertisementsGrid from './AdvertisementsGrid'
import Container from 'react-bootstrap/Container'
import { connect } from 'react-redux'
import React from 'react'
import { useGetExchangeRateQuery, useGetQuoteQuery, useGetSellAdsQuery } from '../services/localBitcoinsApiService'
import { useNavigate } from 'react-router'
import BuySellButton from './BuySellButton'
import ContentHeader from './ContentHeader'
import ContentBody from './ContentBody'
import SummaryContainer from './SummaryContainer'
import SummaryRow from './SummaryRow'
import { formatNumber } from '../stringUtility'

const SellAdvertisements = ( { refreshAuth }) => {
    const navigate = useNavigate();
    const onBuySellClick = () => {
        navigate(`/ads/buy`);
    }

    let date = new Date()
    date.setHours(0, 0, 0, 0)
    const { data: quote, isLoading: isLoadingQuote, isFetching: isFetchingQuote, refetch: refetchQuote } = useGetQuoteQuery({ symbol: 'BTC' })
    const { data: exchangeRate, isLoading: isLoadingExchangeRate, isFetching: isFetchingExchangeRate, refetch: refetchExchangeRate } = useGetExchangeRateQuery({ date: date.toISOString() })
    const { data: advertisements, isLoading: isLoadingBuyAds, isFetching: isFetchingBuyAds, refetch: refetchBuyAds } = useGetSellAdsQuery({ countryCode: 'cr' })
    
    const btcUsd = quote?.quote?.price
    const usdCrc = exchangeRate?.exchangeRate?.value
    const btcCrc = btcUsd * usdCrc
    const btcCrcPercentChange24h = exchangeRate?.exchangeRate?.percentChange24h 
        + quote?.quote?.percentChange24h / 100 
        + (exchangeRate?.exchangeRate?.percentChange24h * quote?.quote?.percentChange24h / 100)
    const btcUsdFormatted = formatNumber(btcUsd, '$')
    const usdCrcFormatted = formatNumber(usdCrc, '₡')
    const btcCrcFormatted = formatNumber(btcCrc, '₡')

    const isLoading = isLoadingBuyAds || isLoadingQuote || isLoadingExchangeRate
    const isFetching = isLoading || isFetchingBuyAds || isFetchingQuote || isFetchingExchangeRate
    const refresh = () => {
        refreshAuth()
        refetchQuote()
        refetchExchangeRate()
        refetchBuyAds()
    }

    return (
        <Container className='pt-2'>
            <ContentHeader title={'Buy Ads'} isLoading={isFetching} onRefreshClick={refresh}>
                <BuySellButton isBuy={true} onClick={onBuySellClick} />
            </ContentHeader>
            <ContentBody isLoading={isLoading}>
                <SummaryContainer>
                    <SummaryRow label={'BTC USD'} value={btcUsdFormatted} reference={quote?.quote?.percentChange24h / 100} />
                    <SummaryRow label={'BTC CRC'} value={btcCrcFormatted} reference={btcCrcPercentChange24h} />
                    <SummaryRow label={'USD CRC'} value={usdCrcFormatted} reference={exchangeRate?.exchangeRate?.percentChange24h} />
                </SummaryContainer>
                <AdvertisementsGrid advertisements={advertisements?.ads?.items ?? []} isBuy={false} btcPrice={quote?.quote?.price} />
            </ContentBody>
        </Container>
    )
}

SellAdvertisements.defaultProps = {
}

SellAdvertisements.propTypes = {
}

const mapStateToProps = state => ({
});

export default connect(mapStateToProps, {})(SellAdvertisements);