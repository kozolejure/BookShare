// app.js
const express = require('express');
const mongoose = require('mongoose');
const swaggerJsDoc = require('swagger-jsdoc');
const swaggerUi = require('swagger-ui-express');

const app = express();
const PORT = process.env.PORT || 3000;

// Swagger konfiguracija
const swaggerOptions = {
  definition: {
    openapi: '3.0.0',
    info: {
      title: 'Stats API',
      description: 'API for logging and retrieving statistics',
      version: '1.0.0',
      contact: {
        name: 'Your Name',
        email: 'yourname@example.com'
      }
    },
  },
  apis: ['app.js'],
};
const swaggerDocs = swaggerJsDoc(swaggerOptions);
app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerDocs));

// Povezava na MongoDB
mongoose.connect('mongodb://stats_db:27017/statsdb', {
  useNewUrlParser: true,
  useUnifiedTopology: true
}).then(() => console.log('Connected to MongoDB'))
  .catch(err => console.error('Connection to MongoDB failed', err));

// Definicija sheme in modela
const StatSchema = new mongoose.Schema({
  string: String,
  count: { type: Number, default: 1 }
});

const Stat = mongoose.model('Stat', StatSchema);

// Middleware za obravnavo JSON podatkov
app.use(express.json());

/**
 * @swagger
 * /stats:
 *  get:
 *    summary: Pridobi vse zapise statistike
 *    responses:
 *      200:
 *        description: Seznam vseh zapisov statistike
 *      500:
 *        description: Napaka na strežniku
 */
app.get('/stats', async (req, res) => {
  try {
    const stats = await Stat.find();
    res.json(stats);
  } catch (err) {
    res.status(500).json({ message: err.message });
  }
});

/**
 * @swagger
 * /stats:
 *  post:
 *    summary: Dodaj nov zapis statistike
 *    requestBody:
 *      required: true
 *      content:
 *        application/json:
 *          schema:
 *            type: object
 *            properties:
 *              string:
 *                type: string
 *                description: Vhodni niz, ki ga želite beležiti
 *    responses:
 *      201:
 *        description: Uspešno ustvarjen nov zapis statistike
 *      400:
 *        description: Neveljavna zahteva
 */
app.post('/stats', async (req, res) => {
  const { string } = req.body;

  try {
    let stat = await Stat.findOne({ string });

    if (stat) {
      stat.count++;
    } else {
      stat = new Stat({ string });
    }

    await stat.save();
    res.status(201).json(stat);
  } catch (err) {
    res.status(400).json({ message: err.message });
  }
});

// Poslušanje na določenem vratu
app.listen(PORT, () => {
  console.log(`Server running on port ${PORT}`);
});
