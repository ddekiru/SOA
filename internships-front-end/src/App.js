import './App.css';
import { BrowserRouter, Route, Routes, Navigate } from 'react-router-dom';
import { Login } from './components/Login';
import { AuthProvider } from './context/AuthProvider';
import { InternshipProvider } from './context/InternshipProvider';
import { PrivateRoute } from './auth/PrivateRoute'
import { Home } from './components/Home'

function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <Routes>
          <Route path='/login' element={<Login />} />
        
          <Route path='/home' element={(
            <PrivateRoute>
              <InternshipProvider>
                <Home />
              </InternshipProvider>
            </PrivateRoute>
          )} />
          <Route exact path='/' element={<Navigate to={'/home'} />} />
        </Routes>
      </BrowserRouter>
    </AuthProvider>
  );
}

export default App;
