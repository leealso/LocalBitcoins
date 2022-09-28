import PropTypes from 'prop-types';
import { Col, Row } from 'react-bootstrap';
import ProfitAndLossIndicator from './ProfitAndLossIndicator';

const SummaryRow = ({ label, value, reference }) => {
  return (
    <Row className='border-bottom border-secondary'>
      <Col xs={4}>
        <span className='text-secondary align-middle'>{label}</span>
      </Col>
      <Col xs={5} className='text-end text-light'>
        <span className='fs-4'>{value}</span>
      </Col>
      <Col xs={3} className='text-end'>
        <ProfitAndLossIndicator percentage={reference} />
      </Col>
    </Row>
  );
}

SummaryRow.defaultProps = {
  label: '',
  value: '',
  reference: 1
}

SummaryRow.propTypes = {
  label: PropTypes.string.isRequired,
  value: PropTypes.string,
  reference: PropTypes.number
}

export default SummaryRow;