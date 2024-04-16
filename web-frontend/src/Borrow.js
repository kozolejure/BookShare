import React, { useState, useEffect } from 'react';
import axios from 'axios';

const Borrowings = () => {
  const [borrowings, setBorrowings] = useState([]);
  const [newBorrowing, setNewBorrowing] = useState({ bookId: '', userId: '', borrowingDate: '', returnDate: '' });
  const [editingBorrowing, setEditingBorrowing] = useState(null);

  // Pridobite vse izposoje ob prvi montaži
  useEffect(() => {
    fetchBorrowings();
  }, []);

  // GET zahteva za pridobivanje izposoj
  const fetchBorrowings = async () => {
    try {
      const response = await axios.get('http://localhost:1000/Borrow');
      setBorrowings(response.data);
    } catch (error) {
      console.error('Error fetching borrowings:', error);
    }
  };

  // POST zahteva za dodajanje nove izposoje
  const addBorrowing = async () => {
    try {
      const { id, ...borrowingData } = newBorrowing;
      const response = await axios.post('http://localhost:1000/Borrow', borrowingData);
      setBorrowings([...borrowings, response.data]); // Dodajte novo izposojo v stanje
      setNewBorrowing({ bookId: '', userId: '', borrowingDate: '', returnDate: '' }); // Ponastavi formo
    } catch (error) {
      console.error('Error adding borrowing:', error);
    }
  };

  // DELETE zahteva za brisanje izposoje
  const deleteBorrowing = async (id) => {
    try {
      await axios.delete(`http://localhost:1000/Borrow/${id}`);
      setBorrowings(borrowings.filter((borrowing) => borrowing.id !== id)); // Odstranite izposojo iz stanja
    } catch (error) {
      console.error('Error deleting borrowing:', error);
    }
  };

  // PUT zahteva za posodabljanje izposoje
  const updateBorrowing = async () => {
    try {
      const { id, ...borrowingData } = editingBorrowing;
      const response = await axios.put(`http://localhost:1000/Borrow/${id}`, borrowingData);
      const updatedBorrowings = borrowings.map((borrowing) =>
        borrowing.id === id ? response.data : borrowing
      );
      setBorrowings(updatedBorrowings); // Posodobite stanje s spremenjeno izposojo
      setEditingBorrowing(null); // Končaj urejanje
    } catch (error) {
      console.error('Error updating borrowing:', error);
    }
  };

  // Ročaji za vhodne spremembe
  const handleNewInputChange = (e) => {
    const { name, value } = e.target;
    setNewBorrowing({ ...newBorrowing, [name]: value });
  };

  const handleEditInputChange = (e) => {
    const { name, value } = e.target;
    setEditingBorrowing({ ...editingBorrowing, [name]: value });
  };

  // Nastavi trenutno urejeno izposojo
  const editBorrowing = (borrowing) => {
    setEditingBorrowing(borrowing);
  };

  // Render funkcija za dodajanje ali urejanje izposoje
  const renderEditForm = () => (
    <div>
      <input
        type="text"
        name="bookId"
        value={editingBorrowing.bookId}
        onChange={handleEditInputChange}
        placeholder="Book ID"
      />
      <input
        type="text"
        name="userId"
        value={editingBorrowing.userId}
        onChange={handleEditInputChange}
        placeholder="User ID"
      />
      <input
        type="text"
        name="borrowingDate"
        value={editingBorrowing.borrowingDate}
        onChange={handleEditInputChange}
        placeholder="Borrowing Date"
      />
      <input
        type="text"
        name="returnDate"
        value={editingBorrowing.returnDate}
        onChange={handleEditInputChange}
        placeholder="Return Date"
      />
      <button onClick={updateBorrowing}>Update Borrowing</button>
      <button onClick={() => setEditingBorrowing(null)}>Cancel</button>
    </div>
  );

  // UI za prikaz in dodajanje izposoj
  return (
    <div>
      <h1>Borrowings</h1>
      {editingBorrowing ? (
        renderEditForm()
      ) : (
        <div>
          <input
            type="text"
            name="bookId"
            value={newBorrowing.bookId}
            onChange={handleNewInputChange}
            placeholder="Book ID"
          />
          <input
            type="text"
            name="userId"
            value={newBorrowing.userId}
            onChange={handleNewInputChange}
            placeholder="User ID"
          />
          <input
            type="text"
            name="borrowingDate"
            value={newBorrowing.borrowingDate}
            onChange={handleNewInputChange}
            placeholder="Borrowing Date"
          />
          <input
            type="text"
            name="returnDate"
            value={newBorrowing.returnDate}
            onChange={handleNewInputChange}
            placeholder="Return Date"
          />
          <button onClick={addBorrowing}>Add Borrowing</button>
        </div>
      )}
      <ul>
        {borrowings.map((borrowing) => (
          <li key={borrowing.id}>
            Book ID: {borrowing.bookId}, User ID: {borrowing.userId}
            <button onClick={() => editBorrowing(borrowing)}>Edit</button>
            <button onClick={() => deleteBorrowing(borrowing.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Borrowings;
