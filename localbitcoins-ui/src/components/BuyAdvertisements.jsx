import PropTypes from 'prop-types'
import AdvertisementsGrid from './AdvertisementsGrid'
import Container from 'react-bootstrap/Container'
import { connect } from 'react-redux'
import React from 'react'
import { useGetBuyAdsQuery } from '../services/localBitcoinsApiService'
import { useNavigate } from 'react-router'
import BuySellButton from './BuySellButton'
import ContentHeader from './ContentHeader'
import ContentBody from './ContentBody'

const BuyAdvertisements = ({ }) => {
    const navigate = useNavigate();
    const onBuySellClick = () => {
        navigate(`/ads/sell`);
    }
    const { data: advertisements, isLoading: isLoadingBuyAds, isFetching: isFetchingBuyAds, refetch: refetchBuyAds } = useGetBuyAdsQuery({ countryCode: 'cr' })
    const isLoading = isLoadingBuyAds || isFetchingBuyAds
    const refresh = () => {
        refetchBuyAds()
    }
    return (
        <Container className='pt-2'>
            <ContentHeader title={'Sell Ads'} isLoading={isLoading} onRefreshClick={refresh}>
                <BuySellButton isBuy={false} onClick={onBuySellClick} />
            </ContentHeader>
            <ContentBody isLoading={isLoading}>
                <div>
                    <AdvertisementsGrid advertisements={advertisements?.ads?.items ?? []} isBuy={true} />
                </div>
            </ContentBody>
        </Container>
    )
}

BuyAdvertisements.defaultProps = {
}

BuyAdvertisements.propTypes = {
}

const mapStateToProps = state => ({
});

export default connect(mapStateToProps, {})(BuyAdvertisements);