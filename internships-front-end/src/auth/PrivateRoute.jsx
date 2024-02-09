import { useContext } from 'react'
import { AuthContext } from '../context/AuthProvider'
import { Navigate } from 'react-router-dom'

export const PrivateRoute = ({ children }) => {
  const { isAuthenticated } = useContext(AuthContext)

  if (isAuthenticated) {
    return (
      <>
        {children}
      </>
    )
  }

  return <Navigate to={{ pathname: '/login' }} replace={true} />
}