import PropTypes from 'prop-types'
import { AreaChart, Area, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer } from 'recharts';
import Container from 'react-bootstrap/Container'
import BtcVolumeChartTooltip from './BtcVolumeChartTooltip'
import React from 'react'

const BtcVolumeChart = ({ dailySummary }) => {
    let data = []
    dailySummary?.response.forEach(x => {
        data.push({
            day: new Date(x.startDate).getDate(),
            Others: x.btcVolume - x.closedBtcVolume,
            Closed: x.closedBtcVolume
        })
    })
    const toPercent = (decimal) => decimal ? `${(decimal * 100)}%` : ''

    return (
        <Container fluid className='g-0'>
            <h5 className='text-light text-center'>BTC Volume</h5>
            <ResponsiveContainer width='100%' height={400}>
                <AreaChart
                    data={data}
                    stackOffset='expand'
                >
                    <CartesianGrid strokeDasharray='3 3' />
                    <XAxis dataKey='day' stroke='#fff' />
                    <YAxis tickFormatter={toPercent} stroke='#fff' />
                    <Tooltip content={<BtcVolumeChartTooltip />} />
                    <Area type='monotone' dataKey='Closed' stackId='1' stroke='#62c462' fill='#62c462' />
                    <Area type='monotone' dataKey='Others' stackId='1' stroke='#ee5f5b' fill='#ee5f5b' />
                </AreaChart>
            </ResponsiveContainer>
        </Container>
    )
}

BtcVolumeChart.propTypes = {
    dailySummary: PropTypes.object
}

export default BtcVolumeChart;