import React from 'react'
import Button from 'react-bootstrap/Button'
import PropTypes from 'prop-types'
import { FaSyncAlt } from 'react-icons/fa'

const LoadingButton = ({ isLoading, handleClick }) => {
    return (
        <Button
            className="ms-2"
            variant="success"
            onClick={!isLoading ? handleClick : null}
        >
            <FaSyncAlt className={isLoading ? 'icon-spin' : ''}/>
        </Button>
    )
}
// <Icon faStyle={"refresh"} animate="spin"/>

LoadingButton.defaultProps = {
    isLoading: false,
    handleClick: null
}

LoadingButton.propTypes = {
    isLoading: PropTypes.bool.isRequired,
    handleClick: PropTypes.func.isRequired
}

export default LoadingButton;