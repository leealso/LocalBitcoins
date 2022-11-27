import React from 'react'
import DailyTrades from './DailyTrades'
import BuyAdvertisements from './BuyAdvertisements'
import SellAdvertisements from './SellAdvertisements'
import MonthlyTrades from './MonthlyTrades'
import { BrowserRouter, Routes, Route } from 'react-router-dom'

const CustomBrowserRouter = ({ refreshAuth }) => {
    return (
        <BrowserRouter>
            <Routes>
                <Route exact path='/' element={<DailyTrades refreshAuth={refreshAuth} />} />
                <Route exact path='/trades' element={<DailyTrades refreshAuth={refreshAuth} />} />
                <Route exact path='/monthly' element={<MonthlyTrades refreshAuth={refreshAuth} />} />
                <Route exact path='/ads/buy' element={<BuyAdvertisements refreshAuth={refreshAuth} />} />
                <Route exact path='/ads/sell' element={<SellAdvertisements refreshAuth={refreshAuth} />} />
            </Routes>
        </BrowserRouter>
    )
}

export default CustomBrowserRouter;