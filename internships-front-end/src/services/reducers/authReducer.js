import { LOG_IN, LOG_IN_ERROR, LOG_OUT } from '../actions/actionTypes'

export const initialState = {
  token: '',
  isAuthenticated: false,
  error: null
}

export const reducer = (state, action) => {
  switch (action.type) {
    case LOG_IN:
      return {
        ...state, id: action.payload.id, username: action.payload.username, token: action.payload.token,
        isAdmin: action.payload.isAdmin, isAuthenticated: true, error: null
      }
    case LOG_IN_ERROR:
      return { ...state, error: action.payload.error }
    case LOG_OUT:
      return { ...state, token: '', isAuthenticated: false }
    default:
      return state
  }
}
