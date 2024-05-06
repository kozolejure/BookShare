// App.js
import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Navbar from './Navbar'; // Uvozite vašo Navbar komponento
import Book from './Book';
import User from './User';
import Borrow from './Borrow';
import Login from './Login';
import './App.css';

function App() {
  return (
    <Router>
      <div className="App">
        <Navbar />
        <Routes>
          <Route path="/book" element={<Book />} />
          <Route path="/User" element={<User />} />
          <Route path="/Borrowing" element={<Borrow />} />
          <Route path="/Login" element={<Login />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
