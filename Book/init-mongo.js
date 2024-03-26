db = new Mongo().getDB('bookshare');

db.createCollection('books');
