package org.acme;

import io.quarkus.test.junit.QuarkusTest;
import io.restassured.http.ContentType;
import org.acme.Models.Borrowing;
import org.junit.jupiter.api.Test;

import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.is;

@QuarkusTest
class GreetingResourceTest {
    @Test
    void testListAll() {
        given()
                .when().get("/borrowings")
                .then()
                .statusCode(200);

    }

    @Test
    void testGetOne() {
        // Assuming you've added a borrowing with an ID
        String testId = "6601c02d611ba353ca1c4831";
        given()
                .when().get("/borrowings/{id}", testId)
                .then()
                .statusCode(204);

    }

    @Test
    void testCreate() {
        given()
                .contentType(ContentType.JSON)
                .body(new Borrowing(/* parameters for your borrowing */))
                .when().post("/borrowings")
                .then()
                .statusCode(201);
    }

    @Test
    void testUpdate() {
        String testId = "6601c02d611ba353ca1c4831";
        given()
                .contentType(ContentType.JSON)
                .body(new Borrowing(/* updated parameters */))
                .when().put("/borrowings/{id}", testId)
                .then()
                .statusCode(200);
    }


    @Test
    void testListSoonEndingBorrowings() {
        given()
                .when().get("/borrowings/soonEnding")
                .then()
                .statusCode(200);


    }
}