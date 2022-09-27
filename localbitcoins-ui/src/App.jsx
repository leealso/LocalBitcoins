import TodayTrades from './components/TodayTrades'
import { Provider } from 'react-redux'
import store from './store/index.ts'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Advertisements from './components/Advertisements'

function App() {
  return (
    <Provider store ={store}>
      <div className="App">
      <BrowserRouter>
          <Routes>
            <Route exact path='/trades' element={<TodayTrades/>} />
            <Route exact path='/ads/:tradeType' element={<Advertisements/>} />
          </Routes>
        </BrowserRouter>
      </div>
    </Provider>
  );
}

export default App;
