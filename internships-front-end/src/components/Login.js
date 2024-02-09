import React, { useContext, useState } from 'react';
import { Navigate } from 'react-router-dom';
import { AuthContext } from '../context/AuthProvider';
import Switch from 'react-switch';

export const Login = () => {
  const { isAuthenticated, login, register, authenticationError } = useContext(AuthContext);
  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')
  const [companyName, setCompanyName] = useState('')
  const [isRegister, setIsRegister] = useState(false)
  const [isRegisterAdmin, setIsRegisterAdmin] = useState(false)

  const clearFields = () => {
    setUsername('')
    setPassword('')
    setCompanyName('')
  }

  const handleLogin = () => {
    login?.(username, password);
  };
  if (isAuthenticated) {
    return <Navigate to={{ pathname: '/' }} />
  }
  return (
    <>
      <span>Register</span>
      {/* <Switch onChange={() => { setIsRegister(!isRegister) }} checked={isRegister} /> */}
      {isRegister ? (
        <div>
          <span>Register as admin</span>
          {/* <Switch onChange={() => { setIsRegisterAdmin(!isRegisterAdmin) }} checked={isRegisterAdmin} /><br /> */}
          <input
            type='text'
            placeholder='Username'
            value={username}
            onChange={e => setUsername(e.target.value)} /><br />
          <input
            type='password'
            placeholder='Password'
            value={password}
            onChange={e => setPassword(e.target.value)} /><br />
          {isRegisterAdmin &&
            <>
              <input
                type='text'
                placeholder='Company name'
                value={companyName}
                onChange={e => setCompanyName(e.target.value)} /><br />
            </>
          }
          <button disabled={!username || !password} onClick={() => {
            register(username, password, isRegisterAdmin, companyName)
            clearFields()
            setIsRegisterAdmin(false)
            setIsRegister(false)
          }}>
            Create account
          </button>
        </div>
      ) : (
        <div>
          <input
            type='text'
            placeholder="Username"
            value={username}
            onChange={e => setUsername(e.target.value)} /><br />
          <input
            type='password'
            placeholder="Password"
            value={password}
            onChange={e => setPassword(e.target.value)} /><br />
          <button disabled={!username || !password} onClick={handleLogin}>Login</button>
        </div>
      )}
    </>
  );
};