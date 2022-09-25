import PropTypes from 'prop-types';
import { Col, Container, Row } from 'react-bootstrap';
import IndicatorCard from './IndicatorCard';

const DailySummaryCard = ({ totalCount, btcVolume, fiatVoulme }) => {
  const fiatVolumeFormatted = `₡${fiatVoulme.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, `$1,`)}`
  return (
    <Container className="d-flex justify-content-center text-light rounded my-3">
      <Row>
        <Col xs={12}>
          <IndicatorCard subtitle={'Transactions'} title={`${totalCount}`} customTitleClass='text-end fs-4' customSubtitleClass='text-end fs-6 text-secondary align-middle' />
        </Col>
        <Col xs={12}>
          <IndicatorCard subtitle={'BTC Volume'} title={`${btcVolume}`} customTitleClass='text-end fs-4' customSubtitleClass='text-end fs-6 text-secondary align-middle' />
        </Col>
        <Col xs={12}>
          <IndicatorCard subtitle={'Fiat Volume'} title={`${fiatVolumeFormatted}`} customTitleClass='text-end fs-4' customSubtitleClass='text-end fs-6 text-secondary align-middle' />
        </Col>
      </Row>
    </Container>
  );
}

DailySummaryCard.defaultProps = {
  totalCount: 0,
  btcVolume: 0,
  fiatVoulme: 0
}

DailySummaryCard.propTypes = {
  totalCount: PropTypes.number.isRequired,
  btcVolume: PropTypes.number.isRequired,
  fiatVoulme: PropTypes.number.isRequired
}

export default DailySummaryCard;