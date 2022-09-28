import PropTypes from 'prop-types'
import { Col, Row } from 'react-bootstrap'
import ProfitAndLossIndicator from './ProfitAndLossIndicator'

const SummaryRow = ({ label, value, reference }) => {
  return (
    <Row className='border-bottom border-secondary'>
      <Col xs={4}>
        <span className='text-secondary align-middle'>{label}</span>
      </Col>
      <Col xs={5} className='text-end text-light fs-4'>
        <span>{value}</span>
      </Col>
      <Col xs={3} className='text-end'>
        <ProfitAndLossIndicator percentage={reference} />
      </Col>
    </Row>
  );
}

SummaryRow.propTypes = {
  label: PropTypes.string.isRequired,
  value: PropTypes.string.isRequired,
  reference: PropTypes.number.isRequired
}

export default SummaryRow;