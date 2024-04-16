package com.bookShare.Book.Controllers;

import com.bookShare.Book.Class.Book;
import com.bookShare.Book.Repository.BookRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/book")
public class BooksController {
    private static final Logger logger = LoggerFactory.getLogger(BooksController.class);

    @Autowired
    private BookRepository bookRepository;


    @GetMapping
    public List<Book> getAllBooks() {
        logger.info("/getAllBooks");
        return bookRepository.findAll();

    }

    @PostMapping
    public Book createBook(@RequestBody Book book) {
        logger.info("createBook");
        return bookRepository.save(book);
    }

    @GetMapping("/{id}")
    public Book getBookById(@PathVariable String id) {
        logger.info("/getBookById "+ id);
        return bookRepository.findById(id).orElse(null);
    }

    @PutMapping("/{id}")
    public Book updateBook(@PathVariable String id, @RequestBody Book bookDetails) {
        logger.info("updateBook " + id);
        Book book = bookRepository.findById(id).orElse(null);

        if (book != null) {
            // Posodobite atribute objekta book
            book.setName(bookDetails.getName());
            book.setDescription(bookDetails.getDescription());
            book.setAuthor(bookDetails.getAuthor());
            book.setImage(bookDetails.getImage());
            // Preskočite nastavljanje ID-ja tukaj
        } else {
            // Če knjiga z danim ID-jem ne obstaja, ročno nastavite ID in ustvarite nov zapis
            bookDetails.setId(id);
            book = bookDetails;
        }

        bookRepository.save(book);
        return book;
    }

    @DeleteMapping("/{id}")
    public void deleteBook(@PathVariable String id) {
        logger.info("deleteBook "+id );
        bookRepository.deleteById(id);
    }

}
