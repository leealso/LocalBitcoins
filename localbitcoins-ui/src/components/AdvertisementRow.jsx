import PropTypes from 'prop-types'
import { truncate } from '../stringUtility'
import BuySellButton from './BuySellButton'
import FormattedNumber from './FormattedNumber'
import ProfitAndLossIndicator from './ProfitAndLossIndicator'

const AdvertisementRow = ({ advertisement, isBuy, btcPrice }) => {
    const openInNewTab = url => {
        window.open(url, '_blank', 'noopener,noreferrer');
    };
    const percentage = advertisement.tempPriceUsd / btcPrice - 1
    return (
        <tr>
            <td>{ truncate(advertisement.username) }</td>
            <td>{advertisement.currency}</td>
            <td>
                <FormattedNumber text={advertisement.tempPriceUsd} decimals={0} prefix='$' />
            </td>
            <td className="text-center">
                <ProfitAndLossIndicator percentage={percentage} />
            </td>
        </tr>
    )
}
//<BuySellButton isBuy={isBuy} onClick={() => openInNewTab(advertisement.publicViewUrl)} />

AdvertisementRow.defaultProps = {
    advertisement: {},
    isBuy: true
}

AdvertisementRow.propTypes = {
    advertisement: PropTypes.any.isRequired,
    isBuy: PropTypes.bool,
    btcPrice: PropTypes.number.isRequired
}

export default AdvertisementRow;