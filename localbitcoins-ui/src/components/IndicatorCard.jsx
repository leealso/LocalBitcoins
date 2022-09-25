import PropTypes from 'prop-types';
import { Col, Row } from 'react-bootstrap';

const IndicatorCard = ({ title, subtitle, customTitleClass, customSubtitleClass }) => {
    return (
      <Row>
        <Col xs={4} className='border-bottom border-secondary'>
          <span className={customSubtitleClass}>{subtitle}</span>
        </Col>
        <Col xs={6} className='text-end border-bottom border-secondary'>
          <span className={customTitleClass}>{title}</span>
        </Col>
        <Col xs={2}>
          <span></span>
        </Col>
      </Row>
      );
}

IndicatorCard.defaultProps = {
    title: '',
    subtitle: '',
    customTitleClass: '',
    customSubtitleClass: ''
}

IndicatorCard.propTypes = {
    title: PropTypes.string.isRequired,
    subtitle: PropTypes.string,
    customTitleClass: PropTypes.string,
    customSubtitleClass: PropTypes.string
}

export default IndicatorCard;