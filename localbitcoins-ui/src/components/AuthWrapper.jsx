import Container from 'react-bootstrap/Container'
import React from 'react'
import LoadingSpinner from './LoadingSpinner'
import { useGetTokenQuery, useRefreshTokenQuery } from '../services/authApiService'

const AuthWrapper = ({ children }) => {
    const { 
        isLoading: isLoadingRefreshToken, 
        refetch: refetchRefreshToken 
    } = useRefreshTokenQuery()
    
    const { 
        isLoading: isLoadingGetToken, 
        refetch: refetchGetToken 
    } = useGetTokenQuery()

    const showSpinner = isLoadingRefreshToken || isLoadingGetToken

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