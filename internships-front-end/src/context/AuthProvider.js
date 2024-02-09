import { initialState, reducer } from '../services/reducers/authReducer'
import React, { useCallback, useReducer } from 'react'
import {
  LOG_IN,
  LOG_IN_ERROR,
  LOG_OUT
} from '../services/actions/actionTypes'
import { postLogin, postRegister } from '../services/actions/authActions'

export const AuthContext = React.createContext(initialState)

export const AuthProvider = ({ children }) => {
  const [authState, dispatch] = useReducer(reducer, initialState)

  const registerCallback = async (username, password, isAdmin, companyName) => {
    try {
      const response = await postRegister(username, password, isAdmin, companyName)
    } catch (error) {
      if (error.response) {
        console.log(error.response.data)
        console.log(error.response.status)
        console.log(error.response.headers)
        if (error.response.data === 'Invalid credentials!') {
          dispatch({ type: LOG_IN_ERROR, payload: { error: 'Invalid credentials!' } })
        }
      } else if (error.request) {
        console.log(error.request)
      } else {
        console.log('Error', error.message)
      }
    }
  }

  const loginCallback = async (username, password) => {
    try {
      const response = await postLogin(username, password)
      const id = response.data['id']
      const token = response.data['token']
      const isAdmin = response.data['isAdmin']
      dispatch({ type: LOG_IN, payload: { id, username, token, isAdmin } })
    } catch (error) {
      if (error.response) {
        console.log(error.response.data)
        console.log(error.response.status)
        console.log(error.response.headers)
        if (error.response.data === 'Invalid credentials!') {
          dispatch({ type: LOG_IN_ERROR, payload: { error: 'Invalid credentials!' } })
        }
      } else if (error.request) {
        console.log(error.request)
      } else {
        console.log('Error', error.message)
      }
    }
  }

  const logoutCallback = () => {
    dispatch({ type: LOG_OUT })
  }

  const login = useCallback(loginCallback, [])
  const logout = useCallback(logoutCallback, [])
  const register = useCallback(registerCallback, [])

  const value = { ...authState, login, logout, register }

  return (
    <AuthContext.Provider value={value}>
      {children}
    </AuthContext.Provider>
  )
}