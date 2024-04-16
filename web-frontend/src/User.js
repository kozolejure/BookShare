import React, { useState, useEffect } from 'react';
import axios from 'axios';

const Users = () => {
  const [users, setUsers] = useState([]);
  const [newUser, setNewUser] = useState({ name: '', surname: '', address: '', city: '', email: '' });
  const [editingUser, setEditingUser] = useState(null);

  // Pridobite vse uporabnike ob prvi montaži
  useEffect(() => {
    fetchUsers();
  }, []);

  // GET zahteva za pridobivanje uporabnikov
  const fetchUsers = async () => {
    try {
      const response = await axios.get('http://localhost:1000/User');
      setUsers(response.data);
    } catch (error) {
      console.error('Error fetching users:', error);
    }
  };

  // POST zahteva za dodajanje novega uporabnika
  const addUser = async () => {
    try {
      const response = await axios.post('http://localhost:1000/User', newUser);
      setUsers([...users, response.data]); // Dodajte novega uporabnika v stanje
      setNewUser({ name: '', surname: '', address: '', city: '', email: '' }); // Ponastavite formo
    } catch (error) {
      console.error('Error adding user:', error);
    }
  };

  // DELETE zahteva za brisanje uporabnika
  const deleteUser = async (id) => {
    try {
      await axios.delete(`http://localhost:1000/User/${id}`);
      setUsers(users.filter((user) => user.id !== id)); // Odstranite uporabnika iz stanja
    } catch (error) {
      console.error('Error deleting user:', error);
    }
  };

  // PUT zahteva za posodabljanje uporabnika
  const updateUser = async () => {
    try {
      const { id, ...userData } = editingUser;
      const response = await axios.put(`http://localhost:1000/User/${id}`, userData);
      const updatedUsers = users.map((user) =>
        user.id === id ? response.data : user
      );
      setUsers(updatedUsers); // Posodobite stanje s spremenjenim uporabnikom
      setEditingUser(null); // Končaj urejanje
    } catch (error) {
      console.error('Error updating user:', error);
    }
  };

  // Ročaji za vhodne spremembe
  const handleNewInputChange = (e) => {
    const { name, value } = e.target;
    setNewUser({ ...newUser, [name]: value });
  };

  const handleEditInputChange = (e) => {
    const { name, value } = e.target;
    setEditingUser({ ...editingUser, [name]: value });
  };

  // Nastavi trenutno urejanega uporabnika
  const editUser = (user) => {
    setEditingUser(user);
  };

  // Render funkcija za dodajanje ali urejanje uporabnika
  const renderEditForm = () => (
    <div>
      <input
        type="text"
        name="name"
        value={editingUser.name}
        onChange={handleEditInputChange}
        placeholder="Name"
      />
      <input
        type="text"
        name="surname"
        value={editingUser.surname}
        onChange={handleEditInputChange}
        placeholder="Surname"
      />
      <input
        type="text"
        name="address"
        value={editingUser.address}
        onChange={handleEditInputChange}
        placeholder="Address"
      />
      <input
        type="text"
        name="city"
        value={editingUser.city}
        onChange={handleEditInputChange}
        placeholder="City"
      />
      <input
        type="email"
        name="email"
        value={editingUser.email}
        onChange={handleEditInputChange}
        placeholder="Email"
      />
      <button onClick={updateUser}>Update User</button>
      <button onClick={() => setEditingUser(null)}>Cancel</button>
    </div>
  );

  // UI za prikaz in dodajanje uporabnikov
  return (
    <div>
      <h1>Users</h1>
      {editingUser ? (
        renderEditForm()
      ) : (
        <div>
          <input
            type="text"
            name="name"
            value={newUser.name}
            onChange={handleNewInputChange}
            placeholder="Name"
          />
          <input
            type="text"
            name="surname"
            value={newUser.surname}
            onChange={handleNewInputChange}
            placeholder="Surname"
          />
          <input
            type="text"
            name="address"
            value={newUser.address}
            onChange={handleNewInputChange}
            placeholder="Address"
          />
          <input
            type="text"
            name="city"
            value={newUser.city}
            onChange={handleNewInputChange}
            placeholder="City"
          />
          <input
            type="email"
            name="email"
            value={newUser.email}
            onChange={handleNewInputChange}
            placeholder="Email"
          />
          <button onClick={addUser}>Add User</button>
        </div>
      )}
      <ul>
        {users.map((user) => (
          <li key={user.id}>
            {user.name} {user.surname}
            <button onClick={() => editUser(user)}>Edit</button>
            <button onClick={() => deleteUser(user.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Users;
