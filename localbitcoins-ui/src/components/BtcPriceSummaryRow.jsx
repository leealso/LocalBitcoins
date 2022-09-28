import { useGetQuoteQuery } from '../services/localBitcoinsApiService';
import ProfitAndLossIndicator from './ProfitAndLossIndicator';
import SummaryRow from './SummaryRow';

const BtcPriceSummaryRow = () => {
  const { data: data, isLoading: isLoading, isFetching: isFetching, refetch: refetch } = useGetQuoteQuery({ symbol: 'BTC' })
  const price = `${data?.quote?.price.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, `$1,`)}`
  return (
    <SummaryRow label={'BTC Price'} value={`$${price}`} reference={data?.quote?.percentChange24h / 100} />
  );
}

export default BtcPriceSummaryRow;