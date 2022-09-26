import PropTypes from 'prop-types'
import { Col, Row } from 'react-bootstrap'
import { FaCaretDown, FaCaretUp } from 'react-icons/fa'

const ProfitAndLossIndicator = ({ percentage }) => {
    const isSuccess = percentage >= 0
    return (
      <Row>
        <Col xs={3}>
        {
            isSuccess
                ? <FaCaretUp className='text-success'></FaCaretUp>
                : <FaCaretDown className='text-danger'></FaCaretDown>
        }
        </Col>
        <Col xs={9} className={isSuccess ? 'text-success' : 'text-danger'}>
          <span className='h-100 align-middle'>{`${(Math.abs(percentage * 100)).toFixed(2)}%`}</span>
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