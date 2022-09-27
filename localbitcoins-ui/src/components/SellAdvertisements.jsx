import PropTypes from 'prop-types'
import AdvertisementsGrid from './AdvertisementsGrid'
import Container from 'react-bootstrap/Container'
import { connect } from 'react-redux'
import React from 'react'
import { useGetSellAdsQuery } from '../services/localBitcoinsApiService'
import { useNavigate } from 'react-router'
import BuySellButton from './BuySellButton'
import ContentHeader from './ContentHeader'
import ContentBody from './ContentBody'

const SellAdvertisements = ({ }) => {
    const navigate = useNavigate();
    const onBuySellClick = () => {
        navigate(`/ads/buy`);
    }
    const { data: advertisements, isLoading: isLoadingBuyAds, isFetching: isFetchingBuyAds, refetch: refetchBuyAds } = useGetSellAdsQuery({ countryCode: 'cr' })
    const isLoading = isLoadingBuyAds || isFetchingBuyAds
    const refresh = () => {
        refetchBuyAds()
    }
    return (
        <Container className='pt-2'>
            <ContentHeader title={'Buy Ads'} isLoading={isLoading} onRefreshClick={refresh}>
                <BuySellButton isBuy={true} onClick={onBuySellClick} />
            </ContentHeader>
            <ContentBody isLoading={isLoading}>
                <div>
                    <AdvertisementsGrid advertisements={advertisements?.ads?.items ?? []} isBuy={false} />
                </div>
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