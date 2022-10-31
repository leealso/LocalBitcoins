import DailyTrades from './components/DailyTrades'
import { Provider } from 'react-redux'
import store from './store/index.ts'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import BuyAdvertisements from './components/BuyAdvertisements'
import SellAdvertisements from './components/SellAdvertisements'
import MonthlyTrades from './components/MonthlyTrades'
import { useGetTokenQuery } from './services/authApiService'

function App() {
  useGetTokenQuery()

  return (
    <Provider store ={store}>
      <div className="App">
      <BrowserRouter>
          <Routes>
            <Route exact path='/' element={<DailyTrades/>} />
            <Route exact path='/trades' element={<DailyTrades/>} />
            <Route exact path='/monthly' element={<MonthlyTrades/>} />
            <Route exact path='/ads/buy' element={<BuyAdvertisements/>} />
            <Route exact path='/ads/sell' element={<SellAdvertisements/>} />
          </Routes>
        </BrowserRouter>
      </div>
    </Provider>
  );
}

export default App;
