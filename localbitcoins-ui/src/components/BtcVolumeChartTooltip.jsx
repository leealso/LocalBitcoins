import PropTypes from 'prop-types'
import Container from 'react-bootstrap/Container'
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import React from 'react'
import { formatNumber, formatPercentage, formatDate } from '../stringUtility'

const BtcVolumeChartTooltip = ({ payload, label }) => {
    const total = payload.reduce((result, entry) => result + entry.value, 0)

    const getPercentage = (value, total) => {
        const ratio = total > 0 ? value / total : 0
        return formatPercentage(ratio)
    }

    return (
        <Container className='bg-light opacity-75'>
            <Row>
                <Col className='text-center text-primary'>
                    <span><b>{ label ? formatDate(label) : '' }</b></span>
                </Col>
            </Row>
            {payload.map((entry, index) => (
                <Row key={index} style={{ color: entry.color }}>
                    <Col xs={3} className='text-end'>
                        <span>{`${entry.name}:`}</span>
                    </Col>
                    <Col xs={5} className='text-end'>
                        <span>{`${formatNumber(entry.value, '', 8, false)}`}</span>
                    </Col>
                    <Col xs={4}>
                        <span>{`(${getPercentage(entry.value, total)})`}</span>
                    </Col>
                </Row>
            ))}
            <Row>
                <Col xs={3} className='text-end'>
                    <span>Total:</span>
                </Col>
                <Col xs={5} className='text-end'>
                    <span>{`${formatNumber(total, '', 8, false)}`}</span>
                </Col>
                <Col xs={4}>
                </Col>
            </Row>
        </Container>
    )
}

BtcVolumeChartTooltip.propTypes = {
    payload: PropTypes.any,
    label: PropTypes.any
}

export default BtcVolumeChartTooltip;