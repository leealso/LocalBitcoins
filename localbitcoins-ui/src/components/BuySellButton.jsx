import PropTypes from 'prop-types'
import ActionButton from './ActionButton';

const BuySellButton = ({ isBuy, onClick }) => {
    const customClassName = isBuy
        ? 'btn-success'
        : 'btn-danger'
    const text = isBuy
        ? 'Buy'
        : 'Sell'
    return (
        <ActionButton text={text} customClassName={customClassName} onClick={onClick} />
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