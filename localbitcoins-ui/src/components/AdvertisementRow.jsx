import PropTypes from 'prop-types'
import FormattedNumber from './FormattedNumber'

const AdvertisementRow = ({ advertisement }) => {
    return (
        <tr>
            <td>{ advertisement.data.profile.username }</td>
            <td>{ advertisement.data.currency }</td>
            <td>
                <FormattedNumber text={advertisement.data.temp_price} decimals={2} prefix='₡'/>
            </td>
            <td>
                2.00%
            </td>
            <td>
                Buy
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