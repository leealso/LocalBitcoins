import PropTypes from 'prop-types'
import { Table } from 'react-bootstrap'
import AdvertisementRow from './AdvertisementRow'
import Pagination from 'react-bootstrap/Pagination'

const AdvertisementsGrid = ({ advertisements, totalCount, pageSize, selectedPage, onPageClick }) => {
    const pages = Math.ceil(totalCount / pageSize)
    let paginationItems = [];
    for (let i = 1; i <= pages; i++) {
        paginationItems.push(
            <Pagination.Item key={i} active={i === selectedPage} onClick={() => onPageClick(i)}>
              {i}
            </Pagination.Item>,
          );
    }
    return (
        <div>
            <Table striped hover variant="dark" className='border-secondary border-bottom'>
                <thead>
                    <tr>
                        <th>User ID</th>
                        <th>Currency</th>
                        <th>Price</th>
                        <th>Reference</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>

                    {
                        advertisements.map((advertisement, i) => (
                            <AdvertisementRow key={i} advertisement={advertisement} />
                        ))
                    }
                </tbody>
            </Table>
            <Pagination className='justify-content-center'>
                {paginationItems}
            </Pagination>
        </div>
    )
}

AdvertisementsGrid.defaultProps = {
    advertisements: [],
    totalCount: 0,
    pageSize: 0,
    selectedPage: 1
}

AdvertisementsGrid.propTypes = {
    advertisements: PropTypes.array.isRequired,
    totalCount: PropTypes.number,
    pageSize: PropTypes.number.isRequired,
    selectedPage: PropTypes.number
}

export default AdvertisementsGrid;