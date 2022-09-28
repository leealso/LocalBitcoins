import PropTypes from 'prop-types'
import { Table } from 'react-bootstrap'
import AdvertisementRow from './AdvertisementRow'

const AdvertisementsGrid = ({ advertisements, isBuy, btcPrice }) => {
    return (
        <div>
            <Table striped hover variant="dark" className='border-secondary border-bottom'>
                <thead>
                    <tr>
                        <th>User ID</th>
                        <th>Currency</th>
                        <th>Price</th>
                        <th>P&L</th>
                    </tr>
                </thead>
                <tbody>

                    {
                        advertisements.map((advertisement, i) => (
                            <AdvertisementRow key={i} advertisement={advertisement} isBuy={isBuy} btcPrice={btcPrice} />
                        ))
                    }
                </tbody>
            </Table>
        </div>
    )
}

AdvertisementsGrid.defaultProps = {
    advertisements: [],
    isBuy: true,
    btcPrice: 1
}

AdvertisementsGrid.propTypes = {
    advertisements: PropTypes.array.isRequired,
    isBuy: PropTypes.bool,
    btcPrice: PropTypes.number.isRequired
}

export default AdvertisementsGrid;