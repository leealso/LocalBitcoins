import { Container } from 'react-bootstrap';
import { BeatLoader } from 'react-spinners';
import { useGetQuoteQuery } from '../services/localBitcoinsApiService';
import SummaryRow from './SummaryRow';

const BtcPriceSummaryRow = () => {
  const { data: data, isLoading: isLoadingQuote, isFetching: isFetchingQuote, refetch: refetch } = useGetQuoteQuery({ symbol: 'BTC' })
  const price = `${data?.quote?.price.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, `$1,`)}`
  const isLoading = isLoadingQuote || isFetchingQuote
  return (
    <div>
      {
        isLoading
            ? <Container className="d-flex w-100 h-100 align-items-center justify-content-center">
                <BeatLoader color="white" loading={isLoading} size={5} />
              </Container>
            : <SummaryRow label={'BTC Price'} value={`$${price}`} reference={data?.quote?.percentChange24h / 100} />
      }
    </div>
  );
}

export default BtcPriceSummaryRow;