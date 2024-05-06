import React from 'react';
import { Link } from 'react-router-dom';
import './Navbar.css'; // Predpostavljamo, da ste ustvarili CSS datoteko za styling

const Navbar = () => {
  return (
    <nav className="navbar">
      <Link to="/Book">Book</Link>
      <Link to="/User">User</Link>
      <Link to="/Borrowing">Borrowing</Link>
      <Link to="/Login">Login</Link>
    </nav>
  );
};

export default Navbar;