import { useContext, useEffect, useState } from 'react';
import { Card } from 'react-bootstrap';
import { InternshipContext } from '../context/InternshipProvider';
import { AiFillPlusCircle, AiFillCloseCircle } from 'react-icons/ai'
import { AuthContext } from '../context/AuthProvider';
import moment from 'moment';

export const InternshipList = () => {
  const { changeInternshipsFilter, filteredInternships, addInternship,
    addInternshipApplication, applications, getUserApplications } = useContext(InternshipContext)
  const { id, username, token, isAdmin } = useContext(AuthContext)
  const [title, setTitle] = useState('')
  const [location, setLocation] = useState('')
  const [domain, setDomain] = useState('')
  const [companyName, setCompanyName] = useState('')
  const [date, setDate] = useState('')
  const [description, setDescription] = useState('')
  const [isCreatingEvent, setIsCreatingEvent] = useState(false)
  const [applicationIds, setApplicationIds] = useState([])

  useEffect(() => {
    if (!isAdmin) {
      getUserApplications(id)
    }
    changeInternshipsFilter({})
  }, [])

  const search = () => {
    const filter = {}
    if (title) filter.title = title
    if (location) filter.location = location
    if (domain) filter.domain = domain
    if (companyName) filter.companyName = companyName
    if (date) filter.date = date
    changeInternshipsFilter(filter)
  }

  useEffect(() => {
    if (applications) {
      const tempIds = []
      for (const application of applications)
        tempIds.push(application.internship.id)
      setApplicationIds(tempIds)
    }
  }, [applications])

  const newApplication = (internshipId) => {
    addInternshipApplication({ internshipId: internshipId, regularUserId: id })
    const tempIds = applicationIds
    tempIds.push(internshipId)
    setApplicationIds(tempIds)
    search()
  }

  useEffect(() => {
    search()
  }, [applications])

  const clearFields = () => {
    setTitle('')
    setLocation('')
    setDomain('')
    setCompanyName('')
    setDate('')
    setDescription('')
  }

  return (
    <>
      {!isCreatingEvent ? (
        <div className='internship-list' >
          <span>Internships</span>
          <div className='search-div'>
            <input placeholder='title' onChange={e => setTitle(e.target.value)} />
            <input placeholder='location' onChange={e => setLocation(e.target.value)} />
            <input placeholder='domain' onChange={e => setDomain(e.target.value)} />
            <input placeholder='company name' onChange={e => setCompanyName(e.target.value)} />
            <input placeholder='date DD/MM/YYYY' onChange={e => setDate(e.target.value)} />
            <button onClick={search}>Search</button>
          </div>
          <div>
            {!filteredInternships || filteredInternships.length === 0 ? (
              <div>
                No internships
              </div>
            ) : (
              <div className='internships'>
                {filteredInternships
                  .map((internship, i) =>
                    <Card key={i} className='internship'>
                      <Card.Title>
                        {internship.title}
                      </Card.Title>
                      <Card.Body>
                        <div> {internship.description} </div>
                        <div> Domain: {internship.domain} </div>
                        <div> Company: {internship.adminUser.companyName} </div>
                        <div> Location: {internship.location} </div>
                        <div> Deadline: {moment(internship.deadline).format('DD/MM/YYYY')} </div>
                        <div> Added on: {moment(internship.dateAdded).format('DD/MM/YYYY')} </div>
                        <div> Number of applications: {internship.noApplications} </div>
                        {!isAdmin &&
                          <button disabled={applicationIds && applicationIds.includes(internship.id)} onClick={() => {
                            newApplication(internship.id)
                          }}>
                            Apply for this internship
                          </button>
                        }
                      </Card.Body>
                    </Card>)
                }
              </div>
            )}
          </div>
          {isAdmin &&
            <button className="btn" onClick={() => {
              clearFields()
              setIsCreatingEvent(true)
            }}>
              <AiFillPlusCircle size={50} />
            </button>
          }

        </div >
      ) : (
        <>
          Add internship
          <button className="btn" onClick={() => {
            clearFields()
            setIsCreatingEvent(false)
          }}>
            <AiFillCloseCircle size={50} />
          </button>
          <div>
            <input placeholder='title' onChange={e => setTitle(e.target.value)} /><br />
            <input placeholder='location' onChange={e => setLocation(e.target.value)} /><br />
            <input placeholder='description' onChange={e => setDescription(e.target.value)} /><br />
            <input placeholder='domain' onChange={e => setDomain(e.target.value)} /><br />
            <input placeholder='deadline dd/mm/yyyy' onChange={e => setDate(e.target.value)} /><br />
            <button disabled={!title || !location || !description || !domain || !date}
              onClick={() => {
                const internship = {}
                internship.adminUserID = id
                internship.title = title
                internship.location = location
                internship.description = description
                internship.domain = domain
                internship.deadline = date
                addInternship(internship)
                clearFields()
                setIsCreatingEvent(false)
              }}>Add internship</button>
          </div>
          {(!title || !location || !description || !domain || !date) &&
            <div>
              Fill all fields!
            </div>
          }
        </>
      )}
    </>
  )
}