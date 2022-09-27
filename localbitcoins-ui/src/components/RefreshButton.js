import React from 'react'
import Button from 'react-bootstrap/Button'
import PropTypes from 'prop-types'
import { FaSyncAlt } from 'react-icons/fa'

const RefreshButton = ({ isLoading, onClick }) => {
    return (
        <Button
            className="ms-2 text-light"
            variant="info"
            onClick={!isLoading ? onClick : null}
        >
            <FaSyncAlt className={isLoading ? 'icon-spin' : ''}/>
        </Button>
    )
}

RefreshButton.defaultProps = {
    isLoading: false,
    onClick: null
}

RefreshButton.propTypes = {
    isLoading: PropTypes.bool.isRequired,
    onClick: PropTypes.func.isRequired
}

export default RefreshButton;