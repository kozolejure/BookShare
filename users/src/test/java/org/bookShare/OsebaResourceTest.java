package org.bookShare;
import Class.User;
import io.quarkus.test.junit.QuarkusTest;

import io.restassured.http.ContentType;
import io.restassured.specification.Argument;
import jakarta.ws.rs.core.Response;
import org.junit.jupiter.api.Test;


import java.util.List;

import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.containsString;
import static org.hamcrest.Matchers.*;

@QuarkusTest
public class OsebaResourceTest {

/*
    @Test
    public void testCreateUser() {
        given()
                .contentType(ContentType.JSON)
                .body("{\"name\": \"Test User\", \"surname\": \"Test Surname\", \"address\": \"Test Address\", \"city\": \"Test City\", \"email\": \"test@example.com\"}") // Prilagodite glede na vaše potrebe
                .when()
                .post("/user")
                .then()
                .statusCode(Response.Status.CREATED.getStatusCode())
                .body("name", equalTo("Test User")); // Prilagodite glede na vaše potrebe
    }

    @Test
    public void testListUsers() {
        given()
                .when()
                .get("/user")
                .then()
                .statusCode(Response.Status.OK.getStatusCode())
                .body("$", hasSize(greaterThan(0))); // Preveri, ali seznam ni prazen
    }

    @Test
    public void testGetUser() {
        // Predpostavljamo, da obstaja uporabnik s tem ID-jem v bazi podatkov
        String firstUserId = given()
                .when()
                .get("/user")
                .then()
                .extract()
                .jsonPath().getString("[0].id");

        given()
                .when()
                .get("/user/{id}", firstUserId)
                .then()
                .statusCode(Response.Status.OK.getStatusCode())
                .body("id", equalTo(firstUserId)); // Preveri, ali je vrnjeni uporabnik pravilen
    }

    @Test
    public void testUpdateUser() {
        // Predpostavljamo, da obstaja uporabnik s tem ID-jem v bazi podatkov
        String firstUserId = given()
                .when()
                .get("/user")
                .then()
                .extract()
                .jsonPath().getString("[0].id");

        given()
                .contentType(ContentType.JSON)
                .body("{\"name\": \"Updated Name\", \"surname\": \"Updated Surname\", \"address\": \"Updated Address\", \"city\": \"Updated City\", \"email\": \"updated@example.com\"}") // Prilagodite glede na vaše potrebe
                .when()
                .put("/user/{id}", firstUserId)
                .then()
                .statusCode(Response.Status.OK.getStatusCode())
                .body("name", equalTo("Updated Name")); // Preveri, ali je uporabnik uspešno posodobljen
    }

 */

}
