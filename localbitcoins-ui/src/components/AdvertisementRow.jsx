import PropTypes from 'prop-types'
import BuySellButton from './BuySellButton'
import FormattedNumber from './FormattedNumber'

const AdvertisementRow = ({ advertisement }) => {
    const openInNewTab = url => {
        window.open(url, '_blank', 'noopener,noreferrer');
    };
    return (
        <tr>
            <td>{advertisement.username}</td>
            <td>{advertisement.currency}</td>
            <td>
                {<FormattedNumber text={advertisement.tempPriceUsd} decimals={0} prefix='$' />}
            </td>
            <td className="text-center">
                <BuySellButton isBuy={true} onClick={() => openInNewTab(advertisement.publicViewUrl)} />
            </td>
        </tr>
    )
}

AdvertisementRow.defaultProps = {
    advertisement: {}
}

AdvertisementRow.propTypes = {
    advertisement: PropTypes.any.isRequired
}

export default AdvertisementRow;