const express = require('express');
const shortid = require('shortid');

const app = express();
const port = 3000;

const urlMap = {};

app.use(express.json());

app.post('/shorten', (req, res) => {
  const longUrl = req.body.longUrl;
  const shortUrl = shortid.generate();
  urlMap[shortUrl] = longUrl;
  res.json({ shortUrl });
});

app.get('/:shortUrl', (req, res) => {
  const shortUrl = req.params.shortUrl;
  const longUrl = urlMap[shortUrl];
  if (longUrl) {
    res.redirect(longUrl);
  } else {
    res.status(404).send('URL not found');
  }
});

app.listen(port, () => {
  console.log(`Server listening on port ${port}`);
});