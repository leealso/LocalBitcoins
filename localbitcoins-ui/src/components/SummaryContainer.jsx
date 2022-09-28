import { Container } from 'react-bootstrap';

const SummaryContainer = ({ children }) => {
  return (
    <Container className='my-3'>
      {children}
    </Container>
  );
}

export default SummaryContainer;