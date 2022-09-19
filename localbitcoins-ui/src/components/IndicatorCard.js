import PropTypes from 'prop-types';
import Card from 'react-bootstrap/Card';

const IndicatorCard = ({ title, text }) => {
    return (
        <Card style={{ width: '18rem' }}>
          <Card.Img variant="top" src="holder.js/100px180" />
          <Card.Body>
            <Card.Title>{title}</Card.Title>
            <Card.Text>
              {text}
            </Card.Text>
          </Card.Body>
        </Card>
      );
}

IndicatorCard.defaultProps = {
    title: '',
    text: ''
}

IndicatorCard.propTypes = {
    title: PropTypes.string.isRequired,
    text: PropTypes.string
}

export default IndicatorCard;