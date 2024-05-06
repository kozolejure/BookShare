const express = require('express');
const jwt = require('jsonwebtoken');

const app = express();
const PORT = 3000;
const secretKey = 'skrivnost'; // Skrivni ključ za podpisovanje JWT žetonov

app.use(express.json());

// Preverjanje uporabniških podatkov in izdaja JWT žetona
app.post('/login', (req, res) => {
    const { username, password } = req.body;

    // Preveri uporabniško ime in geslo
    if (username === 'admin' && password === 'admin') {
        // Ustvari JWT žeton
        const token = jwt.sign({ username }, secretKey, { expiresIn: '1h'

    

         });

        res.json({ token });
    } else {
        res.status(401).json({ message: 'Invalid username or password' });
    }
});

// Preverjanje JWT žetona
function authenticateToken(req, res, next) {
    const token = req.headers['authorization'];

    if (!token) return res.status(401).json({ message: 'Unauthorized' });

    jwt.verify(token, secretKey, (err, decoded) => {
        if (err) return res.status(403).json({ message: 'Forbidden' });
        req.user = decoded;
        next();
    });
}

// Zaščitene poti, ki zahtevajo JWT žeton
app.get('/protected', authenticateToken, (req, res) => {
    res.json({ message: 'This is a protected route' });
});

app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});
