import PropTypes from 'prop-types'
import { Table } from 'react-bootstrap'
import AdvertisementRow from './AdvertisementRow'

const AdvertisementsGrid = ({ advertisements, isBuy }) => {
    return (
        <div>
            <Table striped hover variant="dark" className='border-secondary border-bottom'>
                <thead>
                    <tr>
                        <th>User ID</th>
                        <th>Currency</th>
                        <th>Price</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>

                    {
                        advertisements.map((advertisement, i) => (
                            <AdvertisementRow key={i} advertisement={advertisement} isBuy={isBuy} />
                        ))
                    }
                </tbody>
            </Table>
        </div>
    )
}

AdvertisementsGrid.defaultProps = {
    advertisements: [],
    isBuy: true
}

AdvertisementsGrid.propTypes = {
    advertisements: PropTypes.array.isRequired,
    isBuy: PropTypes.bool
}

export default AdvertisementsGrid;