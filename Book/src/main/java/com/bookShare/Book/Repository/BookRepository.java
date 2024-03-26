package com.bookShare.Book.Repository;
import com.bookShare.Book.Class.Book;
import org.springframework.data.mongodb.repository.MongoRepository;

public interface BookRepository extends MongoRepository<Book, String> {
}
