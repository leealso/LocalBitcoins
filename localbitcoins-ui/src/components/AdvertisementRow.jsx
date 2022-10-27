import PropTypes from 'prop-types'
import { formatNumber, truncate } from '../stringUtility'
import BuySellButton from './BuySellButton'
import ProfitAndLossIndicator from './ProfitAndLossIndicator'

const AdvertisementRow = ({ advertisement, isBuy, btcPrice }) => {
    const openInNewTab = url => {
        window.open(url, '_blank', 'noopener,noreferrer');
    };
    const percentage = advertisement.tempPriceUsd / btcPrice - 1
    const symbol = advertisement.currency === 'CRC'
        ? '₡'
        : '$'
        /*<td className='text-end d-none d-lg-table-cell'>
                { formatNumber(advertisement.minAmountAvailable, symbol, 2) } - { formatNumber(advertisement.maxAmountAvailable, symbol, 2) }
            </td> */
    return (
        <tr className='align-middle'>
            <td className='d-md-none'>{ truncate(advertisement.username) }</td>
            <td className='d-none d-md-table-cell'>{ advertisement.username }</td>
            <td>{advertisement.currency}</td>
            <td className='text-end d-md-none'>
                { formatNumber(advertisement.tempPriceUsd, '$', 0) }
            </td>
            <td className='text-end d-none d-md-table-cell'>
                { formatNumber(advertisement.tempPriceUsd, '$', 2) }
            </td>
            <td className='text-end d-none d-md-table-cell'>
                { formatNumber(advertisement.tempPrice, '₡', 2) }
            </td>
            <td className='text-end d-none d-lg-table-cell'>
                { formatNumber(advertisement.minAmountAvailable, symbol, 2) }
            </td>
            <td className='text-end d-none d-lg-table-cell'>
                { formatNumber(advertisement.maxAmountAvailable, symbol, 2) }
            </td>
            <td className='text-center'>
                <ProfitAndLossIndicator percentage={percentage} />
            </td>
            <td className='d-none d-md-table-cell text-center'>
                <BuySellButton isBuy={isBuy} onClick={() => openInNewTab(advertisement.publicViewUrl)} />
            </td>
        </tr>
    )
}

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