import PropTypes from 'prop-types';
import { Col, Container, Row } from 'react-bootstrap';
import IndicatorCard from './IndicatorCard';

const DailySummaryCard = ({ totalCount, btcVolume, fiatVolume, totalCountPercentage, btcVolumePercentage, fiatVolumePercentage }) => {
  const fiatVolumeFormatted = `₡${fiatVolume.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, `$1,`)}`
  return (
    <Container className="d-flex justify-content-center text-light rounded my-3">
      <Row>
        <Col xs={12}>
          <IndicatorCard subtitle={'Transactions'} title={`${totalCount}`} percentage={totalCountPercentage} customTitleClass='text-end fs-4' customSubtitleClass='text-end fs-6 text-secondary align-middle' customPercentageClass='fs-4' />
        </Col>
        <Col xs={12}>
          <IndicatorCard subtitle={'BTC Volume'} title={`${btcVolume}`} percentage={btcVolumePercentage} customTitleClass='text-end fs-4' customSubtitleClass='text-end fs-6 text-secondary align-middle' customPercentageClass='fs-4' />
        </Col>
        <Col xs={12}>
          <IndicatorCard subtitle={'Fiat Volume'} title={`${fiatVolumeFormatted}`} percentage={fiatVolumePercentage} customTitleClass='text-end fs-4' customSubtitleClass='text-end fs-6 text-secondary align-middle' customPercentageClass='fs-4'/>
        </Col>
      </Row>
    </Container>
  );
}

DailySummaryCard.defaultProps = {
  totalCount: 0,
  btcVolume: 0,
  fiatVolume: 0,
  totalCountPercentage: 1,
  btcVolumePercentage: 1,
  fiatVolumePercentage: 1
}

DailySummaryCard.propTypes = {
  totalCount: PropTypes.number.isRequired,
  btcVolume: PropTypes.number.isRequired,
  fiatVolume: PropTypes.number.isRequired,
  totalCountPercentage: PropTypes.number,
  btcVolumePercentage: PropTypes.number,
  fiatVolumePercentage: PropTypes.number,

}

export default DailySummaryCard;