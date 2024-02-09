import Axios from 'axios'

const baseUrl = process.env.REACT_APP_API_URL;

export const postLogin = (username, password) => {
  return Axios.post(baseUrl + '/account/authenticate',
    { username, password })
}

export const postRegister = (username, password, isAdmin, companyName) => {
  return Axios.post(baseUrl + '/account/register',
    { username, password, isAdmin, companyName })
}
