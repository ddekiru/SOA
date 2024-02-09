import {
  CHANGE_FILTER,
  GET_FILTERED_INTERNSHIPS,
  GET_USER_APPLICATIONS
} from '../actions/actionTypes'

export const initialState = {
  filter: {},
  filteredInternships: []
}

export const reducer = (state, action) => {
  switch (action.type) {
    case CHANGE_FILTER:
      return { ...state, selectedDay: action.payload.selectedDay }
    case GET_FILTERED_INTERNSHIPS:
      return { ...state, filteredInternships: action.payload.filteredInternships }
    case GET_USER_APPLICATIONS:
      return { ...state, applications: action.payload.applications }
    default:
      return state
  }
}
