import Axios from "axios";

const baseUrl = process.env.REACT_APP_API_URL;

export const getFilteredInternships = (filter) => {
  const myParams = {}
  if (filter.title) myParams.title = filter.title
  if (filter.location) myParams.location = filter.location
  if (filter.domain) myParams.domain = filter.domain
  if (filter.date) myParams.date = filter.date
  if (filter.companyName) myParams.companyName = filter.companyName
  return Axios.get(baseUrl + '/internships', {
    params: myParams
  })
}

export const getUserApplications = (id, token) => {
  const config = {
    headers: { Authorization: `Bearer ${token}` }
  };
  return Axios.get(baseUrl + `/internshipApplications/byRegularUserId/${id}`, config)
}

export const postInternship = (internship, token) => {
  const config = {
    headers: { Authorization: `Bearer ${token}` }
  };
  return Axios.post(baseUrl + '/internships', internship, config)
}

export const postInternshipApplication = (internshipApplication, token) => {
  const config = {
    headers: { Authorization: `Bearer ${token}` }
  };
  return Axios.post(baseUrl + '/internshipApplications', internshipApplication, config)
}
