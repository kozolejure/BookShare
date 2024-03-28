package com.bookShare.Book.Class;
import org.bson.types.ObjectId;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

@Document(collection = "books")
public class Book {
    @Id
    private String id;
    private String title;
    private String description;
    private String author;
    private String image;



    // Getterji
    public String getId() {
        return id;
    }

    public String getName() {
        return title;
    }

    public String getDescription() {
        return description;
    }


    public String getImage() {
        return image;
    }

    public String getAuthor() {
        return author;
    }



    // Setterji
    public void setId(String id) {
        this.id = id;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public void setAuthor(String Author) {
        this.author = Author;
    }


    public void setImage(String image) {
        this.image = image;
    }


    public Book( String title, String description, String author, String image) {

        this.title = title;
        this.description = description;
        this.author = author;
        this.image = image;
    }

}
