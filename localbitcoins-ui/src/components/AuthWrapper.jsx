import Container from 'react-bootstrap/Container'
import React from 'react'
import LoadingSpinner from './LoadingSpinner'
import { useGetTokenQuery, useRefreshTokenQuery } from '../services/authApiService'

const AuthWrapper = ({ children }) => {
    const { isLoading, isFetching } = useGetTokenQuery()
    const showSpinner = isLoading || isFetching
    
    useEffect(() => {
        // Every 45 mins
        const interval = setInterval(() => {
            useRefreshTokenQuery()
            useGetTokenQuery()
        }, 2700000);
        return () => clearInterval(interval);
      }, []);

    return (
        <Container fluid className='p-0'>
            {
                showSpinner
                    ? <LoadingSpinner isLoading={showSpinner} />
                    : children
            }
        </Container>
    )
}

export default AuthWrapper;