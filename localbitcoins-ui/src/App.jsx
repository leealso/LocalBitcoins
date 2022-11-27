import { Provider } from 'react-redux'
import store from './store/index.ts'
import AuthWrapper from './components/AuthWrapper'
import CustomBrowserRouter from './components/CustomBrowserRouter'

function App() {

  return (
    <Provider store={store}>
      <div className="App">
        <AuthWrapper>
          <CustomBrowserRouter />
        </AuthWrapper>
      </div>
    </Provider>
  );
}

export default App;
