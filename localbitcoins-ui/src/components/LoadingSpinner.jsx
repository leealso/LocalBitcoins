import PropTypes from 'prop-types'
import Container from 'react-bootstrap/Container'
import React from 'react'
import { GridLoader } from 'react-spinners'

const LoadingSpinner = ({ isLoading }) => {
    return (
        <Container className="d-flex w-100 vh-100 align-items-center justify-content-center">
            <GridLoader loading={isLoading} color="white" size={75} speed={0.8} />
        </Container>
    )
}

LoadingSpinner.defaultProps = {
    isLoading: false
}

LoadingSpinner.propTypes = {
    isLoading: PropTypes.bool
}

export default LoadingSpinner;