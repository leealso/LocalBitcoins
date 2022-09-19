import PropTypes from 'prop-types';
import { FaArrowDown, FaArrowUp } from 'react-icons/fa';

const OrderButton = ({ order }) => {
    let icon;
    if (order == 'DESC') {
        icon = <FaArrowDown />;
    } else {
        icon = <FaArrowUp />;
    }

    return (
        <span style={{ float: 'right' }}>
            {icon}
        </span>
    )
};

OrderButton.defaultProps = {
    order: 'DESC'
};

OrderButton.propTypes = {
    order: PropTypes.string
};

export default OrderButton;