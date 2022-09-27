import PropTypes from 'prop-types'
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import React from 'react'
import LoadingSpinner from './LoadingSpinner'

const ContentBody = ({ children, isLoading }) => {
    return (
        <Row>
            <Col>
                {
                    isLoading
                        ? <LoadingSpinner isLoading={isLoading} />
                        : children
                }
            </Col>
        </Row>
    )
}

ContentBody.defaultProps = {
    isLoading: false
}

ContentBody.propTypes = {
    isLoading: PropTypes.bool
}

export default ContentBody;