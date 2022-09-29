import PropTypes from 'prop-types'
import { Container } from 'react-bootstrap'
import { FaCaretDown, FaCaretUp } from 'react-icons/fa'

const ProfitAndLossIndicator = ({ percentage }) => {
  const isSuccess = percentage >= 0
  return (
    <Container fluid className={`px-0 ${isSuccess ? 'text-success' : 'text-danger'}`}>
        {
          isSuccess
            ? <FaCaretUp />
            : <FaCaretDown />
        }
        <span className='align-middle ms-1'>
          {`${(Math.abs(percentage * 100)).toFixed(2)}%`}
        </span>
    </Container>
  );
}

ProfitAndLossIndicator.defaultProps = {
  percentage: 1
}

ProfitAndLossIndicator.propTypes = {
  percentage: PropTypes.number.isRequired
}

export default ProfitAndLossIndicator;