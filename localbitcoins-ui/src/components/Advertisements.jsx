import PropTypes from 'prop-types'
import AdvertisementsGrid from './AdvertisementsGrid'
import Container from 'react-bootstrap/Container'
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import { connect } from 'react-redux'
import React from 'react'
import { useDispatch } from 'react-redux'
import { useGetBuyAdsQuery, useGetSellAdsQuery } from '../services/localBitcoinsService'
import LoadingSpinner from './LoadingSpinner'
import LoadingButton from './LoadingButton'

const Advertisements = ({  }) => {
    const dispatch = useDispatch()
    const { data: advertisements, isLoading: isLoadingBuyAds, isFetching: isFetchingBuyAds, refetch: refetchBuyAds } = useGetBuyAdsQuery({ countryCode: 'cr', countryName: 'costa-rica' })
    const isLoading = isLoadingBuyAds || isFetchingBuyAds
    const refresh = () => {
        refetchBuyAds()
    }
    return (
        <Container className='pt-2'>
            <Row>
                <Col xs={6} sm={9}>
                    <h1 className="text-light">Sell Ads</h1>
                </Col>
                <Col xs={6} sm={3}>
                    <div className="d-flex h-100 align-items-center">
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
                                <AdvertisementsGrid advertisements={advertisements?.data?.ad_list ?? []} totalCount={advertisements?.data?.ad_count} pageSize={25} selectedPage={1} onPageClick={(page) => {}} />
                            </div> 
                            
                    }
                </Col>
            </Row>
        </Container>
    )
}

Advertisements.defaultProps = {
}

Advertisements.propTypes = {
}

const mapStateToProps = state => ({
});

export default connect(mapStateToProps, {  })(Advertisements);