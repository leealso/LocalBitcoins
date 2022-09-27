import Button from 'react-bootstrap/Button'
import PropTypes from 'prop-types'

const BuySellButton = ({ isBuy, onClick }) => {
    const customClassName = isBuy
        ? 'btn-success'
        : 'btn-danger'
    const text = isBuy
        ? 'Buy'
        : 'Sell'
    return (
        <Button className={customClassName} onClick={onClick}>
            {text}
        </Button>
    )
};

BuySellButton.defaultProps = {
    text: '',
    customClassName: ''
};

BuySellButton.propTypes = {
    text: PropTypes.string,
    customClassName: PropTypes.string
};

export default BuySellButton;