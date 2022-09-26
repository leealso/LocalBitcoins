import PropTypes from 'prop-types';
import { Col, Row } from 'react-bootstrap';
import ProfitAndLossIndicator from './ProfitAndLossIndicator';

const IndicatorCard = ({ title, subtitle, customTitleClass, customSubtitleClass, percentage }) => {
    return (
      <Row>
        <Col xs={4} className='border-bottom border-secondary'>
          <span className={customSubtitleClass}>{subtitle}</span>
        </Col>
        <Col xs={5} className='text-end border-bottom border-secondary'>
          <span className={customTitleClass}>{title}</span>
        </Col>
        <Col xs={3} className='text-end border-bottom border-secondary'>
          <ProfitAndLossIndicator percentage={percentage} />
        </Col>
      </Row>
      );
}

IndicatorCard.defaultProps = {
    title: '',
    subtitle: '',
    percentage: 1,
    customTitleClass: '',
    customSubtitleClass: ''
}

IndicatorCard.propTypes = {
    title: PropTypes.string.isRequired,
    subtitle: PropTypes.string,
    percentage: PropTypes.number,
    customTitleClass: PropTypes.string,
    customSubtitleClass: PropTypes.string
}

export default IndicatorCard;