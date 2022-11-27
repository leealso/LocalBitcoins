import Container from 'react-bootstrap/Container'
import React from 'react'
import LoadingSpinner from './LoadingSpinner'
import { useGetTokenQuery, useRefreshTokenQuery } from '../services/authApiService'

const AuthWrapper = ({ children }) => {
    const { 
        isLoading: isLoadingRefreshToken, 
        isFetching: isFetchingRefreshToken, 
        refetch: refetchRefreshToken 
    } = useRefreshTokenQuery()
    
    const { 
        isLoading: isLoadingGetToken, 
        isFetching: isFetchingGetToken, 
        refetch: refetchGetToken 
    } = useGetTokenQuery()

    const showSpinner = isLoadingRefreshToken || isFetchingRefreshToken 
        || isLoadingGetToken || isFetchingGetToken

    const refresh = () => {
        refetchRefreshToken()
        refetchGetToken()
    }
    
    return (
        <Container fluid className='p-0'>
            {
                showSpinner
                    ? <LoadingSpinner isLoading={showSpinner} />
                    : React.cloneElement(children, { refreshAuth: refresh })
            }
        </Container>
    )
}

export default AuthWrapper;