import Button from 'react-bootstrap/Button'
import PropTypes from 'prop-types'

const ActionButton = ({ text, customClassName, onClick }) => {
    return (
        <Button className={customClassName} onClick={onClick}>
            { text }
        </Button>
    )
};

ActionButton.defaultProps = {
    text: '',
    customClassName: ''
};

ActionButton.propTypes = {
    text: PropTypes.string,
    customClassName: PropTypes.string
};

export default ActionButton;