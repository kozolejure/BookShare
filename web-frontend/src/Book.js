import React, { useState, useEffect } from 'react';
import axios from 'axios';

const apiUrl = process.env.URL_API || 'http://localhost:1000';
console.log(apiUrl);

const Books = () => {
  const [books, setBooks] = useState([]);
  const [newBook, setNewBook] = useState({ description: '', author: '', image: '', name: '' });
  const [editingBook, setEditingBook] = useState(null);

  // Pridobite vse knjige ob prvi montaži
  useEffect(() => {
    fetchBooks();
  }, []);

  // GET zahteva za pridobivanje knjig
  const fetchBooks = async () => {
    try {
      const response = await axios.get(`${apiUrl}/Book`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token")}`, // Vključite žeton v glavo zahteve
        },
      });
      setBooks(response.data);
    } catch (error) {
      console.error('Error fetching books:', error);
    }
  };

  // POST zahteva za dodajanje nove knjige
  const addBook = async () => {
    try {
      const response = await axios.post(apiUrl+'/Book', newBook,{
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token")}`, // Vključite žeton v glavo zahteve
        },
      });
      setBooks([...books, response.data]); // Dodajte novo knjigo v stanje
      setNewBook({ description: '', author: '', image: '', name: '' }); // Ponastavi formo
    } catch (error) {
      console.error('Error adding book:', error);
    }
  };

  // DELETE zahteva za brisanje knjige
  const deleteBook = async (id) => {
    try {
      await axios.delete(apiUrl+`/Book/${id}`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token")}`, // Vključite žeton v glavo zahteve
        },
      });
      setBooks(books.filter((book) => book.id !== id)); // Odstranite knjigo iz stanja
    } catch (error) {
      console.error('Error deleting book:', error);
    }
  };

  // PUT zahteva za posodabljanje knjige
  const updateBook = async () => {
    try {
      const response = await axios.put(apiUrl+`/Book/${editingBook.id}`, editingBook ,{
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token")}`, // Vključite žeton v glavo zahteve
        },
      });
      const updatedBooks = books.map((book) =>
        book.id === editingBook.id ? response.data : book
      );
      setBooks(updatedBooks); // Posodobite stanje s spremenjeno knjigo
      setEditingBook(null); // Končaj urejanje
    } catch (error) {
      console.error('Error updating book:', error);
    }
  };

  // Ročaji za vhodne spremembe
  const handleNewInputChange = (e) => {
    const { name, value } = e.target;
    setNewBook({ ...newBook, [name]: value });
  };

  const handleEditInputChange = (e) => {
    const { name, value } = e.target;
    setEditingBook({ ...editingBook, [name]: value });
  };

  // Nastavi trenutno urejano knjigo
  const editBook = (book) => {
    setEditingBook(book);
  };

  // Render funkcija za dodajanje ali urejanje knjige
  const renderEditForm = () => (
    <div>
      <input
        type="text"
        name="name"
        value={editingBook.name}
        onChange={handleEditInputChange}
        placeholder="Book Name"
      />
      <input
        
        type="text"
        name="description"
        value={editingBook.description}
        onChange={handleEditInputChange}
        placeholder="Description"
      />
      <input
        type="text"
        name="author"
        value={editingBook.author}
        onChange={handleEditInputChange}
        placeholder="Author"
      />
      <input
        type="text"
        name="image"
        value={editingBook.image}
        onChange={handleEditInputChange}
        placeholder="Image URL"
      />
      <button onClick={updateBook}>Update Book</button>
      <button onClick={() => setEditingBook(null)}>Cancel</button>
    </div>
  );

  // UI za prikaz in dodajanje knjig
  return (
    <div>
      <h1>Books</h1>
      {editingBook ? (
        renderEditForm()
      ) : (
        <div>
          <input
            type="text"
            name="name"
            value={newBook.name}
            onChange={handleNewInputChange}
            placeholder="Book Name"
          />
          <input
            type="text"
            name="description"
            value={newBook.description}
            onChange={handleNewInputChange}
            placeholder="Description"
          />
          <input
            type="text"
            name="author"
            value={newBook.author}
            onChange={handleNewInputChange}
            placeholder="Author"
          />
          <input
            type="text"
            name="image"
            value={newBook.image}
            onChange={handleNewInputChange}
            placeholder="Image URL"
          />
          <button onClick={addBook}>Add Book</button>
        </div>
      )}
      <ul>
        {books.map((book) => (
          <li key={book.id}>
            {book.description} by {book.author}
            <button onClick={() => editBook(book)}>Edit</button>
            <button onClick={() => deleteBook(book.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Books;
