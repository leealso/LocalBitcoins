import PropTypes from 'prop-types'
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import React from 'react'
import RefreshButton from './RefreshButton'

const ContentHeader = ({ title, children, isLoading, onRefreshClick }) => {
    return (
        <Row>
            <Col xs={7} sm={9}>
                <h1 className="text-light">{ title }</h1>
            </Col>
            <Col xs={5} sm={3}>
                <div className="d-flex h-100 align-items-center float-end">
                    { children }
                    <RefreshButton isLoading={isLoading} onClick={onRefreshClick} />
                </div>
            </Col>
        </Row>
    )
}

ContentHeader.defaultProps = {
    title: '',
    isLoading: false,
    onRefreshClick: null
}

ContentHeader.propTypes = {
    title: PropTypes.string.isRequired,
    isLoading: PropTypes.bool,
    onRefreshClick: PropTypes.func.isRequired
}

export default ContentHeader;