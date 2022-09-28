import PropTypes from 'prop-types'
import BuySellButton from './BuySellButton'
import FormattedNumber from './FormattedNumber'

const AdvertisementRow = ({ advertisement, isBuy }) => {
    const openInNewTab = url => {
        window.open(url, '_blank', 'noopener,noreferrer');
    };
    return (
        <tr>
            <td>{advertisement.username}</td>
            <td>{advertisement.currency}</td>
            <td>
                <FormattedNumber text={advertisement.tempPriceUsd} decimals={0} prefix='$' />
            </td>
            <td className="text-center">
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
    isBuy: PropTypes.bool
}

export default AdvertisementRow;