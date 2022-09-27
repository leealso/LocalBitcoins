import PropTypes from 'prop-types'
import AdvertisementsGrid from './AdvertisementsGrid'
import Container from 'react-bootstrap/Container'
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import { connect } from 'react-redux'
import React from 'react'
import { useGetSellAdsQuery } from '../services/localBitcoinsApiService'
import LoadingSpinner from './LoadingSpinner'
import { useNavigate } from 'react-router'
import BuySellButton from './BuySellButton'
import ContentHeader from './ContentHeader'

const SellAdvertisements = ({  }) => {
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
            <Row>
                <Col>
                    {
                        isLoading
                            ? <LoadingSpinner isLoading={isLoading} />
                            : <div>
                                <AdvertisementsGrid advertisements={advertisements?.ads?.items ?? []} isBuy={false} />
                            </div> 
                            
                    }
                </Col>
            </Row>
        </Container>
    )
}

SellAdvertisements.defaultProps = {
}

SellAdvertisements.propTypes = {
}

const mapStateToProps = state => ({
});

export default connect(mapStateToProps, {  })(SellAdvertisements);