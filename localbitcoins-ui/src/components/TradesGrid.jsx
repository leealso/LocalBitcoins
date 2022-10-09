import PropTypes from 'prop-types'
import { Table } from 'react-bootstrap'
import TradeRow from './TradeRow'
import Pagination from 'react-bootstrap/Pagination'

const TradesGrid = ({ trades, totalCount, pageSize, selectedPage, onPageClick }) => {
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
            
            <Table striped variant="dark" className='border-secondary border-bottom'>
                <thead>
                    <tr>
                        <th className='d-none d-md-table-cell'>Transaction ID</th>
                        <th className='d-md-none'>Time</th>
                        <th className='d-none d-md-table-cell'>Date</th>
                        <th className='text-center'>Fiat</th>
                        <th className='text-center'>BTC</th>
                        <th className='text-center'>Price</th>
                    </tr>
                </thead>
                <tbody>

                    {
                        trades.map((trade, i) => (
                            <TradeRow key={i} trade={trade} />
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

TradesGrid.defaultProps = {
    trades: [],
    totalCount: 0,
    pageSize: 0,
    selectedPage: 1
}

TradesGrid.propTypes = {
    trades: PropTypes.array.isRequired,
    totalCount: PropTypes.number,
    pageSize: PropTypes.number.isRequired,
    selectedPage: PropTypes.number
}

export default TradesGrid;