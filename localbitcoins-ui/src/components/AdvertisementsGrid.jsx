import PropTypes from 'prop-types'
import { Table } from 'react-bootstrap'
import AdvertisementRow from './AdvertisementRow'

const AdvertisementsGrid = ({ advertisements, isBuy, btcPrice }) => {
    return (
        <div>
            <Table className='border-secondary border-bottom'>
                <thead>
                    <tr>
                        <th>User ID</th>
                        <th>Currency</th>
                        <th className='text-center'>Price</th>
                        <th className='text-center d-none d-md-table-cell'>Price</th>
                        <th className='text-center'>P&L</th>
                        <th className='d-none d-md-table-cell'></th>
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