import { useContext } from "react";
import { AuthContext } from "../context/AuthProvider";
import { InternshipList } from "./InternshipList";

export const Home = () => {
  const { username, logout } = useContext(AuthContext)

  return (
    <div id='home-wrapper'>
      <div className='header'>
        Logged user: {username}
        <button className='btn btn-primary' onClick={logout}>Logout</button>
      </div>
      <div className='internship-list'>
        <InternshipList />
      </div>
    </div>
  );
};