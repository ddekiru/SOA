import { initialState, reducer } from '../services/reducers/internshipReducer'
import React, { useCallback, useContext, useReducer } from 'react'
import { AuthContext } from "./AuthProvider"
import { CHANGE_FILTER, GET_FILTERED_INTERNSHIPS, GET_USER_APPLICATIONS } from '../services/actions/actionTypes'
import { getFilteredInternships, postInternship, postInternshipApplication, getUserApplications as getApplications } from '../services/actions/internshipActions'

export const InternshipContext = React.createContext(initialState)

export const InternshipProvider = ({ children }) => {
  const [eventState, dispatch] = useReducer(reducer, initialState)
  const { token } = useContext(AuthContext)

  const getUserApplicationsCallback = async (id, token) => {
    try {
      const response = await getApplications(id, token)
      const applications = response.data
      dispatch({ type: GET_USER_APPLICATIONS, payload: { applications } })
    } catch (error) {
      if (error.response) {
        console.log(error.response.data)
        console.log(error.response.status)
        console.log(error.response.headers)
      } else if (error.request) {
        console.log(error.request)
      } else {
        console.log('Error', error.message)
      }
    }
  }

  const changeFilterCallback = async (filter) => {
    dispatch({ type: CHANGE_FILTER, payload: { filter: filter } })
    try {
      const response = await getFilteredInternships(filter, token)
      const filteredInternships = response.data
      dispatch({ type: GET_FILTERED_INTERNSHIPS, payload: { filteredInternships } })
    } catch (error) {
      if (error.response) {
        console.log(error.response.data)
        console.log(error.response.status)
        console.log(error.response.headers)
      } else if (error.request) {
        console.log(error.request)
      } else {
        console.log('Error', error.message)
      }
    }
  }

  const addInternshipCallback = async (internship) => {
    try {
      await postInternship(internship, token)
    } catch (error) {
      if (error.response) {
        console.log(error.response.data)
        console.log(error.response.status)
        console.log(error.response.headers)
      } else if (error.request) {
        console.log(error.request)
      } else {
        console.log('Error', error.message)
      }
    }
  }

  const addInternshipApplicationCallback = async (internshipApplication) => {
    try {
      await postInternshipApplication(internshipApplication, token)
    } catch (error) {
      if (error.response) {
        console.log(error.response.data)
        console.log(error.response.status)
        console.log(error.response.headers)
      } else if (error.request) {
        console.log(error.request)
      } else {
        console.log('Error', error.message)
      }
    }
  }

  const changeInternshipsFilter = useCallback(changeFilterCallback, [token])
  const addInternship = useCallback(addInternshipCallback, [token])
  const addInternshipApplication = useCallback(addInternshipApplicationCallback, [token])
  const getUserApplications = useCallback(getUserApplicationsCallback, [token])

  const value = { ...eventState, changeInternshipsFilter, addInternship, addInternshipApplication, getUserApplications }

  return (
    <InternshipContext.Provider value={value}>
      {children}
    </InternshipContext.Provider>
  )
}

export default InternshipProvider