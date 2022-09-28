import PropTypes from 'prop-types';
import { Container } from 'react-bootstrap';
import BtcPriceSummaryRow from './BtcPriceSummaryRow';
import SummaryRow from './SummaryRow';

const DailySummaryCard = ({ totalCount, btcVolume, fiatVolume, totalCountPercentage, btcVolumePercentage, fiatVolumePercentage }) => {
  const fiatVolumeFormatted = `₡${fiatVolume.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, `$1,`)}`
  return (
    <Container className='text-light my-3'>
      <SummaryRow label={'Transactions'} value={`${totalCount}`} reference={totalCountPercentage} />
      <SummaryRow label={'BTC Volume'} value={`${btcVolume}`} reference={btcVolumePercentage} />
      <SummaryRow label={'Fiat Volume'} value={`${fiatVolumeFormatted}`} reference={fiatVolumePercentage} />
      <BtcPriceSummaryRow />
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