import PropTypes from 'prop-types'
import { Col, Row } from 'react-bootstrap'
import { FaCaretDown, FaCaretUp } from 'react-icons/fa'

const ProfitAndLossIndicator = ({ percentage }) => {
  const isSuccess = percentage >= 0
  return (
    <Row className='gx-0'>
      <Col xs={3}>
        {
          isSuccess
            ? <FaCaretUp className='text-success' />
            : <FaCaretDown className='text-danger' />
        }
      </Col>
      <Col xs={9}>
        <span className={`align-middle ${isSuccess ? 'text-success' : 'text-danger'}`}>
          {`${(Math.abs(percentage * 100)).toFixed(2)}%`}
        </span>
      </Col>
    </Row>
  );
}

ProfitAndLossIndicator.defaultProps = {
  percentage: 1
}

ProfitAndLossIndicator.propTypes = {
  percentage: PropTypes.number.isRequired
}

export default ProfitAndLossIndicator;