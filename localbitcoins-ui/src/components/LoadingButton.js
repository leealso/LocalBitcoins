import React from 'react';
import Button from 'react-bootstrap/Button';
import PropTypes from 'prop-types'
import { FaSyncAlt } from 'react-icons/fa'

const LoadingButton = ({ isLoading, handleClick }) => {
    return (
        <Button
            variant="primary"
            disabled={isLoading}
            onClick={!isLoading ? handleClick : null}
        >
            <FaSyncAlt/>
        </Button>
    )
}

LoadingButton.defaultProps = {
    isLoading: false,
    handleClick: null
}

LoadingButton.propTypes = {
    isLoading: PropTypes.bool.isRequired,
    handleClick: PropTypes.func.isRequired
}

export default LoadingButton;