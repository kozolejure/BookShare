package org.acme.Models;

import io.quarkus.mongodb.panache.reactive.ReactivePanacheMongoEntity;
import java.time.LocalDate;

public class Borrowing extends ReactivePanacheMongoEntity {

    public String bookId;
    public String userId;
    public LocalDate borrowingDate;
    public LocalDate returnDate;
}
