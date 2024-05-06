import React, { useState } from 'react';

const Login = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [token, setToken] = useState('');

  const handleLogin = async () => {
    try {
      const response = await fetch('http://localhost:2001/login/', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          username: username,
          password: password,
        }),
      });
      const data = await response.json();
      if (data.token) {
        setToken(data.token);
        localStorage.setItem('token', data.token);
        console.log(data.token)
      } else {
        // Obdelajte primer, če ni bilo mogoče pridobiti žetona
        console.error('Ni bilo mogoče pridobiti žetona');
      }
    } catch (error) {
      console.error('Prišlo je do napake pri pošiljanju zahteve:', error);
    }
  };

  return (
    <div>
      <h2>Login</h2>
      <form onSubmit={handleLogin}>
        <label>
          Username:
          <input
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
        </label>
        <br />
        <label>
          Password:
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </label>
        <br />
        <button type="submit">Login</button>
      </form>
      {token && (
        <div>
          <h3>Token:</h3>
          <p>{token}</p>
        </div>
      )}
    </div>
  );
};

export default Login;
