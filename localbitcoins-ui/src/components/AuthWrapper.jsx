import Container from 'react-bootstrap/Container'
import React from 'react'
import LoadingSpinner from './LoadingSpinner'
import { useGetTokenQuery } from '../services/authApiService'

const AuthWrapper = ({ children }) => {
    const { isLoading, isFetching } = useGetTokenQuery()

    const showSpinner = isLoading || isFetching
    return (
        <Container fluid>
            {
                showSpinner
                    ? <LoadingSpinner isLoading={showSpinner} />
                    : children
            }
        </Container>
    )
}

export default AuthWrapper;