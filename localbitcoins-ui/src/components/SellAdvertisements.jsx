import PropTypes from 'prop-types'
import AdvertisementsGrid from './AdvertisementsGrid'
import Container from 'react-bootstrap/Container'
import { connect } from 'react-redux'
import React from 'react'
import { useGetQuoteQuery, useGetSellAdsQuery } from '../services/localBitcoinsApiService'
import { useNavigate } from 'react-router'
import BuySellButton from './BuySellButton'
import ContentHeader from './ContentHeader'
import ContentBody from './ContentBody'
import SummaryContainer from './SummaryContainer'
import SummaryRow from './SummaryRow'

const SellAdvertisements = ({ }) => {
    const navigate = useNavigate();
    const onBuySellClick = () => {
        navigate(`/ads/buy`);
    }
    const { data: quote, isLoading: isLoadingQuote, isFetching: isFetchingQuote, refetch: refetchQuote } = useGetQuoteQuery({ symbol: 'BTC' })
    const { data: advertisements, isLoading: isLoadingBuyAds, isFetching: isFetchingBuyAds, refetch: refetchBuyAds } = useGetSellAdsQuery({ countryCode: 'cr' })
    const price = `${quote?.quote?.price.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, `$1,`)}`

    const isLoading = isLoadingBuyAds || isFetchingBuyAds || isLoadingQuote || isFetchingQuote
    const refresh = () => {
        refetchQuote()
        refetchBuyAds()
    }
    return (
        <Container className='pt-2'>
            <ContentHeader title={'Buy Ads'} isLoading={isLoading} onRefreshClick={refresh}>
                <BuySellButton isBuy={true} onClick={onBuySellClick} />
            </ContentHeader>
            <ContentBody isLoading={isLoading}>
                <SummaryContainer>
                    <SummaryRow label={'BTC Price'} value={`$${price}`} reference={quote?.quote?.percentChange24h / 100} />
                </SummaryContainer>
                <AdvertisementsGrid advertisements={advertisements?.ads?.items ?? []} isBuy={false} />
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