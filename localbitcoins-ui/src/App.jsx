import TodayTrades from './components/TodayTrades';
import { Provider } from 'react-redux';
import store from './store/index.ts';
import "react-datepicker/dist/react-datepicker.css";

function App() {
  return (
    <Provider store ={store}>
      <div className="App">
        <TodayTrades />
      </div>
    </Provider>
  );
}

export default App;
