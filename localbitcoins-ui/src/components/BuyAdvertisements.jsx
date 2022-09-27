import PropTypes from 'prop-types'
import AdvertisementsGrid from './AdvertisementsGrid'
import Container from 'react-bootstrap/Container'
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import { connect } from 'react-redux'
import React from 'react'
import { useGetBuyAdsQuery, useGetSellAdsQuery } from '../services/localBitcoinsApiService'
import LoadingSpinner from './LoadingSpinner'
import LoadingButton from './LoadingButton'
import { useNavigate, useParams } from 'react-router'
import BuySellButton from './BuySellButton'

const BuyAdvertisements = ({  }) => {
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
            <Row>
                <Col xs={8} sm={10}>
                    <h1 className="text-light">Sell Ads</h1>
                </Col>
                <Col xs={4} sm={2}>
                    <div className="d-flex h-100 align-items-center float-end">
                        <BuySellButton isBuy={false} onClick={onBuySellClick} />
                        <LoadingButton isLoading={isLoading} handleClick={() => refresh() } />
                    </div>
                </Col>
            </Row>
            <Row>
                <Col>
                    {
                        isLoading
                            ? <LoadingSpinner isLoading={isLoading} />
                            : <div>
                                <AdvertisementsGrid advertisements={advertisements?.ads?.items ?? []} isBuy={true} />
                            </div> 
                            
                    }
                </Col>
            </Row>
        </Container>
    )
}

BuyAdvertisements.defaultProps = {
}

BuyAdvertisements.propTypes = {
}

const mapStateToProps = state => ({
});

export default connect(mapStateToProps, {  })(BuyAdvertisements);