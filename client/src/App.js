import { useState, useEffect } from 'react';

const API = 'http://localhost:5164/api/person';

function App() {
  const [people, setPeople] = useState([]); //store our list of people

  useEffect(() => {
    fetchPeople();
  }, []);

  // Method to 
  const fetchPeople = async () => {
    const res = await fetch(API);
    const data = await res.json();
    setPeople(data);
  };

  return (
    <div>
      <h1>React4DotNet</h1>

      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Email</th>
            <th>Phone</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {people.map(p => (
            <tr key={p.id}>
              <td>{p.first_name} {p.last_name}</td>
              <td>{p.email}</td>
              <td>{p.phone}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}



export default App;