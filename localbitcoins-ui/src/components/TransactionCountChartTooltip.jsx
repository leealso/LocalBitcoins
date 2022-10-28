import PropTypes from 'prop-types'
import Container from 'react-bootstrap/Container'
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import React from 'react'
import { formatNumber, formatPercentage } from '../stringUtility'

const TransactionCountChartTooltip = ({ payload, label }) => {
    const total = payload.reduce((result, entry) => result + entry.value, 0)

    const getPercentage = (value, total) => {
        const ratio = total > 0 ? value / total : 0
        return formatPercentage(ratio)
    }

    return (
        <Container className='bg-light opacity-75'>
            <Row>
                <Col xs={4} className='text-end'>
                    <span>Total:</span>
                </Col>
                <Col className='text-end me-1'>
                    <span>{`${formatNumber(total, '', 0, false)}`}</span>
                </Col>
                <Col xs={5}>
                </Col>
            </Row>
            {payload.map((entry, index) => (
                <Row key={index} style={{ color: entry.color }}>
                    <Col xs={4} className='text-end'>
                        <span>{`${entry.name}:`}</span>
                    </Col>
                    <Col className='text-end me-1'>
                        <span>{`${formatNumber(entry.value, '', 0, false)}`}</span>
                    </Col>
                    <Col xs={5}>
                        <span>{`(${getPercentage(entry.value, total)})`}</span>
                    </Col>
                </Row>
            ))}
        </Container>
    )
}

TransactionCountChartTooltip.propTypes = {
    payload: PropTypes.any,
    label: PropTypes.any
}

export default TransactionCountChartTooltip;